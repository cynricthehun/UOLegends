using System;

namespace Server.Items
{
	public class BolaBall : Item
	{
		[Constructable]
		public BolaBall() : this( 1 )
		{
		}

		[Constructable]
		public BolaBall( int amount ) : base( 0xE73 )
		{
			Weight = 4.0;
			Stackable = true;
			Amount = amount;
			Hue = 0x8AC;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " bola balls" );
				}
				else
				{
					LabelTo( from, "a bola ball" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BolaBall( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new BolaBall( amount ), amount );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Hue == 0 )
				Hue = 0x8AC;
		}
	}
}