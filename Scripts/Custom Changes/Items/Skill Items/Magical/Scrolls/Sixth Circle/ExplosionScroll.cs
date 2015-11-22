using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ExplosionScroll : SpellScroll
	{
		[Constructable]
		public ExplosionScroll() : this( 1 )
		{
		}

		[Constructable]
		public ExplosionScroll( int amount ) : base( 42, 0x1F57, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " explosion scrolls" );
				}
				else
				{
					LabelTo( from, "an explosion scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public ExplosionScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new ExplosionScroll( amount ), amount );
		}
	}
}