using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ArchCureScroll : SpellScroll
	{
		[Constructable]
		public ArchCureScroll() : this( 1 )
		{
		}

		[Constructable]
		public ArchCureScroll( int amount ) : base( 24, 0x1F45, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " arch cure scrolls" );
				}
				else
				{
					LabelTo( from, "an arch cure scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public ArchCureScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new ArchCureScroll( amount ), amount );
		}
	}
}