using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ResurrectionScroll : SpellScroll
	{
		[Constructable]
		public ResurrectionScroll() : this( 1 )
		{
		}

		[Constructable]
		public ResurrectionScroll( int amount ) : base( 58, 0x1F67, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " ressurection scrolls" );
				}
				else
				{
					LabelTo( from, "a ressurection scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public ResurrectionScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new ResurrectionScroll( amount ), amount );
		}
	}
}