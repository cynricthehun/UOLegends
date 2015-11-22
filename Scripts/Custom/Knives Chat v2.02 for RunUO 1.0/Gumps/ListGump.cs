using System;
using System.Collections;
using Server;
using Server.Guilds;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using Knives.Utils;

namespace Knives.Chat
{
	public enum Listing{ Public, Friends, Search, IRC, Messages, Ignores, Guild, Faction, Staff, Banned }

	public class ListGump : GumpPlus
	{
		public static void SendTo( Mobile m, Listing listing )
		{
			new ListGump( m, listing );
		}

		public static string s_MainHelp = "     The Public List page displays all players currently online who have Public Chat enabled.  If you want to ineract with one of these players, simply press the blue Profile button to the left of their name!"
			+ "  If you have Quickbar enabled through Options, you can also friend, ignore and send messages directly through this menu, bypassing the Profile."
			+ "  At the bottom of the menu you'll find a bar with additional options, including an away button and the ability to shorten the menu tabs.";
		public static string s_FriendsHelp = "     Your Friend's list contains all those you either added through their profile or the Quickbar.  You can do anything here you can on the Public List.  The difference?  Here you can see when they are on or offline!";
		public static string s_SearchHelp = "     Here you can search through all players and staff to quickly find that one person you're trying to get a hold of!  Can only search online individuals.  You can perform all the same functions as the Public List.";
		public static string s_IrcHelp = "     The IRC list contains all those in your server's IRC room.  At the moment you can't do much with the information except know when a friend is connected.  Does not show those connected through your Ultima Online server!";
		public static string s_MessagesHelp = "     Here's an inbox for your messages, ordered by time received!  This is an alternative to the normal popup message indicator.  Know who is sending you messages before you open them!"
			+ "<BR><BR>     You can delete messages here without reading if you wish."
			+ "<BR><BR>     You may also view and delete your saved messages.";
		public static string s_IgnoresHelp = "     Your Ignore list contains all those you you've chosen to ignore, online and offline.  You can do anything here you can on the Public List.";
		public static string s_GuildHelp = "     Here you can see everyone in your guild, on or offline!  See their guild title, send them messages!  If enabled, you may access your guild's menu from here as well. You can do anything here you can on the Public List.";
		public static string s_FactionHelp = "     See everyone in your faction, along with their rank!  You can do anything here you can on the Public List.";
		public static string s_StaffHelp = "     Find out who your staffers are and send them messages if they allow it.  Shows them whether they are on or offline.  Because they are staff, you naturally won't be able to ignore them, but all other Public List features work the same!";
		public static string s_BannedHelp = "     Here's a list of all players who are currently banned from the system.  The main purpose of this list is to unban them, but you can also do anything you can with the Public List.";
		public static string s_ColorKey = "<CENTER><u>Color Key</u>  Gray: Offline<BR>Green: Guild<BR>Purple: Faction<BR>Blue: Staff<BR>Dark Red: Ignored, Red: Banned.</CENTER>";
		public static string s_StaffOnlyHelp = "     In addition, staff members can ban, go to, and view the clients of players directly from this menu when Quickbar is enabled in Options.";

		private const int Width = 200;

		private ChatInfo c_Info;
		private Listing c_Listing;
		private ArrayList c_List;
		private int c_Page;
		private int c_Height;
		private bool c_Minimized, c_Alert, c_Mailbox;

		public ListGump( Mobile m, Listing listing ) : base( m, 100, 100 )
		{
			c_Info = ChatInfo.GetInfo( m );
			c_Listing = listing;

			c_List = new ArrayList();
			c_Mailbox = true;

			NewGump();
		}

