using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class AgilityScroll : SpellScroll
	{
		[Constructable]
		public AgilityScroll() : this( 1 )
		{
		}

		[Constructable]
		public AgilityScroll( int amount ) : base( 8, 0x1F35, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " agility scrolls" );
				}
				else
				{
					LabelTo( from, "an agility scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public AgilityScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new AgilityScroll( amount ), amount );
		}
	}
}