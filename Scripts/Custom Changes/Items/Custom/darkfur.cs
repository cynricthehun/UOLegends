using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x11F8, 0x11F5 )]
   	public class DarkFur : Item 
	{
		[Constructable]
		public DarkFur( int amount ) : base( 0x11F8 )
		{
        	 	Weight = 15; 
			Amount = amount;
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

		public DarkFur( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new DarkFur( amount ), amount );
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