using System;
using Server;
using Server.Dueling;
using Server.Network;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Dueling
{
	public class DuelAcceptGump : DuelGump
	{

		private Mobile m_Sender;	
		private Mobile m_Reciever;

		public DuelAcceptGump( Mobile from, Mobile to ): base( 0, 0 )
		{

			m_Sender = from;
			m_Reciever = to;

			this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(100, 100, 225, 95, 9200);
			this.AddHtml( 110, 112, 200, 43, from.Name + " has invited you to duel, do you accept?", true, false);
			this.AddButton(115, 165, 247, 248, 0, GumpButtonType.Reply, 0);
			this.AddButton(245, 165, 242, 241, 1, GumpButtonType.Reply, 0);
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Sender == null || m_Reciever == null )
				return;

			switch( info.ButtonID )
			{
				case 0: //Ok
				{
					if ( !m_Sender.Alive )
					{
						m_Sender.SendMessage( "You seem to have died since making the offer." );
						m_Sender.SendMessage( "Your opponent has cancelled the duel because they are dead." );
						((PlayerMobile)m_Reciever).RecievedRequest = false;
						((PlayerMobile)m_Sender).SentRequest = false;
						break;
					}

					if ( !m_Reciever.Alive )
					{
						m_Sender.SendMessage( "You seem to have died since making the offer." );
						m_Sender.SendMessage( "Your opponent has cancelled the duel because they are dead." );
						((PlayerMobile)m_Reciever).RecievedRequest = false;
						((PlayerMobile)m_Sender).SentRequest = false;
						break;
					}

					if ( m_Sender.Aggressed.Count != 0 || m_Sender.Aggressors.Count != 0 )
					{
						m_Sender.SendMessage( "You must cancel your offer because you are in combat." );
						m_Reciever.SendMessage( "Your opponent has cancelled the duel because they are in combat." );
						((PlayerMobile)m_Reciever).RecievedRequest = false;
						((PlayerMobile)m_Sender).SentRequest = false;
						break;
					}

					if ( m_Reciever.Aggressed.Count != 0 || m_Reciever.Aggressors.Count != 0 )
					{
						m_Reciever.SendMessage( "You must cancel because you are in combat." );
						m_Sender.SendMessage( "Your opponent has cancelled the duel because they are in combat." );
						((PlayerMobile)m_Reciever).RecievedRequest = false;
						((PlayerMobile)m_Sender).SentRequest = false;
						break;
					}

					if ( m_Sender.Hits != m_Sender.HitsMax )
					{
						m_Sender.SendMessage( "You must cancel your offer because you are not at full health." );
						m_Reciever.SendMessage( "Your opponent has cancelled the duel because they are not at full health." );
						((PlayerMobile)m_Reciever).RecievedRequest = false;
						((PlayerMobile)m_Sender).SentRequest = false;
						break;
					}

					if ( m_Reciever.Hits != m_Reciever.HitsMax )
					{
						m_Reciever.SendMessage( "You must cancel because you are not at full health." );
						m_Sender.SendMessage( "Your opponent has cancelled the duel because they are not at full health." );
						((PlayerMobile)m_Reciever).RecievedRequest = false;
						((PlayerMobile)m_Sender).SentRequest = false;
						break;
					}

					m_Sender.SendGump( new DuelLocationGump( m_Sender, m_Reciever ) );
					break;
				}
				case 1: //Cancel
				{
					m_Sender.SendAsciiMessage( m_Reciever.Name + " does not wish to duel you." );
					((PlayerMobile)m_Reciever).RecievedRequest = false;
					((PlayerMobile)m_Sender).SentRequest = false;
					m_Reciever.SendAsciiMessage( "You have declined the offer to duel." );	
					break;
				}
			}
		}
	}
}