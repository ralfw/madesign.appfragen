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


        [Test]
        public void Subtrahieren()
        {
            var sut = new Berechnen();
            string json_output = "";
            sut.Json_output += _ => json_output = _;

            var json_input = string.Format(Properties.Resources.json_Subtrahieren, 2, 3);
            sut.Process(json_input);

            dynamic result = json_output.FromJson();

            Assert.AreEqual("Berechnungsergebnis", result.cmd);
            Assert.AreEqual(-1, result.payload.resultat);
        }


        [Test]
        public void Multiplizieren()
        {
            var sut = new Berechnen();
            string json_output = "";
            sut.Json_output += _ => json_output = _;

            var json_input = new Multiplikationskommando { payload = new Multiplikationskommando.Payload { a = 2, b = 3 } }
                             .ToJson();
            sut.Process(json_input);

            dynamic result = json_output.FromJson();

            Assert.AreEqual("Berechnungsergebnis", result.cmd);
            Assert.AreEqual(6, result.payload.resultat);
        }


        class Multiplikationskommando
        {
            public string cmd = "Multiplizieren";
            public Payload payload;

            public class Payload
            {
                public int a;
                public int b;
            }
        }
    }
}