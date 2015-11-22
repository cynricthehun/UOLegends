using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MagicUnTrapScroll : SpellScroll
	{
		[Constructable]
		public MagicUnTrapScroll() : this( 1 )
		{
		}

		[Constructable]
		public MagicUnTrapScroll( int amount ) : base( 13, 0x1F3A, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " magic untrap scrolls" );
				}
				else
				{
					LabelTo( from, "a magic untrap scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public MagicUnTrapScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new MagicUnTrapScroll( amount ), amount );
		}
	}
}