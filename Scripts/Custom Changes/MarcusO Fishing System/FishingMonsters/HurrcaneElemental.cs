using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a hurricane elemental corpse" )]
	public class HurricaneElemental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public HurricaneElemental() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
            Name = "a hurricane elemental ";
			Body = 13;
			Hue = 0x4001;
			BaseSoundID = 655;

			SetStr( 126, 155 );
			SetDex( 166, 185 );
			SetInt( 101, 125 );

			SetHits( 76, 123 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 40 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 25, 35 );

			SetSkill( SkillName.EvalInt, 60.1, 75.0 );
			SetSkill( SkillName.Magery, 40.0, 65.0 );
			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 40;
            CanSwim = true;
            CantWalk = true;

            PackItem(new SpidersSilk(10));
            PackItem(new Garlic(10));
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.MedScrolls );
		}

		public override int TreasureMapLevel{ get{ return 2; } }

        public HurricaneElemental(Serial serial) : base(serial)
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

			if ( BaseSoundID == 263 )
				BaseSoundID = 655;
		}
	}
}
