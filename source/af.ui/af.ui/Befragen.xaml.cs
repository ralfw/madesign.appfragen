using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using af.contracts;

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

        private Ui _ui;

        public Befragen(Ui ui)
        {
            _ui = ui;
            InitializeComponent();
        }

        public List<Befragung.Frage> Fragen
        {
            get { return (List<Befragung.Frage>) GetValue( FragenProperty ); }
            set { SetValue( FragenProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for Fragen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FragenProperty =
            DependencyProperty.Register("Fragen", typeof(List<Befragung.Frage>), typeof(Befragen), new UIPropertyMetadata(null));

        public ICommand RadioClicked
        {
            get
            {
                return _radioClicked ?? (_radioClicked = new RelayCommand(param => _ui.SendCommand((string)param)));
            }
        }

        private ICommand _radioClicked;
    }
}
