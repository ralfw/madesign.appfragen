using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var befragen = new Befragen();
            befragen.Show();
        }
    }
}
