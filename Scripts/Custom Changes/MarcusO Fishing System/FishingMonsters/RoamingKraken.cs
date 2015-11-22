using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a kraken corpse" )]
	public class RoamingKraken : BaseCreature
	{
		[Constructable]
		public RoamingKraken() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "kraken";
			Body = 8;
			BaseSoundID = 684;

            Hue = Utility.RandomYellowHue();

            SetStr(99, 155);
            SetDex(66, 85);
            SetInt(101, 125);

            SetHits(76, 93);

            SetDamage(7, 9);

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 40 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 15, 25 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 20, 30 );

            SetSkill(SkillName.EvalInt, 60.1, 75.0);
            SetSkill(SkillName.Magery, 40.0, 65.0);
			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 60.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 18;
            CanSwim = true;
            CantWalk = true;

			//if ( 0.25 > Utility.RandomDouble() )
			//	PackItem( new Board( 10 ) );
			//else
			//	PackItem( new Log( 10 ) );

			PackItem( new BlackPearl( 10 ) );
            PackItem( new Ginseng( 10 ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

        public RoamingKraken(Serial serial)
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

			if ( BaseSoundID == 352 )
				BaseSoundID = 684;
		}
	}
}
