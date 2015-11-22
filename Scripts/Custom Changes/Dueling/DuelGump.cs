using System;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Dueling
{
	public abstract class DuelGump : Gump
	{
		public static bool Exists( Mobile mob )
		{
			NetState ns = mob.NetState;

			if ( ns == null )
				return false;

			for ( int i = 0; i < ns.Gumps.Count; ++i )
			{
				if ( ns.Gumps[i] is DuelGump )
					return true;
			}

			return false;
		}

		public DuelGump( int x, int y ) : base( x, y )
		{
		}
	}
}