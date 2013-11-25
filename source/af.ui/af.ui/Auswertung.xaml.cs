using System.Collections.ObjectModel;
using System.Windows;

namespace af.ui
{
    /// <summary>
    /// Interaktionslogik für Auswertung.xaml
    /// </summary>
    public partial class Auswertung : Window
    {
        public Auswertung(ObservableCollection<Category> classes)
        {
            InitializeComponent();

            DataContext = classes;
        }
    }
}
