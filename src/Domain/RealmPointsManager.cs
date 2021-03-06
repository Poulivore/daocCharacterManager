using System.Collections.Generic;

namespace daocCharacterManager {
    public  class RealmPointsManager {

        private static List<int> realmRankList = new List<int>{
		0,
		25,
		125,
		350,
		750,
		1375,
		2275,
		3500,
		5100,
		7125,
		9625,
		12650,
		16250,
		20475,
		25375,
		31000,
		37400,
		44625,
		52725,
		61750,
		71750,
		82775,
		94875,
		108100,
		122500,
		138125,
		155025,
		173250,
		192850,
		213875,
		236375,
		260400,
		286000,
		313225,
		342125,
		372750,
		405150,
		438375,
		475475,
		513500,
		553500,
		595525,
		639625,
		685850,
		734250,
		784875,
		837775,
		893000,
		950600,
		1010645,
		1073125,
		1138150,
		1205750,
		1275975,
		1348875,
		1424500,
		1502900,
		1584125,
		1668225,
		1755250,
		1845250,
		1938275,
		2034375,
		2133600,
		2236000,
		2341625,
		2450525,
		2562750,
		2678350,
		2797375,
		2919875,
		3045900,
		3175500,
		3308725,
		3445625,
		3586250,
		3730650,
		3878875,
		4030975,
		4187000,
		4347000,
		4511025,
		4679525,
		4851350,
		5027750,
		5208375,
		5393275,
		5582500,
		5776100,
		5974100,
		6176625,
		6383650,
		6595250,
		6811475,
		7032385,
		7258000,
		7488400,
		7723625,
		7963725,
		8208750,
		9111713,
		10114001,
		11226541,
		12461460,
		13832221,
		15353765,
		17042680,
		18917374,
		20998286,
		23308097,
		25871988,
		28717906,
		31876876,
		35383333,
		39275499,
		43595804,
		48391343,
		53714390,
		59622973,
		66181501,
		73461466,
		81542227,
		90511872,
		100468178,
		111519678,
		123786843,
		137403395,
		152517769,
		169294723,
		187917143
	};

        public static string GetRealmRank( int realmPoints ) {
		string realmRank = "1L0";

		int realmAbilitiesPoints = 0;

		foreach( int i in realmRankList ) {
			if( realmPoints > i ) {
				realmAbilitiesPoints++;
			} else {
				break;
			}
		}

		realmRank = ( realmAbilitiesPoints / 10 + 1 ) + "L" + ( realmAbilitiesPoints % 10 );

		return realmRank;
	}

        public static int GetNextRealmRank( int realmPoints ) {
		int nextRealmPoints = 0;

		foreach( int i in realmRankList ) {
		    nextRealmPoints = i;
			if( realmPoints < i ) {
				break;
			}
		}

		if( realmPoints > nextRealmPoints ) {
			return 0;
		}
		return nextRealmPoints - realmPoints;
	}
    }
}
