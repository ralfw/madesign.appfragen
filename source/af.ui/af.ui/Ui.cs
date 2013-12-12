using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Windows;
using af.contracts;
using jsonserialization;

namespace af.ui
{
    public class Ui : IComponent
    {
        public const string Beantworten = "Beantworten";
        public const string Auswerten = "Auswerten";
        public const string FragenkatalogLaden = "Fragenkatalog laden";
        public const string AuswertungBeenden = "Auswertung beenden";

        public void Process(string json)
        {
            dynamic jsonObject = json.FromJson();
            Console.WriteLine("UI: got cmd: {0}", jsonObject.cmd);
            switch ((string)jsonObject.cmd)
            {
                case "Starten":
                    Starten();
                    break;
                case "Fragebogen anzeigen":
                    FragebogenAnzeigen(jsonObject);
                    break;
                case "Auswertung anzeigen":
                    AuswertungAnzeigen(jsonObject);
                    break;
                case "Auswertung schliessen":
                    AuswertungSchliessen();
                    break;
                default:
                    Console.WriteLine("Mit dem Kommando kann das UI nichts anfangen.");
                    break;
            }
        }

        public event Action<string> Json_output;

        private void Starten()
        {
            Befragen.Show();
            Auswertung.Show();
            Auswertung.Visibility = Visibility.Hidden;
        }

        public void SendCommand(string command, string param)
        {
            dynamic jsonObject = new ExpandoObject();
            Console.WriteLine( "UI: sends cmd: {0}", command );

            switch(command)
            {
                case Beantworten:
                    jsonObject.cmd = Beantworten;
                    jsonObject.payload = new ExpandoObject();
                    jsonObject.payload.AntwortmoeglichkeitId = param;
                    break;
                case Auswerten:
                    jsonObject.cmd = Auswerten;
                    break;
                case FragenkatalogLaden:
                    jsonObject.cmd = FragenkatalogLaden;
                    jsonObject.payload = new ExpandoObject();
                    jsonObject.payload.Dateiname = param;
                    break;
                case AuswertungBeenden:
                    jsonObject.cmd = AuswertungBeenden;
                    break;
            }

            if ( Json_output != null )
            {
                var json = JsonExtensions.ToJson( jsonObject );
                Json_output( json );
            }
        }

        private void FragebogenAnzeigen( dynamic jsonObject )
        {
            // Liste an Fragen mit Antwortmöglichkeiten aus jsonObject holen
            var fragen = GetFragen(jsonObject);

            // Liste in Befragung setzen
            Befragen.Fragen = fragen;

            // Befragung anzeigen
            Befragen.Visibility = Visibility.Visible;
        }

        private List<BefragungViewModel.Frage> GetFragen(dynamic jsonObject)
        {
            var fragen = new List<BefragungViewModel.Frage>();
            foreach (var currentFrage in jsonObject.payload.Fragen)
            {
                var frage = new BefragungViewModel.Frage
                                {
                                    Text = currentFrage.Text,
                                    Antwortmöglichkeiten = new List<BefragungViewModel.Antwortmöglichkeit>()
                                };
                foreach ( var currentAntwortmöglichkeit in currentFrage.Antwortmöglichkeiten )
                {
                    var antwortmöglichkeit = new BefragungViewModel.Antwortmöglichkeit();
                    antwortmöglichkeit.Id = currentAntwortmöglichkeit.Id;
                    antwortmöglichkeit.Text = currentAntwortmöglichkeit.Text;
                    antwortmöglichkeit.IstAlsAntwortSelektiert = currentAntwortmöglichkeit.IstAlsAntwortSelektiert;
                    frage.Antwortmöglichkeiten.Add(antwortmöglichkeit);
                }
                fragen.Add(frage);
            }

            return fragen;
        }

        private void AuswertungAnzeigen( dynamic jsonObject )
        {
            var classes = new ObservableCollection<Category>
                              {
                                  new Category
                                      {
                                          Class = "richtig",
                                          Anzahl = jsonObject.payload.AnzahlRichtig,
                                          AnzahlProzent = jsonObject.payload.ProzentRichtig * 100
                                      },
                                  new Category
                                      {
                                          Class = "falsch",
                                          Anzahl = jsonObject.payload.AnzahlFalsch,
                                          AnzahlProzent = jsonObject.payload.ProzentFalsch * 100
                                      },
                                  new Category
                                      {
                                          Class = "weiß nicht",
                                          Anzahl = jsonObject.payload.AnzahlWeissNicht,
                                          AnzahlProzent = jsonObject.payload.ProzentWeissNicht * 100
                                      }
                              };

            Auswertung.DataContext = classes;
            Auswertung.AnzahlFragen = jsonObject.payload.AnzahlFragen;
            Befragen.Visibility = Visibility.Hidden;
            Auswertung.Visibility = Visibility.Visible;
        }

        private void AuswertungSchliessen()
        {
            // Close Auswertung
            Auswertung.Visibility = Visibility.Hidden;

            Befragen.Fragen = null;
            Befragen.Visibility = Visibility.Visible;
        }

        private Befragen Befragen
        {
            get { return _befragen ?? (_befragen = new Befragen(this)); }
        }
        private Befragen _befragen;

        private Auswertung Auswertung
        {
            get { return _auswertung ?? ( _auswertung = new Auswertung(this) ); }
        }
        private Auswertung _auswertung;
    }
}
