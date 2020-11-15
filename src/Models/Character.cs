namespace daocCharacterManager {

    public  class Character {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ServerName { get; set; }
        public string ClassName { get; set; }
        public int RealmPoints  { get; set; }
        public int TotalKills  { get; set; }
        public int TotalSoloKills  { get; set; }

        public Character() {
        }

        public Character( string id, string name, string serverName, string className ) {
            Id = id;
            Name = name;
            ServerName = serverName;
            ClassName = className;
        }
    }
}

