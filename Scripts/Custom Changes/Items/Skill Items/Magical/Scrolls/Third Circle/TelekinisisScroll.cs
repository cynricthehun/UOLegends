using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class TelekinisisScroll : SpellScroll
	{
		[Constructable]
		public TelekinisisScroll() : this( 1 )
		{
		}

		[Constructable]
		public TelekinisisScroll( int amount ) : base( 20, 0x1F41, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " telekenisis scrolls" );
				}
				else
				{
					LabelTo( from, "a telekenisis scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public TelekinisisScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new TelekinisisScroll( amount ), amount );
		}
	}
}