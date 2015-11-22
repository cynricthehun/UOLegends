using System;
using System.Collections;
using System.IO;
using Server;
using Server.Accounting;
using Knives.Utils;

namespace Knives.Chat
{
	public class ChatInfo
	{
		public enum PublicStyle{ All, Regional, None }

		private static string s_Version = "2.02";
		private static DateTime s_ReleaseDate = new DateTime( 2005, 6, 21 );

		private static Hashtable s_ChatInfos = new Hashtable();
		private static ArrayList s_Filters = new ArrayList();
		private static ArrayList s_IrcList = new ArrayList();
		private static double s_SpamLimiter = 3.0;
		private static double s_FilterBanLength = 5.0;
		private static FilterPenalty s_FilterPenalty;
		private static bool s_ShowLocation = false;
		private static bool s_ShowStaff = true;
		private static PublicStyle s_PublicStyle;
		private static bool s_IrcEnabled = false;
		private static bool s_IrcAutoConnect = false;
		private static bool s_IrcAutoReconnect = false;
		private static bool s_GuildMenuAccess = false;
		private static bool s_AllowFaction = false;
		private static bool s_AllianceChat = false;
		private static bool s_PublicPlusIRC = false;
		private static string s_IrcServer = "";
		private static int s_IrcPort = 6667;
		private static int s_IrcMaxAttempts = 0;
		private static int s_MaxPmHistory = 10;
		private static IrcColor s_IrcStaffColor = Knives.Chat.IrcColor.Black;
		private static string s_IrcRoom = "";
		private static string s_IrcNick = Server.Misc.ServerList.ServerName;

		public static string Version{ get{ return s_Version; } }
		public static DateTime ReleaseDate{ get{ return s_ReleaseDate; } }

		public static Hashtable ChatInfos{ get{ return s_ChatInfos; } }
		public static ArrayList Filters{ get{ return s_Filters; } }
		public static ArrayList IrcList{ get{ return s_IrcList; } }
		public static double SpamLimiter{ get{ return s_SpamLimiter; } set{ s_SpamLimiter = value; } }
		public static double FilterBanLength{ get{ return s_FilterBanLength; } set{ s_FilterBanLength = value; } }
		public static bool Full{ get{ return s_PublicStyle == PublicStyle.All; } }
		public static bool Regional{ get{ return s_PublicStyle == PublicStyle.Regional; } }
		public static bool NoPublic{ get{ return s_PublicStyle == PublicStyle.None; } }
		public static bool ShowLocation{ get{ return s_ShowLocation; } set{ s_ShowLocation = value; } }
		public static bool ShowStaff{ get{ return s_ShowStaff; } set{ s_ShowStaff = value; } }
		public static bool PublicPlusIRC{ get{ return s_PublicPlusIRC; } set{ s_PublicPlusIRC = value; } }
		public static PublicStyle Style{ get{ return s_PublicStyle; } set{ s_PublicStyle = value; } }
		public static bool IrcAutoConnect{ get{ return s_IrcAutoConnect; } set{ s_IrcAutoConnect = value; } }
		public static bool IrcAutoReconnect{ get{ return s_IrcAutoReconnect; } set{ s_IrcAutoReconnect = value; } }
		public static bool GuildMenuAccess{ get{ return s_GuildMenuAccess; } set{ s_GuildMenuAccess = value; } }
		public static bool AllowFaction{ get{ return s_AllowFaction; } set{ s_AllowFaction = value; } }
		public static bool AllianceChat{ get{ return s_AllianceChat; } set{ s_AllianceChat = value; } }
		public static string IrcServer{ get{ return s_IrcServer; } set{ s_IrcServer = value; } }
		public static string IrcRoom{ get{ return s_IrcRoom; } set{ s_IrcRoom = value; } }
		public static string IrcNick{ get{ return s_IrcNick; } set{ s_IrcNick = value; } }
		public static int IrcPort{ get{ return s_IrcPort; } set{ s_IrcPort = value; } }
		public static int IrcMaxAttempts{ get{ return s_IrcMaxAttempts; } set{ s_IrcMaxAttempts = value; } }
		public static int MaxPmHistory{ get{ return s_MaxPmHistory; } set{ s_MaxPmHistory = value; } }
		public static FilterPenalty FilterPenalty{ get{ return s_FilterPenalty; } set{ s_FilterPenalty = value; } }
		public static bool FilterBan{ get{ return s_FilterPenalty == FilterPenalty.ChatBan; } }
		public static bool FilterKick{ get{ return s_FilterPenalty == FilterPenalty.Kick; } }
		public static bool FilterCriminal{ get{ return s_FilterPenalty == FilterPenalty.Criminal; } }
		public static bool NoFilterPenalty{ get{ return s_FilterPenalty == FilterPenalty.None; } }

