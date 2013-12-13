using System;
using System.Dynamic;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using jsonserialization;

namespace af.ui.test
{
    /// <summary>
    /// Summary description for UiTest
    /// </summary>
    [TestClass]
    public class UiTest
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
        public void StartenKommandZeigtDieBefragenSeite()
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

        [TestMethod]
        public void JsonSerialiationTestMitEinemKommando()
        {
            dynamic eingangsObjekt = new ExpandoObject();
            eingangsObjekt.cmd = "Fragebogen anzeigen";
            eingangsObjekt.payload = new ExpandoObject();
            eingangsObjekt.payload.Fragen = Util.FrageListeErstellen();

            Console.WriteLine( eingangsObjekt.payload.Fragen[0].Text );

            // wandeln in json und zurück wandeln
            var json = JsonExtensions.ToJson(eingangsObjekt);
            dynamic ausgangsObjekt = JsonExtensions.FromJson(json);

            Assert.IsNotNull( ausgangsObjekt );
            Assert.AreEqual( eingangsObjekt.cmd, ausgangsObjekt.cmd );
            Console.WriteLine(ausgangsObjekt.cmd);
            Assert.IsNotNull( ausgangsObjekt.payload.Fragen );
            Assert.IsNotNull( ausgangsObjekt.payload.Fragen[0], "Fragen[0] darf nicht null sein" );

            dynamic f = ausgangsObjekt.payload.Fragen[0];
            Assert.AreEqual( eingangsObjekt.payload.Fragen[0].Text, f.Text, "Text aus der ersten Frage als dynamic gecastet muss dem EingangsObjekt-Text entprechen." );

            Assert.IsNotNull( ausgangsObjekt.payload.Fragen[0].Text, "Fragen[0].Text darf nicht null sein" );
            Console.WriteLine( ausgangsObjekt.payload.Fragen[0].Text );
            Assert.AreEqual( eingangsObjekt.payload.Fragen[0].Text, ausgangsObjekt.payload.Fragen[0].Text );
        }

        [TestMethod, Ignore]
        public void FragebogenWirdDargestellt()
        {
            // Visueller Test, dass die Fragen aus der Methode FrageListeErstellen angezeigt werden.
            var ui = new Ui();
            var befragen = new Befragen( ui );
            var fragen = Util.FrageListeErstellen();
            befragen.Fragen = fragen.ConvertToViewModelFragen();

            befragen.ShowDialog();
        }

        [TestMethod, Ignore, STAThread]
        public void ClickAufRadioButtonSendetEinBeantwortenKommando()
        {
            var ui = new Ui();
            ui.Json_output += jsonOutput => MessageBox.Show("Json_output: " + jsonOutput );
           
            var befragen = new Befragen( ui );

            var fragen = Util.FrageListeErstellen();
            befragen.Fragen = fragen.ConvertToViewModelFragen();

            befragen.ShowDialog();
        }

        [TestMethod, Ignore]
        public void AuswertungAnzeigenKommandoZeigtAuswertungAn()
        {
            dynamic auswertungAnzeigenKommando = new ExpandoObject();
            auswertungAnzeigenKommando.cmd = "Auswertung anzeigen";
            auswertungAnzeigenKommando.payload = new ExpandoObject();
            auswertungAnzeigenKommando.payload.AnzahlFragen = 10;
            auswertungAnzeigenKommando.payload.AnzahlRichtig = 3;
            auswertungAnzeigenKommando.payload.ProzentRichtig = 0.3;
            auswertungAnzeigenKommando.payload.AnzahlFalsch = 1;
            auswertungAnzeigenKommando.payload.ProzentFalsch = 0.1;
            auswertungAnzeigenKommando.payload.AnzahlWeissNicht = 6;
            auswertungAnzeigenKommando.payload.ProzentWeissNicht = 0.6;

            var json = JsonExtensions.ToJson( auswertungAnzeigenKommando );

            var ui = new Ui();
            ui.Process( json );
            // TODO: Fix test: Dialog wird sofort wieder geschlossen.
        }

        [TestMethod, Ignore]
        public void TestAuswertungAnzeigenDrückenSendetAuswertenKommando()
        {
            // Visueller Test mit MessageBox: "cmd" muss "Auswerten" sein.
            var ui = new Ui();
            ui.Json_output += jsonOutput => MessageBox.Show( "Json_output: " + jsonOutput );

            var befragen = new Befragen( ui );

            var fragen = Util.FrageListeErstellen();
            befragen.Fragen = fragen.ConvertToViewModelFragen();
            befragen.IstAuswertenAktiv = true;

            befragen.ShowDialog();
        }
        
    }
}
