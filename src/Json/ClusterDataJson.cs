using System.Collections.Generic;

namespace daocCharacterManager.Json
{
	public class ClusterDataJson {
		public string cluster_name { get; set; }
		public bool archived_cluster { get; set; }
        	public List<string> servers { get; set; }
	}
}