		public static bool IrcEnabled
		{
			get{ return s_IrcEnabled; }
			set
			{
				s_IrcEnabled = value;
				if ( !value )
				{
					IrcConnection.Connection.CancelConnect();
					IrcConnection.Connection.Disconnect( false );
				}
			}
		}

		public static IrcColor IrcStaffColor
		{
			get{ return s_IrcStaffColor; }
			set
			{
				if ( (int)value > 15 )
					value = (IrcColor)0;

				if ( (int)value < 0 )
					value = (IrcColor)15;

				s_IrcStaffColor = value;
			}
		}

		public static ChatInfo GetInfo( Mobile m ){ return s_ChatInfos.Contains( m ) ? (ChatInfo)s_ChatInfos[m] : new ChatInfo( m ); }

		public static void Configure()
		{
			EventSink.WorldLoad += new WorldLoadEventHandler( OnLoad );
			EventSink.WorldSave += new WorldSaveEventHandler( OnSave );
		}

		public static void Initialize()
		{
			CheckBans();

			EventSink.Login += new LoginEventHandler( OnLogin );
		}

		private static void CheckBans()
		{try{

			foreach( ChatInfo info in ChatInfos.Values )
				if ( info.Banned && DateTime.Now > info.BanEnds )
					info.RemoveBan();

			}catch{ Errors.Report( "ChatInfo-> CheckBans" ); }

			Timer.DelayCall( TimeSpan.FromMinutes( 1 ), new TimerCallback( CheckBans ) );
		}

		private static void OnSave( WorldSaveEventArgs e )
		{try{

			if ( !Directory.Exists( "Saves/Chat/" ) )
				Directory.CreateDirectory( "Saves/Chat/" );

			GenericWriter writer = new BinaryFileWriter( Path.Combine( "Saves/Chat/", "Chat.bin" ), true );

			writer.Write( 12 ); // version

			// Version 12
			writer.Write( s_PublicPlusIRC );

			// Version 11
			writer.Write( (int)s_FilterPenalty );

			// Version 10
			writer.Write( s_AllianceChat );

			// Version 9
			writer.Write( s_AllowFaction );

			// Version 8
			writer.Write( s_GuildMenuAccess );

			// Version 7
			writer.Write( s_MaxPmHistory );

			// version 6
			writer.Write( s_IrcAutoConnect );

			// Version 5
			writer.Write( s_IrcMaxAttempts );

			// Version 4
			writer.Write( s_IrcAutoConnect );

			// Version 3
			writer.Write( (int)s_IrcStaffColor );

			// Version 2
			writer.Write( s_IrcNick );

			// Version 1
			writer.Write( s_IrcEnabled );
			writer.Write( s_IrcServer );
			writer.Write( s_IrcRoom );
			writer.Write( s_IrcPort );

			// Version 0
			writer.Write( s_Filters.Count );
			foreach( string str in s_Filters )
				writer.Write( str );

			writer.Write( s_SpamLimiter );
			writer.Write( s_FilterBanLength );
			writer.Write( s_ShowLocation );
			writer.Write( s_ShowStaff );
			writer.Write( (int)s_PublicStyle );

			ArrayList list = new ArrayList();
			foreach( ChatInfo info in s_ChatInfos.Values )
				if ( info.Mobile != null
				&& info.Mobile.Player
				&& !info.Mobile.Deleted
				&& info.Mobile.Account != null
				&& ((Account)info.Mobile.Account).LastLogin > DateTime.Now - TimeSpan.FromDays( 30 ) )
					list.Add( info );

			writer.Write( list.Count );
			foreach( ChatInfo info in new ArrayList( list ) )
			{
				writer.Write( info.Mobile );
				if ( !info.Save( writer ) )
					return;
			}

			writer.Close();

		}catch{ Errors.Report( "ChatInfo-> OnSave" ); } }

