using System.Collections.Generic;
using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using af.contracts;
using jsonserialization;

namespace af.modul.befragen.test
{
    [TestClass]
    public class UnitTest1
    {
        private string filename = null;

        [TestMethod]
        public void TestMethod1()
        {
            var befragung = new Befragung();
            befragung.Reset();
            befragung.Fragen.Add(
                new Befragung.Frage
                    {
                        Text = "Was ist kein Säugetier?",
                        Antwortmöglichkeiten = new List<Befragung.Antwortmöglichkeit>
                                                   {
                                                       new Befragung.Antwortmöglichkeit
                                                           {
                                                               Id = "F1A1",
                                                               Text = "Hund"
                                                           },
                                                       new Befragung.Antwortmöglichkeit
                                                           {
                                                               Id = "F1A2",
                                                               Text = "Katze"
                                                           },
                                                       new Befragung.Antwortmöglichkeit
                                                           {
                                                               Id = "F1A3",
                                                               Text = "Fisch",
                                                               IstRichtigeAntwort = true
                                                           },
                                                       new Befragung.Antwortmöglichkeit
                                                           {
                                                               Id = "F1A4",
                                                               Text = "Weiß nicht"
                                                           },
                                                   }
                    });
            var befragen = new Befragen(befragung);
            filename = "FragekatalogTestName1";
            befragen.Process(filename);
            befragen.Json_output += OnResponse;
        }

        private void OnResponse(string obj)
        {
            dynamic output = obj.FromJson();
            Assert.AreEqual(output.Fragen.Text, filename);
        }
    }
}
