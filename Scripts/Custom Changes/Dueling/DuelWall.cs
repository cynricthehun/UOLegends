using System;
using Server;
using Server.Misc;

namespace Server.Dueling
{
	public class DuelWall : Item
	{
		private Timer m_Timer;
		private DateTime m_End;
		private InternalItem m_Item1;
		private InternalItem m_Item2;
		private int m_XOffset1;
		private int m_YOffset1;
		private int m_XOffset2;
		private int m_YOffset2;
		private Mobile m_From;
		private Mobile m_To;

		public DuelWall( bool northsouth, Mobile from, Mobile to ) : base( 0x80 )
		{
			m_From = from;
			m_To = to;
			Movable = false;

			if( northsouth )
			{
				m_Item1 = new InternalItem( this, 1, 0 );
				m_Item2 = new InternalItem( this, -1, 0 );
				m_XOffset1 = 1;
				m_XOffset2 = -1;
				m_YOffset1 = 0;
				m_YOffset2 = 0;
			}
			else
			{
				m_Item1 = new InternalItem( this, 0, 1 );
				m_Item2 = new InternalItem( this, 0, -1 );
				m_XOffset1 = 0;
				m_XOffset2 = 0;
				m_YOffset1 = 1;
				m_YOffset2 = -1;
			}

			m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( 10.0 ), m_From, m_To );
			m_Timer.Start();

			m_End = DateTime.Now + TimeSpan.FromSeconds( 3.0 );

			Effects.PlaySound( new Point3D( X, Y, Z ), Map, 0x1F6 );
		}

		public override void OnLocationChange( Point3D oldLocation )
		{
			if ( m_Item1 != null )
				m_Item1.Location = new Point3D( X + m_XOffset1, Y + m_YOffset1, Z );
			if ( m_Item2 != null )
				m_Item2.Location = new Point3D( X + m_XOffset2, Y + m_YOffset2, Z );


			Effects.PlaySound( new Point3D( X, Y, Z ), Map, 0x1F6 );
		}

		public override void OnMapChange()
		{
			if ( m_Item1 != null )
				m_Item1.Map = Map;
			if ( m_Item2 != null )
				m_Item2.Map = Map;


			Effects.PlaySound( new Point3D( X, Y, Z ), Map, 0x1F6 );
		}

		public DuelWall( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.WriteDeltaTime( m_End );
			writer.Write( (Mobile)m_From );
			writer.Write( (Mobile)m_To );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_End = reader.ReadDeltaTime();

			m_From = reader.ReadMobile();
			m_To = reader.ReadMobile();

			m_Timer = new InternalTimer( this, m_End - DateTime.Now, m_From, m_To );
			m_Timer.Start();
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if ( m_Timer != null )
				m_Timer.Stop();

			if ( m_Item1 != null )
				m_Item1.Delete();

			if ( m_Item2 != null )
				m_Item2.Delete();
		}

		private class InternalItem : Item
		{
			private DuelWall m_Wall;

			public InternalItem( DuelWall wall, int xoffset, int yoffset ) : base( 0x80 )
			{
				Movable = false;
				m_Wall = wall;
				X = m_Wall.X + xoffset;
				Y = m_Wall.Y + yoffset;
			}

			public InternalItem( Serial serial ) : base( serial )
			{
			}

			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );

				writer.Write( (int) 0 ); //version
			}

			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );

				int version = reader.ReadInt();
			}
		}

		private class InternalTimer : Timer
		{
			private DuelWall m_Item;
			private Mobile m_From;
			private Mobile m_To;

			public InternalTimer( DuelWall item, TimeSpan duration, Mobile from, Mobile to ) : base( duration )
			{
				Priority = TimerPriority.OneSecond;
				m_Item = item;
				m_From = from;
				m_To = to;
			}

			protected override void OnTick()
			{
				m_Item.Delete();
				m_From.Frozen = false;
				m_To.Frozen = false;
			}
		}
	}
}