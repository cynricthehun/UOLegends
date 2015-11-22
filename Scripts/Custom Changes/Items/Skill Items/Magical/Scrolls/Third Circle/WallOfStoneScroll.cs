using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class WallOfStoneScroll : SpellScroll
	{
		[Constructable]
		public WallOfStoneScroll() : this( 1 )
		{
		}

		[Constructable]
		public WallOfStoneScroll( int amount ) : base( 23, 0x1F44, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " wall of stone scrolls" );
				}
				else
				{
					LabelTo( from, "a wall of stone scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public WallOfStoneScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new WallOfStoneScroll( amount ), amount );
		}
	}
}