using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MagicArrowScroll : SpellScroll
	{
		[Constructable]
		public MagicArrowScroll() : this( 1 )
		{
		}

		[Constructable]
		public MagicArrowScroll( int amount ) : base( 4, 0x1F32, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " magic arrow scrolls" );
				}
				else
				{
					LabelTo( from, "a magic arrow scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}
		
		public MagicArrowScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new MagicArrowScroll( amount ), amount );
		}
	}
}