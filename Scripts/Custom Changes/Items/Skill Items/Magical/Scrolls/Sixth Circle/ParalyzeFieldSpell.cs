using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ParalyzeFieldScroll : SpellScroll
	{
		[Constructable]
		public ParalyzeFieldScroll() : this( 1 )
		{
		}

		[Constructable]
		public ParalyzeFieldScroll( int amount ) : base( 46, 0x1F5B, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " paralyze field scrolls" );
				}
				else
				{
					LabelTo( from, "a paralyze field scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public ParalyzeFieldScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new ParalyzeFieldScroll( amount ), amount );
		}
	}
}