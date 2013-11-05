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
            string jsonErgebnis;

            dynamic obj = json.FromJson();
            switch ((string) obj.cmd)
            {
                case "Addieren":
                    int summe = obj.payload.a + obj.payload.b;

                    dynamic ergebnis = new ExpandoObject();
                    ergebnis.cmd = "Berechnungsergebnis";
                    ergebnis.payload = new ExpandoObject();
                    ergebnis.payload.resultat = summe;
                    jsonErgebnis = JsonExtensions.ToJson(ergebnis);

                    Json_output(jsonErgebnis);
                    break;

                case "Subtrahieren":
                    var differenz = Subtrahieren(obj.payload.a, obj.payload.b);

                    jsonErgebnis = string.Format(Properties.Resources.json_Berechnungsergebnis, differenz);
                    Json_output(jsonErgebnis);
                    break;

                case "Multiplizieren":
                    int produkt = obj.payload.a * obj.payload.b;

                    jsonErgebnis = new Berechnungsergebnis { payload = new Berechnungsergebnis.Payload { resultat = produkt } }
                                   .ToJson();
                    Json_output(jsonErgebnis);
                    break;
            }
        }


        int Subtrahieren(int a, int b)
        {
            return a - b;
        }


        class Berechnungsergebnis
        {
            public string cmd = "Berechnungsergebnis";
            public Payload payload;

            public class Payload
            {
                public int resultat;
            }
        }


        public event Action<string> Json_output;
    }
}
