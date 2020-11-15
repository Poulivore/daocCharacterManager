using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
	private ObservableCollection<Character> characterList { get; set; }

	GridViewColumnHeader _lastHeaderClicked = null;
	ListSortDirection _lastDirection = ListSortDirection.Ascending;

        public MainWindow()
        {
            InitializeComponent();

	    CharacterManager.Initialize();
	    activeClusterList = CharacterManager.LoadClusterListFromHerald();

	    characterList = new ObservableCollection<Character>(CharacterManager.LoadCharacterListFromDisk());

	    RefreshCharacterList();
        }

	private void menuExit_Click( object sender, RoutedEventArgs e ) {
            Application.Current.Shutdown();
        }

	private void menuImportCharacter_Click( object sender, RoutedEventArgs e ) {
		Dialogs.DialogImportCharacter importCharacterDialog = new Dialogs.DialogImportCharacter( activeClusterList );

		if(importCharacterDialog.ShowDialog() == true) {
			string characterId = importCharacterDialog.CharacterId;

			string url = "http://api.camelotherald.com/character/info/" + characterId;
                HttpClient http = new HttpClient();

                try {
                HttpContent responseMessage = http.GetAsync( url ).Result.Content;

                    if( responseMessage != null ) {
                        string characterData = responseMessage.ReadAsStringAsync().Result;
                        Json.ErrorJson errorJson = new Json.ErrorJson();
                        var options = new JsonSerializerOptions
                        {
                            AllowTrailingCommas = true
                        };

                        try {
                            errorJson = JsonSerializer.Deserialize<Json.ErrorJson>(characterData, options );
                        } catch( Exception exception ) {
                            MessageBox.Show( exception.ToString() );
                        }

                        if( errorJson.error != "" ) {
                            MessageBox.Show( "Error : " + errorJson.error );
                        } else {
                            try {
                                Json.CharacterInfoJson characterInfoJson = new Json.CharacterInfoJson();
                                characterInfoJson = JsonSerializer.Deserialize<Json.CharacterInfoJson>(characterData, options );
                                Character character = new Character( characterInfoJson.character_web_id, characterInfoJson.name, characterInfoJson.server_name, characterInfoJson.ClassName );
                                character.RealmPoints = characterInfoJson.realm_war_stats.current.realm_points;
                                character.ClassName = characterInfoJson.ClassName;
                                character.TotalKills = characterInfoJson.realm_war_stats.current.player_kills.total.kills;
                                character.TotalSoloKills = characterInfoJson.realm_war_stats.current.player_kills.total.solo_kills;

                                CharacterManager.CreateCharacter( character );

                                characterList.Add( character );

                            } catch( Exception exception ) {
                                MessageBox.Show( exception.ToString() );
                            }
                        }
                    }
                } catch( Exception exception) {

                }

		}
	}

	private void menuRefreshCharacterList_Click( object sender, RoutedEventArgs e ) {
	    RefreshCharacterList();
	}

	private void OnViewCharacter( object sender, RoutedEventArgs e ) {
		string characterName = "pouet";
            Character character = (Character)characterListView.SelectedItems[0];
            characterName = character.Name;


            TabControl itemsTab = (TabControl) this.FindName("MainTabControl");
            foreach(  TabItem item in itemsTab.Items ) {
                if( item.Header == characterName ) {
                    item.IsSelected = true;
                    return;
                }
            }

            TabItem newTab = new TabItem();
            newTab.Header = characterName;

            try {
                CharacterWindow newChild = new CharacterWindow();
                newTab.Content = newChild;
            } catch( Exception exception ) {
                MessageBox.Show( exception.ToString() );
            }

            itemsTab.Items.Add(newTab);

            itemsTab.SelectedItem = newTab;

	}

	private void CharacterListViewColumHeader_Click( object sender, RoutedEventArgs e ) {
	    GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null) {
      	        if (headerClicked.Role != GridViewColumnHeaderRole.Padding) {
                    if (headerClicked != _lastHeaderClicked) {
                        direction = ListSortDirection.Ascending;
                    } else {
             if (_lastDirection == ListSortDirection.Ascending)
             {
               direction = ListSortDirection.Descending;
             }
             else
             {
                 direction = ListSortDirection.Ascending;
             }
          }

          string header = headerClicked.Column.Header as string;
          Sort(header, direction);

          _lastHeaderClicked = headerClicked;
          _lastDirection = direction;
       }
    }
	}

	private void Sort(string sortBy, ListSortDirection direction) {
            ICollectionView dataView =
    CollectionViewSource.GetDefaultView(characterListView.ItemsSource);

            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }

	private void RefreshCharacterList() {
	    foreach( Character character in characterList ) {
                CharacterManager.UpdateCharacter( character );
            }

            characterListView.ItemsSource = characterList;
	}
    }
}
