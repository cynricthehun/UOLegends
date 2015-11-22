using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xF61, 0xF61 )]
	public class PracticeSword : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ShadowStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 5; } }
		public override int AosMinDamage{ get{ return 9; } }
		public override int AosMaxDamage{ get{ return 11; } }
		public override int AosSpeed{ get{ return 49; } }

		public override int OldStrengthReq{ get{ return 5; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 5; } }
		public override int OldSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		[Constructable]
		public PracticeSword() : base(0xF61)
		{
			Name = "a longsword (practice weapon)";
			//Hue = 1153;
			Weight = 1.0;
			LootType = LootType.Newbied;
		}

		public PracticeSword(Serial serial)
			: base(serial)
		{
		}

 		public override void OnHit( Mobile attacker, Mobile defender )
	 	{
  			int roll = Utility.Random( 30 );
  			if ( roll == 0 ) 
  			{
   				attacker.Skills[SkillName.Swords].Base ++;
  			}
  			base.OnHit( attacker, defender );
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