using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a deep water elemental corpse" )]
	public class DeepWaterElemental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public DeepWaterElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a deep water elemental";
			Body = 16;
			BaseSoundID = 278;

            Hue = Utility.RandomBlueHue();  // Make it different from vanilla water elemental

			SetStr( 126, 155 );
			SetDex( 66, 85 );
			SetInt( 101, 125 );

			SetHits( 93, 130 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 10, 25 );
			SetResistance( ResistanceType.Cold, 10, 25 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.EvalInt, 60.1, 75.0 );
			SetSkill( SkillName.Magery, 40.0, 65.0 );
			SetSkill( SkillName.MagicResist, 100.1, 115.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 50.1, 70.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 40;
			CanSwim = true;
            CantWalk = true;

			PackItem( new Ginseng( 10 ) );
            PackItem(new Garlic(10));
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.Potions );
		}

		public override int TreasureMapLevel{ get{ return 2; } }

		public DeepWaterElemental( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}