		private static void OnLoad()
		{try{

			if ( !File.Exists( Path.Combine( "Saves/Chat/", "Chat.bin" ) ) )
				return;

			using ( FileStream bin = new FileStream( Path.Combine( "Saves/Chat/", "Chat.bin" ), FileMode.Open, FileAccess.Read, FileShare.Read ) )
			{
				GenericReader reader = new BinaryFileReader( new BinaryReader( bin ) );

				int version = reader.ReadInt();

				if ( version >= 12 )
					s_PublicPlusIRC = reader.ReadBool();

				if ( version >= 11 )
					s_FilterPenalty = (FilterPenalty)reader.ReadInt();

				if ( version >= 10 )
					s_AllianceChat = reader.ReadBool();

				if ( version >= 9 )
					s_AllowFaction = reader.ReadBool();

				if ( version >= 8 )
					s_GuildMenuAccess = reader.ReadBool();

				if ( version >= 7 )
					s_MaxPmHistory = reader.ReadInt();

				if ( version >= 6 )
					s_IrcAutoReconnect = reader.ReadBool();

				if ( version >= 5 )
					s_IrcMaxAttempts = reader.ReadInt();

				if ( version >= 4 )
					s_IrcAutoConnect = reader.ReadBool();

				if ( version >= 3 )
					s_IrcStaffColor = (IrcColor)reader.ReadInt();

				if ( version >= 2 )
					s_IrcNick = reader.ReadString();

				if ( version >= 1 )
				{
					s_IrcEnabled = reader.ReadBool();
					s_IrcServer = reader.ReadString();
					s_IrcRoom = reader.ReadString();
					s_IrcPort = reader.ReadInt();
				}

				if ( version >= 0 )
				{
					int count = reader.ReadInt();

					for( int i = 0; i < count; ++i )
						s_Filters.Add( reader.ReadString() );

					s_SpamLimiter = reader.ReadDouble();
					s_FilterBanLength = reader.ReadDouble();

					if ( version < 11 )
						reader.ReadBool(); // FilterBan removed

					s_ShowLocation = reader.ReadBool();
					s_ShowStaff = reader.ReadBool();
					s_PublicStyle = (PublicStyle)reader.ReadInt();

					count = reader.ReadInt();
					ChatInfo info;

					for( int i = 0; i < count; ++i )
					{
						info = new ChatInfo( reader.ReadMobile() );
						if ( !info.Load( reader ) )
							return;
					}
				}

				reader.End();
			}

			if ( s_IrcAutoConnect )
				IrcConnection.Connection.Connect();

		}catch{ Errors.Report( "ChatInfo-> OnLoad" ); } }

		private static void OnLogin( LoginEventArgs e )
		{
			if ( GetInfo( e.Mobile ).Messages.Count != 0 )
				PmNotifyGump.SendTo( e.Mobile );
		}

		private Mobile c_Mobile;
		private ArrayList c_Friends, c_Ignores, c_GlobalIgnores, c_Messages, c_SavedMsgs;
		private Hashtable c_MsgSounds;
		private int c_PublicColor, c_GuildColor, c_FactionColor, c_StaffColor, c_PmColor, c_WorldColor, c_SystemColor, c_IrcColor, c_DefaultSound;
		private string c_Search, c_AwayMsg;
		private bool c_PublicDisabled, c_PmDisabled, c_PmFriends, c_PmReceipt, c_Hidden, c_Away, c_Banned, c_GlobalAccess, c_GlobalPm, c_GlobalWorld, c_GlobalGuild, c_GlobalFaction, c_Quickbar, c_IrcOn, c_IrcRaw, c_ShowTabs;
		private DateTime c_BanEnds;

