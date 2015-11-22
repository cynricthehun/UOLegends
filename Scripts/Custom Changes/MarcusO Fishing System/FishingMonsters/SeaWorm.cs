using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a sea worm corpse" )]
	public class SeaWorm : BaseCreature
	{
		[Constructable]
		public SeaWorm() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
            Name = "a sea worm";
			Body = 52; // land-snake body
			Hue = Utility.RandomSnakeHue();
			BaseSoundID = 0xDB;

            SetStr(50, 85);
            SetDex(35, 65);
            SetInt(40, 60);

            SetHits(45, 75);

            SetDamage(3, 6);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 30, 40);
            SetResistance(ResistanceType.Fire, 70, 80);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 15, 20);

            SetSkill(SkillName.EvalInt, 10.4, 50.0);
            SetSkill(SkillName.Magery, 35.0, 50.0);
            SetSkill(SkillName.MagicResist, 60.1, 75.0);
            SetSkill(SkillName.Tactics, 60.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 70.0);

			Fame = 300;
			Karma = -300;

			VirtualArmor = 16;
            CanSwim = true;
            CantWalk = true;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 59.1;
		}

		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }
		public override Poison HitPoison{ get{ return Poison.Lesser; } }

		public override bool DeathAdderCharmable{ get{ return true; } }

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Meager);
        }

        public SeaWorm(Serial serial)  : base(serial)
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