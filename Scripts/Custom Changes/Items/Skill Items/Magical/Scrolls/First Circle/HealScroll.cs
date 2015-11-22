using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HealScroll : SpellScroll
	{
		[Constructable]
		public HealScroll() : this( 1 )
		{
		}

		[Constructable]
		public HealScroll( int amount ) : base( 3, 0x1F31, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " heal scrolls" );
				}
				else
				{
					LabelTo( from, "a heal scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public HealScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new HealScroll( amount ), amount );
		}
	}
}