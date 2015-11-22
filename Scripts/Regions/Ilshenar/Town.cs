using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Spells;

namespace Server.Regions
{
	public class IlshenarCity : GuardedRegion
	{
		public static new void Initialize()
		{
		}

		public IlshenarCity( string name ) : this( name, typeof( ArcherGuard ) )
		{
		}

		public IlshenarCity( string name, Type guardType ) : base( "the town of", name, Map.Ilshenar, guardType )
		{
		}
	}
}