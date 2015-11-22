using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SummonWaterElementalScroll : SpellScroll
	{
		[Constructable]
		public SummonWaterElementalScroll() : this( 1 )
		{
		}

		[Constructable]
		public SummonWaterElementalScroll( int amount ) : base( 63, 0x1F6C, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " summon water elemental scrolls" );
				}
				else
				{
					LabelTo( from, "a summon water elemental scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public SummonWaterElementalScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new SummonWaterElementalScroll( amount ), amount );
		}
	}
}