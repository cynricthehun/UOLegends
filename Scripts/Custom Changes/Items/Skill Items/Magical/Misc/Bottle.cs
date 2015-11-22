using System;

namespace Server.Items
{
	public class Bottle : Item
	{
		[Constructable]
		public Bottle() : this( 1 )
		{
		}

		[Constructable]
		public Bottle( int amount ) : base( 0xF0E )
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
				{
					LabelTo( from, this.Amount + " empty bottles" );
				}
				else
				{
					LabelTo( from, "an empty bottle" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Bottle( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Bottle( amount ), amount );
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