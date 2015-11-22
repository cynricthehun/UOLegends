using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CunningScroll : SpellScroll
	{
		[Constructable]
		public CunningScroll() : this( 1 )
		{
		}

		[Constructable]
		public CunningScroll( int amount ) : base( 9, 0x1F36, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " cunning scrolls" );
				}
				else
				{
					LabelTo( from, "a cunning scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public CunningScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new CunningScroll( amount ), amount );
		}
	}
}