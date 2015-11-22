using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class TinkerPoisonTrapDeed : BaseTinkerTrapDeed
	{
		[Constructable]
		public TinkerPoisonTrapDeed()
		{
			Weight = 1.0;
			Name = "a poison trap deed";
			TrapType = TrapType.PoisonTrap;
		}

		public TinkerPoisonTrapDeed( Serial serial ) : base( serial )
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