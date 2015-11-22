using System;

namespace Server.Items
{
	public class TribalBerry : Item
	{
		public override int LabelNumber{ get{ return 1040001; } } // tribal berry

		[Constructable]
		public TribalBerry() : this( 1 )
		{
		}

		[Constructable]
		public TribalBerry( int amount ) : base( 0x9D0 )
		{
			Weight = 1.0;
			Stackable = true;
			Amount = amount;
			Hue = 6;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
					LabelTo( from, this.Amount + " tribal berries" );
				else
					LabelTo( from, "a tribal berry" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public TribalBerry( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new TribalBerry( amount ), amount );
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

			if ( Hue == 4 )
				Hue = 6;
		}
	}
}