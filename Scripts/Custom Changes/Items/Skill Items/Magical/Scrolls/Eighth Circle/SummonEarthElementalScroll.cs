using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SummonEarthElementalScroll : SpellScroll
	{
		[Constructable]
		public SummonEarthElementalScroll() : this( 1 )
		{
		}

		[Constructable]
		public SummonEarthElementalScroll( int amount ) : base( 61, 0x1F6A, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " summon earth elemental scrolls" );
				}
				else
				{
					LabelTo( from, "a summon earth elemental scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public SummonEarthElementalScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new SummonEarthElementalScroll( amount ), amount );
		}
	}
}