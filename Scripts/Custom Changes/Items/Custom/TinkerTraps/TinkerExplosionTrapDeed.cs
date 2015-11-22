using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class TinkerExplosionTrapDeed : BaseTinkerTrapDeed
	{
		[Constructable]
		public TinkerExplosionTrapDeed()
		{
			Weight = 1.0;
			Name = "an explosion trap deed";
			TrapType = TrapType.ExplosionTrap;
		}

		public TinkerExplosionTrapDeed( Serial serial ) : base( serial )
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