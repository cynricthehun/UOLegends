using System;
using Server;
using Server.Misc;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a savage corpse" )]
	public class FireSavage : BaseCreature
	{
		private bool m_Bandage;

		[Constructable]
		public FireSavage() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "savage" );

			if ( Female = Utility.RandomBool() )
				Body = 184;
			else
				Body = 183;

			SetStr( 126, 195 );
			SetDex( 86, 105 );
			SetInt( 151, 165 );

			SetDamage( 23, 27 );

			SetSkill( SkillName.Magery, 70.0, 92.5 );
			SetSkill( SkillName.EvalInt, 40.0, 52.5 );
			SetSkill( SkillName.Poisoning, 80.0, 92.5 );
			SetSkill( SkillName.MagicResist, 77.5, 90.0 );
			SetSkill( SkillName.Swords, 60.0, 82.5 );
			SetSkill( SkillName.Tactics, 60.0, 82.5 );
			SetSkill( SkillName.Veterinary, 60.0, 70.0 );
			SetSkill( SkillName.Healing, 60.0, 70.0 );
			SetSkill( SkillName.Anatomy, 60.0, 90.0 );

			Fame = 1000;
			Karma = -1000;

			PackItem( new Bandage( Utility.RandomMinMax( 1, 15 ) ) );

			if ( Female && 0.1 > Utility.RandomDouble() )
				PackItem( new TribalBerry() );
			else if ( !Female && 0.1 > Utility.RandomDouble() )
				PackItem( new BolaBall() );

			AddItem( new Maul() );
			AddItem( new BoneArms() );
			AddItem( new BoneLegs() );

			if ( 0.1 > Utility.RandomDouble() )
				AddItem( new HornedTribalMask() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int BreathEffectHue{ get{ return 0x210; } }
		public override int Meat{ get{ return 1; } }
		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.SavagesAndOrcs; }
		}

		public override bool IsEnemy( Mobile m )
		{
			if ( m.BodyMod == 183 || m.BodyMod == 184 )
				return false;

			return base.IsEnemy( m );
		}

		public override void AggressiveAction( Mobile aggressor, bool criminal )
		{
			base.AggressiveAction( aggressor, criminal );

			if ( aggressor.BodyMod == 183 || aggressor.BodyMod == 184 )
			{
				AOS.Damage( aggressor, 50, 0, 100, 0, 0, 0 );
				aggressor.BodyMod = 0;
				aggressor.HueMod = -1;
				aggressor.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
				aggressor.PlaySound( 0x307 );
				aggressor.SendAsciiMessage( "Your skin is scorched as the tribal paint burns away!" );

				if ( aggressor is PlayerMobile )
					((PlayerMobile)aggressor).SavagePaintExpiration = TimeSpan.Zero;
			}
		}

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
		   private FireSavage savage;

                   public BandageTimer( FireSavage o ) : base( TimeSpan.FromSeconds( 15 ) ) 
		   { 
                   savage = o;
		   Priority = TimerPriority.OneSecond; 
		   } 
                   protected override void OnTick() 
		   { 
				savage.m_Bandage = false; 
		   } 
		  		
            }

		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
			if ( to is Dragon || to is WhiteWyrm || to is SwampDragon || to is Drake || to is Nightmare || to is Daemon )
				damage *= 5;
		}

		public FireSavage( Serial serial ) : base( serial )
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