		protected override void BuildGump()
		{
			Owner.CloseGump( typeof( ListGump ) );

			c_Alert = c_Info.HasMessages;

			if ( c_Minimized )
			{
				Override = false;

				AddImage( 0, 0, 0x2936, c_Alert ? 0xF8 : 0x0 );
				AddImage( 28, 0, 0x2937, c_Alert ? 0xF8 : 0x0 );
				AddImage( 30, 0, 0x2938, c_Alert ? 0xF8 : 0x0 );

				AddButton( 37, 8, 0x2716, 0x2716, "MinMax", new TimerCallback( MinMax ) );

				if ( c_Alert )
					AddButton( 34, 23, 0x1523, 0x1523, "MinMessage", new TimerStateCallback( List ), 4 );
			}
			else
			{
				Override = true;

				if ( c_Listing == Listing.Messages )
					Owner.CloseGump( typeof( PmNotifyGump ) );

				c_Height = c_Info.ShowTabs ? 360 : 300;

				AddBackground( 0, 0, Width, c_Height, 0x13BE );

				if ( c_Info.ShowTabs )
					BuildTabs();

				BuildList();
				DisplayList();
				DisplayMisc();
			}
		}

		private void BuildTabs()
		{
			int y = c_Height-80;
			int x = 10;

			if ( !c_Info.PublicDisabled )
			{
				if ( c_Listing == Listing.Public )
					AddTemplateButton( x, y, 70, Template.RedSquare, "Public List", HTML.Green + "<CENTER>Public", new TimerStateCallback( List ), (int)Listing.Public, false );
				else
					AddTemplateButton( x, y, 70, Template.RedSquare, "Public List", HTML.White + "<CENTER>Public", new TimerStateCallback( List ), (int)Listing.Public );
			}

			if ( !c_Info.PublicDisabled )
			{
				if ( c_Listing == Listing.Search )
					AddTemplateButton( x, y+15, 70, Template.RedSquare, "Search List", HTML.Green + "<CENTER>Search", new TimerStateCallback( List ), (int)Listing.Search, false );
				else
					AddTemplateButton( x, y+15, 70, Template.RedSquare, "Search List", HTML.White + "<CENTER>Search", new TimerStateCallback( List ), (int)Listing.Search );
			}

			if ( ChatInfo.ShowStaff || Owner.AccessLevel != AccessLevel.Player )
			{
				if ( c_Listing == Listing.Staff )
					AddTemplateButton( x, y+30, 70, Template.RedSquare, "Staff List", HTML.Green + "<CENTER>Staff", new TimerStateCallback( List ), (int)Listing.Staff, false );
				else
					AddTemplateButton( x, y+30, 70, Template.RedSquare, "Staff List", HTML.White + "<CENTER>Staff", new TimerStateCallback( List ), (int)Listing.Staff );
			}

			if ( Owner.AccessLevel > AccessLevel.Player )
			{
				if ( c_Listing == Listing.Banned )
					AddTemplateButton( x, y+45, 70, Template.RedSquare, "Banned List", HTML.Green + "<CENTER>Banned", new TimerStateCallback( List ), (int)Listing.Banned, false );
				else
					AddTemplateButton( x, y+45, 70, Template.RedSquare, "Banned List", HTML.White + "<CENTER>Banned", new TimerStateCallback( List ), (int)Listing.Banned );
			}

			x+=55;

			if ( Owner.Guild != null )
			{
				if ( c_Listing == Listing.Guild )
					AddTemplateButton( x, y, 70, Template.RedSquare, "Guild List", HTML.Green + "<CENTER>Guild", new TimerStateCallback( List ), (int)Listing.Guild, false );
				else
					AddTemplateButton( x, y, 70, Template.RedSquare, "Guild List", HTML.White + "<CENTER>Guild", new TimerStateCallback( List ), (int)Listing.Guild );
			}

			if ( c_Listing == Listing.Friends )
				AddTemplateButton( x, y+15, 70, Template.RedSquare, "Friends List", HTML.Green + "<CENTER>Friends", new TimerStateCallback( List ), (int)Listing.Friends, false );
			else
				AddTemplateButton( x, y+15, 70, Template.RedSquare, "Friends List", HTML.White + "<CENTER>Friends", new TimerStateCallback( List ), (int)Listing.Friends );

			if ( c_Listing == Listing.Ignores )
				AddTemplateButton( x, y+30, 70, Template.RedSquare, "Ignores List", HTML.Green + "<CENTER>Ignores", new TimerStateCallback( List ), (int)Listing.Ignores, false );
			else
				AddTemplateButton( x, y+30, 70, Template.RedSquare, "Ignores List", HTML.White + "<CENTER>Ignores", new TimerStateCallback( List ), (int)Listing.Ignores );

			AddTemplateButton( x, y+45, 70, Template.RedSquare, "Options", HTML.White + "<CENTER>Options", new TimerCallback( Options ) );

			x+=55;

			if ( ChatInfo.AllowFaction && FactionChat.IsInFaction( Owner ) )
			{
				if ( c_Listing == Listing.Faction )
					AddTemplateButton( x, y, 70, Template.RedSquare, "Faction List", HTML.Green + "<CENTER>Faction", new TimerStateCallback( List ), (int)Listing.Faction, false );
				else
					AddTemplateButton( x, y, 70, Template.RedSquare, "Faction List", HTML.White + "<CENTER>Faction", new TimerStateCallback( List ), (int)Listing.Faction );
			}

			if ( c_Listing == Listing.Messages )
				AddTemplateButton( x, y+15, 70, Template.RedSquare, "Message List", HTML.Green + "<CENTER>Messages", new TimerStateCallback( List ), (int)Listing.Messages, false );
			else
				AddTemplateButton( x, y+15, 70, Template.RedSquare, "Message List", HTML.White + "<CENTER>Messages", new TimerStateCallback( List ), (int)Listing.Messages );

			if ( ChatInfo.IrcEnabled && IrcConnection.Connection.Connected && c_Info.IrcOn )
			{
				if ( c_Listing == Listing.IRC )
					AddTemplateButton( x, y+30, 70, Template.RedSquare, "IRC List", HTML.Green + "<CENTER>IRC", new TimerStateCallback( List ), (int)Listing.IRC, false );
				else
					AddTemplateButton( x, y+30, 70, Template.RedSquare, "IRC List", HTML.White + "<CENTER>IRC", new TimerStateCallback( List ), (int)Listing.IRC );
			}
		}

