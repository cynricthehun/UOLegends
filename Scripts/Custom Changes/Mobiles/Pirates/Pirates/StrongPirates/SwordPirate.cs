using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles 
{ 
	public class SwordPirate : BaseCreature 
	{
		private bool m_Bandage;

		[Constructable] 
		public SwordPirate() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.175, 0.2 )
		{ 
			Title = "the pirate";
			Body = 400;
			Team = 1;
			Kills = 10;
            		Hue = Utility.RandomSkinHue(); 
			SpeechHue=1153;
			this.Body = 0x190; 
			this.Name = NameList.RandomName( "male" );
            		SetStr( 90, 100 );
			SetDex( 85, 100 );
			SetInt( 10, 20 );

			SetSkill( SkillName.Archery, 95.0, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 100.0 );
			SetSkill( SkillName.Tactics, 90.0, 100.0 );
			SetSkill( SkillName.Healing, 75.0, 100.0 );
			SetSkill( SkillName.Anatomy, 90.0, 100.0 );
			SetSkill( SkillName.Swords, 95.0, 100.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 0;

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomHairHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;

			Item beard = new Item( Utility.RandomList( 0x2040, 0x203E , 0x204C, 0x204B, 0x203F ) );
			beard.Hue = hair.Hue;
			beard.Layer = Layer.FacialHair;
			beard.Movable = false;
			AddItem( beard );

			AddItem( hair );
			PackGold( 70, 95 );
			PackItem( new Bandage( 20 ) );

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
		   private SwordPirate pirate;

                   public BandageTimer( SwordPirate o ) : base( TimeSpan.FromSeconds( 15 ) ) 
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

		public SwordPirate( Serial serial ) : base( serial )
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