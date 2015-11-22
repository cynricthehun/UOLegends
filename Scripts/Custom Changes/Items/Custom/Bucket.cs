using System;

namespace Server.Items

{
	public class Bucket : Item
	{
		[Constructable]
		public Bucket() : this( 1 )
		{
		}

		[Constructable]
		public Bucket( int amount ) : base( 0x1EB5 )
		{
			Name = "a bucket";
			Weight = 4.0;
			Stackable = false;
			Amount = amount;
		}

		public Bucket( Serial serial ) : base( serial )
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