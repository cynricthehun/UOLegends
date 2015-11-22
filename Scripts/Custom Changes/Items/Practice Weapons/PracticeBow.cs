using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13B2, 0x13B1 )]
	public class PracticeBow : BaseRanged
	{
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosStrengthReq{ get{ return 30; } }
		public override int AosMinDamage{ get{ return 2; } }
		public override int AosMaxDamage{ get{ return 13; } }
		public override int AosSpeed{ get{ return 25; } }

		public override int OldStrengthReq{ get{ return 20; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 5; } }
		public override int OldSpeed{ get{ return 25; } }

		public override int DefMaxRange{ get{ return 10; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public PracticeBow() : base( 0x13B2 )
		{	
			Name = "a bow (practice weapon)";
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			LootType = LootType.Newbied;
		}

		public PracticeBow( Serial serial ) : base( serial )
		{
		}

 		public override void OnHit( Mobile attacker, Mobile defender )
	 	{
  			int roll = Utility.Random( 30 );
  			if ( roll == 0 ) 
  			{
   				attacker.Skills[SkillName.Archery].Base ++;
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

			if ( Weight == 7.0 )
				Weight = 6.0;
		}
	}
}