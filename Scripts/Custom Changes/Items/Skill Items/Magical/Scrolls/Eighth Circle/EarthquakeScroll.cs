using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EarthquakeScroll : SpellScroll
	{
		[Constructable]
		public EarthquakeScroll() : this( 1 )
		{
		}

		[Constructable]
		public EarthquakeScroll( int amount ) : base( 56, 0x1F65, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " earthquake scrolls" );
				}
				else
				{
					LabelTo( from, "an earthquake scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public EarthquakeScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new EarthquakeScroll( amount ), amount );
		}
	}
}