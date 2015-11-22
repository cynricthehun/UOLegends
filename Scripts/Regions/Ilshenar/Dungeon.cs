using System;
using Server;

namespace Server.Regions
{
	public class IlshenarDungeon : DungeonRegion
	{
		public static void Initialize()
		{
		}

		public IlshenarDungeon( string name ) : base( name, Map.Ilshenar )
		{
		}
	}
}