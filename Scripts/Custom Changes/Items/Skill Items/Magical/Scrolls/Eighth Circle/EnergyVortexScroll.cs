using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EnergyVortexScroll : SpellScroll
	{
		[Constructable]
		public EnergyVortexScroll() : this( 1 )
		{
		}

		[Constructable]
		public EnergyVortexScroll( int amount ) : base( 57, 0x1F66, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " energy vortex scrolls" );
				}
				else
				{
					LabelTo( from, "an energy vortex scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public EnergyVortexScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new EnergyVortexScroll( amount ), amount );
		}
	}
}