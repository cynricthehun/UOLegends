using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RecallScroll : SpellScroll
	{
		[Constructable]
		public RecallScroll() : this( 1 )
		{
		}

		[Constructable]
		public RecallScroll( int amount ) : base( 31, 0x1F4C, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " recall scrolls" );
				}
				else
				{
					LabelTo( from, "a recall scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public RecallScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new RecallScroll( amount ), amount );
		}
	}
}