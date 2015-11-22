using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a giant water strider corpse" )] 
	public class GiantWaterStrider : BaseCreature
	{
		[Constructable]
		public GiantWaterStrider() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a giant water strider";
			Body =  0x9D;
			BaseSoundID = 0x388; // TODO: validate

            Hue = 0x001; //JetBlack

            SetStr(99, 115);
            SetDex(66, 85);
            SetInt(101, 125);

            SetHits(93, 120);

            SetDamage(7, 9);

            SetDamageType(ResistanceType.Physical, 100);

            SetResistance(ResistanceType.Physical, 35, 45);
            SetResistance(ResistanceType.Fire, 10, 25);
            SetResistance(ResistanceType.Cold, 10, 25);
            SetResistance(ResistanceType.Poison, 60, 70);
            SetResistance(ResistanceType.Energy, 5, 10);

            SetSkill(SkillName.EvalInt, 60.1, 75.0);
            SetSkill(SkillName.Magery, 50.1, 70.0);
            SetSkill(SkillName.MagicResist, 100.1, 115.0);
            SetSkill(SkillName.Tactics, 50.1, 70.0);
            SetSkill(SkillName.Wrestling, 50.1, 70.0);

            Fame = 4500;
            Karma = -4500;

            VirtualArmor = 40;
            CanSwim = true;
            CantWalk = true;

            PackItem(new BlackPearl(10));
            PackItem(new SpidersSilk(10));
        }

        public override void GenerateLoot()
        {
            AddLoot(LootPack.Average, 2);
            AddLoot(LootPack.Potions);
        }

        public override int TreasureMapLevel { get { return 2; } }

		public override Poison HitPoison{ get{ return Poison.Regular; } }

        public GiantWaterStrider(Serial serial)
            : base(serial)
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