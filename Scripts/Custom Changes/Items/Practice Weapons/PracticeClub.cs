using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13b4, 0x13b3 )]
	public class PracticeClub : BaseBashing
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ShadowStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 11; } }
		public override int AosMaxDamage{ get{ return 13; } }
		public override int AosSpeed{ get{ return 44; } }

		public override int OldStrengthReq{ get{ return 10; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 5; } }
		public override int OldSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		[Constructable]
		public PracticeClub() : base( 0x13B4 )
		{
			Name = "a club (practice weapon)";
			//Hue = 1153;
			Weight = 9.0;
			LootType = LootType.Newbied;
		}

		public PracticeClub( Serial serial ) : base( serial )
		{
		}

 		public override void OnHit( Mobile attacker, Mobile defender )
	 	{
  			int roll = Utility.Random( 30 );
  			if ( roll == 0 ) 
  			{
   				attacker.Skills[SkillName.Macing].Base ++;
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