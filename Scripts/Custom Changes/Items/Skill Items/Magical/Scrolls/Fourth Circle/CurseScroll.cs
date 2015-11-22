using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CurseScroll : SpellScroll
	{
		[Constructable]
		public CurseScroll() : this( 1 )
		{
		}

		[Constructable]
		public CurseScroll( int amount ) : base( 26, 0x1F47, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " curse scrolls" );
				}
				else
				{
					LabelTo( from, "a curse scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public CurseScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new CurseScroll( amount ), amount );
		}
	}
}