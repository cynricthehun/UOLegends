using System;
using Server;

namespace Server.Dueling
{
	public class Corroded2Arena : DuelRegion
	{
		public override Point3D FromStart{ get{ return new Point3D( 5779, 3369, -10 ); } }
		public override Point3D ToStart{ get{ return new Point3D( 5779, 3380, -10 ); } }
		public override Point3D FromEnd{ get{ return new Point3D( 5816, 3373, 3 ); } }
		public override Point3D ToEnd{ get{ return new Point3D( 5816, 3377, 3 ); } }

		public override Point3D BootLocation{ get{ return new Point3D( 5816, 3373, 3 ); } }

		public override Point3D WallArea{ get{ return new Point3D( 5779, 3375, -10 ); } }
		public override bool NorthSouth{ get{ return true; } }

		public override Rectangle2D RegionArea{ get{ return new Rectangle2D( 5769, 3363, 45, 25 ); } }

		public Corroded2Arena( bool para, bool pots, bool reflect, bool fields, bool stun, bool disarm, Mobile from, Mobile to ) : base( "Corroded2Arena", para, pots, reflect, fields, stun, disarm, from, to )
		{
		}
	}
}