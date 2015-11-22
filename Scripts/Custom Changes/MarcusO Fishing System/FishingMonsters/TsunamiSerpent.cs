using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a tsunami serpents corpse" )]
	public class TsunamiSerpent : BaseCreature
	{
		[Constructable]
		public TsunamiSerpent() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
            Name = "a tsunami serpent";
			Body = 150;
			BaseSoundID = 447;

			SetStr( 251, 380 );
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

            SetSkill(SkillName.EvalInt, 10.4, 50.0);
            SetSkill(SkillName.Magery, 60.0, 75.0);
			SetSkill( SkillName.MagicResist, 60.1, 75.0 );
			SetSkill( SkillName.Tactics, 60.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 70.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 60;
			CanSwim = true;
			CantWalk = true;

            Hue = Utility.RandomRedHue();

			PackItem( new Bloodmoss( 10 ) );
			PackItem( new Nightshade( 10 ) );

            if ( 0.25 > Utility.RandomDouble() )
			   PackItem( new SpecialFishingNet() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
		}

		public override bool HasBreath{ get{ return true; } }
		public override int Meat{ get{ return 1; } }

        public override int Hides { get { return 8; } }
        public override HideType HideType { get { return HideType.Regular; } }


        public TsunamiSerpent(Serial serial)
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