using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;

namespace Server.Mobiles
{
	public class Brigand : BaseCreature
	{
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public Brigand() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			SpeechHue = Utility.RandomDyedHue();
			Title = "the brigand";
			Hue = Utility.RandomSkinHue();

			if ( this.Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );
				AddItem( new Skirt( Utility.RandomNeutralHue() ) );
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );
				AddItem( new ShortPants( Utility.RandomNeutralHue() ) );
			}

			SetStr( 86, 100 );
			SetDex( 81, 95 );
			SetInt( 61, 75 );

			SetDamage( 10, 23 );

			SetSkill( SkillName.Fencing, 66.0, 97.5 );
			SetSkill( SkillName.Macing, 65.0, 87.5 );
			SetSkill( SkillName.MagicResist, 25.0, 47.5 );
			SetSkill( SkillName.Swords, 65.0, 87.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );

			Fame = 1000;
			Karma = -1000;

			AddItem( new Boots( Utility.RandomNeutralHue() ) );
			AddItem( new FancyShirt( Utility.RandomDyedHue() ));
			AddItem( new Bandana( Utility.RandomBirdHue() ));

			switch ( Utility.Random( 7 ))
			{
				case 0: AddItem( new Longsword() ); break;
				case 1: AddItem( new Cutlass() ); break;
				case 2: AddItem( new Broadsword() ); break;
				case 3: AddItem( new Axe() ); break;
				case 4: AddItem( new Club() ); break;
				case 5: AddItem( new Dagger() ); break;
				case 6: AddItem( new Spear() ); break;
			}

			AddItem( Server.Items.Hair.GetRandomHair( Female ) );
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
		}

		public override bool AlwaysMurderer{ get{ return true; } }

		public Brigand( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}