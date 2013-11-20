using System;
using System.Collections.Generic;
using af.contracts;
using jsonserialization;

namespace af.ui
{
    public class Ui : IComponent
    {
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

        public void SendCommand(string param)
        {
            if (Json_output != null)
            {
                Json_output(param);
            }
        }

        private void FragebogenAnzeigen( dynamic jsonObject )
        {
            // Liste an Fragen mit Antwortmöglichkeiten erstellen
            //var fragen = jsonObject.payload.Fragen as List<Befragung.Frage>;

            // TODO: Wir bauen uns erstmal ne dummy Liste, bis das json funktioniert.
            var fragen = new List<Befragung.Frage>();
            fragen.Add(FrageErstellen("1", 3));
            fragen.Add(FrageErstellen("2", 2));

            // Liste in Befragung setzen
            var befragen = new Befragen(this) {Fragen = fragen};

            // Befragung anzeigen
            var app = new App();
            app.Run( befragen );
        }

        /// <summary>
        /// TODO: Test method - remove later
        /// </summary>
        /// <param name="nummer"></param>
        /// <param name="nummerDerRichtigenAntwort">1-3</param>
        /// <returns>Gibt eine Frage mit Antwortmöglichkeiten zurück</returns>
        private Befragung.Frage FrageErstellen(string nummer, int nummerDerRichtigenAntwort)
        {
            if (nummerDerRichtigenAntwort < 1 || nummerDerRichtigenAntwort > 3)
            {
                nummerDerRichtigenAntwort = 1;
            }
            var frage = new Befragung.Frage
                {
                    Text = "Frage Nr. " + nummer,
                    Antwortmöglichkeiten = new List<Befragung.Antwortmöglichkeit>
                        {
                            new Befragung.Antwortmöglichkeit
                                {
                                    Id = "F" + nummer + "A1",
                                    IstAlsAntwortSelektiert = false,
                                    IstRichtigeAntwort = nummerDerRichtigenAntwort == 1,
                                    Text = "Antwortmöglichkeit 1"
                                },
                            new Befragung.Antwortmöglichkeit
                                {
                                    Id = "F" + nummer + "A2",
                                    IstAlsAntwortSelektiert = false,
                                    IstRichtigeAntwort = nummerDerRichtigenAntwort == 2,
                                    Text = "Antwortmöglichkeit 2"
                                },
                            new Befragung.Antwortmöglichkeit
                                {
                                    Id = "F" + nummer + "A3",
                                    IstAlsAntwortSelektiert = false,
                                    IstRichtigeAntwort = nummerDerRichtigenAntwort == 3,
                                    Text = "Antwortmöglichkeit 3"
                                },
                            new Befragung.Antwortmöglichkeit
                                {
                                    Id = "F" + nummer + "A4",
                                    IstAlsAntwortSelektiert = false,
                                    IstRichtigeAntwort = false,
                                    Text = "Antwortmöglichkeit weiß nicht"
                                },
                        }
                };
            return frage;
        }
    }
}
