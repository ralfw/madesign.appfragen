﻿using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using af.contracts;
using jsonserialization;

namespace af.modul.befragen.test
{
    [TestClass]
    public class UnitTest1
    {
        private const string FILENAME = "Test-dateiname";

        [TestMethod]
        public void TestMethod1()
        {
            dynamic input = new ExpandoObject();
            input.cmd = "Fragenkatalog laden";
            input.payload = new ExpandoObject();

            input.payload.Dateiname = FILENAME;

            var befragen = new Befragen(new Befragung{ Dateiname = FILENAME});
            var jsonResult = string.Empty;
            befragen.Json_output += _ => jsonResult = _;

            var json = JsonExtensions.ToJson(input);
            befragen.Process(json);

            dynamic result = jsonResult.FromJson();

            Assert.AreEqual(result.Dateiname, FILENAME);

        }
    }
}
