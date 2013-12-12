using System.Collections.Generic;

namespace af.ui.test
{
    public class Util
    {
        /// <summary>
        /// Erstellt eine Frage mit defaultText und 4 default Antwortmöglichkeiten.
        /// </summary>
        /// <param name="nummer"></param>
        /// <param name="nummerDerRichtigenAntwort">1-3</param>
        /// <returns>Gibt eine Frage mit Antwortmöglichkeiten zurück</returns>
        public static TestBefragung.Frage FrageErstellen( string nummer, int nummerDerRichtigenAntwort )
        {
            if ( nummerDerRichtigenAntwort < 1 || nummerDerRichtigenAntwort > 3 )
            {
                nummerDerRichtigenAntwort = 1;
            }
            var frage = new TestBefragung.Frage
            {
                Text = "Frage Nr. " + nummer,
                Antwortmöglichkeiten = new List<TestBefragung.Antwortmöglichkeit>
                        {
                            ErstelleAntwortmöglichkeit( "F" + nummer + "A1", "Antwortmöglichkeit 1", nummerDerRichtigenAntwort == 1),
                            ErstelleAntwortmöglichkeit( "F" + nummer + "A2", "Antwortmöglichkeit 2", nummerDerRichtigenAntwort == 2),
                            ErstelleAntwortmöglichkeit( "F" + nummer + "A3", "Antwortmöglichkeit 3", nummerDerRichtigenAntwort == 3),
                            ErstelleAntwortmöglichkeit( "F" + nummer + "A4", "Antwortmöglichkeit weiß nicht"),
                        }
            };
            return frage;
        }

        public static TestBefragung.Antwortmöglichkeit ErstelleAntwortmöglichkeit( string id, string text, bool istRichtigeAntwort = false, bool istAlsAntwortSelektiert = false )
        {
            var ant = new TestBefragung.Antwortmöglichkeit
            {
                Id = id,
                Text = text,
                IstAlsAntwortSelektiert = istAlsAntwortSelektiert,
                IstRichtigeAntwort = istRichtigeAntwort
            };

            return ant;
        }

        public static List<TestBefragung.Frage> FrageListeErstellen()
        {
            var frageListe = new List<TestBefragung.Frage>();
            var frage = new TestBefragung.Frage
            {
                Text = "Was ist kein Säugetier?",
                Antwortmöglichkeiten = new List<TestBefragung.Antwortmöglichkeit>
                                                           {
                                                               ErstelleAntwortmöglichkeit("F1A1", "Hund"),
                                                               ErstelleAntwortmöglichkeit("F1A2", "Katze"),
                                                               ErstelleAntwortmöglichkeit("F1A3", "Fisch", true),
                                                               ErstelleAntwortmöglichkeit("F1A4", "Weiß nicht")
                                                           }
            };
            frageListe.Add( frage );

            var frage2 = new TestBefragung.Frage
            {
                Text = "Was ist 2+3?",
                Antwortmöglichkeiten = new List<TestBefragung.Antwortmöglichkeit>
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
    }
}
