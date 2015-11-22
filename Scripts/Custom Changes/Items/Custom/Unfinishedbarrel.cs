using System;

namespace Server.Items

{
	public class unfinishedbarrel : Item
	{
		[Constructable]
		public unfinishedbarrel() : this( 1 )
		{
		}

		[Constructable]
		public unfinishedbarrel( int amount ) : base( 0x1EB5 )
		{
			Name = "a unfinished barrel";
			Weight = 4.0;
			Stackable = false;
			Amount = amount;
		}

		public unfinishedbarrel( Serial serial ) : base( serial )
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