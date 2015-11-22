using System;
using System.Collections;
using Server;
using Server.Spells;
using Server.Items;
using Server.Mobiles;
using Server.Gumps;
using Server.Targeting;
using Server.Network;

namespace Server.Dueling
{
	public class DuelRegion : Region
	{
		private bool m_Para;
		private bool m_Pots;
		private bool m_Reflect;
		private bool m_Fields;
		private bool m_Stun;
		private bool m_Disarm;

		private Mobile m_From;
		private Mobile m_To;
		private IMount m_FromMount;
		private IMount m_ToMount;

		private ArrayList m_Coords;

		public virtual Point3D FromStart{ get{ return new Point3D( 0, 0, 0 ); } }
		public virtual Point3D ToStart{ get{ return new Point3D( 0, 0, 0 ); } }
		public virtual Point3D FromEnd{ get{ return new Point3D( 0, 0, 0 ); } }
		public virtual Point3D ToEnd{ get{ return new Point3D( 0, 0, 0 ); } }
		public virtual Point3D BootLocation{ get{ return new Point3D( 0, 0, 0 ); } }

		public virtual Point3D WallArea{ get{ return new Point3D( 0, 0, 0 ); } }
		public virtual bool NorthSouth{ get{ return false; } }

		public virtual Rectangle2D RegionArea{ get{ return new Rectangle2D( 0, 0, 0, 0 ); } }

		public DuelRegion( string name, bool para, bool pots, bool reflect, bool fields, bool stun, bool disarm, Mobile from, Mobile to ) : base( "", name, Map.Felucca )
		{
			m_Para = para;
			m_Pots = pots;
			m_Reflect = reflect;
			m_Fields = fields;
			m_Stun = stun;
			m_Disarm = disarm;

			m_From = from;
			m_To = to;

			m_Coords = new ArrayList();

			m_Coords.Add( RegionArea );
			this.Coords = m_Coords;

			DoDuel( from, to );
		}

		public virtual void DoDuel( Mobile from, Mobile to )
		{
			NetState fromns = from.NetState;
			NetState tons = to.NetState;

			m_FromMount = from.Mount;

			if ( m_FromMount != null )
				m_FromMount.Rider = null;

			m_From.MoveToWorld( FromStart, Map.Felucca );
			from.Direction = Direction.South;
			from.MagicDamageAbsorb = 0;
			from.MeleeDamageAbsorb = 0;
			from.StunReady = false;
			from.DisarmReady = false;
			from.Mana = from.ManaMax;
			from.Stam = from.StamMax;
			//from.Target.Cancel( from );
			if ( fromns != null )
				fromns.Send( CancelTarget.Instance );

			Target fromtarg = from.Target;

			//if ( fromtarg != null )
			//	fromtarg.OnTargetCancel( from, TargetCancelType.Canceled );

			from.Frozen = true;

			m_ToMount = to.Mount;

			if ( m_ToMount != null )
				m_ToMount.Rider = null;

			m_To.MoveToWorld( ToStart, Map.Felucca );
			to.Direction = Direction.North;
			to.MagicDamageAbsorb = 0;
			to.MeleeDamageAbsorb = 0;
			to.StunReady = false;
			to.DisarmReady = false;
			to.Mana = to.ManaMax;
			to.Stam = to.StamMax;
			//to.Target.Cancel( to );
			if ( tons != null )
				tons.Send( CancelTarget.Instance );

			Target totarg = to.Target;

			//if ( totarg != null )
			//	totarg.OnTargetCancel( to, TargetCancelType.Canceled );

			to.Frozen = true;

			((PlayerMobile)m_From).PermaFlags.Add( (PlayerMobile)to );
			((PlayerMobile)m_To).PermaFlags.Add( (PlayerMobile)from );
			((PlayerMobile)m_From).Delta( MobileDelta.Noto );
			((PlayerMobile)m_To).Delta( MobileDelta.Noto );

			DuelWall wall = new DuelWall( NorthSouth, from, to );
			wall.MoveToWorld( WallArea, Map.Felucca );

			m_From.SendAsciiMessage( "Duel Started" );
			m_To.SendAsciiMessage( "Duel Started" );
		}

