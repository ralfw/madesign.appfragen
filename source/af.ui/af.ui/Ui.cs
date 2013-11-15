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
                    break;
            }
        }

        public event Action<string> Json_output;

        private void Starten()
        {
            var app = new App();
            var befragen = new Befragen();
            app.Run(befragen);
        }

        private void FragebogenAnzeigen( dynamic jsonObject )
        {
            // Liste an Fragen mit Antwortmöglichkeiten erstellen
            var fragen = jsonObject.payload.Fragen as List<Befragung.Frage>;
            // Liste in Befragung setzen
            var befragen = new Befragen {Fragen = fragen};

            // Befragung anzeigen
            var app = new App();
            app.Run( befragen );
        }
    }
}
