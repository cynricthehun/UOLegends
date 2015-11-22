using System;
using Server.Network;

namespace Server.Items
{
	public class Lute : BaseInstrument
	{
		[Constructable]
		public Lute() : base( 0xEB3, 0x4C, 0x4D )
		{
			Weight = 5.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Quality != InstrumentQuality.Exceptional && this.LootType != LootType.Blessed )
			{
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a lute" ) );
			}
			else if ( this.Quality != InstrumentQuality.Exceptional && this.LootType == LootType.Blessed )
			{
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed lute" ) );
			}
			else if ( this.Quality == InstrumentQuality.Exceptional )
			{
				if ( this.LootType != LootType.Blessed )
				{
					if ( this.Crafter == null )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional lute" ) );
					}
					else
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional lute (crafted by:" + this.Crafter.Name + ")" ) );
					}
				}
				else if ( this.LootType == LootType.Blessed )
				{
					if ( this.Crafter == null )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional lute" ) );
					}
					else
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional lute (crafted by:" + this.Crafter.Name + ")" ) );
					}
				}
			}
			base.OnSingleClick( from );
		}

		public Lute( Serial serial ) : base( serial )
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
				Weight = 5.0;
		}
	}
}