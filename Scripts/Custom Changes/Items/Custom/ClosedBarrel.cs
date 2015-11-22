using System;

namespace Server.Items

{
	public class closedbarrel : Item
	{
		[Constructable]
		public closedbarrel() : this( 1 )
		{
		}

		[Constructable]
		public closedbarrel( int amount ) : base( 0xFAE )
		{
			Name = "a closed barrel";
			Weight = 4.0;
			Stackable = false;
			Amount = amount;
		}

		public closedbarrel( Serial serial ) : base( serial )
		{
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
		}
	}
}