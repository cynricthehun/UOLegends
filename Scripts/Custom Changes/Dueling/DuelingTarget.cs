using System;
using Server;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Dueling
{
	public class DuelTarget : Target
	{
		private Mobile m_Sender;
		private Mobile m_Reciever;

		public DuelTarget( Mobile from ) : base( 10, false, TargetFlags.None )
		{
			m_Sender = from;
		}

		protected override void OnTarget( Mobile from, object targeted )
		{
			if ( targeted is Mobile )
			{
				if ( targeted is PlayerMobile )
				{
					if ( targeted == m_Sender )
						from.SendAsciiMessage( "You cannot duel yourself." );
					else if ( DuelGump.Exists( (Mobile)targeted ) || ((Mobile)targeted).Region is DuelRegion || ((PlayerMobile)targeted).SentRequest || ((PlayerMobile)targeted).RecievedRequest )
					{
						from.SendAsciiMessage( "They look busy, maybe you should try again later." );
						((PlayerMobile)from).SentRequest = false;
					}
					else if ( !((Mobile)targeted).Alive )
					{
						from.SendMessage( "A live opponent would be a better choice." );
						((PlayerMobile)from).SentRequest = false;
					}
					else if ( !from.Alive )
					{
						from.SendMessage( "You cannot duel while dead." );
						((PlayerMobile)from).SentRequest = false;
					}
					else if ( from.Hits != from.HitsMax )
					{
						from.SendMessage( "You need to heal before offering a duel." );
						((PlayerMobile)from).SentRequest = false;
					}
					else if ( ((Mobile)targeted).Hits != ((Mobile)targeted).HitsMax )
					{
						from.SendMessage( "They are not at full health." );
						((PlayerMobile)from).SentRequest = false;
					}
					else
					{
						m_Reciever = (PlayerMobile)targeted;
						((PlayerMobile)m_Reciever).RecievedRequest = true;
						m_Reciever.SendGump( new DuelAcceptGump( m_Sender, m_Reciever ) );
					}
				}
				else if ( ((Mobile)targeted).Body.IsHuman )
				{
					from.SendAsciiMessage( ((Mobile)targeted).Name + " does not wish to duel you." );
					((PlayerMobile)from).SentRequest = false;
				}
				else
				{
					from.SendAsciiMessage( "Maybe you should just attack it." );
					((PlayerMobile)from).SentRequest = false;
				}
			}
			else
			{
				from.SendAsciiMessage( "You seem to have died since making the offer." );
			}
		}

		protected override void OnTargetCancel( Mobile from, TargetCancelType type )
		{
			((PlayerMobile)from).SentRequest = false;
		}
	}
}