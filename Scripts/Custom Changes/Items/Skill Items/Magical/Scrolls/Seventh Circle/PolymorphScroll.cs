using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PolymorphScroll : SpellScroll
	{
		[Constructable]
		public PolymorphScroll() : this( 1 )
		{
		}

		[Constructable]
		public PolymorphScroll( int amount ) : base( 55, 0x1F64, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " polymorph scrolls" );
				}
				else
				{
					LabelTo( from, "a polymorph scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public PolymorphScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new PolymorphScroll( amount ), amount );
		}
	}
}