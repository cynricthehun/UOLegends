using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EnergyBoltScroll : SpellScroll
	{
		[Constructable]
		public EnergyBoltScroll() : this( 1 )
		{
		}

		[Constructable]
		public EnergyBoltScroll( int amount ) : base( 41, 0x1F56, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " energy bolt scrolls" );
				}
				else
				{
					LabelTo( from, "an energy bolt scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public EnergyBoltScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new EnergyBoltScroll( amount ), amount );
		}
	}
}