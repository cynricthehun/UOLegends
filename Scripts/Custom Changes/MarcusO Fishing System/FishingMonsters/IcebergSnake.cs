using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "an iceberg snake corpse" )]
	public class IcebergSnake : BaseCreature
	{
		[Constructable]
		public IcebergSnake() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an iceberg snake";
			Body = 52;
			Hue = 0x480;
			BaseSoundID = 0xDB;

			SetStr( 100, 150 );
			SetDex( 36, 45 );
			SetInt( 55, 65 );

            SetHits(100, 150);

			SetDamage( 8, 15 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Cold, 25 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Cold, 80, 90 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 30, 40 );

            SetSkill(SkillName.EvalInt, 60.1, 75.0);
            SetSkill(SkillName.Magery, 35.0, 60.0);
			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 39.3, 54.0 );
			SetSkill( SkillName.Wrestling, 39.3, 54.0 );

			Fame = 900;
			Karma = -900;

            VirtualArmor = 30;
            CanSwim = true;
            CantWalk = true;

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 59.1;

		}

        public override Poison PoisonImmune { get { return Poison.Lesser; } }
        public override Poison HitPoison { get { return Poison.Lesser; } }
        public override FoodType FavoriteFood { get { return FoodType.Meat; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override int Meat{ get{ return 1; } }

		public IcebergSnake(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}