		private void BuildList()
		{try{

			c_List.Clear();

			switch( c_Listing )
			{
				case Listing.Public:

					foreach( ChatInfo info in new ArrayList( ChatInfo.ChatInfos.Values ) )
					{
						if ( info.Mobile == null || info.Mobile.Deleted )
						{
							ChatInfo.ChatInfos.Remove( info );
							continue;
						}

						if ( info.Mobile.NetState == null
						|| info.Mobile.AccessLevel != AccessLevel.Player )
							continue;

						if ( PublicChat.CanChat( info ) || Owner.AccessLevel > info.Mobile.AccessLevel )
							c_List.Add( info.Mobile );
					}

					c_List.Sort( new MobileName() );

					break;

				case Listing.Friends:

					c_List = new ArrayList( c_Info.Friends );

					if ( !ChatInfo.ShowStaff )
						foreach( Mobile m in new ArrayList( c_List ) )
							if ( m.AccessLevel != AccessLevel.Player )
								c_List.Remove( m );

					c_List.Sort( new MobileName() );

					break;

				case Listing.Search:

					if ( c_Info.Search == "" )
						break;

					foreach( ChatInfo info in ChatInfo.ChatInfos.Values )
					{
						if ( !ChatInfo.ShowStaff && info.Mobile.AccessLevel != AccessLevel.Player )
							continue;

						if ( info.Mobile.Name.ToLower().IndexOf( c_Info.Search.ToLower() ) != -1
						&& info.Mobile.NetState != null )
							c_List.Add( info.Mobile );
					}

					c_List.Sort( new MobileName() );

					break;

				case Listing.IRC:

					c_List = new ArrayList( ChatInfo.IrcList );
					c_List.Sort( new Alpha() );

					break;

				case Listing.Messages:

					c_List = new ArrayList( c_Mailbox ? c_Info.Messages : c_Info.SavedMsgs );
					c_List.Sort( new MessageTime() );

					break;

				case Listing.Ignores:

					c_List = new ArrayList( c_Info.Ignores );
					c_List.AddRange( c_Info.GlobalIgnores );

					if ( !ChatInfo.ShowStaff )
						foreach( Mobile m in new ArrayList( c_List ) )
							if ( m.AccessLevel != AccessLevel.Player )
								c_List.Remove( m );

					c_List.Sort( new MobileName() );

					break;

				case Listing.Guild:

					foreach( ChatInfo info in ChatInfo.ChatInfos.Values )
						if ( info.Mobile.Guild != null
						&& info.Mobile.Guild == Owner.Guild )
							c_List.Add( info.Mobile );

					c_List.Sort( new MobileName() );

					break;

				case Listing.Faction:

					foreach( ChatInfo info in ChatInfo.ChatInfos.Values )
						if ( FactionChat.SameFaction( Owner, info.Mobile ) )
							c_List.Add( info.Mobile );

					c_List.Sort( new MobileName() );

					break;

				case Listing.Staff:

					foreach( ChatInfo info in ChatInfo.ChatInfos.Values )
						if ( info.Mobile.AccessLevel != AccessLevel.Player )
							c_List.Add( info.Mobile );

					c_List.Sort( new MobileAccessLevel() );

					break;

				case Listing.Banned:

					foreach( ChatInfo info in ChatInfo.ChatInfos.Values )
						if ( info.Banned )
							c_List.Add( info.Mobile );

					c_List.Sort( new MobileName() );

					break;

			}

		}catch{ Errors.Report( String.Format( "ListGump-> BuildList-> |{0}|", Owner ) ); } }

