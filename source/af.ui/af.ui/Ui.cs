using System;
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
    }
}
