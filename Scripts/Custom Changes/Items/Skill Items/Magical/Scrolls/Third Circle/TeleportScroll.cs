using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class TeleportScroll : SpellScroll
	{
		[Constructable]
		public TeleportScroll() : this( 1 )
		{
		}

		[Constructable]
		public TeleportScroll( int amount ) : base( 21, 0x1F42, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " teleport scrolls" );
				}
				else
				{
					LabelTo( from, "a teleport scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public TeleportScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new TeleportScroll( amount ), amount );
		}
	}
}