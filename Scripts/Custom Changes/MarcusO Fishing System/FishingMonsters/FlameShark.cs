using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a flame shark corpse" )]
	public class FlameShark : BaseCreature
	{
		[Constructable]
		public FlameShark() : base( AIType.AI_Animal, FightMode.Agressor, 10, 1, 0.2, 0.4 )
		{
            Name = "a flame shark";
			Body = 0x97;
			BaseSoundID = 0x8A;

            Hue = Utility.RandomRedHue();

            SetStr(251, 400);
            SetDex(87, 135);
            SetInt(87, 155);

            SetHits(181, 255);

            SetDamage(6, 14);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 30, 40);
            SetResistance(ResistanceType.Fire, 70, 80);
            SetResistance(ResistanceType.Cold, 40, 50);
            SetResistance(ResistanceType.Poison, 30, 40);
            SetResistance(ResistanceType.Energy, 15, 20);

            SetSkill(SkillName.EvalInt, 40.1, 65.0);
            SetSkill(SkillName.Magery, 40.0, 65.0);
            SetSkill(SkillName.MagicResist, 60.1, 75.0);
            SetSkill(SkillName.Tactics, 60.1, 70.0);
            SetSkill(SkillName.Wrestling, 60.1, 70.0);

            Fame = 6000;
            Karma = -6000;

            VirtualArmor = 60;
            CanSwim = true;
            CantWalk = true;

            PackItem(new Bloodmoss(10));
            PackItem(new MandrakeRoot(10));

            // PackItem(new SpecialFishingNet());

            Tamable = true;
            ControlSlots = 2;
            MinTameSkill = 95.1;
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Rich);
        }

        public override bool HasBreath { get { return true; } }
        public override int Meat { get { return 1; } }

        public override FoodType FavoriteFood { get { return FoodType.Meat; } }

        public override int Hides { get { return 8; } }
        public override HideType HideType { get { return HideType.Regular; } }

        public FlameShark(Serial serial)
            : base(serial)
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