using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Engines.Craft;

namespace Server.Items
{
	public delegate void InstrumentPickedCallback( Mobile from, BaseInstrument instrument );

	public enum InstrumentQuality
	{
		Low,
		Regular,
		Exceptional
	}

	public abstract class BaseInstrument : Item, ICraftable
	{
		private string m_Slr;
		private string m_Slr2;
		private int m_WellSound, m_BadlySound;
		private SlayerName m_Slayer, m_Slayer2;
		private InstrumentQuality m_Quality;
		private Mobile m_Crafter;
		private int m_UsesRemaining;

		[CommandProperty( AccessLevel.GameMaster )]
		public int SuccessSound
		{
			get{ return m_WellSound; }
			set{ m_WellSound = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int FailureSound
		{
			get{ return m_BadlySound; }
			set{ m_BadlySound = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SlayerName Slayer
		{
			get{ return m_Slayer; }
			set{ m_Slayer = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public SlayerName Slayer2
		{
			get{ return m_Slayer2; }
			set{ m_Slayer2 = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public InstrumentQuality Quality
		{
			get{ return m_Quality; }
			set{ UnscaleUses(); m_Quality = value; InvalidateProperties(); ScaleUses(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; InvalidateProperties(); }
		}

		public virtual int InitMinUses{ get{ return 350; } }
		public virtual int InitMaxUses{ get{ return 450; } }

		public int UsesRemaining
		{
			get{ return m_UsesRemaining; }
			set{ m_UsesRemaining = value; InvalidateProperties(); }
		}

		public void ScaleUses()
		{
			m_UsesRemaining = (m_UsesRemaining * GetUsesScalar()) / 100;
			InvalidateProperties();
		}

		public void UnscaleUses()
		{
			m_UsesRemaining = (m_UsesRemaining * 100) / GetUsesScalar();
		}

		public int GetUsesScalar()
		{
			if ( m_Quality == InstrumentQuality.Exceptional )
				return 200;

			return 100;
		}

		public void ConsumeUse( Mobile from )
		{
			// TODO: Confirm what must happen here?

			if ( UsesRemaining > 1 )
			{
				--UsesRemaining;
			}
			else
			{
				if ( from != null )
					from.SendAsciiMessage( "The instrument played its last tune." ); // 

				Delete();
			}
		}

		private static Hashtable m_Instruments = new Hashtable();

		public static BaseInstrument GetInstrument( Mobile from )
		{
			BaseInstrument item = m_Instruments[from] as BaseInstrument;

			if ( item == null )
				return null;

			if ( !item.IsChildOf( from.Backpack ) )
			{
				m_Instruments.Remove( from );
				return null;
			}

			return item;
		}

		public static int GetBardRange( Mobile bard, SkillName skill )
		{
			return 8 + (int)(bard.Skills[skill].Value / 15);
		}

		public static void PickInstrument( Mobile from, InstrumentPickedCallback callback )
		{
			BaseInstrument instrument = GetInstrument( from );

			if ( instrument != null )
			{
				if ( callback != null )
					callback( from, instrument );
			}
			else
			{
				from.SendAsciiMessage( "What instrument shall you play?" ); // 
				from.BeginTarget( 1, false, TargetFlags.None, new TargetStateCallback( OnPickedInstrument ), callback );
			}
		}

		public static void OnPickedInstrument( Mobile from, object targeted, object state )
		{
			BaseInstrument instrument = targeted as BaseInstrument;

			if ( instrument == null )
			{
				from.SendAsciiMessage( "That is not a musical instrument." ); // 
			}
			else
			{
				SetInstrument( from, instrument );

				InstrumentPickedCallback callback = state as InstrumentPickedCallback;

				if ( callback != null )
					callback( from, instrument );
			}
		}

		public static bool IsMageryCreature( BaseCreature bc )
		{
			return ( bc != null && bc.AI == AIType.AI_Mage && bc.Skills[SkillName.Magery].Base > 5.0 );
		}

		public static bool IsFireBreathingCreature( BaseCreature bc )
		{
			if ( bc == null )
				return false;

			return bc.HasBreath;
		}

		public static bool IsPoisonImmune( BaseCreature bc )
		{
			return ( bc != null && bc.PoisonImmune != null );
		}

		public static int GetPoisonLevel( BaseCreature bc )
		{
			if ( bc == null )
				return 0;

			Poison p = bc.HitPoison;

			if ( p == null )
				return 0;

			return p.Level + 1;
		}

		public double GetDifficultyFor( Mobile targ )
		{
			/* Difficulty TODO: Add another 100 points for each of the following abilities:
				- Radiation or Aura Damage (Heat, Cold etc.)
				- Summoning Undead
			*/

			double val = targ.Hits + targ.Stam + targ.Mana;

			for ( int i = 0; i < targ.Skills.Length; i++ )
				val += targ.Skills[i].Base;

			if ( val > 700 )
				val = 700 + ((val - 700) / 3.66667);

			BaseCreature bc = targ as BaseCreature;

			if ( IsMageryCreature( bc ) )
				val += 100;

			if ( IsFireBreathingCreature( bc ) )
				val += 100;

			if ( IsPoisonImmune( bc ) )
				val += 100;

			if ( targ is VampireBat || targ is VampireBatFamiliar )
				val += 100;

			val += GetPoisonLevel( bc ) * 20;

			val /= 10;

			if ( bc != null && bc.IsParagon )
				val += 40.0;

			if ( m_Quality == InstrumentQuality.Exceptional )
				val -= 5.0; // 10%

			if ( m_Slayer != SlayerName.None )
			{
				SlayerEntry entry = SlayerGroup.GetEntryByName( m_Slayer );

				if ( entry != null )
				{
					if ( entry.Slays( targ ) )
						val -= 10.0; // 20%
					else if ( entry.Group.OppositionSuperSlays( targ ) )
						val += 10.0; // -20%
				}
			}

			if ( m_Slayer2 != SlayerName.None )
			{
				SlayerEntry entry = SlayerGroup.GetEntryByName( m_Slayer2 );

				if ( entry != null )
				{
					if ( entry.Slays( targ ) )
						val -= 10.0; // 20%
					else if ( entry.Group.OppositionSuperSlays( targ ) )
						val += 10.0; // -20%
				}
			}

			return val;
		}

		public static void SetInstrument( Mobile from, BaseInstrument item )
		{
			m_Instruments[from] = item;
		}

		public BaseInstrument( int itemID, int wellSound, int badlySound ) : base( itemID )
		{
			m_WellSound = wellSound;
			m_BadlySound = badlySound;
			m_UsesRemaining = Utility.RandomMinMax( InitMinUses, InitMaxUses );
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Crafter != null )
				list.Add( 1050043, m_Crafter.Name ); // crafted by ~1_NAME~

			if ( m_Quality == InstrumentQuality.Exceptional )
				list.Add( 1060636 ); // exceptional

			if ( m_Slayer != SlayerName.None )
				list.Add( SlayerGroup.GetEntryByName( m_Slayer ).Title );

			if ( m_Slayer2 != SlayerName.None )
				list.Add( SlayerGroup.GetEntryByName( m_Slayer2 ).Title );

			list.Add( 1060584, m_UsesRemaining.ToString() ); // uses remaining: ~1_val~
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( m_Slayer != SlayerName.None )
			{
				if ( m_Slayer == SlayerName.Silver )
					m_Slr = "silver";
				if ( m_Slayer == SlayerName.OrcSlaying )
					m_Slr = "orc slaying";
				if ( m_Slayer == SlayerName.TrollSlaughter )
					m_Slr = "troll slaughter";
				if ( m_Slayer == SlayerName.OgreTrashing )
					m_Slr = "ogre thrashing";
				if ( m_Slayer == SlayerName.Repond )
					m_Slr = "repond";
				if ( m_Slayer == SlayerName.DragonSlaying )
					m_Slr = "dragon slaying";
				if ( m_Slayer == SlayerName.Terathan )
					m_Slr = "Terathan";
				if ( m_Slayer == SlayerName.SnakesBane )
					m_Slr = "snake's bane";
				if ( m_Slayer == SlayerName.LizardmanSlaughter )
					m_Slr = "lizardman slaughter";
				if ( m_Slayer == SlayerName.ReptilianDeath )
					m_Slr = "reptilian death";
				if ( m_Slayer == SlayerName.DaemonDismissal )
					m_Slr = "daemon dismissal";
				if ( m_Slayer == SlayerName.GargoylesFoe )
					m_Slr = "gargoyle's foe";
				if ( m_Slayer == SlayerName.BalronDamnation )
					m_Slr = "balron damnation";
				if ( m_Slayer == SlayerName.Exorcism )
					m_Slr = "exorcism";
				if ( m_Slayer == SlayerName.Ophidian )
					m_Slr = "ophidian";
				if ( m_Slayer == SlayerName.SpidersDeath )
					m_Slr = "spider's death";
				if ( m_Slayer == SlayerName.ScorpionsBane )
					m_Slr = "scorpion's bane";
				if ( m_Slayer == SlayerName.ArachnidDoom )
					m_Slr = "arachnid doom";
				if ( m_Slayer == SlayerName.FlameDousing )
					m_Slr = "flame dousing";
				if ( m_Slayer == SlayerName.WaterDissipation )
					m_Slr = "water dissipation";
				if ( m_Slayer == SlayerName.Vacuum )
					m_Slr = "vacuum";
				if ( m_Slayer == SlayerName.ElementalHealth )
					m_Slr = "elemental health";
				if ( m_Slayer == SlayerName.EarthShatter )
					m_Slr = "earth shatter";
				if ( m_Slayer == SlayerName.BloodDrinking )
					m_Slr = "blood drinking";
				if ( m_Slayer == SlayerName.SummerWind )
					m_Slr = "summer wind";
				if ( m_Slayer == SlayerName.ElementalBan )
					m_Slr = "elemental ban";
				if ( m_Slayer == SlayerName.Fey )
					m_Slr = "fey slayer";
			}
			if ( m_Slayer2 != SlayerName.None )
			{
				if ( m_Slayer2 == SlayerName.Silver )
					m_Slr2 = "silver";
				if ( m_Slayer2 == SlayerName.OrcSlaying )
					m_Slr2 = "orc slaying";
				if ( m_Slayer2 == SlayerName.TrollSlaughter )
					m_Slr2 = "troll slaughter";
				if ( m_Slayer2 == SlayerName.OgreTrashing )
					m_Slr2 = "ogre thrashing";
				if ( m_Slayer2 == SlayerName.Repond )
					m_Slr2 = "repond";
				if ( m_Slayer2 == SlayerName.DragonSlaying )
					m_Slr2 = "dragon slaying";
				if ( m_Slayer2 == SlayerName.Terathan )
					m_Slr2 = "Terathan";
				if ( m_Slayer2 == SlayerName.SnakesBane )
					m_Slr2 = "snake's bane";
				if ( m_Slayer2 == SlayerName.LizardmanSlaughter )
					m_Slr2 = "lizardman slaughter";
				if ( m_Slayer2 == SlayerName.ReptilianDeath )
					m_Slr2 = "reptilian death";
				if ( m_Slayer2 == SlayerName.DaemonDismissal )
					m_Slr2 = "daemon dismissal";
				if ( m_Slayer2 == SlayerName.GargoylesFoe )
					m_Slr2 = "gargoyle's foe";
				if ( m_Slayer2 == SlayerName.BalronDamnation )
					m_Slr2 = "balron damnation";
				if ( m_Slayer2 == SlayerName.Exorcism )
					m_Slr2 = "exorcism";
				if ( m_Slayer2 == SlayerName.Ophidian )
					m_Slr2 = "ophidian";
				if ( m_Slayer2 == SlayerName.SpidersDeath )
					m_Slr2 = "spider's death";
				if ( m_Slayer2 == SlayerName.ScorpionsBane )
					m_Slr2 = "scorpion's bane";
				if ( m_Slayer2 == SlayerName.ArachnidDoom )
					m_Slr2 = "arachnid doom";
				if ( m_Slayer2 == SlayerName.FlameDousing )
					m_Slr2 = "flame dousing";
				if ( m_Slayer2 == SlayerName.WaterDissipation )
					m_Slr2 = "water dissipation";
				if ( m_Slayer2 == SlayerName.Vacuum )
					m_Slr2 = "vacuum";
				if ( m_Slayer2 == SlayerName.ElementalHealth )
					m_Slr2 = "elemental health";
				if ( m_Slayer2 == SlayerName.EarthShatter )
					m_Slr2 = "earth shatter";
				if ( m_Slayer2 == SlayerName.BloodDrinking )
					m_Slr2 = "blood drinking";
				if ( m_Slayer2 == SlayerName.SummerWind )
					m_Slr2 = "summer wind";
				if ( m_Slayer2 == SlayerName.ElementalBan )
					m_Slr2 = "elemental ban";
				if ( m_Slayer2 == SlayerName.Fey )
					m_Slr2 = "fey slayer";
			}
			if ( m_Slayer != SlayerName.None || m_Slayer2 != SlayerName.None )
			{
				if ( m_Slayer != SlayerName.None && m_Slayer2 != SlayerName.None )
				{
					LabelTo( from, "({0}/{1})", m_Slr, m_Slr2 );
				}
				else if ( m_Slayer != SlayerName.None && m_Slayer2 == SlayerName.None )
				{
					LabelTo( from, "({0})", m_Slr );
				}
				else if ( m_Slayer == SlayerName.None && m_Slayer2 != SlayerName.None )
				{
					LabelTo( from, "({0})", m_Slr2 );
				}
			}
		}

		public BaseInstrument( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			writer.Write( m_Crafter );

			writer.WriteEncodedInt( (int) m_Quality );
			writer.WriteEncodedInt( (int) m_Slayer );
			writer.WriteEncodedInt( (int) m_Slayer2 );

			writer.WriteEncodedInt( (int) m_UsesRemaining );

			writer.WriteEncodedInt( (int) m_WellSound );
			writer.WriteEncodedInt( (int) m_BadlySound );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				{
					m_Crafter = reader.ReadMobile();

					m_Quality = (InstrumentQuality)reader.ReadEncodedInt();
					m_Slayer = (SlayerName)reader.ReadEncodedInt();
					m_Slayer2 = (SlayerName)reader.ReadEncodedInt();

					m_UsesRemaining = reader.ReadEncodedInt();

					m_WellSound = reader.ReadEncodedInt();
					m_BadlySound = reader.ReadEncodedInt();
					
					break;
				}
				case 1:
				{
					m_Crafter = reader.ReadMobile();

					m_Quality = (InstrumentQuality)reader.ReadEncodedInt();
					m_Slayer = (SlayerName)reader.ReadEncodedInt();

					m_UsesRemaining = reader.ReadEncodedInt();

					m_WellSound = reader.ReadEncodedInt();
					m_BadlySound = reader.ReadEncodedInt();

					break;
				}
				case 0:
				{
					m_WellSound = reader.ReadInt();
					m_BadlySound = reader.ReadInt();
					m_UsesRemaining = Utility.RandomMinMax( InitMinUses, InitMaxUses );

					break;
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 1 ) )
			{
				from.SendAsciiMessage( "That is too far away." ); // 
			}
			else if ( from.BeginAction( typeof( BaseInstrument ) ) )
			{
				SetInstrument( from, this );

				// Delay of 7 second before beign able to play another instrument again
				new InternalTimer( from ).Start();

				if ( CheckMusicianship( from ) )
					PlayInstrumentWell( from );
				else
					PlayInstrumentBadly( from );
			}
			else
			{
				from.SendAsciiMessage( "You must wait to perform another action" ); // 
			}
		}

		public static bool CheckMusicianship( Mobile m )
		{
			m.CheckSkill( SkillName.Musicianship, 0.0, 120.0 );

			return ( (m.Skills[SkillName.Musicianship].Value / 100) > Utility.RandomDouble() );
		}

		public void PlayInstrumentWell( Mobile from )
		{
			from.PlaySound( m_WellSound );
		}

		public void PlayInstrumentBadly( Mobile from )
		{
			from.PlaySound( m_BadlySound );
		}

		private class InternalTimer : Timer
		{
			private Mobile m_From;

			public InternalTimer( Mobile from ) : base( TimeSpan.FromSeconds( 6.0 ) )
			{
				m_From = from;
				Priority = TimerPriority.TwoFiftyMS;
			}

			protected override void OnTick()
			{
				m_From.EndAction( typeof( BaseInstrument ) );
			}
		}
		#region ICraftable Members

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			Quality = (InstrumentQuality)quality;

			if ( makersMark )
				Crafter = from;

			return quality;
		}

		#endregion
	}
}
