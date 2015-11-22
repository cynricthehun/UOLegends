using System;
using Server.Network;

namespace Server.Items
{
	public class Drums : BaseInstrument
	{
		[Constructable]
		public Drums() : base( 0xE9C, 0x38, 0x39 )
		{
			Weight = 4.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Quality != InstrumentQuality.Exceptional && this.LootType != LootType.Blessed )
			{
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a drum" ) );
			}
			else if ( this.Quality != InstrumentQuality.Exceptional && this.LootType == LootType.Blessed )
			{
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed drum" ) );
			}
			else if ( this.Quality == InstrumentQuality.Exceptional )
			{
				if ( this.LootType != LootType.Blessed )
				{
					if ( this.Crafter == null )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional drum" ) );
					}
					else
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional drum (crafted by:" + this.Crafter.Name + ")" ) );
					}
				}
				else if ( this.LootType == LootType.Blessed )
				{
					if ( this.Crafter == null )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional drum" ) );
					}
					else
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional drum (crafted by:" + this.Crafter.Name + ")" ) );
					}
				}
			}
			base.OnSingleClick( from );
		}

		public Drums( Serial serial ) : base( serial )
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

			if ( Weight == 3.0 )
				Weight = 4.0;
		}
	}
}