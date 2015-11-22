using System;

namespace Server.Items
{
	[FlipableAttribute( 0xFAF, 0xFB0 )]
	[Server.Engines.Craft.Anvil]
	public class Anvil : Item
	{
		[Constructable]
		public Anvil() : base( 0xFAF )
		{
			Movable = false;
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

		public Anvil( Serial serial ) : base( serial )
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

	[Server.Engines.Craft.Forge]
	public class Forge : Item
	{
		[Constructable]
		public Forge() : base( 0xFB1 )
		{
			Movable = false;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a forge" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Forge( Serial serial ) : base( serial )
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