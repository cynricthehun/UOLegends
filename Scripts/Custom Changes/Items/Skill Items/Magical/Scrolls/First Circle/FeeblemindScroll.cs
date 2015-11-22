using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class FeeblemindScroll : SpellScroll
	{
		[Constructable]
		public FeeblemindScroll() : this( 1 )
		{
		}

		[Constructable]
		public FeeblemindScroll( int amount ) : base( 2, 0x1F30, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " feeblemind scrolls" );
				}
				else
				{
					LabelTo( from, "a feeblemind scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public FeeblemindScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new FeeblemindScroll( amount ), amount );
		}
	}
}