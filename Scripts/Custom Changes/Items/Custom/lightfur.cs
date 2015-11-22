using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x11F6, 0x11FB )]
	public class LightFur : Item 
	{
		[Constructable]
		public LightFur( int amount ) : base( 0x11FB )
		{
			Amount = amount;
        	 	Weight = 15; 
         		Stackable = true;  
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Amount > 1 )
			{
				LabelTo( from, this.Amount + " furs" );
			}
			else
			{
				LabelTo( from, "a fur" );
			}
		}


		public LightFur( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new LightFur( amount ), amount );
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
	}
}