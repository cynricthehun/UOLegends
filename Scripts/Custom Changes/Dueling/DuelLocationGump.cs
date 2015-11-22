using System;
using Server;
using Server.Dueling;
using Server.Network;
using Server.Gumps;
using Server.Mobiles;
using Server.Regions;

namespace Server.Dueling
{
	public class DuelLocationGump : DuelGump
	{

		private Mobile m_Sender;
		private Mobile m_Reciever;

		public DuelLocationGump( Mobile from, Mobile to ) : this( true, true, true, true, true, true, from, to )
		{
		}

		public DuelLocationGump( bool para, bool pots, bool reflect, bool fields, bool stun, bool disarm, Mobile from, Mobile to ): base( 0, 0 )
		{
			m_Sender = from;
			m_Reciever = to;

			this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=true;
			this.AddPage(1);
			this.AddBackground(100, 100, 225, 185, 9200);
			this.AddHtml( 110, 112, 200, 44, "Choose the location for the duel.", true, false);
			this.AddButton(175, 250, 242, 241, 0, GumpButtonType.Reply, 0);
			if ( Region.Regions.Count > 0 )
			{
				bool dock = false;
				bool pit = false;
				bool dead = false;

				for( int i = 0; i < Region.Regions.Count; i++ )
				{
					Region region = Region.Regions[i] as Region;

					if ( region is DockSideArena )
					{
						if ( region.Players.Count == 0 )
						{
							Region.RemoveRegion( region );
						}
						else
						{
							this.AddLabel(145, 172, 0, "Dockside is currently in use.");
							dock = true;
						}
					}
					else if ( region is SandPitArena )
					{
						if ( region.Players.Count == 0 )
						{
							Region.RemoveRegion( region );
						}
						else
						{
							this.AddLabel(145, 172, 0, "The Sandpit is currently in use.");
							pit = true;
						}
					}
					else if ( region is Corroded2Arena )
					{
						if ( region.Players.Count == 0 )
						{
							Region.RemoveRegion( region );
						}
						else
						{
							this.AddLabel(145, 172, 0, "The Corroded Arena is currently in use.");
							dead = true;
						}
					}
				}

				if ( !dock )
				{
					this.AddButton(115, 175, 1209, 1210, 1, GumpButtonType.Reply, 0);
					this.AddLabel(145, 172, 0, "Dockside.");
				}
				if ( !pit )
				{
					this.AddButton(115, 200, 1209, 1210, 2, GumpButtonType.Reply, 0);
					this.AddLabel(145, 197, 0, "The Sandpit.");
				}
				if ( !dead )
				{
					this.AddButton(115, 225, 1209, 1210, 3, GumpButtonType.Reply, 0);
					this.AddLabel(145, 222, 0, "Corroded Arena.");
				}
			}

			this.AddButton(175, 225, 2006, 2007, 3, GumpButtonType.Page, 2);
			this.AddPage(2);
			this.AddBackground(100, 100, 225, 296, 9200);
			this.AddHtml( 110, 112, 200, 41, "Choose the options for the duel.", true, false);
			//this.AddLabel(145, 173, 0, "Allow summons.");
			this.AddLabel(144, 198, 0, "Allow paralyze.");
			this.AddLabel(145, 223, 0, "Allow potions.");
			this.AddLabel(145, 248, 0, "Allow magic reflection.");
			this.AddLabel(145, 273, 0, "Allow field spells.");
			this.AddLabel(145, 298, 0, "Allow stun.");
			this.AddLabel(145, 323, 0, "Allow disarm.");
			//this.AddCheck(115, 175, 210, 211, true, 4);
			this.AddCheck(115, 200, 210, 211, para, 5);
			this.AddCheck(115, 225, 210, 211, pots, 6);
			this.AddCheck(115, 250, 210, 211, reflect, 7);
			this.AddCheck(115, 275, 210, 211, fields, 8);
			this.AddCheck(115, 300, 210, 211, stun, 9);
			this.AddCheck(115, 325, 210, 211, disarm, 10);
			this.AddButton(245, 360, 242, 241, 11, GumpButtonType.Reply, 0);
			this.AddButton(115, 360, 247, 248, 12, GumpButtonType.Page, 1);
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			if ( m_Sender == null || m_Reciever == null )
				return;

			switch( info.ButtonID )
			{
				case 0: //Cancel
				{
					m_Sender.SendAsciiMessage( "You have cancelled your request for a duel." );
					m_Reciever.SendAsciiMessage( m_Sender.Name + " has cancelled the request for a duel." );
					((PlayerMobile)m_Sender).SentRequest = false;
					((PlayerMobile)m_Reciever).RecievedRequest = false;
					break;
				}
				case 1: //Dockside
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

					if ( Region.Regions.Count > 0 )
					{
						bool found = false;
						for( int i = 0; i < Region.Regions.Count; i++ )
						{
							Region region = Region.Regions[i] as Region;

							if ( region is DockSideArena )
							{
								if ( region.Players.Count == 0 )
								{
									Region.RemoveRegion( region );
								}
								else
								{
									found = true;
									m_Sender.SendMessage( "Dockside is currently in use." );
									m_Sender.SendGump( new DuelLocationGump( info.IsSwitched(5), info.IsSwitched(6), info.IsSwitched(7), info.IsSwitched(8), info.IsSwitched(9), info.IsSwitched(10), m_Sender, m_Reciever ) );
									break;
								}
							}
						}

						if ( found )
							break;
					}

					Region.AddRegion( new DockSideArena( info.IsSwitched(5), info.IsSwitched(6), info.IsSwitched(7), info.IsSwitched(8), info.IsSwitched(9), info.IsSwitched(10), m_Sender, m_Reciever ) );
					break;
				}
				case 2: //Sandpit
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

					if ( Region.Regions.Count > 0 )
					{
						bool found = false;
						for( int i = 0; i < Region.Regions.Count; i++ )
						{
							Region region = Region.Regions[i] as Region;

							if ( region is SandPitArena )
							{
								if ( region.Players.Count == 0 )
								{
									Region.RemoveRegion( region );
								}
								else
								{
									found = true;
									m_Sender.SendMessage( "The Sandpit is currently in use." );
									m_Sender.SendGump( new DuelLocationGump( info.IsSwitched(5), info.IsSwitched(6), info.IsSwitched(7), info.IsSwitched(8), info.IsSwitched(9), info.IsSwitched(10), m_Sender, m_Reciever ) );
									break;
								}
							}
						}

						if ( found )
							break;
					}

					Region.AddRegion( new SandPitArena( info.IsSwitched(5), info.IsSwitched(6), info.IsSwitched(7), info.IsSwitched(8), info.IsSwitched(9), info.IsSwitched(10), m_Sender, m_Reciever ) );
					break;
				}
				case 3: //Corroded2Arena
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

					if ( Region.Regions.Count > 0 )
					{
						bool found = false;
						for( int i = 0; i < Region.Regions.Count; i++ )
						{
							Region region = Region.Regions[i] as Region;

							if ( region is Corroded2Arena )
							{
								if ( region.Players.Count == 0 )
								{
									Region.RemoveRegion( region );
								}
								else
								{
									found = true;
									m_Sender.SendMessage( "The corroded arena is currently in use." );
									m_Sender.SendGump( new DuelLocationGump( info.IsSwitched(5), info.IsSwitched(6), info.IsSwitched(7), info.IsSwitched(8), info.IsSwitched(9), info.IsSwitched(10), m_Sender, m_Reciever ) );
									break;
								}
							}
						}

						if ( found )
							break;
					}

					Region.AddRegion( new Corroded2Arena( info.IsSwitched(5), info.IsSwitched(6), info.IsSwitched(7), info.IsSwitched(8), info.IsSwitched(9), info.IsSwitched(10), m_Sender, m_Reciever ) );
					break;
				}
				case 11: //Cancel
				{
					goto case 0;
				}
			}
		}
	}
}