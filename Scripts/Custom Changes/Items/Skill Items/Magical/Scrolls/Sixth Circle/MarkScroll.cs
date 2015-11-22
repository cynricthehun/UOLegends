using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MarkScroll : SpellScroll
	{
		[Constructable]
		public MarkScroll() : this( 1 )
		{
		}

		[Constructable]
		public MarkScroll( int amount ) : base( 44, 0x1F59, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " mark scrolls" );
				}
				else
				{
					LabelTo( from, "a mark scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public MarkScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new MarkScroll( amount ), amount );
		}
	}
}