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

        public static readonly DependencyProperty AnzahlFragenProperty =
            DependencyProperty.Register("AnzahlFragen", typeof (int), typeof (Auswertung), new PropertyMetadata(default(int)));

        public int AnzahlFragen
        {
            get { return (int) GetValue(AnzahlFragenProperty); }
            set { SetValue(AnzahlFragenProperty, value); }
        }


    }
}
