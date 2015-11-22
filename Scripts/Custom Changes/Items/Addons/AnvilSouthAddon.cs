using System;
using Server;

namespace Server.Items
{
	public class AnvilSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new AnvilSouthDeed(); } }

		[Constructable]
		public AnvilSouthAddon()
		{
			AddComponent( new AnvilComponent( 0xFB0 ), 0, 0, 0 );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "an anvil" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public AnvilSouthAddon( Serial serial ) : base( serial )
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

	public class AnvilSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new AnvilSouthAddon(); } }
		public override int LabelNumber{ get{ return 1044334; } } // anvil (south)

		[Constructable]
		public AnvilSouthDeed()
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "an anvil deed facing south" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public AnvilSouthDeed( Serial serial ) : base( serial )
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