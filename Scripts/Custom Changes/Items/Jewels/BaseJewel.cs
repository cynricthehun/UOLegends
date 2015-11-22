using System;
using System.Collections;
using Server.Engines.Craft;

namespace Server.Items
{
	public enum GemType
	{
		None,
		StarSapphire,
		Emerald,
		Sapphire,
		Ruby,
		Citrine,
		Amethyst,
		Tourmaline,
		Amber,
		Diamond
	}

	public enum MagicEffect
	{
		None,
		Clumsy,
		Feeblemind,
		Nightsight,
		Weaken,
		Agility,
		Cunning,
		Protection,
		Stength,
		Bless,
		Invisibility,
		MagicReflection
	}

	public abstract class BaseJewel : Item, ICraftable
	{
		private AosAttributes m_AosAttributes;
		private AosElementAttributes m_AosResistances;
		private AosSkillBonuses m_AosSkillBonuses;
		private CraftResource m_Resource;
		private GemType m_GemType;
		private MagicEffect m_Effect;
		private int m_Uses;
		private bool m_Identified;
		private static Hashtable m_Registry = new Hashtable();
		public static Hashtable Registry { get { return m_Registry; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Identified
		{
			get{ return m_Identified; }
			set{ m_Identified = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Uses
		{
			get{ return m_Uses; }
			set{ m_Uses = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public MagicEffect Effect
		{
			get{ return m_Effect; }
			set{ m_Effect = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosAttributes Attributes
		{
			get{ return m_AosAttributes; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosElementAttributes Resistances
		{
			get{ return m_AosResistances; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public AosSkillBonuses SkillBonuses
		{
			get{ return m_AosSkillBonuses; }
			set{}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; Hue = CraftResources.GetHue( m_Resource ); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public GemType GemType
		{
			get{ return m_GemType; }
			set{ m_GemType = value; InvalidateProperties(); }
		}

		public override int PhysicalResistance{ get{ return m_AosResistances.Physical; } }
		public override int FireResistance{ get{ return m_AosResistances.Fire; } }
		public override int ColdResistance{ get{ return m_AosResistances.Cold; } }
		public override int PoisonResistance{ get{ return m_AosResistances.Poison; } }
		public override int EnergyResistance{ get{ return m_AosResistances.Energy; } }
		public virtual int BaseGemTypeNumber{ get{ return 0; } }

		public override int LabelNumber
		{
			get
			{
				if ( m_GemType == GemType.None )
					return base.LabelNumber;

				return BaseGemTypeNumber + (int)m_GemType - 1;
			}
		}

		public virtual int ArtifactRarity{ get{ return 0; } }

		public BaseJewel( int itemID, Layer layer ) : base( itemID )
		{
			m_AosAttributes = new AosAttributes( this );
			m_AosResistances = new AosElementAttributes( this );
			m_AosSkillBonuses = new AosSkillBonuses( this );
			m_Resource = CraftResource.Iron;
			m_GemType = GemType.None;

			Layer = layer;
		}

		public override void OnAdded( object parent )
		{
			if ( m_Uses > 0 && parent is Mobile )
			{
				Mobile from = (Mobile)parent;
				string modName = from.Serial.ToString();
				switch( m_Effect )
				{
					case MagicEffect.None:
					{
						break;
					}
					case MagicEffect.Clumsy:
					{
						Timer t = new StatTimer( from, TimeSpan.FromSeconds( 30 ), this );
						from.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", Utility.Random( -5, -11 ), TimeSpan.Zero ) );
						//from.FixedParticles( 0x3779, 10, 15, 5002, EffectLayer.Head );
						//from.PlaySound( 0x1DF );
						t.Start();
						break;
					}
					case MagicEffect.Feeblemind:
					{
						Timer t = new StatTimer( from, TimeSpan.FromSeconds( 30 ), this );
						from.AddStatMod( new StatMod( StatType.Int, modName + "Int", Utility.Random( -5, -11 ), TimeSpan.Zero ) );
						//from.FixedParticles( 0x3779, 10, 15, 5004, EffectLayer.Head );
						//from.PlaySound( 0x1E4 );
						t.Start();
						break;
					}
					case MagicEffect.Nightsight:
					{
						from.LightLevel = 35;
						new LightCycle.NightSightTimer( from ).Start();
						//from.FixedParticles( 0x376A, 9, 32, 5007, EffectLayer.Waist );
						//from.PlaySound( 0x1E3 );
						break;
					}
					case MagicEffect.Weaken:
					{
						Timer t = new StatTimer( from, TimeSpan.FromSeconds( 30 ), this );
						from.AddStatMod( new StatMod( StatType.Str, modName + "Str", Utility.Random( -5, -11 ), TimeSpan.Zero ) );
						//from.FixedParticles( 0x3779, 10, 15, 5009, EffectLayer.Waist );
						//from.PlaySound( 0x1E6 );
						t.Start();
						break;
					}
					case MagicEffect.Agility:
					{
						Timer t = new StatTimer( from, TimeSpan.FromSeconds( 30 ), this );
						from.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", Utility.Random( 5, 11 ), TimeSpan.Zero ) );
						//from.FixedParticles( 0x375A, 10, 15, 5010, EffectLayer.Waist );
						//from.PlaySound( 0x28E );
						t.Start();
						break;
					}
					case MagicEffect.Cunning:
					{
						Timer t = new StatTimer( from, TimeSpan.FromSeconds( 30 ), this );
						from.AddStatMod( new StatMod( StatType.Int, modName + "Int", Utility.Random( 5, 11 ), TimeSpan.Zero ) );
						//from.FixedParticles( 0x375A, 10, 15, 5011, EffectLayer.Head );
						//from.PlaySound( 0x1EB );
						t.Start();
						break;
					}
					case MagicEffect.Protection:
					{
						if ( m_Registry.ContainsKey( from ) )
						{
							from.SendAsciiMessage( "This spell is already in effect." );
						}
						else if ( from.BeginAction( typeof( DefensiveSpell ) ) )
						{
							double value = (int)(from.Skills[SkillName.EvalInt].Value + from.Skills[SkillName.Meditation].Value + from.Skills[SkillName.Inscribe].Value);
							value /= 4;

							if ( value < 0 )
								value = 0;
							else if ( value > 75 )
								value = 75.0;

							Registry.Add( from, value );
							new ProtectionTimer( from, this ).Start();

							from.FixedParticles( 0x375A, 9, 20, 5016, EffectLayer.Waist );
							from.PlaySound( 0x1ED );
						}
						else
						{
							from.SendAsciiMessage( "The spell will not adhere to you at this time." );
						}
						break;
					}
					case MagicEffect.Stength:
					{
						Timer t = new StatTimer( from, TimeSpan.FromSeconds( 30 ), this );
						from.AddStatMod( new StatMod( StatType.Str, modName + "Str", Utility.Random( 5, 11 ), TimeSpan.Zero ) );
						//from.FixedParticles( 0x375A, 10, 15, 5017, EffectLayer.Waist );
						//from.PlaySound( 0x1EE );
						t.Start();
						break;
					}
					case MagicEffect.Bless:
					{
						Timer t = new StatTimer( from, TimeSpan.FromSeconds( 30 ), this );
						from.AddStatMod( new StatMod( StatType.Str, modName + "Str", Utility.Random( 5, 11 ), TimeSpan.Zero ) );
						from.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", Utility.Random( 5, 11 ), TimeSpan.Zero ) );
						from.AddStatMod( new StatMod( StatType.Int, modName + "Int", Utility.Random( 5, 11 ), TimeSpan.Zero ) );
						//from.FixedParticles( 0x373A, 10, 15, 5018, EffectLayer.Waist );
						//from.PlaySound( 0x1EA );
						t.Start();
						break;
					}
					case MagicEffect.Invisibility:
					{
						Timer t = new InvisTimer( from, TimeSpan.FromSeconds( 10 ), this );
						from.Hidden = true;
						//Effects.SendLocationParticles( EffectItem.Create( new Point3D( from.X, from.Y, from.Z + 16 ), from.Map, EffectItem.DefaultDuration ), 0x376A, 10, 15, 5045 );
						//from.PlaySound( 0x203 );
						t.Start();
						break;
					}
					case MagicEffect.MagicReflection:
					{
						if ( from.BeginAction( typeof( DefensiveSpell ) ) )
						{
							Timer t = new MRTimer( from, TimeSpan.FromSeconds( 60 ), this );
							int value = (int)(from.Skills[SkillName.Magery].Value + from.Skills[SkillName.Inscribe].Value);
							value = (int)(2 + (value/200)*7.0);//absorb from 8 to 15 "circles"

							from.MagicDamageAbsorb = value;

							from.FixedParticles( 0x375A, 10, 15, 5037, EffectLayer.Waist );
							from.PlaySound( 0x1E9 );
							t.Start();
						}
						else
						{
							from.SendAsciiMessage( "The spell will not adhere to you at this time." );
						}
						break;
					}
				}
			--m_Uses;
			}
		}

		public virtual void RedoMR( Mobile from, BaseJewel item )
		{
			if( from.MagicDamageAbsorb == 0 )
			{
				if ( from.BeginAction( typeof( DefensiveSpell ) ) )
				{
					Timer t = new MRTimer( from, TimeSpan.FromSeconds( 60 ), this );
					int value = (int)(from.Skills[SkillName.Magery].Value + from.Skills[SkillName.Inscribe].Value);
					value = (int)(2 + (value/200)*7.0);//absorb from 8 to 15 "circles"
					from.MagicDamageAbsorb = value;

					from.FixedParticles( 0x375A, 10, 15, 5037, EffectLayer.Waist );
					from.PlaySound( 0x1E9 );
					--m_Uses;
					t.Start();
				}
				else
				{
					Timer t = new MRTimer( from, TimeSpan.FromSeconds( 10 ), this );
					t.Start();
				}
			}
			else
			{
				--m_Uses;
			}
		}

		public virtual void RedoProtection( Mobile from, BaseJewel item )
		{
			if ( m_Registry.ContainsKey( from ) )
			{
				new RedoProtectionTimer( from, this ).Start();
			}
			else if ( from.BeginAction( typeof( DefensiveSpell ) ) )
			{
				double value = (int)(from.Skills[SkillName.EvalInt].Value + from.Skills[SkillName.Meditation].Value + from.Skills[SkillName.Inscribe].Value);
				value /= 4;

				if ( value < 0 )
					value = 0;
				else if ( value > 75 )
					value = 75.0;

				Registry.Add( from, value );
				new ProtectionTimer( from, this ).Start();

				from.FixedParticles( 0x375A, 9, 20, 5016, EffectLayer.Waist );
				from.PlaySound( 0x1ED );
				--m_Uses;
			}
			else
			{
				new RedoProtectionTimer( from, this ).Start();
			}
		}

		/*public override void OnRemoved( object parent )
		{
			if ( parent is Mobile )
			{
				Mobile from = (Mobile)parent;

				m_AosSkillBonuses.Remove();

				string modName = this.Serial.ToString();

				from.RemoveStatMod( modName + "Str" );
				from.RemoveStatMod( modName + "Dex" );
				from.RemoveStatMod( modName + "Int" );

				from.CheckStatTimers();
			}
		}*/

//=========================================================================================================================
//				MAGIC REFLECTION TIMER
//=========================================================================================================================

		private static Hashtable m_MRTable = new Hashtable();

		public static bool HasMRTimer( Mobile m )
		{
			return m_MRTable[m] != null;
		}

		public static void RemoveMRTimer( Mobile m )
		{
			Timer t = (Timer)m_MRTable[m];

			if ( t != null )
			{
				t.Stop();
				m_MRTable.Remove( m );
			}
		}

		private class MRTimer : Timer
		{
			private Mobile m_Mobile;
			private BaseJewel m_Item;

			public MRTimer( Mobile m, TimeSpan duration, BaseJewel item ) : base( duration )
			{
				Priority = TimerPriority.OneSecond;
				m_Mobile = m;
				m_Item = item;
			}

			protected override void OnTick()
			{
				if ( m_Item.Uses > 0 && m_Item.Parent == m_Mobile )
				{
					m_Item.RedoMR( m_Mobile, m_Item );
					RemoveMRTimer( m_Mobile );
				}
				else
				{
					RemoveMRTimer( m_Mobile );
				}
			}
		}

//=========================================================================================================================
//=========================================================================================================================

//=========================================================================================================================
//				PROTECTION TIMER
//=========================================================================================================================

		private class ProtectionTimer : Timer
		{
			private Mobile m_Mobile;
			private BaseJewel m_Item;

			public ProtectionTimer( Mobile from, BaseJewel item ) : base( TimeSpan.FromSeconds( 0 ) )
			{
				double val = from.Skills[SkillName.Magery].Value * 2.0;
				if ( val < 15 )
					val = 15;
				else if ( val > 240 )
					val = 240;

				m_Mobile = from;
				m_Item = item;
				Delay = TimeSpan.FromSeconds( val );
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				if ( m_Item.Uses > 0 && m_Item.Parent == m_Mobile )
				{
					m_Registry.Remove( m_Mobile );
					DefensiveSpell.Nullify( m_Mobile );
					m_Item.RedoProtection( m_Mobile, m_Item );
				}
				else
				{
					m_Registry.Remove( m_Mobile );
					DefensiveSpell.Nullify( m_Mobile );
				}
			}
		}

		private class RedoProtectionTimer : Timer
		{
			private Mobile m_Mobile;
			private BaseJewel m_Item;

			public RedoProtectionTimer( Mobile from, BaseJewel item ) : base( TimeSpan.FromSeconds( 10 ) )
			{
				m_Mobile = from;
				m_Item = item;
			}

			protected override void OnTick()
			{
				if ( !m_Registry.ContainsKey( m_Mobile ) )
				{
					if ( m_Item.Uses > 0 && m_Item.Parent == m_Mobile )
					{
						m_Item.RedoProtection( m_Mobile, m_Item );
					}
					else
					{
						m_Registry.Remove( m_Mobile );
						DefensiveSpell.Nullify( m_Mobile );	
					}
				}
				else
				{
					if ( m_Item.Parent == m_Mobile )
					{
						m_Item.RedoProtection( m_Mobile, m_Item );
					}
					else
					{
						m_Registry.Remove( m_Mobile );
						DefensiveSpell.Nullify( m_Mobile );	
					}
				}
			}
		}

//=========================================================================================================================
//=========================================================================================================================

//=========================================================================================================================
//				STATMOD TIMER
//=========================================================================================================================

		private static Hashtable m_StatTable = new Hashtable();

		public static bool HasStatTimer( Mobile m )
		{
			return m_StatTable[m] != null;
		}

		public static void RemoveStatTimer( Mobile m )
		{
			Timer t = (Timer)m_StatTable[m];

			if ( t != null )
			{
				t.Stop();
				m_StatTable.Remove( m );
			}
		}

		private class StatTimer : Timer
		{
			private Mobile m_Mobile;
			private BaseJewel m_Item;

			public StatTimer( Mobile m, TimeSpan duration, BaseJewel item ) : base( duration )
			{
				Priority = TimerPriority.OneSecond;
				m_Mobile = m;
				m_Item = item;
			}

			protected override void OnTick()
			{
				if ( m_Item.Uses > 0 && m_Item.Parent == m_Mobile )
				{
					m_Item.OnAdded( m_Mobile );
					RemoveStatTimer( m_Mobile );
				}
				else
				{
					string modName = m_Mobile.Serial.ToString();

					m_Mobile.RemoveStatMod( modName + "Str" );
					m_Mobile.RemoveStatMod( modName + "Dex" );
					m_Mobile.RemoveStatMod( modName + "Int" );
					m_Mobile.CheckStatTimers();

					RemoveStatTimer( m_Mobile );
				}
			}
		}

//=========================================================================================================================
//=========================================================================================================================

//=========================================================================================================================
//				INVISIBILITY TIMER
//=========================================================================================================================
		private static Hashtable m_Table = new Hashtable();

		public static bool HasInvisTimer( Mobile m )
		{
			return m_Table[m] != null;
		}

		public static void RemoveInvisTimer( Mobile m )
		{
			Timer t = (Timer)m_Table[m];

			if ( t != null )
			{
				t.Stop();
				m_Table.Remove( m );
			}
		}

		private class InvisTimer : Timer
		{
			private Mobile m_Mobile;
			private BaseJewel m_Item;

			public InvisTimer( Mobile m, TimeSpan duration, BaseJewel item ) : base( duration )
			{
				Priority = TimerPriority.OneSecond;
				m_Mobile = m;
				m_Item = item;
			}

			protected override void OnTick()
			{
				if ( m_Item.Uses > 0 && m_Item.Parent == m_Mobile )
				{
					m_Item.OnAdded( m_Mobile );
					RemoveInvisTimer( m_Mobile );
				}
				else
				{
					m_Mobile.RevealingAction();
					RemoveInvisTimer( m_Mobile );
				}
			}
		}

//=========================================================================================================================
//=========================================================================================================================

		public BaseJewel( Serial serial ) : base( serial )
		{
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			m_AosSkillBonuses.GetProperties( list );

			int prop;

			if ( (prop = ArtifactRarity) > 0 )
				list.Add( 1061078, prop.ToString() ); // artifact rarity ~1_val~

			if ( (prop = m_AosAttributes.WeaponDamage) != 0 )
				list.Add( 1060401, prop.ToString() ); // damage increase ~1_val~%

			if ( (prop = m_AosAttributes.DefendChance) != 0 )
				list.Add( 1060408, prop.ToString() ); // defense chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusDex) != 0 )
				list.Add( 1060409, prop.ToString() ); // dexterity bonus ~1_val~

			if ( (prop = m_AosAttributes.EnhancePotions) != 0 )
				list.Add( 1060411, prop.ToString() ); // enhance potions ~1_val~%

			if ( (prop = m_AosAttributes.CastRecovery) != 0 )
				list.Add( 1060412, prop.ToString() ); // faster cast recovery ~1_val~

			if ( (prop = m_AosAttributes.CastSpeed) != 0 )
				list.Add( 1060413, prop.ToString() ); // faster casting ~1_val~

			if ( (prop = m_AosAttributes.AttackChance) != 0 )
				list.Add( 1060415, prop.ToString() ); // hit chance increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusHits) != 0 )
				list.Add( 1060431, prop.ToString() ); // hit point increase ~1_val~

			if ( (prop = m_AosAttributes.BonusInt) != 0 )
				list.Add( 1060432, prop.ToString() ); // intelligence bonus ~1_val~

			if ( (prop = m_AosAttributes.LowerManaCost) != 0 )
				list.Add( 1060433, prop.ToString() ); // lower mana cost ~1_val~%

			if ( (prop = m_AosAttributes.LowerRegCost) != 0 )
				list.Add( 1060434, prop.ToString() ); // lower reagent cost ~1_val~%

			if ( (prop = m_AosAttributes.Luck) != 0 )
				list.Add( 1060436, prop.ToString() ); // luck ~1_val~

			if ( (prop = m_AosAttributes.BonusMana) != 0 )
				list.Add( 1060439, prop.ToString() ); // mana increase ~1_val~

			if ( (prop = m_AosAttributes.RegenMana) != 0 )
				list.Add( 1060440, prop.ToString() ); // mana regeneration ~1_val~

			if ( (prop = m_AosAttributes.NightSight) != 0 )
				list.Add( 1060441 ); // night sight

			if ( (prop = m_AosAttributes.ReflectPhysical) != 0 )
				list.Add( 1060442, prop.ToString() ); // reflect physical damage ~1_val~%

			if ( (prop = m_AosAttributes.RegenStam) != 0 )
				list.Add( 1060443, prop.ToString() ); // stamina regeneration ~1_val~

			if ( (prop = m_AosAttributes.RegenHits) != 0 )
				list.Add( 1060444, prop.ToString() ); // hit point regeneration ~1_val~

			if ( (prop = m_AosAttributes.SpellChanneling) != 0 )
				list.Add( 1060482 ); // spell channeling

			if ( (prop = m_AosAttributes.SpellDamage) != 0 )
				list.Add( 1060483, prop.ToString() ); // spell damage increase ~1_val~%

			if ( (prop = m_AosAttributes.BonusStam) != 0 )
				list.Add( 1060484, prop.ToString() ); // stamina increase ~1_val~

			if ( (prop = m_AosAttributes.BonusStr) != 0 )
				list.Add( 1060485, prop.ToString() ); // strength bonus ~1_val~

			if ( (prop = m_AosAttributes.WeaponSpeed) != 0 )
				list.Add( 1060486, prop.ToString() ); // swing speed increase ~1_val~%

			base.AddResistanceProperties( list );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 ); // version

			writer.Write( (bool) m_Identified );
			writer.Write( (int) m_Uses );
			writer.WriteEncodedInt( (int) m_Effect );

			writer.WriteEncodedInt( (int) m_Resource );
			writer.WriteEncodedInt( (int) m_GemType );

			m_AosAttributes.Serialize( writer );
			m_AosResistances.Serialize( writer );
			m_AosSkillBonuses.Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 3: 
				{
					m_Identified = reader.ReadBool();
					m_Uses = reader.ReadInt();
					m_Effect = (MagicEffect)reader.ReadEncodedInt();
					goto case 2;
				}
				case 2:
				{
					m_Resource = (CraftResource)reader.ReadEncodedInt();
					m_GemType = (GemType)reader.ReadEncodedInt();

					goto case 1;
				}
				case 1:
				{
					m_AosAttributes = new AosAttributes( this, reader );
					m_AosResistances = new AosElementAttributes( this, reader );
					m_AosSkillBonuses = new AosSkillBonuses( this, reader );

					if ( Core.AOS && Parent is Mobile )
						m_AosSkillBonuses.AddTo( (Mobile)Parent );

					int strBonus = m_AosAttributes.BonusStr;
					int dexBonus = m_AosAttributes.BonusDex;
					int intBonus = m_AosAttributes.BonusInt;

					if ( Parent is Mobile && (strBonus != 0 || dexBonus != 0 || intBonus != 0) )
					{
						Mobile m = (Mobile)Parent;

						string modName = Serial.ToString();

						if ( strBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Str, modName + "Str", strBonus, TimeSpan.Zero ) );

						if ( dexBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Dex, modName + "Dex", dexBonus, TimeSpan.Zero ) );

						if ( intBonus != 0 )
							m.AddStatMod( new StatMod( StatType.Int, modName + "Int", intBonus, TimeSpan.Zero ) );
					}

					if ( Parent is Mobile )
						((Mobile)Parent).CheckStatTimers();

					break;
				}
				case 0:
				{
					m_AosAttributes = new AosAttributes( this );
					m_AosResistances = new AosElementAttributes( this );
					m_AosSkillBonuses = new AosSkillBonuses( this );

					break;
				}
			}

			if ( version < 2 )
			{
				m_Resource = CraftResource.Iron;
				m_GemType = GemType.None;
			}
			if ( version < 3 )
			{
				m_Effect = MagicEffect.None;
				m_Identified = false;
				m_Uses = 0;
			}
		}
		#region ICraftable Members

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Type resourceType = typeRes;

			if ( resourceType == null )
				resourceType = craftItem.Ressources.GetAt( 0 ).ItemType;

			Resource = CraftResources.GetFromType( resourceType );

			if ( 1 < craftItem.Ressources.Count )
			{
				resourceType = craftItem.Ressources.GetAt( 1 ).ItemType;

				if ( resourceType == typeof( StarSapphire ) )
					GemType = GemType.StarSapphire;
				else if ( resourceType == typeof( Emerald ) )
					GemType = GemType.Emerald;
				else if ( resourceType == typeof( Sapphire ) )
					GemType = GemType.Sapphire;
				else if ( resourceType == typeof( Ruby ) )
					GemType = GemType.Ruby;
				else if ( resourceType == typeof( Citrine ) )
					GemType = GemType.Citrine;
				else if ( resourceType == typeof( Amethyst ) )
					GemType = GemType.Amethyst;
				else if ( resourceType == typeof( Tourmaline ) )
					GemType = GemType.Tourmaline;
				else if ( resourceType == typeof( Amber ) )
					GemType = GemType.Amber;
				else if ( resourceType == typeof( Diamond ) )
					GemType = GemType.Diamond;
			}

			return 1;
		}

		#endregion
	}
}