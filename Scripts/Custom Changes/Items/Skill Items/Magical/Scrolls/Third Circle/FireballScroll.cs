using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class FireballScroll : SpellScroll
	{
		[Constructable]
		public FireballScroll() : this( 1 )
		{
		}

		[Constructable]
		public FireballScroll( int amount ) : base( 17, 0x1F3E, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " fireball scrolls" );
				}
				else
				{
					LabelTo( from, "a fireball scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public FireballScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new FireballScroll( amount ), amount );
		}
	}
}