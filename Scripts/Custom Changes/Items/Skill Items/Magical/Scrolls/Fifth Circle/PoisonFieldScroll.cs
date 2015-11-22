using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PoisonFieldScroll : SpellScroll
	{
		[Constructable]
		public PoisonFieldScroll() : this( 1 )
		{
		}

		[Constructable]
		public PoisonFieldScroll( int amount ) : base( 38, 0x1F53, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " poison field scrolls" );
				}
				else
				{
					LabelTo( from, "a poison field scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public PoisonFieldScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new PoisonFieldScroll( amount ), amount );
		}
	}
}