using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;
using Server.ContextMenus;
using Server.Network;

namespace Server.Mobiles 
{ 
	public class UndeadBlackBart : BaseCreature 
	{
        private bool m_OnSpeech;
        private bool m_DoSpeech;
		private static bool m_Talked;
		string[] PirateSay = new string[] 
		{ 
			"Ye be dealing with a pirate, Matey!", 
			"Ye find it wise to cross blades with a pirate, fool?",
			"To the depths ye shall be fallin'!",
			"Yer time has come yeh mongrel.",
			"Arrrgh-Bring me a servin' wench to bid me me pleasures!",
			"Argh-lad, is that Lee Elliott over there",
			"Billions of blue blistering barnacles!",
			"Avast, men! Get a telescope full of the doubloons on *that* vessel.",
			"To arms, me lads! The spoils of the gold shall be ours!",
			"Be that a peg leg, or arrr ye just happy to cast yer eyes upon me?",
			"Avast, ya scurvy knave! Brave be ye, for certain, but arrr ye willin' ta die fer that spot?",
			"Aaaarrrrrghhh! Who among us floated the air mead?"
		}; 

		private bool m_Bandage;

		[Constructable] 
		public UndeadBlackBart() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.175, 0.2 )
		{ 
			Body = 400;
			Team = 1;
			Kills = 10;
            Hue = Utility.RandomSkinHue(); 
			SpeechHue = 1153;
			this.Body = 0x190; 
			Name = NameList.RandomName( "Male" );
            		SetStr( 100 );
			SetDex( 100 );
			SetInt( 20 );

			SetSkill( SkillName.Archery, 100.0 );
			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Healing, 100.0 );
			SetSkill( SkillName.Anatomy, 100.0 );
            SetSkill(SkillName.Swords, 100.0);
            SetSkill(SkillName.Hiding, 120.0, 180.0);
            SetSkill(SkillName.Stealth, 120.0, 180.0);

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 0;

			Item hair = new LongHair();
			hair.Hue = 1175;
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );
			Item beard = new MediumLongBeard();
			beard.Hue = 1175;
			beard.Layer = Layer.FacialHair;
			beard.Movable = false;
			AddItem( beard );
			PackGold( 120, 160 );
			PackItem( new Bandage( 100 ) );

			Item fancyshirt = new FancyShirt();
			EquipItem( fancyshirt );
			Item necklace = new Necklace();
			EquipItem( necklace );
			Item goldring = new GoldRing();
			EquipItem( goldring );
			Item longpants = new LongPants();
			longpants.Hue = Utility.RandomNeutralHue();
			EquipItem( longpants );
			Item boots = new Boots();
			EquipItem( boots );
			Item gloves = new LeatherGloves();
			EquipItem( gloves);
			Item hat = new TricorneHat();
			hat.Hue = 1;
			EquipItem( hat );

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

		}

			public override int TreasureMapLevel{ get{ return 4; } }

        public override void OnThink()
        {
            SpeechTimer st = new SpeechTimer(this);
            switch (Utility.Random( 7 ) )
            {
				case  0: 
					DoSpeech("Raise Anchor", new int[] {0x6B}, MessageType.Regular, 0x3B2);
                    st.Start();
                    break;
                case  1:
                    DoSpeech("Backwards", new int[] { 0x6B }, MessageType.Regular, 0x3B2);
                    st.Start();
                    break;
                case  2:
                    DoSpeech("Forward", new int[] { 0x6B }, MessageType.Regular, 0x3B2);
                    st.Start();
                    break;
                case  3:
                    DoSpeech("Right", new int[] { 0x6B }, MessageType.Regular, 0x3B2);
                    st.Start();
                    break;
                case  4:
                    DoSpeech("Left", new int[] { 0x6B }, MessageType.Regular, 0x3B2);
                    st.Start();
                    break;
                case  5:
                    DoSpeech("Turn Right", new int[] { 0x6B }, MessageType.Regular, 0x3B2);
                    st.Start();
                    break;
                case  6:
                    DoSpeech("Turn Left", new int[] { 0x6B }, MessageType.Regular, 0x3B2);
                    st.Start();
                    break;
            }
        }

        private class SpeechTimer : Timer
        {
            private UndeadBlackBart pirate;

            public SpeechTimer(UndeadBlackBart o)
                : base(TimeSpan.FromSeconds(15))
            {
                pirate = o;
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                pirate.m_DoSpeech = false;
            }

        }

        public override void OnCombatantChange()
        {
            SpeechTimer st = new SpeechTimer(this);
            DoSpeech("Drop Anchor", new int[] { 0x6A }, MessageType.Regular, 0x3B2);
            st.Start();
        }

			public override void GenerateLoot()
			{
				AddLoot( LootPack.Average );
			}

		public override bool ShowFameTitle{ get{ return true; } }

        public override void OnMovement( Mobile m, Point3D oldLocation ) 
           {                                                    
       		  	if( m_Talked == false ) 
        	  	{ 
      		      		if ( m.InRange( this, 3 ) && m is PlayerMobile ) 
        			{                
            		   	    m_Talked = true; 
            		   		SayRandom( PirateSay, this ); 
           		   			this.Move( GetDirectionTo( m.Location ) );
             		   		SpamTimer t = new SpamTimer(); 
           		   			t.Start(); 
            		} 
        	  	} 
			}

			public override bool OnBeforeDeath()
			{
				this.Say( "Me crew shall seek their revenge on ye!" );
				return base.OnBeforeDeath();
			}

    	  		private class SpamTimer : Timer 
			{ 
		   		public SpamTimer() : base( TimeSpan.FromSeconds( 12 ) ) 
	       	   		{ 
          				Priority = TimerPriority.OneSecond; 
       		   		} 

         	   		protected override void OnTick() 
        	   		{ 
           				m_Talked = false; 
        	   		} 
      			}

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
		   private UndeadBlackBart pirate;

                   public BandageTimer( UndeadBlackBart o ) : base( TimeSpan.FromSeconds( 15 ) ) 
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
        public override void OnActionWander()
        {
            this.Frozen = false;
            this.Hidden = true;
            this.UseSkill(SkillName.Stealth);
        }

		private static void SayRandom( string[] say, Mobile m ) 
	    { 
	           	m.Say( say[Utility.Random( say.Length )] ); 
		}

		public UndeadBlackBart( Serial serial ) : base( serial )
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