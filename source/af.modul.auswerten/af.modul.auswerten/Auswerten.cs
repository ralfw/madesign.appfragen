using System;
using System.Dynamic;
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
        }

        public void Process(string json)
        {
            dynamic jsonObject = json.FromJson();

            if (jsonObject.cmd == "Auswerten")
            {
                AuswertenMethode(jsonObject);
            }
            if (jsonObject.cmd == "Auswertung beenden")
            {
                AuswertenBeendenMethode(jsonObject);
            }
        }

        private void AuswertenMethode(object jsonObject)
        {
            // Auswertung erstellen (evtl. per foreach durch jede Frage in der Liste und inkrementieren; danach dann Prozente berechnen.)
                // AnzahlFragen zählen
                // AnzahlRichtig(Antworten) zählen
                // AnzahlFalsch zählen
                // AnzahlWeissNicht zählen

                // ProzentRichtig berechnen
                // ProzentFalsch berechnen
                // ProzentWeissNicht berechnen

            // Kommando "Auswertung anzeigen" zusammenbauen
            dynamic jsonAuswertungObj = new ExpandoObject();
            jsonAuswertungObj.cmd = "Auswertung anzeigen";
            jsonAuswertungObj.payload = new ExpandoObject();

            jsonAuswertungObj.payload.AnzahlFragen = 10;
            jsonAuswertungObj.payload.AnzahlRichtig = 3;
            jsonAuswertungObj.payload.ProzentRichtig = 0.3;
            jsonAuswertungObj.payload.AnzahlFalsch = 1;
            jsonAuswertungObj.payload.ProzentFalsch = 0.1;
            jsonAuswertungObj.payload.AnzahlWeissNicht = 6;
            jsonAuswertungObj.payload.ProzentWeissNicht = 0.6;

            var jsonAuswertung = JsonExtensions.ToJson(jsonAuswertungObj);

            // Kommando "Auswertung anzeigen" senden
            Json_output(jsonAuswertung);
        }

        private void AuswertenBeendenMethode(object jsonObject)
        {
            // Auswertung speichern (*.csv)

            // Kommando "Auswertung schliessen" senden
            dynamic jsonAuswertungSchliessenObj = new ExpandoObject();
            jsonAuswertungSchliessenObj.cmd = "Auswertung schliessen";

            var jsonAuswertungSchliessen = JsonExtensions.ToJson(jsonAuswertungSchliessenObj);
            Json_output(jsonAuswertungSchliessen);
        }

        public event Action<string> Json_output;
    }
}
