using System;
using System.Collections.Generic;
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
            var f = new Foo {Name = "peter", Age = 42};
            var js = f.ToJson();
            Console.WriteLine(js);

            dynamic jo = js.FromJson();
            Console.WriteLine(jo.Name);
            Console.WriteLine(jo.Age);
        }
    }

    class Foo
    {
        public string Name { get; set; }
        public int Age;
    }
}
