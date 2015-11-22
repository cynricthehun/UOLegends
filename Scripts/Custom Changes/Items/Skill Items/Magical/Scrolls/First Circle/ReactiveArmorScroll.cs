using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ReactiveArmorScroll : SpellScroll
	{
		[Constructable]
		public ReactiveArmorScroll() : this( 1 )
		{
		}

		[Constructable]
		public ReactiveArmorScroll( int amount ) : base( 6, 0x1F2D, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " reactive armor scrolls" );
				}
				else
				{
					LabelTo( from, "a reactive armor scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public ReactiveArmorScroll( Serial ser ) : base(ser)
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
			return base.Dupe( new ReactiveArmorScroll( amount ), amount );
		}
	}
}