		private void DisplayList()
		{try{

			int toList = 10;

			if ( c_Listing == Listing.Guild
			|| c_Listing == Listing.Staff
			|| c_Listing == Listing.Messages
			|| c_Listing == Listing.Faction )
				toList /= 2;

			if ( c_Listing == Listing.Search )
				toList-=2;

			int beginAt = toList*c_Page;

			while( c_Page > 0 )
			{
				if ( beginAt > c_List.Count )
					beginAt = toList * --c_Page;
				else
					break;
			}

			if ( c_Page != 0 )
				AddButton( Width/2-7, 5, 0x15E0, 0x15E4, "Next Page", new TimerCallback( PageDown ) );

			if ( c_Page < (c_List.Count-1)/toList )
				AddButton( Width/2-7, c_Listing == Listing.Search ? 230 : 260, 0x15E2, 0x15E6, "Previous Page", new TimerCallback( PageUp ) );

			int y = -10;
			int x = 30;

			if ( c_Listing == Listing.Guild )
				y+=15;

			if ( c_Listing == Listing.Messages )
			{
				Message msg;

				if ( c_Mailbox )
					AddTemplateButton( x, y+25, 70, Template.RedSquare, "Inbox", HTML.Green + "<CENTER>Inbox", new TimerCallback( MailBox ), false );
				else
					AddTemplateButton( x, y+25, 70, Template.RedSquare, "Inbox", HTML.White + "<CENTER>Inbox", new TimerCallback( MailBox ) );

				if ( !c_Mailbox )
					AddTemplateButton( x+75, y+25, 70, Template.RedSquare, "Saved", HTML.Green + "<CENTER>Saved", new TimerCallback( SavedMail ), false );
				else
					AddTemplateButton( x+75, y+25, 70, Template.RedSquare, "Saved", HTML.White + "<CENTER>Saved", new TimerCallback( SavedMail ) );

				y = 0;
				x = 40;

				for( int i = beginAt; i < c_List.Count && i < beginAt+toList; ++i )
				{
					msg = (Message)c_List[i];

					if ( msg.History.Count == 0 )
						continue;

					string text = msg.LastText;

					if ( text.Length > 20 )
						text = text.Substring( 0, 20 ) + "...";

					AddHtml( x, y+=30, Width, 25, HTML.White + text, false, false );

					AddButton( 20, y, 0x1523, 0x1523, "Open Message", new TimerStateCallback( OpenMessage ), i );
					AddButton( Width-20, y, 0x5686, 0x5686, "Delete Message", new TimerStateCallback( DeleteMessage ), i );

					AddHtml( x, y+=20, Width, 25, HTML.Gray + "From " + msg.LastSender.Name, false, false );

					if ( i+1 < c_List.Count && i+1 < beginAt+toList )
						AddBackground( 50, y+18, Width-100, 3, 0x13BE );
				}
			}
			else if ( c_Listing == Listing.IRC )
			{
				for( int i = beginAt; i < c_List.Count && i < beginAt+toList; ++i )
				{
					AddHtml( x, y+=25, Width, 25, HTML.White + c_List[i].ToString(), false, false );

					if ( i+1 < c_List.Count && i+1 < beginAt+toList )
						AddBackground( 50, y+18, Width-100, 3, 0x13BE );
				}
			}
			else
			{
				if ( c_Listing == Listing.Guild && Owner.Guild != null )
				{
					if ( ((Guild)Owner.Guild).Abbreviation != "none" )
						AddHtml( 20, 15-3, 70, 25, HTML.White + ((Guild)Owner.Guild).Abbreviation, false, false );

					if ( ChatInfo.GuildMenuAccess )
						AddTemplateButton( Width-60, 15, 70, Template.RedSquare, "Guild Menu", HTML.White + "<CENTER>Menu", new TimerCallback( GuildMenu ) );
				}

				string text = "";
				ChatInfo info;

				for( int i = beginAt; i < c_List.Count && i < beginAt+toList; ++i )
				{
					info = ChatInfo.GetInfo( (Mobile)c_List[i] );

					if ( info.Mobile == null || info.Mobile.Name == null || info.Mobile.Name == "" )
						continue;

					text = Color( info ) + info.Mobile.Name;

					if ( info.Hidden && Owner.AccessLevel > info.Mobile.AccessLevel )
						text += HTML.White + " (Hidden)";
					else if ( info.Away )
						text += HTML.White + " (Away)";

					AddHtml( x, y+=25, Width, 25, text, false, false, false );

					if ( Owner != info.Mobile )
						AddButton( x-20, y+3, 0x93A, 0x93A, "Profile", new TimerStateCallback( Profile ), i );

					if ( Owner != info.Mobile && c_Info.Quickbar )
					{
						int xx = Width-15;

						if ( Owner.AccessLevel > info.Mobile.AccessLevel )
						{
							if ( info.Mobile.NetState != null )
							{
								AddButton( xx-=12, y+3, 0x13A8, 0x13A8, "Mini Goto", new TimerStateCallback( Goto ), i );
								AddLabel( xx+4, y, 0x47E, "g" );
								AddButton( xx-=12, y+3, 0x13A8, 0x13A8, "Mini Client", new TimerStateCallback( Client ), i );
								AddLabel( xx+4, y, 0x47E, "c" );
							}

							AddButton( xx-=12, y+3, 0x13A8, 0x13A8, "Mini Ban", info.Banned ? new TimerStateCallback( Unban ) : new TimerStateCallback( Ban ), i );
							AddLabel( xx+4, y, info.Banned ? 0x20 : 0x47E, "b" );
						}

						AddButton( xx-=12, y+3, 0x13A8, 0x13A8, "Mini Friend", c_Info.Friends.Contains( info.Mobile ) ? new TimerStateCallback( Unfriend ) : new TimerStateCallback( Friend ), i );
						AddLabel( xx+4, y-1, c_Info.Friends.Contains( info.Mobile ) ? 0x35 : 0x47E, "f" );

						if ( Owner.AccessLevel >= info.Mobile.AccessLevel )
						{
							AddButton( xx-=12, y+3, 0x13A8, 0x13A8, "Mini Ignore", c_Info.Ignoring( info.Mobile ) ? new TimerStateCallback( Unignore ) : new TimerStateCallback( Ignore ), i );
							AddLabel( xx+4, y-1, c_Info.Ignoring( info.Mobile ) ? 0x20 : 0x47E, "i" );
						}

						if ( Pm.CanPm( c_Info, info ) )
						{
							AddButton( xx-=12, y+3, 0x13A8, 0x13A8, "Mini New Message", new TimerStateCallback( NewMessage ), i );
							AddLabel( xx+2, y-2, 0x47E, "m" );
						}
					}

					if ( c_Listing == Listing.Guild )
						AddHtml( x, y+=20, Width-20, 25, HTML.White + info.Mobile.GuildTitle, false, false );
					else if ( c_Listing == Listing.Faction )
						AddHtml( x, y+=20, Width-20, 25, HTML.White + ((PlayerMobile)info.Mobile).FactionPlayerState.Rank.Title.String, false, false );
					else if ( c_Listing == Listing.Staff )
						AddHtml( x, y+=20, Width-20, 25, HTML.White + info.Mobile.AccessLevel.ToString(), false, false );

					if ( i+1 < c_List.Count && i+1 < beginAt+toList )
						AddBackground( 50, y+18, Width-100, 3, 0x13BE );
				}
			}

		}catch{ Errors.Report( String.Format( "ListGump-> DisplayList-> |{0}|-> {1}", Owner, c_Listing ) ); } }

