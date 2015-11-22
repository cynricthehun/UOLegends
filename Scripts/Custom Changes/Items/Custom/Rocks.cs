using System;

namespace Server.Items
{
	public class Rocks : Item
	{
		[Constructable]
		public Rocks() : base( 0x1367 )
		{
			Weight = 23.0;
			Stackable = false;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "rocks" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Rocks( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Rocks( amount ), amount );
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