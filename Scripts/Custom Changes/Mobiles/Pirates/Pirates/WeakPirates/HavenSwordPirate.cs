using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	public class HavenSwordPirate : BaseCreature 
	{
		private bool m_Bandage;

		[Constructable] 
		public HavenSwordPirate() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.175, 0.2 )
		{ 
			Title = "the pirate";
			Body = 400;
			Team = 1;
			Kills = 10;
            		Hue = Utility.RandomSkinHue(); 
			SpeechHue=1153;
			this.Body = 0x190; 
			this.Name = NameList.RandomName( "male" );
            		SetStr( 60, 85 );
			SetDex( 50, 70 );
			SetInt( 10, 20 );
			SetDamage( 5, 7 );

			SetSkill( SkillName.Archery, 35.0, 50.0 );
			SetSkill( SkillName.MagicResist, 23.0, 27.5 );
			SetSkill( SkillName.Tactics, 40.0, 45.5 );
			SetSkill( SkillName.Healing, 30.2, 35.9 );
			SetSkill( SkillName.Anatomy, 45.2, 50.9 );
			SetSkill( SkillName.Swords, 50.0, 60.0);

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 0;

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x204A, 0x2044, 0x2030 ) );
			hair.Hue = Utility.RandomHairHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );

			Item beard = new Item( Utility.RandomList( 0x2040, 0x203E , 0x204C, 0x204B, 0x203F ) );
			beard.Hue = hair.Hue;
			beard.Layer = Layer.FacialHair;
			beard.Movable = false;
			AddItem( beard );

			PackGold( 70, 95 );
			PackItem( new Bandage( 10 ) );

			switch ( Utility.Random( 2 ) )
			{
				case 0:
				{
				Item scimitar = new Scimitar();
				EquipItem( scimitar );
				break;
				}
				case 1:
				{
				Item cutlass = new Cutlass();
				EquipItem( cutlass );
				break;
				}
			}

			switch ( Utility.Random( 3 ) )
			{
				case 0:
				{
				Item fancyshirt = new FancyShirt();
				fancyshirt.Hue = Utility.RandomNeutralHue();
				EquipItem( fancyshirt );
				break;
				}
				case 1:
				{
				Item doublet = new Doublet();
				doublet.Hue = Utility.RandomNeutralHue();
				EquipItem( doublet );
				break;
				}
				case 2:
				{
				break;
				}
			}

			switch ( Utility.Random( 2 ) )
			{
				case 0:
				{
				Item longpants = new LongPants();
				longpants.Hue = Utility.RandomNeutralHue();
				EquipItem( longpants );
				break;
				}
				case 1:
				{
				Item shortpants = new ShortPants();
				shortpants.Hue = Utility.RandomNeutralHue();
				EquipItem( shortpants );
				break;
				}
			}

	                switch ( Utility.Random( 4 ) )  
			{  
           			case 0:
				{
				Item boots = new Boots();
				boots.Hue = 0;
				EquipItem( boots );
				break;
				}
				case 1:
				{
				Item shoes = new Shoes();
				shoes.Hue = 1713;
				EquipItem( shoes );
				break;
				}
				case 3:
				{
				Item thighboots = new ThighBoots();
				thighboots.Hue = 0;
				EquipItem( thighboots );
				break;
				}
				case 4:
				{
				break;
				}
        		 }

                    switch ( Utility.Random( 3 ) )  
        		 {  
           			case 0:
				{
				Item Bandana = new Bandana();
				Bandana.Hue = Utility.RandomBirdHue();
				EquipItem( Bandana );
				break;
				}
				case 1:
				{
				Item skullcap = new SkullCap();
				skullcap.Hue = Utility.RandomBirdHue();
				EquipItem( skullcap );
				break;
				}
				case 3:
				{
				break;
				}
        		 }
		}
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool ClickTitle{ get{ return false; } }

//*---------------->	BANDAGE

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
		   private HavenSwordPirate pirate;

                   public BandageTimer( HavenSwordPirate o ) : base( TimeSpan.FromSeconds( 15 ) ) 
		   { 
                   pirate = o;
		   Priority = TimerPriority.OneSecond; 
		   } 
		
                   protected override void OnTick() 
		   { 
				pirate.m_Bandage = false; 
		   } 
		  		
            }

//<----------------------------* BANDAGE

		public HavenSwordPirate( Serial serial ) : base( serial )
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