		public Mobile Mobile{ get{ return c_Mobile; } }
		public ArrayList Friends{ get{ return c_Friends; } }
		public ArrayList Ignores{ get{ return c_Ignores; } }
		public ArrayList GlobalIgnores{ get{ return c_GlobalIgnores; } }
		public ArrayList Messages{ get{ return c_Messages; } }
		public ArrayList SavedMsgs{ get{ return c_SavedMsgs; } }
		public Hashtable MsgSounds{ get{ return c_MsgSounds; } }
		public int PublicColor{ get{ return c_PublicColor; } set{ c_PublicColor = value; } }
		public int GuildColor{ get{ return c_GuildColor; } set{ c_GuildColor = value; } }
		public int FactionColor{ get{ return c_FactionColor; } set{ c_FactionColor = value; } }
		public int StaffColor{ get{ return c_StaffColor; } set{ c_StaffColor = value; } }
		public int PmColor{ get{ return c_PmColor; } set{ c_PmColor = value; } }
		public int WorldColor{ get{ return c_WorldColor; } set{ c_WorldColor = value; } }
		public int SystemColor{ get{ return c_SystemColor; } set{ c_SystemColor = value; } }
		public int IrcColor{ get{ return c_IrcColor; } set{ c_IrcColor = value; } }
		public string Search{ get{ return c_Search; } set{ c_Search = value; } }
		public string AwayMsg{ get{ return c_AwayMsg; } set{ c_AwayMsg = value; } }
		public bool PmDisabled{ get{ return c_PmDisabled; } set{ c_PmDisabled = value; } }
		public bool PmFriends{ get{ return c_PmFriends; } set{ c_PmFriends = value; } }
		public bool PmReceipt{ get{ return c_PmReceipt; } set{ c_PmReceipt = value; } }
		public bool PublicDisabled{ get{ return c_PublicDisabled; } set{ c_PublicDisabled = value; } }
		public bool Hidden{ get{ return c_Hidden; } set{ c_Hidden = value; } }
		public bool Away{ get{ return c_Away; } set{ c_Away = value; } }
		public bool Banned{ get{ return c_Banned; } set{ c_Banned = value; } }
		public bool GlobalPm{ get{ return c_GlobalPm; } set{ c_GlobalPm = value; } }
		public bool GlobalWorld{ get{ return c_GlobalWorld; } set{ c_GlobalWorld = value; } }
		public bool GlobalGuild{ get{ return c_GlobalGuild; } set{ c_GlobalGuild = value; } }
		public bool GlobalFaction{ get{ return c_GlobalFaction; } set{ c_GlobalFaction = value; } }
		public bool Quickbar{ get{ return c_Quickbar; } set{ c_Quickbar = value; } }
		public bool IrcOn{ get{ return c_IrcOn || s_PublicPlusIRC; } set{ c_IrcOn = value; } }
		public bool IrcRaw{ get{ return c_IrcRaw; } set{ c_IrcRaw = value; } }
		public bool ShowTabs{ get{ return c_ShowTabs; } set{ c_ShowTabs = value; } }
		public DateTime BanEnds{ get{ return c_BanEnds; } }

		public bool HasMessages{ get{ return c_Messages.Count != 0; } }

		public bool GlobalAccess
		{ 
			get{ return c_GlobalAccess || c_Mobile.AccessLevel == AccessLevel.Administrator; }
			set
			{
				if ( !value )
				{
					c_GlobalPm = false;
					c_GlobalWorld = false;
					c_GlobalGuild = false;
				}

				c_GlobalAccess = value;
			}
		}

