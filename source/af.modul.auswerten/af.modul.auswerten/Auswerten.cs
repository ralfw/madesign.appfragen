using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using af.contracts;
using jsonserialization;

namespace af.modul.auswerten
{
    public class Auswerten : IComponent
    {
        private readonly Befragung _befragung;

        public Auswerten(Befragung befragung)
        {
            _befragung = befragung;

            // TODO: Kommando "Auswertung Anzeigen" für das UI feuern
            //jsonObject.cmd = "Auswertung anzeigen"

            //    jsonObject.payload.AnzahlFragen = AnzahlFragen;
            //    jsonObject.payload.AnzahlRichtig = AnzahlRichtig;
            //    jsonObject.payload.ProzentRichtig = ProzentRichtig;
            //    jsonObject.payload.AnzahlFalsch = AnzahlFalsch;
            //    jsonObject.payload.ProzentFalsch = ProzentFalsch;
            //    jsonObject.payload.AnzahlWeissNicht = AnzahlWeissNicht;
            //    jsonObject.payload.ProzentWeissNicht = ProzentWeissNicht;
        }

        public void Process(string json)
        {
            dynamic jsonObject = json.FromJson();

            if (jsonObject.cmd == "Auswerten")
            {

            }
            if (jsonObject.cmd == "Auswerten beenden")
            {

            }
        }

        public event Action<string> Json_output;
    }
}
