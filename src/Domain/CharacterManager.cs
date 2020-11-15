using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace daocCharacterManager {
    public  class CharacterManager {

        private static string rootPath = "Characters";

        public static void Initialize( ) {
            if( !Directory.Exists( rootPath ) ) {
                Directory.CreateDirectory( rootPath );
            }
        }

	public static void CreateCharacter( Character character ) {
            string characterRootPath = Path.Combine( rootPath, character.Id );
            string characterPath = Path.Combine( characterRootPath, "character.json" );


            if( !Directory.Exists( characterRootPath ) ) {
                Directory.CreateDirectory( characterRootPath );
            }



            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            System.IO.File.WriteAllText( characterPath, JsonSerializer.Serialize( character, options ) );
        }

	public static void UpdateCharacter( Character character ) {
            string url = "http://api.camelotherald.com/character/info/" + character.Id;

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
                                //Character character = new Character( characterInfoJson.character_web_id, characterInfoJson.name, characterInfoJson.server_name, characterInfoJson.ClassName );
                                character.RealmPoints = characterInfoJson.realm_war_stats.current.realm_points;
                                character.ClassName = characterInfoJson.ClassName;
                                character.TotalKills = characterInfoJson.realm_war_stats.current.player_kills.total.kills;
                                character.TotalSoloKills = characterInfoJson.realm_war_stats.current.player_kills.total.solo_kills;

                                CharacterManager.CreateCharacter( character );

                            } catch( Exception exception ) {
                                MessageBox.Show( exception.ToString() );
                            }
                        }
                    }
            } catch( Exception exception) {

            }
        }


	public static List<string> LoadClusterListFromHerald( ) {
	    List<string> activeClusterList = new List<string>();		
            string url = "http://api.camelotherald.com/data/clusters/";

	    HttpClient http = new HttpClient();

	    try {
                HttpContent responseMessage = http.GetAsync( url ).Result.Content;
		var options = new JsonSerializerOptions
                        {
                            AllowTrailingCommas = true
                        };


		if( responseMessage != null ) {
                        string clusterDataResult = responseMessage.ReadAsStringAsync().Result;

			List<Json.ClusterDataJson> clusterDataListJson = new List<Json.ClusterDataJson>();
			clusterDataListJson = JsonSerializer.Deserialize<List<Json.ClusterDataJson>>(clusterDataResult, options);

			foreach( Json.ClusterDataJson clusterData in clusterDataListJson ) {
				if( clusterData.archived_cluster == false ) {
					activeClusterList.Add( clusterData.cluster_name );
				}
			}
		}

	    } catch( Exception exception) {
		MessageBox.Show(exception.ToString());
            }

	    return activeClusterList;

	}

	public static List<Tuple<string, string>> SearchCharacterFromHerald( string name, string cluster ) {
	    List<Tuple<string, string>> foundCharacterList = new List<Tuple<string, string>>();		
            string url = "http://api.camelotherald.com/character/search?name=" + name + "&cluster=" + cluster;

	     HttpClient http = new HttpClient();

            try {
                HttpContent responseMessage = http.GetAsync( url ).Result.Content;
                var options = new JsonSerializerOptions
                        {
                            AllowTrailingCommas = true
                        };


                if( responseMessage != null ) {
			string characterSearchResult = responseMessage.ReadAsStringAsync().Result;

			Json.CharacterSearchResultJson characterSearchResultJson = new Json.CharacterSearchResultJson();

			characterSearchResultJson = JsonSerializer.Deserialize<Json.CharacterSearchResultJson>( characterSearchResult, options );
			foreach( Json.CharacterSearchResult result in characterSearchResultJson.results ) {
				Tuple<string, string> tuple = new Tuple<string, string>(  result.character_web_id, result.name );
				foundCharacterList.Add( tuple );
			}
		}

	    } catch( Exception exception) {
                MessageBox.Show(exception.ToString());
            }


	    return foundCharacterList;
	}

	public static List<Character> LoadCharacterListFromDisk( ) {
            List<Character> characterList = new List<Character>();

            string[] directoryList;
            directoryList = Directory.GetDirectories( rootPath );

            foreach( string directory in directoryList ) {

                string characterPath = Path.Combine( directory, "character.json" );

                Character character;
                try {
                    character = JsonSerializer.Deserialize<Character>( File.ReadAllText( characterPath ) );
                    characterList.Add( character );
                } catch( Exception exception ) {
                     MessageBox.Show( exception.ToString());
                }
            }
	    return characterList;
	}

    }
}
