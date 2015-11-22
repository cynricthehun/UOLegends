using System; 
using Server;
using Server.Items;

namespace Server.Mobiles 
{ 
	[CorpseName( "an evil mage lord corpse" )] 
	public class EvilMageLord : BaseCreature 
	{ 
		[Constructable] 
		public EvilMageLord() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{ 
			Name = NameList.RandomName( "evil mage lord" );
			Body = Utility.RandomList( 400, 401 );

			AddItem( new Robe( Utility.RandomMetalHue() ) ); 
			AddItem( new WizardsHat( Utility.RandomMetalHue() ) ); 
			AddItem( new Sandals( Utility.RandomBirdHue() ) ); 

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomHairHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );

			SpeechHue = Utility.RandomDyedHue();

			Hue = Utility.RandomSkinHue();

			SetStr( 81, 105 );
			SetDex( 191, 215 );
			SetInt( 126, 150 );
			Body = 400;

			SetHits( 49, 63 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 40 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 80.2, 100.0 );
			SetSkill( SkillName.Magery, 95.1, 100.0 );
			SetSkill( SkillName.Meditation, 27.5, 50.0 );
			SetSkill( SkillName.MagicResist, 77.5, 100.0 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.3, 80.0 );

			Fame = 10500;
			Karma = -10500;

			VirtualArmor = 8;

			PackReg( 23 );

			if ( 1 > Utility.Random( 3 ) )
				PackItem( Loot.RandomWand() );

			if ( 1 > Utility.Random( 100 ) )
			{
				AddItem( new Robe( 1175 ) );
			}
			else
			{
				AddItem( new Robe( Utility.RandomNeutralHue() ) );
			}
			if ( 1 > Utility.Random( 100 ) )
			{
				AddItem( new Sandals( 1175 ) );
			}
			else
			{
				AddItem( new Sandals( Utility.RandomBirdHue() ) );
			}

		}

		public override int GetAngerSound()
		{
			if ( this.Female )
			{
				switch( Utility.Random( 3 ) )
				{
					case 0: { return 779; } //aha
					case 1: { return 797; } //hey
					case 3: { return 825; } //yell
					default: { return 779; } //aha
				}
			}
			else
			{
				switch( Utility.Random( 4 ) )
				{
					case 0: { return 1050; } //aha
					case 1: { return 1069; } //hey
					case 2: { return 1085; } //oooh
					case 3: { return 1098; } //yell
					default: { return 1050; } //yell
				}
			}
		}

		public override int GetIdleSound()
		{
			if ( this.Female )
			{
				switch( Utility.Random( 8 ) )
				{
					case 0: { return 784; } //clear throat
					case 1: { return 785; } //cough
					case 2: { return 795; } //groan
					case 3: { return 796; } //growl
					case 4: { return 816; } //sigh
					case 5: { return 817; } //sneeze
					case 6: { return 818; } //sniff
					case 7: { return 822; } //yawn
					default: { return 784; } //clear throat
				}
			}
			else
			{
				switch( Utility.Random( 8 ) )
				{
					case 0: { return 1055; } //clear throat
					case 1: { return 1056; } //cough
					case 2: { return 1067; } //groan
					case 3: { return 1068; } //growl
					case 4: { return 1090; } //sigh
					case 5: { return 1091; } //sneeze
					case 6: { return 1092; } //sniff
					case 7: { return 1096; } //yawn
					default: { return 1055; } //clear throat
				}
			}
		}

		public override int GetHurtSound()
		{
			if ( this.Female )
			{
				switch( Utility.Random( 8 ) )
				{
					case 0: { return 804; }
					case 1: { return 805; }
					case 2: { return 806; }
					case 3: { return 807; }
					case 4: { return 808; }
					case 5: { return 809; }
					case 6: { return 810; }
					default: { return 804; }
				}
			}
			else
			{
				switch( Utility.Random( 9 ) )
				{
					case 0: { return 1076; }
					case 1: { return 1077; }
					case 2: { return 1078; }
					case 3: { return 1079; }
					case 4: { return 1080; }
					case 5: { return 1081; }
					case 6: { return 1082; }
					case 7: { return 1083; }
					case 8: { return 1084; }
					default: { return 1076; }
				}
			}
		}

		public override int GetDeathSound()
		{
			if ( this.Female )
			{
				switch( Utility.Random( 4 ) )
				{
					case 0: { return 336; }
					case 1: { return 337; }
					case 2: { return 338; }
					case 3: { return 339; }
					default: { return 336; }
				}
			}
			else
			{
				switch( Utility.Random( 4 ) )
				{
					case 0: { return 347; }
					case 1: { return 348; }
					case 2: { return 349; }
					case 3: { return 350; }
					default: { return 347; }
				}
			}
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		public override int Meat{ get{ return 1; } }
		public override int TreasureMapLevel{ get{ return Core.AOS ? 2 : 0; } }

		public EvilMageLord( Serial serial ) : base( serial ) 
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
