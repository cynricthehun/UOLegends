using System;
using Server.Network;

namespace Server.Items
{
	public class Harp : BaseInstrument
	{
		[Constructable]
		public Harp() : base( 0xEB1, 0x43, 0x44 )
		{
			Weight = 35.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Quality != InstrumentQuality.Exceptional && this.LootType != LootType.Blessed )
			{
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a standing harp" ) );
			}
			else if ( this.Quality != InstrumentQuality.Exceptional && this.LootType == LootType.Blessed )
			{
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed standing harp" ) );
			}
			else if ( this.Quality == InstrumentQuality.Exceptional )
			{
				if ( this.LootType != LootType.Blessed )
				{
					if ( this.Crafter == null )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional standing harp" ) );
					}
					else
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional standing harp (crafted by:" + this.Crafter.Name + ")" ) );
					}
				}
				else if ( this.LootType == LootType.Blessed )
				{
					if ( this.Crafter == null )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional standing harp" ) );
					}
					else
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional standing harp (crafted by:" + this.Crafter.Name + ")" ) );
					}
				}
			}
			base.OnSingleClick( from );
		}

		public Harp( Serial serial ) : base( serial )
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
				Weight = 35.0;
		}
	}
}