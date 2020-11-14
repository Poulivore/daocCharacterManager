using System.IO;

namespace daocCharacterManager {
    public  class CharacterManager {

        private static string rootPath = "Characters";

        public static void Initialize( ) {
            if( !Directory.Exists( rootPath ) ) {
                Directory.CreateDirectory( rootPath );
            }
        }
    }
}
