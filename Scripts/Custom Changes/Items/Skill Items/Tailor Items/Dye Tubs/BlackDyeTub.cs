using System;

namespace Server.Items
{
	public class BlackDyeTub : DyeTub
	{
		[Constructable]
		public BlackDyeTub()
		{
			Hue = DyedHue = 0x0001;
			Redyable = false;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a black dye tub" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}


		public BlackDyeTub( Serial serial ) : base( serial )
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
	}
}