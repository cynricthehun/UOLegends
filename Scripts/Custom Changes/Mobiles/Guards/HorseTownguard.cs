using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	public class HorseTownGuard : BaseCreature 
	{
		private bool m_Bandage;

		[Constructable] 
		public HorseTownGuard() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.175, 0.2 )
		{ 
			Title = "the guard";
			Body = 400;
            		Hue = Utility.RandomSkinHue(); 
			SpeechHue=1153;
			this.Body = 0x190; 
			this.Name = NameList.RandomName( "male" );
            		SetStr( 150 );
			SetDex( 100 );
			SetInt( 25 );

			SetSkill( SkillName.MagicResist, 200.0 );
			SetSkill( SkillName.Swords, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Healing, 100.0 );
			SetSkill( SkillName.Anatomy, 100.0 );
			SetSkill( SkillName.Parry, 100.0 );
			SetSkill( SkillName.DetectHidden, 100.0 );

			Fame = 2500;
			Karma = 5000;

			VirtualArmor = 30;

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomHairHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );
			PackGold( 0 );

			switch ( Utility.Random( 50 ) )
			{
				case 0:
				{
					Item beard = new Item( Utility.RandomList( 0x2040, 0x204B, 0x203F, 0x2041 ) );
					beard.Hue = hair.Hue;
					beard.Layer = Layer.FacialHair;
					beard.Movable = false;
					AddItem( beard );
					break;
				}
			}

			Item platechest = new PlateChest();
			EquipItem( platechest );
			Item platearms = new PlateArms();
			EquipItem( platearms );
			Item platelegs = new PlateLegs();
			EquipItem( platelegs );
			Item plategorget = new PlateGorget();
			EquipItem( plategorget );
			Longsword longsword = new Longsword();
			longsword.Movable = false;
			longsword.MinDamage = 35;
			longsword.MaxDamage = 40;
			EquipItem( longsword );
			Item shield = new MetalKiteShield();
			EquipItem( shield );
			PackItem( new Longsword() );
			PackItem( new Bandage( 100 ) );
			Horse horse = new Horse();
			horse.Rider = this;
			horse.Controled = true;
			horse.ControlMaster = this;
		}
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }

//*----------------------------> Enemy Check

		public override bool IsEnemy( Mobile m )
		{
			if ( m.Player && m.Kills < 5 && m.Criminal == false )
			{
				return false;
			}
			if ( m.Criminal == true || m.Kills >= 5 )
			{
				Combatant = m;
				return true;
			}
			if ( m is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)m;
				if ( bc.Controled && bc.ControlMaster != null )
				{
					return IsEnemy( bc.ControlMaster );
				}
				if ( bc.Summoned && bc.SummonMaster != null )
				{
					return IsEnemy( bc.SummonMaster );
				}
				if ( bc.FightMode == FightMode.None || bc.FightMode == FightMode.Agressor )
				{
					return false;
				}
			}
			return m.Karma < 0;
		}
//<----------------------------* Enemy Check

//*----------------------------> BANDAGE

             public override void OnDamage( int amount, Mobile m, bool willKill )
	     {
                   if ( this.Hits < this.HitsMax && m_Bandage == false && this.Hidden == false )
                   {  
                           m_Bandage = true; 
                                      
                           Container backpack = this.Backpack;

                           Bandage bandage = (Bandage) backpack.FindItemByType( typeof( Bandage ) );
				 
                           if ( bandage != null )
			   {

   				if ( BandageContext.BeginHeal( this, this ) != null )
					bandage.Consume();

                                BandageTimer bt = new BandageTimer( this );
                                        bt.Start();
                           }
                   }
             }

            private class BandageTimer : Timer 
	    { 
		   private HorseTownGuard guard;

                   public BandageTimer( HorseTownGuard o ) : base( TimeSpan.FromSeconds( 15 ) ) 
		   { 
                   guard = o;
		   Priority = TimerPriority.OneSecond; 
		   } 
		
                   protected override void OnTick() 
		   { 
				guard.m_Bandage = false; 
		   } 
		  		
            }

//<----------------------------* BANDAGE

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
		}

		public HorseTownGuard( Serial serial ) : base( serial )
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