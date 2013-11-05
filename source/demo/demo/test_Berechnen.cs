using System.Dynamic;
using NUnit.Framework;
using jsonserialization;

namespace demo
{
    [TestFixture]
    public class test_Berechnen
    {
        [Test]
        public void Addieren()
        {
            dynamic jo = new ExpandoObject();
            jo.cmd = "Addieren";
            jo.payload = new ExpandoObject();
            jo.payload.a = 2;
            jo.payload.b = 3;

            var sut = new Berechnen();
            string json_result = "";
            sut.Json_output += _ => json_result = _;

            var js = JsonExtensions.ToJson(jo);
            sut.Process(js);

            dynamic result = json_result.FromJson();

            Assert.AreEqual("Berechnungsergebnis", result.cmd);
            Assert.AreEqual(5, result.payload.resultat);
        } 
    }
}