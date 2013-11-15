using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;
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



        public List<Befragung.Frage> Fragen
        {
            get { return (List<Befragung.Frage>) GetValue( FragenProperty ); }
            set { SetValue( FragenProperty, value ); }
        }

        // Using a DependencyProperty as the backing store for Fragen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FragenProperty = 
    DependencyProperty.Register( "Fragen", typeof( List<Befragung.Frage> ), typeof( Befragen ), new UIPropertyMetadata( null ) );

        
    }
}
