using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace af.ui
{
    /// <summary>
    /// Interaction logic for Befragen.xaml
    /// </summary>
    public partial class Befragen : Window
    {
        public Befragen()
        {
            InitializeComponent();
        }

        private readonly Ui _ui;

        public Befragen(Ui ui)
        {
            _ui = ui;
            InitializeComponent();
        }

        public List<UiBefragung.Frage> Fragen
        {
            get { return (List<UiBefragung.Frage>) GetValue( FragenProperty ); }
            set { SetValue( FragenProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for Fragen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FragenProperty =
            DependencyProperty.Register("Fragen", typeof(List<UiBefragung.Frage>), typeof(Befragen), new UIPropertyMetadata(null));

        public ICommand RadioClicked
        {
            get
            {
                return _radioClicked ?? ( _radioClicked = new RelayCommand( param => _ui.SendCommand( Ui.Beantworten, (string) param ) ) );
            }
        }

        private ICommand _radioClicked;

        public ICommand AuswertungAnzeigenClicked
        {
            get
            {
                return _auswertungAnzeigenClicked ?? ( _auswertungAnzeigenClicked = new RelayCommand( param => _ui.SendCommand( Ui.Auswerten, "" ) ) );
            }
        }

        private ICommand _auswertungAnzeigenClicked;

        public ICommand LadeFragenkatalogClicked
        {
            get
            {
                return _ladeFragenkatalogClicked ?? ( _ladeFragenkatalogClicked = new RelayCommand( param =>
                    {
                        // TODO: öffne OpenDialog
                        // nehme Ergebnis und sende es
                        var zuÖffnendeDatei = "Pisa.txt";
                        _ui.SendCommand( Ui.FragenkatalogLaden, zuÖffnendeDatei );
                    } ) );
            }
        }

        private ICommand _ladeFragenkatalogClicked;

        
    }
}
