using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class GreaterHealScroll : SpellScroll
	{
		[Constructable]
		public GreaterHealScroll() : this( 1 )
		{
		}

		[Constructable]
		public GreaterHealScroll( int amount ) : base( 28, 0x1F49, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " greater heal scrolls" );
				}
				else
				{
					LabelTo( from, "a greater heal scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public GreaterHealScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new GreaterHealScroll( amount ), amount );
		}
	}
}