		private void DisplayMisc()
		{
			if ( c_Listing == Listing.Search )
			{
				AddHtml( Width/2-70, 250, 60, 25, HTML.White + "Search:", false, false );
				AddImageTiled( Width/2-20, 250, 80, 21, 0xBBA );
				AddTextField( Width/2-20, 250, 80, 21, 0x480, 0, c_Info.Search );
				AddButton( Width/2+70, 255, 0x93A, 0x93A, "Search", new TimerCallback( Search ) );	
			}

			AddButton( Width-40, c_Height-20, 0x5689, 0x5689, "Help", new TimerCallback( Help ) );
			AddButton( Width-20, c_Height-20, 0x2716, 0x2716, "MinMax", new TimerCallback( MinMax ) );
			AddButton( 10, c_Height-20, c_Info.ShowTabs ? 0x15E0 : 0x15E2, c_Info.ShowTabs ? 0x15E4 : 0x15E6, "Hide Tabs", new TimerCallback( ShowTabs ) );

			AddButton( Width-60, c_Height-20, 0x5689, 0x5689, "Mini Away", new TimerCallback( Away ) );
			AddLabel( Width-57, c_Height-22, c_Info.Away ? 0x35 : 0x47E, "A" );

			if ( !c_Info.ShowTabs )
			{
				AddButton( Width-80, c_Height-20, 0x5689, 0x5689, "Mini Options", new TimerCallback( Options ) );
				AddLabel( Width-77, c_Height-22, 0x47E, "O" );

				AddTemplateButton( 40, c_Height-20, 70, Template.RedSquare, "Mini List", HTML.White + "<CENTER>" + c_Listing, new TimerCallback( MiniList ) );
			}
		}

