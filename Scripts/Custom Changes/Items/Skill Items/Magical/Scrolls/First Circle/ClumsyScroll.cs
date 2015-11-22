using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ClumsyScroll : SpellScroll
	{
		[Constructable]
		public ClumsyScroll() : this( 1 )
		{
		}

		[Constructable]
		public ClumsyScroll( int amount ) : base( 0, 0x1F2E, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " clumsy scrolls" );
				}
				else
				{
					LabelTo( from, "a clumsy scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public ClumsyScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new ClumsyScroll( amount ), amount );
		}
	}
}