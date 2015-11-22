using System; 
using Server; 

namespace Server.Regions 
{ 
public class CustomSeasonChanges 
{ 
public static void Initialize() 
{ 
// Seasons are based on 5 different themes 
// 0 = Spring 
// 1 = Summer 
// 2 = Fall 
// 3 = Winter 
// 4 = Desolation 

Map.Felucca.Season = 1; 
Map.Trammel.Season = 1; 
Map.Ilshenar.Season = 1; 
Map.Malas.Season = 1; 
} 
} 
} 
namespace Server.Regions 
{ 
public class CustomMapRules 
{ 
public static void Initialize() 
{ 
// MapRules are based on 2 different themes 
// = MapRules.FeluccaRules 
// = MapRules.TrammelRules 


//Map.Fellucca.Rules = MapRules.FeluccaRules;//Commented out due to I want the defualt for this map 
//Map.Trammel.Rules = MapRules.FeluccaRules; //Commented out due to I want the defualt for this map 
//Map.Ilshenar.Rules = MapRules.FeluccaRules; //Commented out due to I want the defualt for this map 
Map.Malas.Rules = MapRules.FeluccaRules; 
} 
} 
}