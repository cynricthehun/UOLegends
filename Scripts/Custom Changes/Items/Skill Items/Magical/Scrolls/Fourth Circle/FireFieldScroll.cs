using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class FireFieldScroll : SpellScroll
	{
		[Constructable]
		public FireFieldScroll() : this( 1 )
		{
		}

		[Constructable]
		public FireFieldScroll( int amount ) : base( 27, 0x1F48, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " fire field scrolls" );
				}
				else
				{
					LabelTo( from, "a fire field scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public FireFieldScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new FireFieldScroll( amount ), amount );
		}
	}
}