using System;

namespace Server.Items
{
	public class Hay : Item
	{
		[Constructable]
		public Hay() : this( 1 )
		{
		}

		[Constructable]
		public Hay( int amount ) : base( 0xF34 )
		{
			Weight = 4.0;
			Stackable = true;
			Amount = amount;
		}
		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name  == null )
			{
				LabelTo( from, "hay" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Hay( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Hay( amount ), amount );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}