using System;
using System.Collections.Generic;
using System.Dynamic;
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
            var json = jsonserialization.JsonExtensions.ToJson(expandoObject);
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
            expandoObject.payload.Fragen = FrageListeErstellen();

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

            var ui = new Ui();
            ui.Process(json);
        }

        [TestMethod, Ignore]
        public void TestErsteAntwortAuswählen()
        {
            dynamic expandoObject = new ExpandoObject();
            expandoObject.cmd = "Fragebogen anzeigen";
            expandoObject.payload = new ExpandoObject();
            expandoObject.payload.Fragen = FrageListeErstellen();

            var json = JsonExtensions.ToJson(expandoObject);
            var ui = new Ui();
            var uiOutput = string.Empty;
            ui.Json_output += jsonOutput => uiOutput = jsonOutput; ;
            ui.Process(json);

            dynamic result = uiOutput.FromJson();
            Assert.AreEqual( "F1A1", result.payload.AntwortmoeglichkeitId, "Dieser Test erwartet das aus der 1. Frage die Antwortmögl. 1 gewählt wurde." );
        }

        [TestMethod, Ignore]
        public void TestAuswertungAnzeigen()
        {
            dynamic expandoObject = new ExpandoObject();
            expandoObject.cmd = "Auswertung anzeigen";
            expandoObject.payload = "Bau dir selber 'ne Auswertung";

            var json = JsonExtensions.ToJson( expandoObject );

            var ui = new Ui();
            ui.Process( json );
        }

        private List<Befragung.Frage> FrageListeErstellen()
        {
            var frageListe = new List<Befragung.Frage>();
            var frage = new Befragung.Frage
                            {
                                Text = "Was ist kein Säugetier?",
                                Antwortmöglichkeiten = new List<Befragung.Antwortmöglichkeit>
                                                           {
                                                               ErstelleAntwortmöglichkeit("F1A1", "Hund"),
                                                               ErstelleAntwortmöglichkeit("F1A2", "Katze"),
                                                               ErstelleAntwortmöglichkeit("F1A3", "Fisch", true),
                                                               ErstelleAntwortmöglichkeit("F1A4", "Weiß nicht")
                                                           }
                            };
            frageListe.Add( frage );

            var frage2 = new Befragung.Frage
                             {
                                 Text = "Was ist 2+3?",
                                 Antwortmöglichkeiten = new List<Befragung.Antwortmöglichkeit>
                                                            {
                                                                ErstelleAntwortmöglichkeit("F2A1", "3"),
                                                                ErstelleAntwortmöglichkeit("F2A2", "5", true),
                                                                ErstelleAntwortmöglichkeit("F2A3", "8"),
                                                                ErstelleAntwortmöglichkeit("F2A4", "Weiß nicht")
                                                            }
                             };
            frageListe.Add( frage2 );

            var frage3 = FrageErstellen( "3", 1 );
            frageListe.Add( frage3 );

            return frageListe;
        }

        /// <summary>
        /// Erstellt eine Frage mit defaultText und 4 default Antwortmöglichkeiten.
        /// </summary>
        /// <param name="nummer"></param>
        /// <param name="nummerDerRichtigenAntwort">1-3</param>
        /// <returns>Gibt eine Frage mit Antwortmöglichkeiten zurück</returns>
        private Befragung.Frage FrageErstellen( string nummer, int nummerDerRichtigenAntwort )
        {
            if ( nummerDerRichtigenAntwort < 1 || nummerDerRichtigenAntwort > 3 )
            {
                nummerDerRichtigenAntwort = 1;
            }
            var frage = new Befragung.Frage
            {
                Text = "Frage Nr. " + nummer,
                Antwortmöglichkeiten = new List<Befragung.Antwortmöglichkeit>
                        {
                            new Befragung.Antwortmöglichkeit
                                {
                                    Id = "F" + nummer + "A1",
                                    IstAlsAntwortSelektiert = false,
                                    IstRichtigeAntwort = nummerDerRichtigenAntwort == 1,
                                    Text = "Antwortmöglichkeit 1"
                                },
                            new Befragung.Antwortmöglichkeit
                                {
                                    Id = "F" + nummer + "A2",
                                    IstAlsAntwortSelektiert = false,
                                    IstRichtigeAntwort = nummerDerRichtigenAntwort == 2,
                                    Text = "Antwortmöglichkeit 2"
                                },
                            new Befragung.Antwortmöglichkeit
                                {
                                    Id = "F" + nummer + "A3",
                                    IstAlsAntwortSelektiert = false,
                                    IstRichtigeAntwort = nummerDerRichtigenAntwort == 3,
                                    Text = "Antwortmöglichkeit 3"
                                },
                            new Befragung.Antwortmöglichkeit
                                {
                                    Id = "F" + nummer + "A4",
                                    IstAlsAntwortSelektiert = false,
                                    IstRichtigeAntwort = false,
                                    Text = "Antwortmöglichkeit weiß nicht"
                                },
                        }
            };
            return frage;
        }

        private Befragung.Antwortmöglichkeit ErstelleAntwortmöglichkeit( string id, string text, bool istRichtigeAntwort = false )
        {
            var ant = new Befragung.Antwortmöglichkeit
                          {
                              Id = id,
                              Text = text,
                              IstRichtigeAntwort = istRichtigeAntwort
                          };

            return ant;
        }
    }
}
