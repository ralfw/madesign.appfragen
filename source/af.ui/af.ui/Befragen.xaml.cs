using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

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

        public List<BefragungViewModel.Frage> Fragen
        {
            get { return (List<BefragungViewModel.Frage>) GetValue( FragenProperty ); }
            set { SetValue( FragenProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for Fragen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FragenProperty =
            DependencyProperty.Register("Fragen", typeof(List<BefragungViewModel.Frage>), typeof(Befragen), new UIPropertyMetadata(null));

        public bool IstAuswertenAktiv
        {
            get { return (bool) GetValue( IstAuswertenAktivProperty ); }
            set { SetValue( IstAuswertenAktivProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for IstAuswertenAktiv.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IstAuswertenAktivProperty =
            DependencyProperty.Register( "IstAuswertenAktiv", typeof( bool ), typeof( Befragen ), new UIPropertyMetadata( false ) );
        
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
                        var openFileDialog = new OpenFileDialog
                                                 {
                                                     Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                                                     FilterIndex = 2,
                                                     RestoreDirectory = true
                                                 };

                        var result = openFileDialog.ShowDialog();

                        if ( result == true )
                        {
                            // nehme Ergebnis und sende es
                            var zuÖffnendeDatei = openFileDialog.FileName;
                            _ui.SendCommand( Ui.FragenkatalogLaden, zuÖffnendeDatei );
                        }
                    } ) );
            }
        }

        private ICommand _ladeFragenkatalogClicked;
    }
}
