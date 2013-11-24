using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace jsonserialization.tests
{
    [TestFixture]
    public class test_JsonExtensions
    {
        [Test]
        public void ToAndFro()
        {
            var f = new Foo {Name = "peter", Age = 42, TooOld = false};
            var js = f.ToJson();
            Console.WriteLine(js);

            dynamic jo = js.FromJson();
            Console.WriteLine(jo.Name);
            Console.WriteLine(jo.Age);


            dynamic ex = new ExpandoObject();
            ex.Name = "Mary";
            ex.Age = 39;
            ex.TooOld = false;
            js = JsonExtensions.ToJson(ex);
            Console.WriteLine(js);
        }

        [Test]
        public void JsonFx_POCO()
        {
            var jsr = new JsonFx.Json.JsonReader();
            var jsw = new JsonFx.Json.JsonWriter();

            var f = new Foo { Name = "peter", Age = 42, TooOld = false };
            f.Bars = new List<Bar>();
            f.Bars.Add(new Bar(){Str = "Isestr", Ort = "Hamburg"});
            var json = jsw.Write(f);
            Console.WriteLine(json);

            dynamic df = jsr.Read(json);
            Assert.AreEqual("peter", df.Name);
            Assert.IsFalse(df.TooOld);
            Assert.AreEqual(1, df.Bars.Length);


            var foo = jsr.Read<Foo>(json);
            Console.WriteLine(foo.Name);
        }


        [Test]
        public void JsonFx_Expando()
        {
            var jsr = new JsonFx.Json.JsonReader();
            var jsw = new JsonFx.Json.JsonWriter();

            dynamic cmd = new ExpandoObject();
            cmd.cmd = "Anzeigen";
            dynamic frage = new ExpandoObject();
            frage.Text = "Welches Tier ist kein Säugetier?";
            cmd.payload = frage;

            var antwortoptionen = new List<dynamic>();
            dynamic antwortoption = new ExpandoObject();
            antwortoption.Text = "Hund";
            antwortoption.IstKorrekt = false;
            antwortoptionen.Add(antwortoption);
            antwortoption = new ExpandoObject();
            antwortoption.Text = "Ameise";
            antwortoption.IstKorrekt = true;
            antwortoptionen.Add(antwortoption);
            frage.Antwortoptionen = antwortoptionen;

            var json = jsw.Write(cmd);
            Console.WriteLine(json);

            dynamic dc = jsr.Read(json);
            Console.WriteLine(dc.cmd);
            Console.WriteLine(dc.payload.Text);
            foreach (dynamic ao in dc.payload.Antwortoptionen)
                Console.WriteLine("  {0}, {1}", ao.Text, ao.IstKorrekt);
        }
    }

    class Foo
    {
        public string Name { get; set; }
        public int Age;
        public bool TooOld;
        public List<Bar> Bars;
    }

    class Bar
    {
        public string Str;
        public string Ort;
    }
}
