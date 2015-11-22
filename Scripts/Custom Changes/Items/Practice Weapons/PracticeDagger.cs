using System;
using Server.Network;
using Server.Targeting;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute(0x1401, 0x1401)]
	public class PracticeDagger : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.InfectiousStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ShadowStrike; } }

		public override int AosStrengthReq{ get{ return 10; } }
		public override int AosMinDamage{ get{ return 10; } }
		public override int AosMaxDamage{ get{ return 11; } }
		public override int AosSpeed{ get{ return 56; } }

		public override int DefHitSound { get { return 0x23C; } }

		public override int OldStrengthReq{ get{ return 1; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 5; } }
		public override int OldSpeed{ get{ return 55; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		public override SkillName DefSkill{ get{ return SkillName.Fencing; } }
		public override WeaponType DefType{ get{ return WeaponType.Piercing; } }
		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public PracticeDagger()	: base(0x1401)
		{
			Name = "a kryss (practice weapon)";
			//Hue = 1153;
			Weight = 1.0;
			LootType = LootType.Newbied;
		}

		public PracticeDagger( Serial serial ) : base( serial )
		{
		}

 		public override void OnHit( Mobile attacker, Mobile defender )
	 	{
  			int roll = Utility.Random( 30 );
  			if ( roll == 0 ) 
  			{
   				attacker.Skills[SkillName.Fencing].Base ++;
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