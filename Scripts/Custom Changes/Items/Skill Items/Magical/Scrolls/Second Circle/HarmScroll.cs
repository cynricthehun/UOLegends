using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HarmScroll : SpellScroll
	{
		[Constructable]
		public HarmScroll() : this( 1 )
		{
		}

		[Constructable]
		public HarmScroll( int amount ) : base( 11, 0x1F38, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " harms scrolls" );
				}
				else
				{
					LabelTo( from, "a harm scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public HarmScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new HarmScroll( amount ), amount );
		}
	}
}