using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class WeakenScroll : SpellScroll
	{
		[Constructable]
		public WeakenScroll() : this( 1 )
		{
		}

		[Constructable]
		public WeakenScroll( int amount ) : base( 7, 0x1F34, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " weaken scrolls" );
				}
				else
				{
					LabelTo( from, "a weaken scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public WeakenScroll( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new WeakenScroll( amount ), amount );
		}
	}
}