using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jsonserialization;

namespace af.ui.test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod, Ignore]
        public void TestStarten()
        {
            dynamic expandoObject = new ExpandoObject();
            expandoObject.cmd = "Starten";

            // Json functionality available after NuGet installation and deinstallation of Microsoft.AspNet.Web.Helpers.Mvc 2.0.20710
            // System.Web.helpers v1.0.20105.407 is now available in lib-dir - but also not needed as reference.
            var json = JsonExtensions.ToJson(expandoObject);
            Console.WriteLine(json);
            //jsonObject.ToJson();

            var ui = new Ui();
            ui.Process(json);
        }

        [TestMethod, Ignore]
        public void TestFragebogenDatenSenden()
        {
            dynamic expandoObject = new ExpandoObject();
            expandoObject.cmd = "Fragebogen anzeigen";

            expandoObject.payload = new ExpandoObject();
            expandoObject.payload.Fragen = Util.FrageListeErstellen();

            Console.WriteLine( expandoObject.payload.Fragen[0].Text );

            var json = JsonExtensions.ToJson(expandoObject);
            dynamic testJsonObject = JsonExtensions.FromJson(json);
            Assert.AreEqual("Fragebogen anzeigen", testJsonObject.cmd);
            Console.WriteLine(testJsonObject.cmd);
            Assert.IsNotNull(testJsonObject);
            Assert.IsNotNull( testJsonObject.payload.Fragen );
            Assert.IsNotNull( testJsonObject.payload.Fragen[0], "Fragen[0] darf nicht null sein" );

            dynamic f = testJsonObject.payload.Fragen[0];
            Assert.AreEqual( "Was ist kein Säugetier?", f.Text, "Text aus der ersten Frage als dynamic gecastet darf nicht null sein." );

            Console.WriteLine( testJsonObject.payload.Fragen[0].Text );
            Assert.AreEqual( "Was ist kein Säugetier?", testJsonObject.payload.Fragen[0].Text );
            Assert.IsNotNull( testJsonObject.payload.Fragen[0].Text, "Fragen[0].Text darf nicht null sein" );
        }

        [TestMethod, Ignore, STAThread]
        public void TestBeantwortenKommandoSenden()
        {
            var ui = new Ui();
            ui.Json_output += jsonOutput => MessageBox.Show("Json_output: " + jsonOutput );
           
            var befragen = new Befragen( ui );

            var fragen = Util.FrageListeErstellen();
            befragen.Fragen = fragen;

            befragen.ShowDialog();
        }

        [TestMethod, Ignore]
        public void TestAuswertungAnzeigen()
        {
            dynamic expandoObject = new ExpandoObject();
            expandoObject.cmd = "Auswertung anzeigen";
            expandoObject.payload = new ExpandoObject();
            expandoObject.payload.AnzahlFragen = 10;
            expandoObject.payload.AnzahlRichtig = 3;
            expandoObject.payload.ProzentRichtig = 0.3;
            expandoObject.payload.AnzahlFalsch = 1;
            expandoObject.payload.ProzentFalsch = 0.1;
            expandoObject.payload.AnzahlWeissNicht = 6;
            expandoObject.payload.ProzentWeissNicht = 0.6;

            var json = JsonExtensions.ToJson( expandoObject );

            var ui = new Ui();
            ui.Process( json );
        }

        [TestMethod, Ignore]
        public void TestAuswertungAnzeigenDrückenUndAnzeigen()
        {
            dynamic start = new ExpandoObject();
            start.cmd = "Starten";

            var startJson = JsonExtensions.ToJson( start );
            var ui = new Ui();
            var uiOutput = string.Empty;
            ui.Json_output += jsonOutput => uiOutput = jsonOutput;
            ui.Process( startJson );

            // User muss Button Auswerten drücken
            dynamic result = uiOutput.FromJson();
            Assert.AreEqual( "Auswerten", result.cmd, "Dieser Test erwartet das Auswerten gedrückt wurde." );
            
            //TODO: Auswertung anzeigen (während das Programm noch läuft).
        }
        
    }
}
