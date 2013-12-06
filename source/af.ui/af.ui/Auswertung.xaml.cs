using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

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

        public ICommand AuswertungSchließenClicked
        {
            get
            {
                return _auswertungSchließenClicked ?? ( _auswertungSchließenClicked = new RelayCommand( param => _ui.SendCommand( Ui.AuswertungBeenden, "" ) ) );
            }
        }

        private ICommand _auswertungSchließenClicked;
    }
}
