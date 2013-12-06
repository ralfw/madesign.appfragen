using System;
using System.Dynamic;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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
        public void TestFragebogenBekommen()
        {
            // Neue Instanz von der Auswerten Klasse
            var auswerten = new Auswerten(new Befragung());

            // ExpandoObject mit Kommando erstellen zum Anfeuern von Auswerten
            dynamic expando = new ExpandoObject();
            expando.cmd = "Auswerten";

            // ExpandoObject to JSONstring
            var jsonstring = JsonExtensions.ToJson(expando);

            var output = string.Empty;
            auswerten.Json_output += param => AuswertenOnJsonOutput(param, out output);
            auswerten.Process(jsonstring);

            // JSONstring to ExpandoObject
            dynamic outputExpandoObject = output.FromJson();

            // Vergleich (expected, actual)
            Assert.AreEqual("Auswertung anzeigen", outputExpandoObject.cmd);
        }

        private void AuswertenOnJsonOutput(string s, out string output)
        {
            output = s;
        }
    }
}
