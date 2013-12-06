using System;
using System.Dynamic;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace af.auswerten.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestFragebogenBekommen()
        {
            dynamic expandoObject = new ExpandoObject();
            expandoObject.cmd = "Auswerten";
            // ...
        }
    }
}
