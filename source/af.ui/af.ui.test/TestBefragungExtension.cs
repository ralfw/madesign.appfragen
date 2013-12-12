using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace af.ui.test
{
    public static class TestBefragungExtension
    {
        public static List<BefragungViewModel.Frage> ConvertToViewModelFragen( this List<TestBefragung.Frage> testBefragung )
        {
            var result = new List<BefragungViewModel.Frage>();

            foreach ( var currentFrage in testBefragung )
            {
                var frage = new BefragungViewModel.Frage
                                {
                                    Text = currentFrage.Text,
                                    Antwortmöglichkeiten = new List<BefragungViewModel.Antwortmöglichkeit>()
                                };
                foreach ( var currentAntwortmöglichkeit in currentFrage.Antwortmöglichkeiten )
                {
                    var antwortmöglichkeit = new BefragungViewModel.Antwortmöglichkeit
                                                 {
                                                     Id = currentAntwortmöglichkeit.Id,
                                                     Text = currentAntwortmöglichkeit.Text,
                                                     IstAlsAntwortSelektiert = currentAntwortmöglichkeit.IstAlsAntwortSelektiert
                                                 };
                    frage.Antwortmöglichkeiten.Add(antwortmöglichkeit);
                }
                result.Add( frage );
            }
            return result;
        }
    }
}
