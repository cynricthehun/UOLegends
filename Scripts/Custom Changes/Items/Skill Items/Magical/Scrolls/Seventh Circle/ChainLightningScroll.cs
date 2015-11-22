using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ChainLightningScroll : SpellScroll
	{
		[Constructable]
		public ChainLightningScroll() : this( 1 )
		{
		}

		[Constructable]
		public ChainLightningScroll( int amount ) : base( 48, 0x1F5D, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " chain lightning scrolls" );
				}
				else
				{
					LabelTo( from, "a chain lightning scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public ChainLightningScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new ChainLightningScroll( amount ), amount );
		}
	}
}