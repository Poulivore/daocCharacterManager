using System;
using System.Collections.Generic;
using System.Windows;

namespace daocCharacterManager.Dialogs
{
	public partial class DialogImportCharacter : Window
	{
        	public DialogImportCharacter( List<string> activeClusterList) {
            		InitializeComponent();

			ClusterList.ItemsSource = activeClusterList;
			ClusterList.SelectedIndex = 0;
        	}

        	private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

	private void comboBoxFindCharacter_Click( object sender, RoutedEventArgs e ) {
	
	    List<Tuple<string, string>> foundCharacterList = CharacterManager.SearchCharacterFromHerald( characterNameTextBox.Text, ClusterList.SelectedItem.ToString() );

	    CharacterList.ItemsSource = foundCharacterList;
	    CharacterList.SelectedIndex = 0;

	}
    }
}

