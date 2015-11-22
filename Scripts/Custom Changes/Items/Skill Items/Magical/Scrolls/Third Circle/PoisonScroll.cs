using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PoisonScroll : SpellScroll
	{
		[Constructable]
		public PoisonScroll() : this( 1 )
		{
		}

		[Constructable]
		public PoisonScroll( int amount ) : base( 19, 0x1F40, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " poison scrolls" );
				}
				else
				{
					LabelTo( from, "a poison scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public PoisonScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new PoisonScroll( amount ), amount );
		}
	}
}