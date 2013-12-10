using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using af.contracts;
using af.modul.auswerten;
using jsonserialization;

namespace af.auswerten.test
{
    [TestClass]
    public class UnitTestsAuswerten
    {
        private Befragung _befragung;

        [TestInitialize]
        public void TestInit()
        {
            _befragung = new Befragung();
            _befragung.Reset();
        }

        [TestMethod]
        public void AuswertenZuAuswertungAnzeigenKommandoTest()
        {
            // Neue Instanz von der Auswerten Klasse
            var auswerten = new Auswerten(_befragung);
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
        public void AuswertungBeendenZuAuswertungSchliessenKommandoTest()
        {
            var auswerten = new Auswerten(_befragung);
            dynamic expando = new ExpandoObject();
            expando.cmd = "Auswertung beenden";
            var jsonstring = JsonExtensions.ToJson(expando);

            var output = string.Empty;
            auswerten.Json_output += (arg) => AuswertenOnJsonOutput(out output, arg);

            auswerten.Process(jsonstring);
            dynamic outputExpandoObject = output.FromJson();

            Assert.AreEqual("Auswertung schliessen", outputExpandoObject.cmd);
        }

        [TestMethod]
        public void FragenZählenTest()
        {
            // _befragung mit Fragen füllen
            _befragung.Fragen = Util.FrageListeErstellen();

            var auswerten = new Auswerten(_befragung);
            dynamic expando = new ExpandoObject();
            expando.cmd = "Auswerten";
            var jsonstring = JsonExtensions.ToJson(expando);

            var output = string.Empty;
            auswerten.Json_output += arg => output = arg;

            auswerten.Process(jsonstring);
            dynamic outputExpandoObject = output.FromJson();

            Assert.AreEqual(4, outputExpandoObject.payload.AnzahlFragen);
        }

        [TestMethod]
        public void RichtigeAntwortenZählenUndProzentTest()
        {
            // _befragung mit Fragen füllen
            _befragung.Fragen = Util.FrageListeErstellen();

            var auswerten = new Auswerten(_befragung);
            dynamic expando = new ExpandoObject();
            expando.cmd = "Auswerten";
            var jsonstring = JsonExtensions.ToJson(expando);

            var output = string.Empty;
            auswerten.Json_output += arg => output = arg;

            auswerten.Process(jsonstring);
            dynamic outputExpandoObject = output.FromJson();

            Assert.AreEqual(2, outputExpandoObject.payload.AnzahlRichtig);
            Assert.AreEqual(0.5, outputExpandoObject.payload.ProzentRichtig);
        }

        [TestMethod]
        public void FalscheAntwortenZählenUndProzentTest()
        {
            // _befragung mit Fragen füllen
            _befragung.Fragen = Util.FrageListeErstellen();

            var auswerten = new Auswerten(_befragung);
            dynamic expando = new ExpandoObject();
            expando.cmd = "Auswerten";
            var jsonstring = JsonExtensions.ToJson(expando);

            var output = string.Empty;
            auswerten.Json_output += arg => output = arg;

            auswerten.Process(jsonstring);
            dynamic outputExpandoObject = output.FromJson();

            Assert.AreEqual(1, outputExpandoObject.payload.AnzahlFalsch);
            Assert.AreEqual(0.25, outputExpandoObject.payload.ProzentFalsch);
        }

        [TestMethod]
        public void WeissNichtAntwortenZählenUndProzentTest()
        {
            // _befragung mit Fragen füllen
            _befragung.Fragen = Util.FrageListeErstellen();

            var auswerten = new Auswerten(_befragung);
            dynamic expando = new ExpandoObject();
            expando.cmd = "Auswerten";
            var jsonstring = JsonExtensions.ToJson(expando);

            var output = string.Empty;
            auswerten.Json_output += arg => output = arg;

            auswerten.Process(jsonstring);
            dynamic outputExpandoObject = output.FromJson();

            Assert.AreEqual(1, outputExpandoObject.payload.AnzahlWeissNicht);
            Assert.AreEqual(0.25, outputExpandoObject.payload.ProzentWeissNicht);
        }
    }
}
