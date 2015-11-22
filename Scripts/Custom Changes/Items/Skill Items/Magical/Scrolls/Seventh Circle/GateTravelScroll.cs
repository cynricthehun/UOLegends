using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class GateTravelScroll : SpellScroll
	{
		[Constructable]
		public GateTravelScroll() : this( 1 )
		{
		}

		[Constructable]
		public GateTravelScroll( int amount ) : base( 51, 0x1F60, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " gate travel scrolls" );
				}
				else
				{
					LabelTo( from, "a gate travel scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public GateTravelScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new GateTravelScroll( amount ), amount );
		}
	}
}