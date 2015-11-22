using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PotionKeg : Item
	{
		private PotionEffect m_Type;
		private int m_Held;
		private ArrayList m_Tasters;
		private Mobile m_Owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get
			{
				return m_Owner;
			}
			set
			{
				if ( m_Owner == value )
					return;

				m_Owner = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Held
		{
			get
			{
				return m_Held;
			}
			set
			{
				if ( m_Held != value )
				{
					this.Weight += (value - m_Held) * 0.8;

					m_Held = value;
					InvalidateProperties();
				}
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public PotionEffect Type
		{
			get
			{
				return m_Type;
			}
			set
			{
				m_Type = value;
				InvalidateProperties();
			}
		}

		[Constructable]
		public PotionKeg() : base( 0x1940 )
		{
			m_Tasters = new ArrayList();
			this.Weight = 1.0;
		}

		public PotionKeg( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			writer.Write( (Mobile) m_Owner );

			writer.Write( (bool) ( m_Tasters != null && m_Tasters.Count > 0 ) );
			if ( m_Tasters != null && m_Tasters.Count > 0 )
				writer.WriteMobileList( m_Tasters, true );

			writer.Write( (int) m_Type );
			writer.Write( (int) m_Held );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Type = (PotionEffect)reader.ReadInt();
					m_Held = reader.ReadInt();
					break;
				}
				case 1:
				{
					if ( reader.ReadBool() )
						m_Tasters = reader.ReadMobileList();
					goto case 0;
				}
				case 2:
				{
					m_Owner = reader.ReadMobile();
					goto case 1;
				}
			}
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

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			int number;

			if ( m_Held <= 0 )
				number = 502246; // The keg is empty.
			else if ( m_Held < 5 )
				number = 502248; // The keg is nearly empty.
			else if ( m_Held < 20 )
				number = 502249; // The keg is not very full.
			else if ( m_Held < 30 )
				number = 502250; // The keg is about one quarter full.
			else if ( m_Held < 40 )
				number = 502251; // The keg is about one third full.
			else if ( m_Held < 47 )
				number = 502252; // The keg is almost half full.
			else if ( m_Held < 54 )
				number = 502254; // The keg is approximately half full.
			else if ( m_Held < 70 )
				number = 502253; // The keg is more than half full.
			else if ( m_Held < 80 )
				number = 502255; // The keg is about three quarters full.
			else if ( m_Held < 96 )
				number = 502256; // The keg is very full.
			else if ( m_Held < 100 )
				number = 502257; // The liquid is almost to the top of the keg.
			else
				number = 502258; // The keg is completely full.

			list.Add( number );
		}

		public override void OnSingleClick( Mobile from )
		{
			string type = "";
			string color = "";

			switch ( m_Type )
			{
				default:
				case PotionEffect.Nightsight:{ type = "nightsight"; color = "black"; break;}

				case PotionEffect.CureLesser:{ type = "lesser cure"; color = "orange"; break;}
				case PotionEffect.Cure:{ type = "cure"; color = "orange"; break;}
				case PotionEffect.CureGreater:{ type = "greater cure"; color = "orange"; break;}

				case PotionEffect.Agility:{ type = "agility"; color = "blue"; break;}
				case PotionEffect.AgilityGreater:{ type = "greater agility"; color = "blue"; break;}

				case PotionEffect.Strength:{ type = "strength"; color = "white"; break;}
				case PotionEffect.StrengthGreater:{ type = "greater strength"; color = "white"; break;}

				case PotionEffect.PoisonLesser:{ type = "lesser poison"; color = "green"; break;}
				case PotionEffect.Poison:{ type = "poison"; color = "green"; break;}
				case PotionEffect.PoisonGreater:{ type = "greater poison"; color = "green"; break;}
				case PotionEffect.PoisonDeadly:{ type = "deadly poison"; color = "green"; break;}

				case PotionEffect.Refresh:{ type = "refresh"; color = "red"; break;}
				case PotionEffect.RefreshTotal:{ type = "total refresh"; color = "red"; break;}

				case PotionEffect.HealLesser:{ type = "lesser heal"; color = "yellow"; break;}
				case PotionEffect.Heal:{ type = "heal"; color = "yellow"; break;}
				case PotionEffect.HealGreater:{ type = "greater heal"; color = "yellow"; break;}

				case PotionEffect.ExplosionLesser:{ type = "lesser explosion"; color = "purple"; break;}
				case PotionEffect.Explosion:{ type = "explosion"; color = "purple"; break;}
				case PotionEffect.ExplosionGreater:{ type = "greater explosion"; color = "purple"; break;}
			}
				
			if ( this.Name == null )
			{
				if ( m_Held <= 0 )
				{
					LabelTo( from, "a specially lined keg for holding potions" );
				}
				else if ( ( this.m_Tasters != null && this.m_Tasters.Contains( from ) ) || from == m_Owner )
				{
					LabelTo( from, "a keg of " + type + " potions" );
				}
				else
				{
					LabelTo( from, "a keg of " + color + " liquid" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}

			if ( m_Held <= 0 )
				LabelTo( from, "the keg is empty." );
			else if ( m_Held < 5 )
				LabelTo( from, "the keg is nearly empty." );
			else if ( m_Held < 20 )
				LabelTo( from, "the keg is not very full." );
			else if ( m_Held < 30 )
				LabelTo( from, "the keg is about one quarter full." );
			else if ( m_Held < 40 )
				LabelTo( from, "the keg is about one third full." );
			else if ( m_Held < 47 )
				LabelTo( from, "the keg is almost half full." );
			else if ( m_Held < 54 )
				LabelTo( from, "the keg is approximately half full." );
			else if ( m_Held < 70 )
				LabelTo( from, "the keg is more than half full." );
			else if ( m_Held < 80 )
				LabelTo( from, "the keg is about three quarters full." );
			else if ( m_Held < 96 )
				LabelTo( from, "the keg is very full." );
			else if ( m_Held < 100 )
				LabelTo( from, "the liquid is almost to the top of the keg." );
			else
				LabelTo( from, "the keg is completely full." );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), 2 ) )
			{
				if ( m_Held > 0 )
				{
					Container pack = from.Backpack;

					if ( pack != null && pack.ConsumeTotal( typeof( Bottle ), 1 ) )
					{
						from.SendAsciiMessage( "You pour some of the keg's contents into an empty bottle..." );

						BasePotion pot = FillBottle();
						if ( ( m_Tasters != null && m_Tasters.Contains( from ) ) || from == m_Owner )
						{
							pot.AddTasters( from );
						}

						if ( pack.TryDropItem( from, pot, false ) )
						{
							from.SendAsciiMessage( "...and place it into your backpack." );
							from.PlaySound( 0x240 );

							if ( --Held == 0 )
								from.SendAsciiMessage( "The keg is now empty." );
						}
						else
						{
							from.SendAsciiMessage( "...but there is no room for the bottle in your backpack." );
							pot.Delete();
						}
					}
					else
					{
						// TODO: Target a bottle
					}
				}
				else
				{
					from.SendAsciiMessage( "The keg is empty." );
				}
			}
			else
			{
				from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
			}
		}

		public override bool OnDragDrop( Mobile from, Item item )
		{
			if ( item is BasePotion )
			{
				if ( this.IsChildOf( from.Backpack ) )
				{
					m_Owner = from;
				}

				BasePotion pot = (BasePotion)item;

				if ( m_Held == 0 )
				{
					if ( GiveBottle( from ) )
					{
						m_Type = pot.PotionEffect;
						Held = 1;

						from.PlaySound( 0x240 );

						from.SendAsciiMessage( "You place the empty bottle in your backpack." );

						item.Delete();
						return true;
					}
					else
					{
						from.SendAsciiMessage( "You don't have room for the empty bottle in your backpack." );
						return false;
					}
				}
				else if ( pot.PotionEffect != m_Type )
				{
					from.SendAsciiMessage( "You decide that it would be a bad idea to mix different types of potions." );
					return false;
				}
				else if ( m_Held >= 100 )
				{
					from.SendAsciiMessage( "The keg will not hold any more!" );
					return false;
				}
				else
				{
					if ( GiveBottle( from ) )
					{
						++Held;
						item.Delete();

						from.PlaySound( 0x240 );

						from.SendAsciiMessage( "You place the empty bottle in your backpack." );

						item.Delete();
						return true;
					}
					else
					{
						from.SendAsciiMessage( "You don't have room for the empty bottle in your backpack." );
						return false;
					}
				}
			}
			else
			{
				from.SendAsciiMessage( "The keg is not designed to hold that type of object." );
				return false;
			}
		}

		public bool GiveBottle( Mobile m )
		{
			Container pack = m.Backpack;

			Bottle bottle = new Bottle();

			if ( pack == null || !pack.TryDropItem( m, bottle, false ) )
			{
				bottle.Delete();
				return false;
			}

			return true;
		}

		public BasePotion FillBottle()
		{
			switch ( m_Type )
			{
				default:
				case PotionEffect.Nightsight:		return new NightSightPotion();

				case PotionEffect.CureLesser:		return new LesserCurePotion();
				case PotionEffect.Cure:				return new CurePotion();
				case PotionEffect.CureGreater:		return new GreaterCurePotion();

				case PotionEffect.Agility:			return new AgilityPotion();
				case PotionEffect.AgilityGreater:	return new GreaterAgilityPotion();

				case PotionEffect.Strength:			return new StrengthPotion();
				case PotionEffect.StrengthGreater:	return new GreaterStrengthPotion();

				case PotionEffect.PoisonLesser:		return new LesserPoisonPotion();
				case PotionEffect.Poison:			return new PoisonPotion();
				case PotionEffect.PoisonGreater:	return new GreaterPoisonPotion();
				case PotionEffect.PoisonDeadly:		return new DeadlyPoisonPotion();

				case PotionEffect.Refresh:			return new RefreshPotion();
				case PotionEffect.RefreshTotal:		return new TotalRefreshPotion();

				case PotionEffect.HealLesser:		return new LesserHealPotion();
				case PotionEffect.Heal:				return new HealPotion();
				case PotionEffect.HealGreater:		return new GreaterHealPotion();

				case PotionEffect.ExplosionLesser:	return new LesserExplosionPotion();
				case PotionEffect.Explosion:		return new ExplosionPotion();
				case PotionEffect.ExplosionGreater:	return new GreaterExplosionPotion();
			}
		}

		public static void Initialize()
		{
			TileData.ItemTable[0x1940].Height = 4;
		}
	}
}