		public override bool OnDeath( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				if ( m == m_From || m == m_To )
				{
					if( m == m_From )
					{
						((PlayerMobile)m_From).DuelLosses++;
						((PlayerMobile)m_To).DuelWins++;
						m_From.SendGump( new AfterGump( m_From, false ) );
						m_To.SendGump( new AfterGump( m_To, true ) );
					}
					else
					{
						((PlayerMobile)m_To).DuelLosses++;
						((PlayerMobile)m_From).DuelWins++;
						m_From.SendGump( new AfterGump( m_From, true ) );
						m_To.SendGump( new AfterGump( m_To, false ) );
					}

					m_From.RemoveAggressed( m_To );
					m_To.RemoveAggressed( m_From );
					m_From.RemoveAggressor( m_To );
					m_To.RemoveAggressor( m_From );
					((PlayerMobile)m_From).PermaFlags.Remove( m_To );
					((PlayerMobile)m_To).PermaFlags.Remove( m_From );
					((PlayerMobile)m_From).Delta( MobileDelta.Noto );
					((PlayerMobile)m_To).Delta( MobileDelta.Noto );

					if ( m_FromMount != null && m_FromMount is Mobile )
					{
						m_FromMount.Rider = m_From;
					}

					m_From.MoveToWorld( FromEnd, Map.Felucca );

					m_From.Stam = m_From.StamMax;
					m_From.Mana = m_From.ManaMax;
					m_From.Hits = m_From.HitsMax;
					m_From.Poison = null;
					((PlayerMobile)m_From).SentRequest = false;

					if ( m_ToMount != null && m_ToMount is Mobile )
					{
						m_ToMount.Rider = m_To;
					}

					m_To.MoveToWorld( ToEnd, Map.Felucca );

					m_To.Stam = m_To.StamMax;
					m_To.Mana = m_To.ManaMax;
					m_To.Hits = m_To.HitsMax;
					m_To.Poison = null;
					((PlayerMobile)m_To).RecievedRequest = false;

					Region.RemoveRegion( this );

					return false;
				}
				return true;
			}

			return true;
		}

		public override bool OnBeginSpellCast( Mobile m, ISpell s )
		{
			if ( ((Spell)s).Info.Name == "Ethereal Mount" )
			{
				m.SendMessage( "You cannot mount your ethereal during a duel." );
				return false; 
			}
			if ( s is Spells.Fourth.RecallSpell || s is Spells.Seventh.GateTravelSpell )
			{
				m.SendAsciiMessage( 0x22, "Wouldst thou flee during the heat of battle?" );
				return false;
			}
			if ( s is Spells.Sixth.MarkSpell )
			{
				m.SendMessage( " Thy spell doth not seem to work here." );
				return false;
			}
			if ( s is Spells.Eighth.AirElementalSpell || s is Spells.Eighth.EarthElementalSpell || s is Spells.Eighth.FireElementalSpell || s is Spells.Eighth.WaterElementalSpell || s is Spells.Fifth.BladeSpiritsSpell || s is Spells.Eighth.EnergyVortexSpell || s is Spells.Fifth.SummonCreatureSpell )
			{
				m.SendAsciiMessage( "That spell has been disabled for this duel." );
				return false;
			}
			if( !m_Para )
			{
				if ( s is Spells.Fifth.ParalyzeSpell )
				{
					m.SendAsciiMessage( "That spell has been disabled for this duel." );
					return false;
				}
			}
			if( !m_Reflect )
			{
				if ( s is Spells.Fifth.MagicReflectSpell )
				{
					m.SendAsciiMessage( "That spell has been disabled for this duel." );
					return false;
				}
			}
			if( !m_Fields )
			{
				if ( s is Spells.Fifth.PoisonFieldSpell || s is Spells.Fourth.FireFieldSpell || s is Spells.Seventh.EnergyFieldSpell || s is Spells.Sixth.ParalyzeFieldSpell )
				{
					m.SendAsciiMessage( "That spell has been disabled for this duel." );
					return false;
				}
			}
			return base.OnBeginSpellCast( m, s );
		}

		public override bool OnDoubleClick( Mobile m, object o )
		{
			if( o is BasePotion && !m_Pots )
			{
				m.SendMessage( "Potions have been disabled for this duel." );
				return false;
			}

			return base.OnDoubleClick( m, o );
		}

		public override void OnEnter( Mobile m )
		{
			if ( m != m_From && m != m_To && m.AccessLevel != AccessLevel.Player )
				m.MoveToWorld( BootLocation, Map.Felucca );
		}

		public virtual bool Stun()
		{
			return m_Stun;
		}

		public virtual bool Disarm()
		{
			return m_Disarm;
		}

		public override bool AllowHousing( Mobile from, Point3D p )
		{
			return false;
		}
	}
}