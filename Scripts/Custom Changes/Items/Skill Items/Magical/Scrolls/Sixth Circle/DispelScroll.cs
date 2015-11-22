using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DispelScroll : SpellScroll
	{
		[Constructable]
		public DispelScroll() : this( 1 )
		{
		}

		[Constructable]
		public DispelScroll( int amount ) : base( 40, 0x1F55, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " dispel scrolls" );
				}
				else
				{
					LabelTo( from, "a dispel scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public DispelScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new DispelScroll( amount ), amount );
		}
	}
}