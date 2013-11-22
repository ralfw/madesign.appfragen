using System;
using System.Collections.Generic;
using System.Dynamic;
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
                    break;
                default:
                    Console.WriteLine("Mit dem Kommando kann das UI nichts anfangen.");
                    break;
            }
        }

        public event Action<string> Json_output;

        private void Starten()
        {
            var app = new App();
            var befragen = new Befragen(this);
            app.Run(befragen);
        }

        public void SendCommand(string command, string param)
        {
            dynamic jsonObject = new ExpandoObject();

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
            var befragen = new Befragen(this) {Fragen = fragen};

            // Befragung anzeigen
            var app = new App();
            app.Run( befragen );
        }

        private List<Befragung.Frage> GetFragen(dynamic jsonObject)
        {
            var fragen = new List<Befragung.Frage>();
            foreach (var currentFrage in jsonObject.payload.Fragen)
            {
                var frage = new Befragung.Frage
                                {
                                    Text = currentFrage.Text,
                                    Antwortmöglichkeiten = new List<Befragung.Antwortmöglichkeit>()
                                };
                foreach ( var currentAntwortmöglichkeit in currentFrage.Antwortmöglichkeiten )
                {
                    var antwortmöglichkeit = new Befragung.Antwortmöglichkeit();
                    antwortmöglichkeit.Id = currentAntwortmöglichkeit.Id;
                    antwortmöglichkeit.Text = currentAntwortmöglichkeit.Text;
                    antwortmöglichkeit.IstAlsAntwortSelektiert = currentAntwortmöglichkeit.IstAlsAntwortSelektiert;
                    antwortmöglichkeit.IstRichtigeAntwort = currentAntwortmöglichkeit.IstRichtigeAntwort;
                    frage.Antwortmöglichkeiten.Add(antwortmöglichkeit);
                }
                fragen.Add(frage);
            }

            return fragen;
        }

        private void AuswertungAnzeigen( dynamic jsonObject )
        {
            var auswertung = new Auswertung();
            var app = new App();
            app.Run( auswertung );
        }

    }
}