		public int DefaultSound
		{
			get{ return c_DefaultSound; }
			set
			{
				if( value < 0 )
					value = 0;
				c_DefaultSound = value;
			}
		}

		public ChatInfo( Mobile m )
		{
			c_Mobile = m;

			c_Friends = new ArrayList();
			c_Ignores = new ArrayList();
			c_GlobalIgnores = new ArrayList();
			c_Messages = new ArrayList();
			c_SavedMsgs = new ArrayList();
			c_MsgSounds = new Hashtable();
			
			c_PublicColor = 0x5B;
			c_GuildColor = 0x107;
			c_FactionColor = 0x15;
			c_StaffColor = 0x1BF;
			c_SystemColor = 0x99;
			c_PmColor = 0x26;
			c_WorldColor = 0x3CB;
			c_IrcColor = 0x310;

			c_Search = "";
			c_AwayMsg = "";

			c_ShowTabs = true;

			s_ChatInfos.Add( m, this );
		}

		public void AddFriend( Mobile m )
		{
			if ( c_Friends.Contains( m ) )
				return;

			c_Friends.Add( m );
			c_Mobile.SendMessage( c_SystemColor, "{0} is now on your friends list.", m.Name );
		}

		public void RemoveFriend( Mobile m )
		{
			if ( !c_Friends.Contains( m ) )
				return;

			c_Friends.Remove( m );
			c_Mobile.SendMessage( c_SystemColor, "{0} has been removed from your friends list.", m.Name );
		}

		public void AddIgnore( Mobile m )
		{
			if ( c_Ignores.Contains( m ) )
				return;

			c_Ignores.Add( m );
			c_Mobile.SendMessage( c_SystemColor, "{0} is now on your ignore list.", m.Name );

			foreach( Message msg in new ArrayList( c_Messages ) )
				if ( m == msg.LastSender )
					c_Messages.Remove( msg );
		}

		public void RemoveIgnore( Mobile m )
		{
			if ( !c_Ignores.Contains( m ) )
				return;

			c_Ignores.Remove( m );
			c_Mobile.SendMessage( c_SystemColor, "{0} has been removed from your ignore list.", m.Name );
		}

		public bool Ignoring( Mobile m )
		{
			return c_Ignores.Contains( m );
		}

		public void AddGlobalIgnore( Mobile m )
		{
			if ( c_GlobalIgnores.Contains( m ) )
				return;

			c_GlobalIgnores.Add( m );
			c_Mobile.SendMessage( c_SystemColor, "{0} is now on your global ignore list.", m.Name );
		}

		public void RemoveGlobalIgnore( Mobile m )
		{
			if ( !c_GlobalIgnores.Contains( m ) )
				return;

			c_GlobalIgnores.Remove( m );
			c_Mobile.SendMessage( c_SystemColor, "{0} has been removed from your global ignore list.", m.Name );
		}

		public bool GlobalIgnoring( Mobile m )
		{
			return c_GlobalIgnores.Contains( m );
		}

		public void Ban( double min ){ Ban( TimeSpan.FromMinutes( min ) ); }

		public void Ban( TimeSpan ts )
		{
			c_Banned = true;
			c_BanEnds = DateTime.Now + ts;
			c_Mobile.SendMessage( c_SystemColor, "You have been banned from chat." );
		}

		public void RemoveBan()
		{
			c_Banned = false;
			c_Mobile.SendMessage( c_SystemColor, "Your ban from chat has been lifted." );
		}

		public void AddMessage( Mobile from, Message msg )
		{
			c_Messages.Add( msg );
			PmNotifyGump.SendTo( c_Mobile );

			if ( GetSound( from ) != 0 )
				c_Mobile.SendSound( GetSound( from ) );

			if ( c_Away && c_AwayMsg != "" )
				from.SendMessage( GetInfo( from ).SystemColor, "{0}: {1}", c_Mobile.Name, c_AwayMsg );
		}

		public void RemoveMessage( Message msg )
		{
			c_Messages.Remove( msg );
		}

