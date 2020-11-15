using System.Text.Json.Serialization;

namespace daocCharacterManager.Json
{
    public class Kills {
        public int kills { get; set; }	    
        public int death_blows { get; set; }	    
        public int solo_kills { get; set; }	    
    }

    public class PlayerKills {
        public Kills total { get; set; }
    }

    public class CurrentRealmWarStats {
        public int realm_points { get; set; }
	public PlayerKills player_kills { get; set; }
    }

    public class RealmWarStats {
        public CurrentRealmWarStats current { get; set; }
    }

	public class CharacterInfoJson {
        public string character_web_id  { get; set; }
        public string name { get; set; }
        public string server_name { get; set; }
        [JsonPropertyName("class_name")]
        public string ClassName { get; set; }
        public RealmWarStats realm_war_stats { get; set; }

    }
}

