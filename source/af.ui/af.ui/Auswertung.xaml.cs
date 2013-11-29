using System.Collections.ObjectModel;
using System.Windows;

namespace af.ui
{
    /// <summary>
    /// Interaktionslogik für Auswertung.xaml
    /// </summary>
    public partial class Auswertung : Window
    {
        private readonly Ui _ui;

        public Auswertung(Ui ui)
        {
            InitializeComponent();

            _ui = ui;
        }

        public Auswertung(Ui ui, ObservableCollection<Category> classes)
        {
            InitializeComponent();

            _ui = ui;
            DataContext = classes;
        }
    }
}