		private string Color( ChatInfo info )
		{
			if ( info.Mobile.NetState == null
			|| info.PublicDisabled
			|| ( info.Hidden && info.Mobile.AccessLevel > Owner.AccessLevel ) )
				return HTML.Gray;
			if ( info.Banned )
				return HTML.Red;
			if ( c_Info.Ignoring( info.Mobile ) )
				return HTML.AshRed;
			if ( c_Info.GlobalIgnoring( info.Mobile ) )
				return HTML.DarkGray;
			if ( info.Mobile.AccessLevel != AccessLevel.Player )
				return HTML.Blue;
			if ( Owner.Guild != null && Owner.Guild == info.Mobile.Guild )
				return HTML.Green;
			if ( FactionChat.SameFaction( Owner, info.Mobile ) )
				return HTML.Purple;

			return HTML.White;
		}

		private void Search()
		{
			c_Info.Search = GetTextField( 0 );
			NewGump();
		}

		private void Help()
		{
			NewGump();

			string str = "";

			switch( c_Listing )
			{
				case Listing.Public:
					str = s_MainHelp + "<BR><BR>" + (Owner.AccessLevel != AccessLevel.Player ? s_StaffOnlyHelp : "") + "<BR><BR>" + s_ColorKey;
					break;
				case Listing.Friends:
					str = s_FriendsHelp + "<BR><BR>" + s_ColorKey;
					break;
				case Listing.Search:
					str = s_SearchHelp + "<BR><BR>" + s_ColorKey;
					break;
				case Listing.IRC:
					str = s_IrcHelp;
					break;
				case Listing.Messages:
					str = s_MessagesHelp;
					break;
				case Listing.Ignores:
					str = s_IgnoresHelp + "<BR><BR>" + s_ColorKey;
					break;
				case Listing.Guild:
					str = s_GuildHelp + "<BR><BR>" + s_ColorKey;
					break;
				case Listing.Faction:
					str = s_FactionHelp + "<BR><BR>" + s_ColorKey;
					break;
				case Listing.Staff:
					str = s_StaffHelp + "<BR><BR>" + s_ColorKey;
					break;
				case Listing.Banned:
					str = s_BannedHelp + "<BR><BR>" + s_ColorKey;
					break;
			}

			InfoGump.SendTo( Owner, 300, 300, str, true );
		}

