using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace daocCharacterManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> activeClusterList;

        public MainWindow()
        {
            InitializeComponent();

	    CharacterManager.Initialize();
	    activeClusterList = CharacterManager.LoadClusterListFromHerald();
	    /*foreach( string data in activeClusterList ) {
		    MessageBox.Show( data );
	    }*/
        }

	private void menuExit_Click( object sender, RoutedEventArgs e ) {
            Application.Current.Shutdown();
        }

	private void menuImportCharacter_Click( object sender, RoutedEventArgs e ) {
		Dialogs.DialogImportCharacter importCharacterDialog = new Dialogs.DialogImportCharacter( activeClusterList );

		if(importCharacterDialog.ShowDialog() == true) {
		}
	}

    }
}
