using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Dueling
{
	[Flipable( 0x1E5E, 0x1E5F )]
	public class DuelRankBoard : Item
	{
		[Constructable]
		public DuelRankBoard() : base( 0x1E5E )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a duel ranking board" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendGump( new DuelRankGump( from ) );
		}

		public DuelRankBoard( Serial serial ) : base( serial )
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
	public class RankSort : IComparable
	{
            	public Mobile m_Mobile;
		public int Points;

            	public RankSort( Mobile m )
            	{
                	m_Mobile = m;
			Points = ((PlayerMobile)m).DuelWins;
           	}

            	public int CompareTo( object obj )
            	{
                	RankSort p = (RankSort)obj;

                	if( p.m_Mobile == null || p == null )
				return 0;

               		return p.Points - Points;
           	}
	}
	
	public class DuelRankGump : Gump
	{
		public Mobile m_From;
		public ArrayList m_List;
		
		private const int LabelColor = 0x7FFF;
		private const int SelectedColor = 0x421F;
		private const int DisabledColor = 0x4210;

		private const int LabelColor32 = 0xFFFFFF;
		private const int SelectedColor32 = 0x8080FF;
		private const int DisabledColor32 = 0x808080;

		private const int LabelHue = 0x480;
		private const int GreenHue = 0x40;
		private const int RedHue = 0x20;

		public DuelRankGump( Mobile from ) : base( 50, 40 )
		{
			from.CloseGump( typeof( DuelRankGump ) );
			
			m_From = from;
			
			ArrayList playerlist = new ArrayList();

			foreach( Mobile m in World.Mobiles.Values )
			{
				if( m.Player/*&& m.AccessLevel == AccessLevel.Player*/ )
				{
         				playerlist.Add( new RankSort( m ) );
				}
			}

			playerlist.Sort();

			AddPage( 0 );

			AddBackground( 0, 0, 300, 300, 9200 );

			AddLabel( 100, 25, 0, "Top 10 Duelers" );
			AddLabel( 25, 50, 0, "Players" );
			AddLabel( 175, 50, 0, "Wins" );
			AddLabel( 225, 50, 0, "Losses" );
			
			for ( int i = 0; i < playerlist.Count; ++i )
			{
				if ( i == 9 )
				{
					break;
				}

				RankSort g = (RankSort)playerlist[i];

				string name = null;
				int hue = ( m_From == g.m_Mobile ) ? 0x40 : 0;
				int wins = ((PlayerMobile)g.m_Mobile).DuelWins;
				int losses = ((PlayerMobile)g.m_Mobile).DuelLosses;

				if ( (name = g.m_Mobile.Name) != null && (name = name.Trim()).Length <= 15 )
					name = g.m_Mobile.Name;

				AddLabel( 25, 75 + ((i % 10) * 20), hue, name );
				AddLabel( 175, 75 + ((i % 10) * 20), hue, wins.ToString() );
				AddLabel( 225, 75 + ((i % 10) * 20), hue, losses.ToString() );
			}
		}
	}
}