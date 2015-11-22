using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ProtectionScroll : SpellScroll
	{
		[Constructable]
		public ProtectionScroll() : this( 1 )
		{
		}

		[Constructable]
		public ProtectionScroll( int amount ) : base( 14, 0x1F3B, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " protection scrolls" );
				}
				else
				{
					LabelTo( from, "a protection scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public ProtectionScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new ProtectionScroll( amount ), amount );
		}
	}
}