using System;
using Server;

namespace Server.Items
{
	public class AnvilEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new AnvilEastDeed(); } }

		[Constructable]
		public AnvilEastAddon()
		{
			AddComponent( new AnvilComponent( 0xFAF ), 0, 0, 0 );
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

		public AnvilEastAddon( Serial serial ) : base( serial )
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

	public class AnvilEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new AnvilEastAddon(); } }
		public override int LabelNumber{ get{ return 1044333; } } // anvil (east)

		[Constructable]
		public AnvilEastDeed()
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "an anvil deed facing east" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public AnvilEastDeed( Serial serial ) : base( serial )
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