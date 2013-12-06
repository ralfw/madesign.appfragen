using System.Collections.Generic;
using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using af.contracts;
using jsonserialization;

namespace af.modul.befragen.test
{
    [TestClass]
    public class BefragenUnitTests
    {
        private const string FILENAME = "Saeugetiere.txt";
        private Befragen _befragen;

        [TestMethod]
        public void QuestionaireLoading()
        {
            dynamic input = new ExpandoObject();
            input.cmd = "Fragenkatalog laden";
            input.payload = new ExpandoObject();

            input.payload.Dateiname = FILENAME;

            _befragen = new Befragen(new Befragung { Dateiname = FILENAME });
            var jsonResult = string.Empty;
            _befragen.Json_output += _ => jsonResult = _;

            var json = JsonExtensions.ToJson(input);
            _befragen.Process(json);

            dynamic result = jsonResult.FromJson();

            dynamic dateiname = result.Dateiname;
            Assert.AreEqual("Saeugetiere.txt", dateiname);
            dynamic fragen = result.Fragen[0];

            Assert.AreEqual("Was ist kein Säugetier?", fragen.Text);
            dynamic antwortmöglichkeiten = fragen.Antwortmöglichkeiten;
            Assert.AreEqual(5, antwortmöglichkeiten.Length);
            foreach (dynamic antwortmöglichkeit in antwortmöglichkeiten)
            {
                switch ((string)antwortmöglichkeit.Id)
                {
                    case "1":
                        Assert.AreEqual(false, antwortmöglichkeit.IstRichtigeAntwort);
                        Assert.AreEqual("Maus", antwortmöglichkeit.Text);
                        break;
                    case "2":
                        Assert.AreEqual(true, antwortmöglichkeit.IstRichtigeAntwort);
                        Assert.AreEqual("Ameise", antwortmöglichkeit.Text);
                        break;
                    case "3":
                        Assert.AreEqual(false, antwortmöglichkeit.IstRichtigeAntwort);
                        Assert.AreEqual("Elefant", antwortmöglichkeit.Text);
                        break;
                    case "4":
                        Assert.AreEqual(false, antwortmöglichkeit.IstRichtigeAntwort);
                        Assert.AreEqual("Katze", antwortmöglichkeit.Text);
                        break;
                    case "5":
                         Assert.AreEqual(false, antwortmöglichkeit.IstRichtigeAntwort);
                        Assert.AreEqual("Weiß nicht", antwortmöglichkeit.Text);
                        break;
                    default:
                        Assert.AreEqual(false, antwortmöglichkeit.IstAlsAntwortSelektiert);
                        break;
                }
            }
        }

        [TestMethod]
        public void AnsweringQuestion()
        {
            var befragung = new Befragung
                                {
                                    Dateiname = "Testdateiname",
                                };
            befragung.Reset();
            befragung.Fragen.Add(
                new Befragung.Frage
                    {
                        Text = "Was ist kein Säugetier",
                        Antwortmöglichkeiten = new List<Befragung.Antwortmöglichkeit>
                                                   {
                                                       new Befragung.Antwortmöglichkeit
                                                           {
                                                               Id = "1",
                                                               IstAlsAntwortSelektiert = false,
                                                               IstRichtigeAntwort = true,
                                                               Text = "Ameise"
                                                           }
                                                   }
                    });

            Assert.AreEqual(false, befragung.Fragen[0].Antwortmöglichkeiten[0].IstAlsAntwortSelektiert);
            var befragen = new Befragen(befragung);
            dynamic input = new ExpandoObject();
            input.cmd = "Beantworten";
            input.payload = new ExpandoObject();
            input.payload.AntwortmoeglichkeitId = "1";

            var jsonResult = string.Empty;
            befragen.Json_output += _ => jsonResult = _;

            var json = JsonExtensions.ToJson(input);
            befragen.Process(json);

            dynamic result = jsonResult.FromJson();
            dynamic fragen = result.Fragen;
            Assert.IsNotNull(fragen[0]);
            Assert.AreEqual(true, fragen[0].Antwortmöglichkeiten[0].IstAlsAntwortSelektiert);
        }
    }
}
