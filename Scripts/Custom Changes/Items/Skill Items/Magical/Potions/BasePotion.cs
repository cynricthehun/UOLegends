using System;
using System.Collections;
using Server;
using Server.Engines.Craft;

namespace Server.Items
{
	public enum PotionEffect
	{
		Nightsight,
		CureLesser,
		Cure,
		CureGreater,
		Agility,
		AgilityGreater,
		Strength,
		StrengthGreater,
		PoisonLesser,
		Poison,
		PoisonGreater,
		PoisonDeadly,
		Refresh,
		RefreshTotal,
		HealLesser,
		Heal,
		HealGreater,
		ExplosionLesser,
		Explosion,
		ExplosionGreater
	}

	public abstract class BasePotion : Item, ICraftable
	{
		private PotionEffect m_PotionEffect;
		private ArrayList m_Tasters;

		public PotionEffect PotionEffect
		{
			get
			{
				return m_PotionEffect;
			}
			set
			{
				m_PotionEffect = value;
				InvalidateProperties();
			}
		}

		public override int LabelNumber{ get{ return 1041314 + (int)m_PotionEffect; } }

		public BasePotion( int itemID, PotionEffect effect ) : base( itemID )
		{
			m_PotionEffect = effect;
			m_Tasters = new ArrayList();

			Stackable = false;
			Weight = 1.0;
		}

		public BasePotion( Serial serial ) : base( serial )
		{
		}

		public virtual bool RequireFreeHand{ get{ return true; } }

		public static bool HasFreeHand( Mobile m )
		{
			Item handOne = m.FindItemOnLayer( Layer.OneHanded );
			Item handTwo = m.FindItemOnLayer( Layer.TwoHanded );

			if ( handTwo is BaseWeapon )
				handOne = handTwo;

			return ( handOne == null || handTwo == null );
		}

		public bool CheckTasters( Mobile from )
		{
			if ( m_Tasters == null )
			{
				return false;
			}
			else if ( m_Tasters.Contains( from ) )
			{
				return true;
			}
			return false;
		}

