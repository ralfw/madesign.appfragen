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


        [TestMethod]
        public void TestQuestionaireLoading()
        {
            dynamic input = new ExpandoObject();
            input.cmd = "Fragenkatalog laden";
            input.payload = new ExpandoObject();

            input.payload.Dateiname = FILENAME;

            var befragen = new Befragen(new Befragung { Dateiname = FILENAME });
            var jsonResult = string.Empty;
            befragen.Json_output += _ => jsonResult = _;

            var json = JsonExtensions.ToJson(input);
            befragen.Process(json);

            dynamic result = jsonResult.FromJson();

            dynamic dateiname = result.Dateiname;
            Assert.AreEqual("Saeugetiere.txt", dateiname);
            dynamic fragen = result.Fragen[0];

            Assert.AreEqual("Was ist kein Säugetier?", fragen.Text);
            dynamic antwortmöglichkeiten = fragen.Antwortmöglichkeiten;
            Assert.AreEqual(4, antwortmöglichkeiten.Length);
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

                    default:
                        Assert.AreEqual(false, antwortmöglichkeit.IstAlsAntwortSelektiert);
                        break;
                }
            }
        }
    }
}
