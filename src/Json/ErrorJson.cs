namespace daocCharacterManager.Json
{
	public class ErrorJson {
        public int timestamp;
        public string requested_uri;
        public string error { get; set; }

        public ErrorJson() {
            error = "";
        }
    }
}

