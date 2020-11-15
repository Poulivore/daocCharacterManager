using System.Collections.Generic;

namespace daocCharacterManager.Json
{
	public class CharacterSearchResultParameter {
		public string name { get; set; }
		public string value { get; set; }
	}

	public class CharacterSearchResult {
		public string character_web_id { get; set; }
		public string name { get; set; }
	}

	public class CharacterSearchResultJson {
		public List<CharacterSearchResultParameter> parameters { get; set; }
		public List<CharacterSearchResult> results { get; set; }
	}
}