		public Message NextMessage()
		{
			if ( c_Messages.Count == 0 )
				return new Message();

			return (Message)c_Messages[0];
		}

		public Message GetNextMessage()
		{
			if ( c_Messages.Count == 0 )
				return new Message();

			Message msg = (Message)c_Messages[0];

			c_Messages.Remove( msg );

			if ( c_Messages.Count != 0 )
				PmNotifyGump.SendTo( c_Mobile );

			return msg;
		}

		public void DeleteMessage( Message msg )
		{
			c_Messages.Remove( msg );
			c_SavedMsgs.Remove( msg );

			c_Mobile.SendMessage( c_SystemColor, "You have deleted the message." );
		}

		public bool IsMail( Message msg ){ return c_Messages.Contains( msg ); }
		public bool IsSaved( Message msg ){ return c_SavedMsgs.Contains( msg ); }

		public void Save( Message msg )
		{
			Message m = new Message();
			m.History = new ArrayList( msg.History );
			m.Saved = true;

			c_SavedMsgs.Add( m );
			c_Mobile.SendMessage( c_SystemColor, "A copy of this message has been saved." );
		}

		public int GetSound( Mobile m )
		{

			if ( m == null )
				return c_DefaultSound;

			if ( c_MsgSounds[m] == null || (int)c_MsgSounds[m] == 0 )
				return c_DefaultSound;

			return (int)c_MsgSounds[m];
		}

		public void SetSound( Mobile m, int num )
		{
			if ( m == null )
				return;

			if ( num < 0 )
				num = 0;

			c_MsgSounds[m] = 0;
		}

		public bool Save( GenericWriter writer )
		{try{

			writer.Write( 10 ); // version


			// Version 10:  Changed the way messages are saved
			writer.Write( c_FactionColor );
			writer.Write( c_GlobalFaction );

			// Version 9:  Obsolete:  Transparency, Background, MenuColor, SubColor, TextBackground

			// Version 8
			writer.Write( c_AwayMsg );

			// Version 7
			writer.Write( c_SavedMsgs.Count );
			foreach( Message msg in new ArrayList( c_SavedMsgs ) )
			{
				if ( c_Mobile == null || msg.LastSender == null )
				{
					c_SavedMsgs.Remove( msg );
					continue;
				}

				msg.Save( writer );
			}

			// Version 6
			writer.Write( c_PmReceipt );

			// Version 5
			writer.Write( c_PmFriends );

			// Version 4
			writer.Write( c_DefaultSound );

			// Version 3
			writer.Write( c_IrcOn );
			writer.Write( c_IrcRaw );

			// Version 2
			writer.Write( c_IrcColor );

			// Version 1
			writer.Write( c_Quickbar );

			// Version 0
			writer.WriteMobileList( c_Friends, true );
			writer.WriteMobileList( c_Ignores, true );
			writer.WriteMobileList( c_GlobalIgnores, true );

			writer.Write( c_Messages.Count );
			foreach( Message msg in new ArrayList( c_Messages ) )
			{
				if ( c_Mobile == null || msg.LastSender == null )
				{
					c_Messages.Remove( msg );
					continue;
				}

				msg.Save( writer );
			}

			writer.Write( c_MsgSounds.Keys.Count );
			foreach( Mobile m in c_MsgSounds.Keys )
			{
				if ( m == null )
					continue;

				writer.Write( m );
				writer.Write( (int)c_MsgSounds[m] );
			}

			writer.Write( c_PublicColor );
			writer.Write( c_GuildColor );
			writer.Write( c_SystemColor );
			writer.Write( c_StaffColor );
			writer.Write( c_WorldColor );
			writer.Write( c_PmColor );

			writer.Write( c_PublicDisabled );
			writer.Write( c_PmDisabled );
			writer.Write( c_Hidden );
			writer.Write( c_Banned );
			writer.Write( c_GlobalAccess );
			writer.Write( c_GlobalWorld );
			writer.Write( c_GlobalGuild );
			writer.Write( c_GlobalPm );

			writer.Write( c_BanEnds );

			return true;

			}catch{ Errors.Report( String.Format( "ChatInfo-> Save-> |{0}|", c_Mobile ) ); }

			return false;
		}

