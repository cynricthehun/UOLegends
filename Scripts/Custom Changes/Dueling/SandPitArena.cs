using System;
using Server;

namespace Server.Dueling
{
	public class SandPitArena : DuelRegion
	{
		public override Point3D FromStart{ get{ return new Point3D( 5753, 2675, 47 ); } }
		public override Point3D ToStart{ get{ return new Point3D( 5753, 2667, 47 ); } }
		public override Point3D FromEnd{ get{ return new Point3D( 5763, 2666, 55 ); } }
		public override Point3D ToEnd{ get{ return new Point3D( 5763, 2671, 55 ); } }

		public override Point3D BootLocation{ get{ return new Point3D( 5763, 2661, 55 ); } }

		public override Point3D WallArea{ get{ return new Point3D( 5753, 2671, 47 ); } }
		public override bool NorthSouth{ get{ return true; } }

		public override Rectangle2D RegionArea{ get{ return new Rectangle2D( 5747, 2664, 15, 15 ); } }

		public SandPitArena( bool para, bool pots, bool reflect, bool fields, bool stun, bool disarm, Mobile from, Mobile to ) : base( "DocksideArena", para, pots, reflect, fields, stun, disarm, from, to )
		{
		}
	}
}