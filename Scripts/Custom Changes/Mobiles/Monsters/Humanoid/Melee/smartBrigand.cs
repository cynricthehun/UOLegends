using System;
using System.Collections;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Mobiles
{
	public class SBrigand : BaseCreature
	{
        private bool m_Bandage;
        private Timer m_SoundTimer;
        private bool m_HasTeleportedAway;
		public override bool ClickTitle{ get{ return false; } }

		[Constructable]
		public SBrigand() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
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
			SetSkill( SkillName.MagicResist, 85.0, 97.5 );
			SetSkill( SkillName.Swords, 65.0, 87.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 15.0, 37.5 );
            SetSkill(SkillName.Hiding, 115.0, 137.5);
            SetSkill(SkillName.Stealth, 85.0, 107.5);
            SetSkill(SkillName.Anatomy, 85.0, 107.5);
            SetSkill(SkillName.Healing, 85.0, 107.5);

			Fame = 1000;
			Karma = -1000;

			AddItem( new Boots( Utility.RandomNeutralHue() ) );
			AddItem( new FancyShirt( Utility.RandomDyedHue() ));
			AddItem( new Bandana( Utility.RandomBirdHue() ));

            PackItem(new Bandage(Utility.RandomMinMax(10, 15)));
            PackItem(new Arrow(Utility.RandomMinMax(60, 70)));
            PackItem(new Apple(Utility.RandomMinMax(3, 5)));

			switch ( Utility.Random( 8 ))
			{
				case 0: AddItem( new Longsword() ); break;
				case 1: AddItem( new Cutlass() ); break;
				case 2: AddItem( new Broadsword() ); break;
				case 3: AddItem( new Axe() ); break;
				case 4: AddItem( new Club() ); break;
				case 5: AddItem( new Dagger() ); break;
				case 6: AddItem( new Spear() ); break;
                case 7: AddItem(new Bow()); break;
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
            AddLoot(LootPack.Average);
            AddLoot(LootPack.Average);
		}

		public override bool AlwaysMurderer{ get{ return true; } }

        public override bool IsEnemy(Mobile m)
        {
            if (m.Player && m.FindItemOnLayer(Layer.Helm) is Bandana)
                return false;

            return base.IsEnemy(m);
        }

        public override void AggressiveAction(Mobile aggressor, bool criminal)
        {
            base.AggressiveAction(aggressor, criminal);

            Item item = aggressor.FindItemOnLayer(Layer.Helm);

            if (item is Bandana)
            {
                AOS.Damage(aggressor, 50, 0, 100, 0, 0, 0);
                item.Delete();
                aggressor.FixedParticles(0x36BD, 20, 10, 5044, EffectLayer.Head);
                aggressor.PlaySound(0x307);
            }
        }

        public override void OnCombatantChange()
        {
            base.OnCombatantChange();

            if (Hidden && Combatant != null)
                Combatant = null;
        }

        public virtual void SendTrackingSound()
        {
            if (Hidden)
            {
                Effects.PlaySound(this.Location, this.Map, 0x12C);
                Combatant = null;
            }
            else
            {
                Frozen = false;

                if (m_SoundTimer != null)
                    m_SoundTimer.Stop();

                m_SoundTimer = null;
            }
        }

        public override void OnThink()
        {

            if (!m_HasTeleportedAway && Hits < (HitsMax / 2) && Poisoned == false)
            {
                Map map = this.Map;

                if (map != null)
                {
                    for (int i = 0; i < 10; ++i)
                    {
                        int x = X + (Utility.RandomMinMax(5, 10) * (Utility.RandomBool() ? 1 : -1));
                        int y = Y + (Utility.RandomMinMax(5, 10) * (Utility.RandomBool() ? 1 : -1));
                        int z = Z;

                        if (!map.CanFit(x, y, z, 16, false, false))
                            continue;

                        Point3D from = this.Location;
                        Point3D to = new Point3D(x, y, z);

                        this.Location = to;
                        this.ProcessDelta();
                        this.Hidden = true;
                        this.Combatant = null;

                        Effects.SendLocationParticles(EffectItem.Create(from, map, EffectItem.DefaultDuration), 0x3728, 10, 10, 2023);
                        Effects.SendLocationParticles(EffectItem.Create(to, map, EffectItem.DefaultDuration), 0x3728, 10, 10, 5023);

                        Effects.PlaySound(to, map, 0x1FE);

                        m_HasTeleportedAway = true;
                        m_SoundTimer = Timer.DelayCall(TimeSpan.FromSeconds(5.0), TimeSpan.FromSeconds(2.5), new TimerCallback(SendTrackingSound));

                        this.UseSkill(SkillName.Stealth);
                        AIObject.Action = ActionType.Flee;

                        break;
                    }
                }
            }

            if (this.Hits < (this.HitsMax - 10) && m_Bandage == false)
            {
                m_Bandage = true;

                Container backpack = this.Backpack;

                Bandage bandage = (Bandage)backpack.FindItemByType(typeof(Bandage));

                if (bandage != null)
                {
                    if (BandageContext.BeginHeal(this, this) != null)
                        bandage.Consume();

                    BandageTimer bt = new BandageTimer(this);
                    bt.Start();
                }
            }

            base.OnThink();
        }

        public override void OnActionWander()
        {
            this.Frozen = false;
            this.Hidden = true;
            this.UseSkill(SkillName.Stealth); 
        }

        public int GetRange(PlayerMobile pm)
        {
            return 4;
        }

        public override void OnMovement(Mobile m, Point3D oldLocation)
        {
            if (m.Alive && m is PlayerMobile)
            {
                PlayerMobile pm = (PlayerMobile)m;
                int range = GetRange(pm);

                if (range >= 0 && InRange(m, range) && !InRange(oldLocation, range) && this.Hits == this.HitsMax && this.Hidden == true && IsEnemy(m))
                {
                    this.Frozen = false;
                    this.Hidden = false;
                    this.Combatant = m;
                }
            }
        }

        public override void OnDamage(int amount, Mobile m, bool willKill)
        {
            if (this.Hits < (this.HitsMax - 10) && m_Bandage == false && this.Hidden == false)
            {
                m_Bandage = true;

                Container backpack = this.Backpack;

                Bandage bandage = (Bandage)backpack.FindItemByType(typeof(Bandage));

                if (bandage != null)
                {

                    if (BandageContext.BeginHeal(this, this) != null)
                        bandage.Consume();

                    BandageTimer bt = new BandageTimer(this);
                    bt.Start();
                }
            }
        }

        private class BandageTimer : Timer
        {
            private SBrigand brig;

            public BandageTimer(SBrigand o)
                : base(TimeSpan.FromSeconds(15))
            {
                brig = o;
                Priority = TimerPriority.OneSecond;
            }

            protected override void OnTick()
            {
                brig.m_Bandage = false;
            }

        }

		public SBrigand( Serial serial ) : base( serial )
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