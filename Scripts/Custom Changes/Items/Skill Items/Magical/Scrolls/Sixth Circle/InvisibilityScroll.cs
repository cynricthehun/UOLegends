using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class InvisibilityScroll : SpellScroll
	{
		[Constructable]
		public InvisibilityScroll() : this( 1 )
		{
		}

		[Constructable]
		public InvisibilityScroll( int amount ) : base( 43, 0x1F58, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " invisiblity scrolls" );
				}
				else
				{
					LabelTo( from, "an invisibility scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public InvisibilityScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new InvisibilityScroll( amount ), amount );
		}
	}
}