		private void MinMax()
		{
			c_Minimized = !c_Minimized;
			NewGump();
		}

		private void ShowTabs()
		{
			c_Info.ShowTabs = !c_Info.ShowTabs;
			NewGump();
		}

		private void Away()
		{
			c_Info.Away = !c_Info.Away;

			if ( c_Info.Away )
				ResponseGump.SendTo( Owner, 200, 200, c_Info.AwayMsg, "Enter your away message", new TimerStateCallback( AwayMsg ) );
			else
				NewGump();
		}

		private void AwayMsg( object obj )
		{
			if ( !(obj is string ) )
				return;

			c_Info.AwayMsg = obj.ToString();
			NewGump();
		}

		private void MiniList()
		{
			Hashtable table = new Hashtable();

			if ( Owner.AccessLevel > AccessLevel.Player )
				table.Add( (int)Listing.Banned, "Banned" );

			if ( ChatInfo.ShowStaff
			|| Owner.AccessLevel != AccessLevel.Player )
				table.Add( (int)Listing.Staff, "Staff" );

			if ( Owner.Guild != null )
				table.Add( (int)Listing.Guild, "Guild" );

			if ( FactionChat.IsInFaction( Owner ) && ChatInfo.AllowFaction )
				table.Add( (int)Listing.Faction, "Faction" );

			table.Add( (int)Listing.Ignores, "Ignores" );
			table.Add( (int)Listing.Messages, "Messages" );

			if ( ChatInfo.IrcEnabled
			&& IrcConnection.Connection.Connected
			&& c_Info.IrcOn )
				table.Add( (int)Listing.IRC, "IRC" );

			if ( !c_Info.PublicDisabled )
				table.Add( (int)Listing.Search, "Search" );

			table.Add( (int)Listing.Friends, "Friends" );

			if ( !c_Info.PublicDisabled )
				table.Add( (int)Listing.Public, "Public" );

			ChoiceGump.SendTo( Owner, "", 100, new TimerStateCallback( List ), table );
		}

		private void GuildMenu()
		{
			NewGump();

			if ( ChatInfo.GuildMenuAccess && Owner.Guild != null )
				Owner.SendGump( new GuildGump( Owner, (Guild)Owner.Guild ) );
		}

		private void List( object obj )
		{
			if ( !(obj is int) )
				return;

			if ( (int)obj == -1 )
				obj = (int)c_Listing;

			if ( (Listing)obj == Listing.Staff
			&& !ChatInfo.ShowStaff
			&& Owner.AccessLevel == AccessLevel.Player )
				NewGump();
			else if ( (Listing)obj == Listing.Faction
			&& !ChatInfo.AllowFaction )
				NewGump();
			else
			{
				c_Listing = (Listing)obj;
				NewGump();
			}
		}

		private void MailBox()
		{
			c_Mailbox = true;
			NewGump();
		}

		private void SavedMail()
		{
			c_Mailbox = false;
			NewGump();
		}

		private void Options()
		{
			NewGump();
			OptionsGump.SendTo( Owner );
		}

