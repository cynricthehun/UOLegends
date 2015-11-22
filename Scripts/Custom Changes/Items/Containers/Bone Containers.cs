using Server;
using Server.Items;

namespace Server.Items
{
	public class BoneContainer : BaseContainer
	{
		public override int DefaultGumpID{ get{ return 0x9; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		[Constructable]
		public BoneContainer() : base( 0x990 )
		{
			Weight = 1.0;
			switch( Utility.Random( 9 ) )
			{
				case 0:
					ItemID = 0xECA;
					break;
				case 1:
					ItemID = 0xECB;
					break;
				case 2:
					ItemID = 0xECC;
					break;
				case 3:
					ItemID = 0xECD;
					break;
				case 4:
					ItemID = 0xECE;
					break;
				case 5:
					ItemID = 0xECF;
					break;
				case 6:
					ItemID = 0xED0;
					break;
				case 7:
					ItemID = 0xED1;
					break;
				case 8:
					ItemID = 0xED2;
					break;
			}
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a bone container");
			}
			else
			{
				LabelTo( from, this.Name );
			}
			LabelTo( from, "({0} items, {1} stones)", TotalItems, TotalWeight );
		}

		public BoneContainer( Serial serial ) : base( serial )
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