		public virtual void AddTasters( Mobile from )
		{
			if ( m_Tasters == null )
			{
				m_Tasters = new ArrayList();
			}

			m_Tasters.Add( from );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( m_Tasters != null && m_Tasters.Contains( from ) )
				{
					switch ( m_PotionEffect )
					{
						default:
						case PotionEffect.Nightsight:		LabelTo( from, "a nightsight potion" );		break;

						case PotionEffect.CureLesser:		LabelTo( from, "a lesser cure potion" );	break;
						case PotionEffect.Cure:			LabelTo( from, "a cure potion" );		break;
						case PotionEffect.CureGreater:		LabelTo( from, "a greater cure potion" );	break;

						case PotionEffect.Agility:		LabelTo( from, "an agility potion" );		break;
						case PotionEffect.AgilityGreater:	LabelTo( from, "a greater agility potion" );	break;

						case PotionEffect.Strength:		LabelTo( from, "a strength potion" );		break;
						case PotionEffect.StrengthGreater:	LabelTo( from, "a greater strength potion" );	break;

						case PotionEffect.PoisonLesser:		LabelTo( from, "a lesser poison potion" );	break;
						case PotionEffect.Poison:		LabelTo( from, "a poison potion" );		break;
						case PotionEffect.PoisonGreater:	LabelTo( from, "a greater poison potion" );	break;
						case PotionEffect.PoisonDeadly:		LabelTo( from, "a deadly poison potion" );	break;

						case PotionEffect.Refresh:		LabelTo( from, "a refresh potion" );		break;
						case PotionEffect.RefreshTotal:		LabelTo( from, "a total refresh potion" );	break;

						case PotionEffect.HealLesser:		LabelTo( from, "a lesser heal potion" );	break;
						case PotionEffect.Heal:			LabelTo( from, "a heal potion" );		break;
						case PotionEffect.HealGreater:		LabelTo( from, "a greater heal potion" );	break;

						case PotionEffect.ExplosionLesser:	LabelTo( from, "a lesser explosion potion" );	break;
						case PotionEffect.Explosion:		LabelTo( from, "an explosion potion" );		break;
						case PotionEffect.ExplosionGreater:	LabelTo( from, "a greater explosion potion" );	break;
					}
				}
				else
				{
					switch ( m_PotionEffect )
					{
						default:
						case PotionEffect.Nightsight:		LabelTo( from, "a black potion" );		break;

						case PotionEffect.CureLesser:		LabelTo( from, "an orange potion" );		break;
						case PotionEffect.Cure:			LabelTo( from, "an orange potion" );		break;
						case PotionEffect.CureGreater:		LabelTo( from, "an orange potion" );		break;

						case PotionEffect.Agility:		LabelTo( from, "a blue potion" );		break;
						case PotionEffect.AgilityGreater:	LabelTo( from, "a blue agility potion" );	break;

						case PotionEffect.Strength:		LabelTo( from, "a white potion" );		break;
						case PotionEffect.StrengthGreater:	LabelTo( from, "a white potion" );		break;

						case PotionEffect.PoisonLesser:		LabelTo( from, "a green potion" );		break;
						case PotionEffect.Poison:		LabelTo( from, "a green potion" );		break;
						case PotionEffect.PoisonGreater:	LabelTo( from, "a green potion" );		break;
						case PotionEffect.PoisonDeadly:		LabelTo( from, "a green potion" );		break;

						case PotionEffect.Refresh:		LabelTo( from, "a red potion" );		break;
						case PotionEffect.RefreshTotal:		LabelTo( from, "a red potion" );		break;

						case PotionEffect.HealLesser:		LabelTo( from, "a yellow potion" );		break;
						case PotionEffect.Heal:			LabelTo( from, "a yellow potion" );		break;
						case PotionEffect.HealGreater:		LabelTo( from, "a yellow potion" );		break;

						case PotionEffect.ExplosionLesser:	LabelTo( from, "a purple potion" );		break;
						case PotionEffect.Explosion:		LabelTo( from, "a purple potion" );		break;
						case PotionEffect.ExplosionGreater:	LabelTo( from, "a purple potion" );		break;
					}
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				if ( !RequireFreeHand || HasFreeHand( from ) )
					Drink( from );
				else
					from.SendAsciiMessage( "You must have a free hand to drink a potion." );
			}
			else
			{
				from.SendAsciiMessage( "That is too far away for you to use." );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (bool) ( m_Tasters != null && m_Tasters.Count > 0 ) );
			if ( m_Tasters != null && m_Tasters.Count > 0 )
				writer.WriteMobileList( m_Tasters, true );

			writer.Write( (int) m_PotionEffect );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_PotionEffect = (PotionEffect)reader.ReadInt();
					break;
				}
				case 1:
				{
					if( reader.ReadBool() )
						m_Tasters = reader.ReadMobileList();
					goto case 0;
				}
			}
		}

		public abstract void Drink( Mobile from );

		public static void PlayDrinkEffect( Mobile m )
		{
			m.RevealingAction();

			m.PlaySound( 0x2D6 );
			m.AddToBackpack( new Bottle() );

			if ( m.Body.IsHuman /*&& !m.Mounted*/ )
				m.Animate( 34, 5, 1, true, false, 0 );
		}

		public static TimeSpan Scale( Mobile m, TimeSpan v )
		{
			if ( !Core.AOS )
				return v;

			double scalar = 1.0 + (0.01 * AosAttributes.GetValue( m, AosAttribute.EnhancePotions ));

			return TimeSpan.FromSeconds( v.TotalSeconds * scalar );
		}

		public static double Scale( Mobile m, double v )
		{
			if ( !Core.AOS )
				return v;

			double scalar = 1.0 + (0.01 * AosAttributes.GetValue( m, AosAttribute.EnhancePotions ));

			return v * scalar;
		}

		public static int Scale( Mobile m, int v )
		{
			if ( !Core.AOS )
				return v;

			return AOS.Scale( v, 100 + AosAttributes.GetValue( m, AosAttribute.EnhancePotions ) );
		}
		#region ICraftable Members

		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			if ( craftSystem is DefAlchemy )
			{
				Container pack = from.Backpack;

				if ( pack != null )
				{
					Item[] kegs = pack.FindItemsByType( typeof( PotionKeg ), true );

					for ( int i = 0; i < kegs.Length; ++i )
					{
						PotionKeg keg = kegs[i] as PotionKeg;

						if ( keg == null )
							continue;

						if ( keg.Held <= 0 || keg.Held >= 100 )
							continue;

						if ( keg.Type != PotionEffect )
							continue;

						++keg.Held;

						Delete();
						from.AddToBackpack( new Bottle() );

						return -1; // signal placed in keg
					}
				}
			}

			m_Tasters.Add( from );

			return 1;
		}

		#endregion
	}
}