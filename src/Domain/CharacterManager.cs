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

	public static void LoadClusterListFromHerald( ) {
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
					MessageBox.Show( clusterData.cluster_name);
				}
			}
		}

	    } catch( Exception exception) {
		MessageBox.Show(exception.ToString());

            }

	}
    }
}
