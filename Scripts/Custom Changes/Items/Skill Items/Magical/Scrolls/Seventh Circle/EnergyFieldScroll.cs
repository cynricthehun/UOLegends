using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EnergyFieldScroll : SpellScroll
	{
		[Constructable]
		public EnergyFieldScroll() : this( 1 )
		{
		}

		[Constructable]
		public EnergyFieldScroll( int amount ) : base( 49, 0x1F5E, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " energy field scrolls" );
				}
				else
				{
					LabelTo( from, "an energy field scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public EnergyFieldScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new EnergyFieldScroll( amount ), amount );
		}
	}
}