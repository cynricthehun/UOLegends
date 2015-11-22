using System;
using Server;

namespace Server.Dueling
{
	public class DockSideArena : DuelRegion
	{
		public override Point3D FromStart{ get{ return new Point3D( 1920, 1499, 8 ); } }
		public override Point3D ToStart{ get{ return new Point3D( 1920, 1491, 8 ); } }
		public override Point3D FromEnd{ get{ return new Point3D( 1916, 1504, 0 ); } }
		public override Point3D ToEnd{ get{ return new Point3D( 1924, 1504, 0 ); } }

		public override Point3D BootLocation{ get{ return new Point3D( 1928, 1503, 0 ); } }

		public override Point3D WallArea{ get{ return new Point3D( 1920, 1495, 8 ); } }
		public override bool NorthSouth{ get{ return true; } }

		public override Rectangle2D RegionArea{ get{ return new Rectangle2D( 1914, 1489, 13, 13 ); } }

		public DockSideArena( bool para, bool pots, bool reflect, bool fields, bool stun, bool disarm, Mobile from, Mobile to ) : base( "DocksideArena", para, pots, reflect, fields, stun, disarm, from, to )
		{
		}
	}
}