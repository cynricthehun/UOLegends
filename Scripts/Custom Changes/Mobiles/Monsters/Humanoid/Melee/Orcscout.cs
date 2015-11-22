using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using Server.Network;
using Server.Mobiles;


namespace Server.Mobiles
{
	[CorpseName( "an orc scout corpse" )]
	public class OrcScout : BaseCreature
        {
	     private bool m_Bandage;
	     private Timer m_SoundTimer;
	     private bool m_HasTeleportedAway;
              
             public override WeaponAbility GetWeaponAbility()
	     {
	     return WeaponAbility.MortalStrike;
	     }

	     public override InhumanSpeech SpeechType{ get{ return InhumanSpeech.Orc; } }
            
	            
             [Constructable]
	     public OrcScout() : base( AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4 )
	     {
		   Name = "an orc scout";
		   Body = 17;
                   	   Hue = 0x306;
		   BaseSoundID = 432;

               
		   SetStr( 96, 120 );
		   SetDex( 161, 170 );
		   SetInt( 36, 60 );

		   SetHits( 101, 130 );

	   	   SetDamage( 28, 31 );

		   SetDamageType( ResistanceType.Physical, 100 );

		   SetResistance( ResistanceType.Physical, 25, 30 );
		   SetResistance( ResistanceType.Fire, 20, 30 );
		   SetResistance( ResistanceType.Cold, 10, 20 );
		   SetResistance( ResistanceType.Poison, 10, 20 );
		   SetResistance( ResistanceType.Energy, 20, 30 );

		   SetSkill( SkillName.MagicResist, 80.1, 95.0 );
		   SetSkill( SkillName.Tactics, 95.1, 100.0 ); 
           SetSkill( SkillName.Veterinary, 80.1, 90.0 );
           SetSkill( SkillName.Hiding, 500.0 ); 
           SetSkill( SkillName.Stealth, 500.0 );
                      		      
		   SetFameLevel( 2 );
		   SetKarmaLevel( 2 );

		   VirtualArmor = 25;

		Item bow = new OrcBow();
		bow.Movable = false;
		AddItem( bow );

                   PackItem( new Bandage(Utility.RandomMinMax(1,15)) );
                   PackItem( new Arrow(Utility.RandomMinMax(60,70)) );
                   PackItem( new Apple(Utility.RandomMinMax(3,5)) );

	     if ( 0.2 > Utility.RandomDouble() )
		PackItem( new BolaBall() );
		else if( 0.1 > Utility.RandomDouble() )
		PackItem( new OrcMask() );
		else if( 0.01 > Utility.RandomDouble() )
		PackItem( new OrcBow() );

	     }


	     public override void GenerateLoot()
	     {
		   AddLoot( LootPack.Average );
	     }

	     public override bool CanRummageCorpses{ get{ return true; } }
	     public override int Meat{ get{ return 1; } }

	     public override OppositionGroup OppositionGroup
	     {
		   get{ return OppositionGroup.SavagesAndOrcs; }
	     }

	     public override bool IsEnemy( Mobile m )
	     {
		   if ( m.Player && m.FindItemOnLayer( Layer.Helm ) is OrcishKinMask )
			   return false;

			   return base.IsEnemy( m );
	     }

	     public override void AggressiveAction( Mobile aggressor, bool criminal )
	     {
		   base.AggressiveAction( aggressor, criminal );

		   Item item = aggressor.FindItemOnLayer( Layer.Helm );

		   if ( item is OrcishKinMask )
		   {
			   AOS.Damage( aggressor, 50, 0, 100, 0, 0, 0 );
			   item.Delete();
			   aggressor.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
			   aggressor.PlaySound( 0x307 );
		   }
	     }

             public override void OnCombatantChange()
	     {
		   base.OnCombatantChange();

		   if ( Hidden && Combatant != null )
			   Combatant = null;
	     }

             public virtual void SendTrackingSound()
	     {
		   if ( Hidden )
		   {
			   Effects.PlaySound( this.Location, this.Map, 0x12C );
			   Combatant = null;
		   }
		   else
		   {
			   Frozen = false;

			   if ( m_SoundTimer != null )
				m_SoundTimer.Stop();

			   m_SoundTimer = null;
		   }
	     }

             public override void OnThink()
	     {
                 
                   if ( !m_HasTeleportedAway && Hits < (HitsMax / 2) && Poisoned == false )
		   {
			   Map map = this.Map;

			   if ( map != null )
			   {
				for ( int i = 0; i < 10; ++i )
				{
					int x = X + (Utility.RandomMinMax( 5, 10 ) * (Utility.RandomBool() ? 1 : -1));
					int y = Y + (Utility.RandomMinMax( 5, 10 ) * (Utility.RandomBool() ? 1 : -1));
					int z = Z;

					if ( !map.CanFit( x, y, z, 16, false, false ) )
					continue;

					Point3D from = this.Location;
					Point3D to = new Point3D( x, y, z );

					this.Location = to;
					this.ProcessDelta();
					this.Hidden = true;
					this.Combatant = null;

					Effects.SendLocationParticles( EffectItem.Create( from, map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
					Effects.SendLocationParticles( EffectItem.Create(   to, map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );

					Effects.PlaySound( to, map, 0x1FE );

					m_HasTeleportedAway = true;
					m_SoundTimer = Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 2.5 ), new TimerCallback( SendTrackingSound ) );

				        this.UseSkill( SkillName.Stealth );
                                        AIObject.Action = ActionType.Flee;

 					break;
				}
			   }
		   }

                   if ( this.Hits < ( this.HitsMax - 10) && m_Bandage == false )
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

          	   base.OnThink();
	     }


             public int GetRange( PlayerMobile pm )
	     {
  			return 4;
             }

             public override void OnMovement( Mobile m, Point3D oldLocation )
	     {
		   if ( m.Alive && m is PlayerMobile )
                   {
		   PlayerMobile pm = (PlayerMobile)m;
  		   int range = GetRange( pm );
                           
                           if ( range >= 0 && InRange( m, range ) && !InRange( oldLocation, range ) && this.Hits == this.HitsMax && this.Hidden == true && IsEnemy( m ) )
                           {
                                 this.Frozen = false;
                                 this.Hidden = false;
                                 this.Combatant = m;
                           }
                   }
	     }
	     
             public override void OnDamage( int amount, Mobile m, bool willKill )
	     {
                   if ( this.Hits < ( this.HitsMax - 10) && m_Bandage == false && this.Hidden == false )
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
		   private OrcScout scout;

                   public BandageTimer( OrcScout o ) : base( TimeSpan.FromSeconds( 15 ) ) 
		   { 
                   scout = o;
		   Priority = TimerPriority.OneSecond; 
		   } 
		
                   protected override void OnTick() 
		   { 
				scout.m_Bandage = false; 
		   } 
		  		
            }

	    public OrcScout( Serial serial ) : base( serial )
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
