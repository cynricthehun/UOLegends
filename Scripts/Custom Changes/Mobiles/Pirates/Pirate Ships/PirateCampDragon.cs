using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Multis
{
	public class PirateCampDragonBoat : BaseCamp
	{
		[Constructable]
		public PirateCampDragonBoat() : base( 0x3EAC )
		{
		}

		public override void AddComponents()
		{

			Item largedragonboat = new LargeDragonBoat();
			largedragonboat.Movable = false;
			
			AddItem( largedragonboat, 0, 0, 0 );
		
			AddMobile( new ArcherPirate(), 4, 0,  -1, 4 );
			AddMobile( new SwordPirate(), 4,  0, 2, 4 );
			AddMobile( new SwordPirate(), 4, 0, 3, 4 );
			AddMobile( new BlackBart(), 4, 0, 4, 4 );
		}

		public PirateCampDragonBoat( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}