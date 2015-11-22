using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BladeSpiritsScroll : SpellScroll
	{
		[Constructable]
		public BladeSpiritsScroll() : this( 1 )
		{
		}

		[Constructable]
		public BladeSpiritsScroll( int amount ) : base( 32, 0x1F4D, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " blade spirits scrolls" );
				}
				else
				{
					LabelTo( from, "a blade spirits scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BladeSpiritsScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new BladeSpiritsScroll( amount ), amount );
		}
	}
}