using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using af.contracts;
using jsonserialization;

namespace demo
{
    public class Berechnen : IComponent
    {
        public void Process(string json)
        {
            dynamic obj = json.FromJson();
            switch ((string) obj.cmd)
            {
                case "Addieren":
                    int summe = obj.payload.a + obj.payload.b;

                    dynamic ergebnis = new ExpandoObject();
                    ergebnis.cmd = "Berechnungsergebnis";
                    ergebnis.payload = new ExpandoObject();
                    ergebnis.payload.resultat = summe;

                    json = JsonExtensions.ToJson(ergebnis);
                    Json_output(json);
                    break;
            }
        }

        public event Action<string> Json_output;
    }
}
