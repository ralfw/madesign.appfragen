using System.Collections.ObjectModel;
using System.Windows;

namespace af.ui
{
    /// <summary>
    /// Interaktionslogik für Auswertung.xaml
    /// </summary>
    public partial class Auswertung : Window
    {
        private readonly ObservableCollection<Category> classes;

        public Auswertung()
        {
            InitializeComponent();

            classes = new ObservableCollection<Category>();
            classes.Add( new Category{
                Class = "richtig",
                Anzahl=50.0,
                AnzahlProzent=50.0});
            classes.Add( new Category
            {
                Class = "falsch",
                Anzahl = 30.0,
                AnzahlProzent = 30.0
            } );
            classes.Add( new Category
            {
                Class = "weiß nicht",
                Anzahl = 20.0,
                AnzahlProzent = 20.0
            } );
            DataContext = classes;
        }
    }
}