		public bool Load( GenericReader reader )
		{try{

			int version = reader.ReadInt();

			// Version 10:  Changed the way messages are saved
			if ( version >= 10 )
			{
				c_FactionColor = reader.ReadInt();
				c_GlobalFaction = reader.ReadBool();
			}

			// Version 9:  Obsolete:  Transparency, Background, MenuColor, SubColor, TextBackground

			if ( version >= 8 )
				c_AwayMsg = reader.ReadString();

			if ( version >= 7 )
			{
				int count = reader.ReadInt();
				Message msg;

				if ( version >= 10 )
				{
					for( int i = 0; i < count; ++i )
					{
						msg = new Message();
						msg.Load( reader );
						c_SavedMsgs.Add( msg );
					}
				}
				else
				{
					int iCount = 0;

					for( int i = 0; i < count; ++i )
					{
						reader.ReadMobile();
						reader.ReadMobile();
						reader.ReadDateTime();
						iCount = reader.ReadInt();
						for( int ii = 0; ii < iCount; ++ii )
							reader.ReadString();
					}
				}
			}

			if ( version >= 6 )
				c_PmReceipt = reader.ReadBool();

			if ( version >= 5 )
				c_PmFriends = reader.ReadBool();

			if ( version >= 4 )
				c_DefaultSound = reader.ReadInt();

			if ( version >= 3 )
			{
				c_IrcOn = reader.ReadBool();
				c_IrcRaw = reader.ReadBool();
			}

			if ( version >= 2 )
				c_IrcColor = reader.ReadInt();

			if ( version >= 1 )
				c_Quickbar = reader.ReadBool();

			if ( version >= 0 )
			{
				c_Friends = reader.ReadMobileList();
				c_Ignores = reader.ReadMobileList();
				c_GlobalIgnores = reader.ReadMobileList();

				int count = reader.ReadInt();
				Message msg;

				if ( version >= 10 )
				{
					for( int i = 0; i < count; ++i )
					{
						msg = new Message();
						msg.Load( reader );
						AddMessage( msg.LastSender, msg );
					}
				}
				else
				{
					int iCount = 0;

					for( int i = 0; i < count; ++i )
					{
						reader.ReadMobile();
						reader.ReadMobile();
						reader.ReadDateTime();
						iCount = reader.ReadInt();
						for( int ii = 0; ii < iCount; ++ii )
							reader.ReadString();
					}
				}

				Mobile m;
				count = reader.ReadInt();

				for( int i = 0; i < count; ++i )
				{
					m = reader.ReadMobile();

					if ( m == null )
					{
						reader.ReadInt();
						continue;
					}

					c_MsgSounds[m] = reader.ReadInt();
				}

				c_PublicColor = reader.ReadInt();
				c_GuildColor = reader.ReadInt();
				c_SystemColor = reader.ReadInt();
				c_StaffColor = reader.ReadInt();
				c_WorldColor = reader.ReadInt();
				c_PmColor = reader.ReadInt();

				if ( version < 9 )
				{
					reader.ReadInt();
					reader.ReadInt();
					reader.ReadString();
				}

				c_PublicDisabled = reader.ReadBool();
				c_PmDisabled = reader.ReadBool();
				c_Hidden = reader.ReadBool();
				c_Banned = reader.ReadBool();

				if ( version < 9 )
					reader.ReadBool();

				c_GlobalAccess = reader.ReadBool();
				c_GlobalWorld = reader.ReadBool();
				c_GlobalGuild = reader.ReadBool();
				c_GlobalPm = reader.ReadBool();

				c_BanEnds = reader.ReadDateTime();
			}

			return true;

			}catch{ Errors.Report( String.Format( "ChatInfo-> Load-> |{0}|", c_Mobile ) ); }

			return false;
		}
	}
}