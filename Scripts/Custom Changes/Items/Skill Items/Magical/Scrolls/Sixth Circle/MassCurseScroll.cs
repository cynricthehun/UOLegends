using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MassCurseScroll : SpellScroll
	{
		[Constructable]
		public MassCurseScroll() : this( 1 )
		{
		}

		[Constructable]
		public MassCurseScroll( int amount ) : base( 45, 0x1F5A, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " mass curse scrolls" );
				}
				else
				{
					LabelTo( from, "a mass curse scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public MassCurseScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new MassCurseScroll( amount ), amount );
		}
	}
}