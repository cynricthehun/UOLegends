using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a storm serpents corpse" )]
	[TypeAlias( "Server.Mobiles.Seaserpant" )]
	public class StormSerpent : BaseCreature
	{
		[Constructable]
		public StormSerpent() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a storm serpent";
			Body = 150;
			BaseSoundID = 447;

			SetStr( 168, 225 );
			SetDex( 58, 85 );
			SetInt( 53, 95 );

			SetHits( 110, 127 );

			SetDamage( 7, 13 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 15, 20 );

            SetSkill(SkillName.EvalInt, 10.4, 50.0);
            SetSkill(SkillName.Magery, 40.0, 65.0);
			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 60.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 70.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 30;
			CanSwim = true;
			CantWalk = true;

            Hue = Utility.RandomGreenHue();

			//PackItem( new SpecialFishingNet() );
            PackItem(new Nightshade(10));
            PackItem(new Garlic(10));

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool HasBreath{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		// TODO: Fish steaks??

        public override int Hides { get { return 8; } }
        public override HideType HideType { get { return HideType.Horned; } }
        public override int Scales { get { return 8; } }
        public override ScaleType ScaleType { get { return ScaleType.Green; } }

        public StormSerpent(Serial serial)
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