using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class TinkerDartTrapDeed : BaseTinkerTrapDeed
	{
		[Constructable]
		public TinkerDartTrapDeed()
		{
			Weight = 1.0;
			Name = "a dart trap deed";
			TrapType = TrapType.DartTrap;
		}

		public TinkerDartTrapDeed( Serial serial ) : base( serial )
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