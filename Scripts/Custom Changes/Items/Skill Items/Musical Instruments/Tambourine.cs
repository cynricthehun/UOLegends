using System;
using Server.Network;

namespace Server.Items
{
	public class Tambourine : BaseInstrument
	{
		[Constructable]
		public Tambourine() : base( 0xE9D, 0x52, 0x53 )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Quality != InstrumentQuality.Exceptional && this.LootType != LootType.Blessed )
			{
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a tambourine" ) );
			}
			else if ( this.Quality != InstrumentQuality.Exceptional && this.LootType == LootType.Blessed )
			{
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed tambourine" ) );
			}
			else if ( this.Quality == InstrumentQuality.Exceptional )
			{
				if ( this.LootType != LootType.Blessed )
				{
					if ( this.Crafter == null )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tambourine" ) );
					}
					else
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tambourine (crafted by:" + this.Crafter.Name + ")" ) );
					}
				}
				else if ( this.LootType == LootType.Blessed )
				{
					if ( this.Crafter == null )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tambourine" ) );
					}
					else
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tambourine (crafted by:" + this.Crafter.Name + ")" ) );
					}
				}
			}
			base.OnSingleClick( from );
		}

		public Tambourine( Serial serial ) : base( serial )
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

			if ( Weight == 2.0 )
				Weight = 1.0;
		}
	}
}