using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a deep sea snake corpse" )]
	public class DeepSeaSnake : BaseCreature
	{
		[Constructable]
		public DeepSeaSnake() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a deep sea snake";
			Body = 93; // a colored land-serpent body
			BaseSoundID = 447;

			SetStr( 251, 385 );
			SetDex( 87, 135 );
			SetInt( 87, 155 );

			SetHits( 151, 255 );

			SetDamage( 6, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 40 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 15, 20 );

            SetSkill(SkillName.EvalInt, 60.1, 75.0);
            SetSkill(SkillName.Magery, 40.0, 65.0);
			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 60.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 70.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 60;
			CanSwim = true;
			CantWalk = true;

            Hue = Utility.RandomBlueHue();
            // FYI:
            // Utility.RandomGreenHue();
            // Utility.RandomRedHue();
            // Utility.RandomYellowHue();
            // Utility.RandomNeutralHue();


			PackItem( new MandrakeRoot( 10 ) );
			PackItem( new SpidersSilk( 10 ) );

			//PackItem( new SpecialFishingNet() );
		}

        public override Poison PoisonImmune { get { return Poison.Regular; } }
        public override Poison HitPoison { get { return Poison.Regular; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool HasBreath{ get{ return true; } }
		public override int Meat{ get{ return 3; } }

        public override int Hides { get { return 10; } }
        public override HideType HideType { get { return HideType.Regular; } }

        public DeepSeaSnake(Serial serial)  : base(serial)
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