		private void PageDown()
		{
			c_Page--;
			NewGump();
		}

		private void PageUp()
		{
			c_Page++;
			NewGump();
		}

		private void Profile( object obj )
		{
			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			Mobile m = (Mobile)c_List[(int)obj];

			NewGump();
			ProfileGump.SendTo( Owner, m );
		}

		private void Goto( object obj )
		{try{

			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			Mobile m = (Mobile)c_List[(int)obj];

			if ( m.NetState == null )
				Owner.SendMessage( "{0} is no longer online.", m.Name );
			else
			{
				Owner.Location = m.Location;
				Owner.Map = m.Map;
			}

			NewGump();

		}catch{ Errors.Report( String.Format( "ListGump-> Goto-> |{0}|", Owner ) ); } }

		private void Client( object obj )
		{
			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			Mobile m = (Mobile)c_List[(int)obj];

			if ( m.NetState == null )
				Owner.SendMessage( "{0} is no longer online.", m.Name );
			else
				Owner.SendGump( new ClientGump( Owner, m.NetState ) );

			NewGump();
		}

		private void Ban( object obj )
		{
			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			Hashtable table = new Hashtable();

			table.Add( new object[2]{ c_List[(int)obj], TimeSpan.FromMinutes( 5 ) }, "Five Minutes" );
			table.Add( new object[2]{ c_List[(int)obj], TimeSpan.FromMinutes( 30 ) }, "Thirty Minutes" );
			table.Add( new object[2]{ c_List[(int)obj], TimeSpan.FromHours( 5 ) }, "Five Hours" );
			table.Add( new object[2]{ c_List[(int)obj], TimeSpan.FromDays( 1 ) }, "One Day" );
			table.Add( new object[2]{ c_List[(int)obj], TimeSpan.FromDays( 7 ) }, "One Week" );
			table.Add( new object[2]{ c_List[(int)obj], TimeSpan.FromDays( 30 ) }, "One Month" );
			table.Add( new object[2]{ c_List[(int)obj], TimeSpan.FromDays( 365 ) }, "One Year" );

			ChoiceGump.SendTo( Owner, "Select Duration", 200, new TimerStateCallback( BanCallback ), table );
		}

		private void BanCallback( object obj )
		{
			if ( !(obj is object[]) )
				return;

			object[] info = (object[])obj;

			if ( info.Length != 2
			|| !(info[0] is Mobile )
			|| !(info[1] is TimeSpan ) )
				return;

			ChatInfo.GetInfo( (Mobile)info[0] ).Ban( (TimeSpan)info[1] );

			NewGump();
		}

		private void Unban( object obj )
		{
			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			ChatInfo.GetInfo( (Mobile)c_List[(int)obj] ).RemoveBan();
			NewGump();
		}

		private void Friend( object obj )
		{
			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			c_Info.AddFriend( (Mobile)c_List[(int)obj] );
			NewGump();
		}

		private void Unfriend( object obj )
		{
			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			c_Info.RemoveFriend( (Mobile)c_List[(int)obj] );
			NewGump();
		}

		private void Ignore( object obj )
		{
			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			c_Info.AddIgnore( (Mobile)c_List[(int)obj] );
			NewGump();
		}

		private void Unignore( object obj )
		{
			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			c_Info.RemoveIgnore( (Mobile)c_List[(int)obj] );
			NewGump();
		}

		private void OpenMessage( object obj )
		{
			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			Message msg = (Message)c_List[(int)obj];

			c_Info.RemoveMessage( msg );
			NewGump();
			PmGump.SendTo( Owner, msg );
		}

		private void DeleteMessage( object obj )
		{
			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			c_Info.DeleteMessage( (Message)c_List[(int)obj] );
			NewGump();
		}

		private void NewMessage( object obj )
		{
			if( !(obj is int) )
				return;

			if( (int)obj >= c_List.Count || (int)obj < 0 )
				return;

			Mobile m = (Mobile)c_List[(int)obj];

			NewGump();
			PmGump.SendTo( Owner, m );
		}
	}
}