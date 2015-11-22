using System;

namespace Server.Items
{
	public class Vase : Item
	{
		[Constructable]
		public Vase() : base( 0xB46 )
		{
			Weight = 10;
		}

		public override void OnSingleClick( Mobile from)
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a vase" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Vase( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LargeVase : Item
	{
		[Constructable]
		public LargeVase() : base( 0xB45 )
		{
			Weight = 15;
		}

		public override void OnSingleClick( Mobile from)
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a large vase" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public LargeVase( Serial serial ) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class SmallUrn : Item
	{
		[Constructable]
		public SmallUrn() : base( 0x241C )
		{
			Weight = 20.0;
		}

		public override void OnSingleClick( Mobile from)
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a small urn" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public SmallUrn(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write( (int)0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}