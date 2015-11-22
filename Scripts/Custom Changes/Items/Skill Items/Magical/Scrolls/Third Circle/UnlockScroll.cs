using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class UnlockScroll : SpellScroll
	{
		[Constructable]
		public UnlockScroll() : this( 1 )
		{
		}

		[Constructable]
		public UnlockScroll( int amount ) : base( 22, 0x1F43, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " unlock scrolls" );
				}
				else
				{
					LabelTo( from, "an unlock scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public UnlockScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new UnlockScroll( amount ), amount );
		}
	}
}