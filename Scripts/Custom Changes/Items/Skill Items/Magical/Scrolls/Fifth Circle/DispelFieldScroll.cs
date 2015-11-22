using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DispelFieldScroll : SpellScroll
	{
		[Constructable]
		public DispelFieldScroll() : this( 1 )
		{
		}

		[Constructable]
		public DispelFieldScroll( int amount ) : base( 33, 0x1F4E, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " dispel field scrolls" );
				}
				else
				{
					LabelTo( from, "a dispel field scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public DispelFieldScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new DispelFieldScroll( amount ), amount );
		}
	}
}