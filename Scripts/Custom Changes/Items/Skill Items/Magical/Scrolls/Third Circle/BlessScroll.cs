using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BlessScroll : SpellScroll
	{
		[Constructable]
		public BlessScroll() : this( 1 )
		{
		}

		[Constructable]
		public BlessScroll( int amount ) : base( 16, 0x1F3D, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " bless scrolls" );
				}
				else
				{
					LabelTo( from, "a bless scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BlessScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new BlessScroll( amount ), amount );
		}
	}
}