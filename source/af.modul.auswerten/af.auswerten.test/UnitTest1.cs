using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using af.contracts;
using af.modul.auswerten;
using jsonserialization;

namespace af.auswerten.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AuswertenZuAuswertungAnzeigenKommandoTest()
        {
            // Neue Instanz von der Auswerten Klasse
            var auswerten = new Auswerten(new Befragung());
            // ExpandoObject mit Kommando erstellen zum Anfeuern von Auswerten
            dynamic expando = new ExpandoObject();
            expando.cmd = "Auswerten";
            // ExpandoObject to JSONstring
            var jsonstring = JsonExtensions.ToJson(expando);

            var output = string.Empty;
            // EventHandler definieren für das Json_output Event.
            // (arg) = Name des ersten Parameters der Action Json_output, welcher dann in der folgenden Funktion genutzt wird.
            // To Expression: auswerten.Json_output += arg => AuswertenOnJsonOutput(out output, arg);
            // Shortened: auswerten.Json_output += arg => output = arg;
            auswerten.Json_output += (arg) =>
                                         {
                                             AuswertenOnJsonOutput(out output, arg);
                                         };

            // output = param;
            auswerten.Process(jsonstring);
            // JSONstring to ExpandoObject
            dynamic outputExpandoObject = output.FromJson();

            // Vergleich (expected, actual)
            Assert.AreEqual("Auswertung anzeigen", outputExpandoObject.cmd);
        }

        private void AuswertenOnJsonOutput(out string output, string s)
        {
            output = s;
        }

        [TestMethod]
        public void AuswertungSchliessenKommandoTest()
        {
            var auswerten = new Auswerten(new Befragung());
            dynamic expando = new ExpandoObject();
            expando.cmd = "Auswerten beenden";
            var jsonstring = JsonExtensions.ToJson(expando);

            var output = string.Empty;
            auswerten.Json_output += (arg) => AuswertenOnJsonOutput(out output, arg);

            auswerten.Process(jsonstring);
            dynamic outputExpandoObject = output.FromJson();

            Assert.AreEqual("Auswertung schliessen", outputExpandoObject.cmd);
        }
    }
}
