using System;
using Server;

namespace Server.Items
{
	public class FertileDirt : Item
	{
		[Constructable]
		public FertileDirt() : this( 1 )
		{
		}

		[Constructable]
		public FertileDirt( int amount ) : base( 0xF81 )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
					LabelTo( from, this.Amount + " fertile dirt" );
				else
					LabelTo( from, "fertile dirt" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public FertileDirt( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new FertileDirt( amount ), amount );
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