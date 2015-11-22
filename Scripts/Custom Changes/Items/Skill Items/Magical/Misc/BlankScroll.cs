using System;

namespace Server.Items
{
	public class BlankScroll : Item
	{
		[Constructable]
		public BlankScroll() : this( 1 )
		{
		}

		[Constructable]
		public BlankScroll( int amount ) : base( 0xEF3 )
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
					LabelTo( from, this.Amount + " blank scrolls" );
				}
				else
				{
					LabelTo( from, "a blank scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BlankScroll( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new BlankScroll( amount ), amount );
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