using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "an oiley slimey corpse" )]
	public class OilSlime : BaseCreature
	{
		[Constructable]
		public OilSlime() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "oil slime";
			Body = 51;
			BaseSoundID = 456;

            Hue = 0x001; // JetBlack

			SetStr( 50, 75 );
			SetDex( 16, 21 );
			SetInt( 16, 20 );

			SetHits( 35, 69 );

			SetDamage( 1, 5 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 5, 10 );
			SetResistance( ResistanceType.Poison, 10, 20 );

            SetSkill( SkillName.EvalInt, 60.1, 75.0 );
			SetSkill( SkillName.Magery, 35.0, 50.0 );
			SetSkill( SkillName.MagicResist, 15.1, 20.0 );
			SetSkill( SkillName.Tactics, 19.3, 34.0 );
			SetSkill( SkillName.Wrestling, 19.3, 34.0 );

			Fame = 300;
			Karma = -300;

			VirtualArmor = 8;
            CanSwim = true;
            CantWalk = true;

		}

        public override Poison PoisonImmune { get { return Poison.Lesser; } }
        public override Poison HitPoison { get { return Poison.Lesser; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public OilSlime( Serial serial ) : base( serial )
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
