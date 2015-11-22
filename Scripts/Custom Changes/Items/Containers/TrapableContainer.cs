using System;

namespace Server.Items
{
	public enum TrapType
	{
		None,
		MagicTrap,
		ExplosionTrap,
		DartTrap,
		PoisonTrap
	}

	public abstract class TrapableContainer : BaseContainer, ITelekinesisable
	{
		private TrapType m_TrapType;
		private int m_TrapPower;
		private bool m_PlayerTrapped;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool PlayerTrapped
		{
			get
			{
				return m_PlayerTrapped;
			}
			set
			{
				m_PlayerTrapped = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TrapType TrapType
		{
			get
			{
				return m_TrapType;
			}
			set
			{
				m_TrapType = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int TrapPower
		{
			get
			{
				return m_TrapPower;
			}
			set
			{
				m_TrapPower = value;
			}
		}

		public TrapableContainer( int itemID ) : base( itemID )
		{
		}

		public TrapableContainer( Serial serial ) : base( serial )
		{
		}

		public virtual bool ExecuteTrap( Mobile from )
		{
			if ( m_TrapType != TrapType.None )
			{
				switch ( m_TrapType )
				{
					case TrapType.ExplosionTrap:
					{
						from.SendAsciiMessage( "You set off a trap!" );

						if ( from.InRange( GetWorldLocation(), 2 ) )
						{
							AOS.Damage( from, m_TrapPower, 0, 100, 0, 0, 0 );
							from.SendAsciiMessage( "Your skin blisters from the heat!" );
						}

						Point3D loc = GetWorldLocation();

						Effects.PlaySound( loc, Map, 0x307 );
						Effects.SendLocationEffect( new Point3D( loc.X + 1, loc.Y + 1, loc.Z - 11 ), Map, 0x36BD, 15 );

						break;
					}
					case TrapType.MagicTrap:
					{
						if ( from.InRange( GetWorldLocation(), 1 ) )
							from.Damage( m_TrapPower );
							//AOS.Damage( from, m_TrapPower, 0, 100, 0, 0, 0 );

						Point3D loc = GetWorldLocation();

						Effects.PlaySound( loc, Map, 0x307 );

						Effects.SendLocationEffect( new Point3D( loc.X - 1, loc.Y, loc.Z ), Map, 0x36BD, 15 );
						Effects.SendLocationEffect( new Point3D( loc.X + 1, loc.Y, loc.Z ), Map, 0x36BD, 15 );

						Effects.SendLocationEffect( new Point3D( loc.X, loc.Y - 1, loc.Z ), Map, 0x36BD, 15 );
						Effects.SendLocationEffect( new Point3D( loc.X, loc.Y + 1, loc.Z ), Map, 0x36BD, 15 );

						Effects.SendLocationEffect( new Point3D( loc.X + 1, loc.Y + 1, loc.Z + 11 ), Map, 0x36BD, 15 );

						break;
					}
					case TrapType.DartTrap:
					{
						AOS.Damage( from, m_TrapPower, 100, 0, 0, 0, 0 );

						Effects.SendMovingEffect( this, from, 0x1BFE, 7, 0, false, false );
						Effects.PlaySound( from.Location, from.Map, 0x234 );
						break;
					}
					case TrapType.PoisonTrap:
					{
						if ( m_TrapPower < 30 )
						{
							from.Damage( m_TrapPower );
							from.Poison = Poison.Lesser;
						}
						if ( m_TrapPower >= 30 && m_TrapPower < 50 )
						{
							from.Damage( m_TrapPower );
							from.Poison = Poison.Regular;
						}
						if ( m_TrapPower >= 50 && m_TrapPower < 70 )
						{
							from.Damage( m_TrapPower );
							from.Poison = Poison.Greater;
						}
						if ( m_TrapPower >= 70 && m_TrapPower < 90 )
						{
							from.Damage( m_TrapPower / 2 );
							from.Poison = Poison.Deadly;
						}
						if ( m_TrapPower >= 90 )
						{
							from.Damage( m_TrapPower );
							from.Poison = Poison.Lethal;
						}

						Point3D loc = GetWorldLocation();
						Effects.PlaySound( loc, Map, 0x1DE );
						Effects.SendLocationEffect( new Point3D( from.Location.X + 1, from.Location.Y + 1, from.Location.Z + 15 ), from.Map, 0x11A6, 48 );
						break;
					}
				}

				if ( m_PlayerTrapped == false )
				{
					m_TrapType = TrapType.None;
				}
				return true;
			}

			return false;
		}

		public virtual void OnTelekinesis( Mobile from )
		{
			Effects.SendLocationParticles( EffectItem.Create( Location, Map, EffectItem.DefaultDuration ), 0x376A, 9, 32, 5022 );
			Effects.PlaySound( Location, Map, 0x1F5 );

			if ( !ExecuteTrap( from ) )
				base.DisplayTo( from );
		}

		public override void Open( Mobile from )
		{
			if ( !ExecuteTrap( from ) )
				base.Open( from );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			writer.Write( (bool) m_PlayerTrapped );
			writer.Write( (int) m_TrapPower );
			writer.Write( (int) m_TrapType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				{
					m_PlayerTrapped = reader.ReadBool();
					goto case 1;
				}
				case 1:
				{
					m_TrapPower = reader.ReadInt();

					goto case 0;
				}

				case 0:
				{
					m_TrapType = (TrapType)reader.ReadInt();

					break;
				}
			}
		}
	}
}
