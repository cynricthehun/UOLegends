using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class StrengthScroll : SpellScroll
	{
		[Constructable]
		public StrengthScroll() : this( 1 )
		{
		}

		[Constructable]
		public StrengthScroll( int amount ) : base( 15, 0x1F3C, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " strength scrolls" );
				}
				else
				{
					LabelTo( from, "a strength scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public StrengthScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new StrengthScroll( amount ), amount );
		}
	}
}