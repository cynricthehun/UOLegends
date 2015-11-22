using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.HuePickers;
using Knives.Utils;

namespace Knives.Chat
{
	public class OptionsGump : GumpPlus
	{
		public enum Page{ General, Pm, System, Filter, IRC }

		#region Help

		private static string s_GeneralHelp = "     On the left side you can change the many different colors within the chat system."
			+ "<BR><BR>     With <u>Public On</u> disabled, you will no longer be able to acess most of the chat features."
			+ "<BR><BR>     <u>Away</u> let's you leave a message to players who contant you while away."
			+ "<BR><BR>     The <u>Quickbar</u> works with the main menu listings, giving you more options readily available.";
		private static string s_StaffGeneral = "     Certain staff members also have access to more features here, highlighted in blue."
			+ "<BR><BR>     The <u>Global</u> features are enabled for staff members by administrators.  Each lets you listen to different communication on the shard.";
		private static string s_PmHelp = "     You have various options available for your Pming pleasure!"
			+ "<BR><BR>     Firstly, you can <u>Disallow Provate Messages</u> all together if you wish."
			+ "<BR><BR>     Setting <u>Only Friends</u> limits who can send you Pms, which also works in reverse.  if you aren't on someone's friend list, you can't Pm them either."
			+ "<BR><BR>     <u>Read Receipts</u> tell you when someone opens you messages, as long as they aren't staffers!"
			+ "<BR><BR>     Set a <u>Default Sound</u> for incoming messages.  You can also visit a player's profile to set specific sounds.";
		private static string s_StaffPm = "     Administrators may also change the maximum Pm history size here, defaulting to 10.  Warning:  the higher the limit, the longer saving will potentially take!";
		private static string s_SystemHelp = "     These options are only available to Administrators, and affect global aspects of the chat system."
			+ "<BR><BR>     Your <u>Public Chat</u> setting determines how players can interact with the [chat command.  <u>Full</u> means all messages are seen everywhere, where <u>Regional</u> is only in that player's specific region.  Naturally <u>None</u> removes the public chat capability."
			+ "<BR><BR>     Disabling <u>Show Staff</u> hides all staff members from players, including the Staff tab on the main menu."
			+ "<BR><BR>     <u>Show Location</u> will allow players to view player's regional information through their profile.  This is disabled by default."
			+ "<BR><BR>     Enabling <u>Guild Access</u> will let players access their guildstone menu from the guild menu."
			+ "<BR><BR>     Since <u>Faction Chat</u> already exists through the UO system, you may disable this version here if you wish!"
			+ "<BR><BR>     <u>Spam Limiter</u> lets you set how many seconds players must wait between chat messages.";
		private static string s_FilterHelp = "     Want to enforce strict language rules on your shard?  Enabling the filter ban penalizes violators for an amount of time you determine."
			+ "<BR><BR>     With <u>Filter Ban</u> enabled, you can have players instantly banned form the chat system for a duration you set in the option below it!"
			+ "<BR><BR>     <u>Adding and Removing Filters</u> is quite simple, just type in the word and submit.  If the word is already filtered, it will remove the filter.";
		private static string s_IrcHelp = "     Connect your entire shard to an IRC room!  This feature uses a single IRC connection for all your members, bypassing IRC server connection limits.  To connect, you will naturally need the IRC server info.  This System doesn't handle Nickname registration!  Use another IRC client for this."
			+ "<BR><BR>      In addition to the server info at the bottom, you can enable <u>Raw IRC</u>, which will show just you all information sent in and out of the IRC connection.  Useful for debugging!"
			+ "<BR><BR>      <u>Auto Connect</u>, when enabled, will automatically connect your shard to the IRC server specified at server start, attempting the number of times you note below."
			+ "<BR><BR>      <u>Auto Reconnect</u> works in the same fashion, but begins when your connection is abnormally terminated."
			+ "<BR><BR>      Finally, you may change the <u>Staff IRC Color</u> here, which will display in the actual IRC channel.";

		#endregion

		public static void SendTo( Mobile m )
		{
			new OptionsGump( m );
		}

		private const int Width = 300;
		private const int Height = 300;

		private ChatInfo c_Info;
		private Page c_Page;

		public OptionsGump( Mobile m ) : base( m, 100, 100 )
		{
			m.CloseGump( typeof( OptionsGump ) );
			c_Info = ChatInfo.GetInfo( m );
			NewGump();
		}

		protected override void BuildGump()
		{
			AddBackground( 0, 0, Width, Height, 0x13BE );

			BuildTabs();

			if ( c_Page == Page.General )
				General();
			else if ( c_Page == Page.Pm )
				Pm();
			else if ( c_Page == Page.System )
				System();
			else if ( c_Page == Page.Filter )
				Filter();
			else if ( c_Page == Page.IRC )
				IRC();

			AddButton( Width-20, Height-20, 0x5689, 0x5689, "Help", new TimerCallback( Help ) );
			AddButton( Width-40, Height-20, 0xFC1, 0xFC1, "About", new TimerCallback( About ) );
		}

		private void BuildTabs()
		{
			int x = 45;

			if ( Owner.AccessLevel != AccessLevel.Administrator )
				x = 95;

			if ( c_Page == Page.General )
				AddTemplateButton( x, Height-30, 70, Template.RedSquare, "General Page", HTML.Green + "<CENTER>General", new TimerStateCallback( ChangePage ), (int)Page.General, false );
			else
				AddTemplateButton( x, Height-30, 70, Template.RedSquare, "General Page", HTML.White + "<CENTER>General", new TimerStateCallback( ChangePage ), (int)Page.General );

			x+=40;

			if ( c_Page == Page.Pm )
				AddTemplateButton( x, Height-30, 70, Template.RedSquare, "Pm Page", HTML.Green + "<CENTER>Pm", new TimerStateCallback( ChangePage ), (int)Page.Pm, false );
			else
				AddTemplateButton( x, Height-30, 70, Template.RedSquare, "Pm Page", HTML.White + "<CENTER>Pm", new TimerStateCallback( ChangePage ), (int)Page.Pm );

			if ( Owner.AccessLevel != AccessLevel.Administrator )
				return;

			x+=40;

			if ( c_Page == Page.System )
				AddTemplateButton( x, Height-30, 70, Template.RedSquare, "System Page", HTML.Green + "<CENTER>System", new TimerStateCallback( ChangePage ), (int)Page.System, false );
			else
				AddTemplateButton( x, Height-30, 70, Template.RedSquare, "System Page", HTML.White + "<CENTER>System", new TimerStateCallback( ChangePage ), (int)Page.System );

			x+=40;

			if ( c_Page == Page.Filter )
				AddTemplateButton( x, Height-30, 70, Template.RedSquare, "Filter Page", HTML.Green + "<CENTER>Filter", new TimerStateCallback( ChangePage ), (int)Page.Filter, false );
			else
				AddTemplateButton( x, Height-30, 70, Template.RedSquare, "Filter Page", HTML.White + "<CENTER>Filter", new TimerStateCallback( ChangePage ), (int)Page.Filter );

			x+=40;

			if ( c_Page == Page.IRC )
				AddTemplateButton( x, Height-30, 70, Template.RedSquare, "IRC Page", HTML.Green + "<CENTER>IRC", new TimerStateCallback( ChangePage ), (int)Page.IRC, false );
			else
				AddTemplateButton( x, Height-30, 70, Template.RedSquare, "IRC Page", HTML.White + "<CENTER>IRC", new TimerStateCallback( ChangePage ), (int)Page.IRC );
		}

		private void General()
		{
			AddHtml( 0, 10, Width, 25, HTML.White + "<CENTER>General Options", false, false );
			AddBackground( 20, 30, Width-40, 3, 0x13BE );

			int x = 30;
			int y = 45;

			AddButton( x, y, 0x145E, 0x145E, "Public Color", new TimerStateCallback( Color ), 0 );
			AddLabel( x+25, y-1, c_Info.PublicColor, "Public" );

			if ( Owner.Guild != null || c_Info.GlobalAccess )
			{
				AddButton( x, y+=20, 0x145E, 0x145E, "Guild Color", new TimerStateCallback( Color ), 1 );
				AddLabel( x+25, y-1, c_Info.GuildColor, "Guild" );
			}

			if ( FactionChat.IsInFaction( Owner ) && ChatInfo.AllowFaction )
			{
				AddButton( x, y+=20, 0x145E, 0x145E, "Faction Color", new TimerStateCallback( Color ), 2 );
				AddLabel( x+25, y-1, c_Info.GuildColor, "Faction" );
			}

			AddButton( x, y+=20, 0x145E, 0x145E, "System Color", new TimerStateCallback( Color ), 3 );
			AddLabel( x+25, y-1, c_Info.SystemColor, "System" );

			if ( ChatInfo.IrcEnabled )
			{
				AddButton( x, y+=20, 0x145E, 0x145E, "IRC Color", new TimerStateCallback( Color ), 4 );
				AddLabel( x+25, y-1, c_Info.IrcColor, "IRC" );
			}

			if ( Owner.AccessLevel != AccessLevel.Player )
			{
				AddButton( x, y+=20, 0x145E, 0x145E, "StaffColor", new TimerStateCallback( Color ),5 );
				AddLabel( x+25, y-1, c_Info.StaffColor, "Staff" );
				AddItem( x-30, y+3, 0x186A );
			}

			if ( c_Info.GlobalAccess )
			{
				AddButton( x, y+=20, 0x145E, 0x145E, "World Color", new TimerStateCallback( Color ), 6 );
				AddLabel( x+25, y-1, c_Info.WorldColor, "World" );
				AddItem( x-30, y+3, 0x186A );

				AddButton( x, y+=20, 0x145E, 0x145E, "World Pm Color", new TimerStateCallback( Color ), 7 );
				AddLabel( x+25, y-1, c_Info.PmColor, "Pm" );
				AddItem( x-30, y+3, 0x186A );
			}

			x = Width/2+30;
			y = 45;

			if ( Owner.AccessLevel == AccessLevel.Player )
			{
				AddHtml( x, y, 90, 25, HTML.White + "<CENTER>Public On", false, false );
				AddButton( x-20, y, c_Info.PublicDisabled ? 0xD2 : 0xD3, c_Info.PublicDisabled ? 0xD2 : 0xD3, "Disable Public", new TimerCallback( PublicDisabled ) );
			}
			else
			{
				AddHtml( x, y, 90, 25, HTML.White + "<CENTER>Hidden", false, false );
				AddButton( x-20, y, c_Info.Hidden ? 0xD3 : 0xD2, c_Info.Hidden ? 0xD3 : 0xD2, "Hide", new TimerCallback( Hidden ) );
			}

			AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>Away", false, false );
			AddButton( x-20, y, c_Info.Away ? 0xD3 : 0xD2, c_Info.Away ? 0xD3 : 0xD2, "Away", new TimerCallback( Away ) );

			if ( ChatInfo.IrcEnabled && !ChatInfo.PublicPlusIRC )
			{
				AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>IRC On", false, false );
				AddButton( x-20, y, c_Info.IrcOn ? 0xD3 : 0xD2, c_Info.IrcOn ? 0xD3 : 0xD2, "Irc On", new TimerCallback( IrcOn ) );
			}

			AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>Show Quickbar", false, false );
			AddButton( x-20, y, c_Info.Quickbar ? 0xD3 : 0xD2, c_Info.Quickbar ? 0xD3 : 0xD2, "Quickbar", new TimerCallback( Quickbar ) );

			if ( c_Info.GlobalAccess )
			{
				AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>Global World", false, false );
				AddButton( x-20, y, c_Info.GlobalWorld ? 0xD3 : 0xD2, c_Info.GlobalWorld ? 0xD3 : 0xD2, "Global World", new TimerCallback( GlobalWorld ) );
				AddItem( x-50, y+3, 0x186A );

				AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>Global Pm", false, false );
				AddButton( x-20, y, c_Info.GlobalPm ? 0xD3 : 0xD2, c_Info.GlobalPm ? 0xD3 : 0xD2, "Global Pm", new TimerCallback( GlobalPm ) );
				AddItem( x-50, y+3, 0x186A );

				AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>Global Guild", false, false );
				AddButton( x-20, y, c_Info.GlobalGuild ? 0xD3 : 0xD2, c_Info.GlobalGuild ? 0xD3 : 0xD2, "Global Guild", new TimerCallback( GlobalGuild ) );
				AddItem( x-50, y+3, 0x186A );

				AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>Global Faction", false, false );
				AddButton( x-20, y, c_Info.GlobalFaction ? 0xD3 : 0xD2, c_Info.GlobalFaction ? 0xD3 : 0xD2, "Global Faction", new TimerCallback( GlobalFaction ) );
				AddItem( x-50, y+3, 0x186A );
			}
		}

		private void Pm()
		{
			AddHtml( 0, 10, Width, 25, HTML.White + "<CENTER>Pm Options", false, false );
			AddBackground( 20, 30, Width-40, 3, 0x13BE );

			int x = 50;
			int y = 45;

			AddHtml( x, y, 150, 25, HTML.White + "Allow Private Messages", false, false );
			AddButton( x-25, y, c_Info.PmDisabled ? 0xD2 : 0xD3, c_Info.PmDisabled ? 0xD2 : 0xD3, "Disable Pms", new TimerCallback( PmDisabled ) );

			if ( c_Info.PmDisabled )
				return;

			AddHtml( x, y+=25, 180, 25, HTML.White + "Only Friends can message you", false, false );
			AddButton( x-25, y, c_Info.PmFriends ? 0xD3 : 0xD2, c_Info.PmFriends ? 0xD3 : 0xD2, "Pm Friends", new TimerCallback( PmFriends ) );

			AddHtml( x, y+=25, 180, 25, HTML.White + "Message read receipt", false, false );
			AddButton( x-25, y, c_Info.PmReceipt ? 0xD3 : 0xD2, c_Info.PmReceipt ? 0xD3 : 0xD2, "Pm Receipt", new TimerCallback( PmReceipt ) );

			AddHtml( x+20, y+=30, 120, 25, HTML.White + "Default Sound:", false, false );
			AddImageTiled( x+120, y, 35, 21, 0xBBA );
			AddTextField( x+120, y, 35, 21, 0x480, 30, c_Info.DefaultSound.ToString() );
			AddButton( x+175, y+2, 0x15E1, 0x15E5, "Play Sound", new TimerCallback( PlaySound ) );
			AddButton( x+160, y, 0x983, 0x983, "Sound Up", new TimerCallback( SoundUp ) );
			AddButton( x+160, y+10, 0x985, 0x985, "Sound Down", new TimerCallback( SoundDown ) );

			if ( Owner.AccessLevel == AccessLevel.Administrator )
			{
				AddHtml( x+30, y+=30, 150, 25, HTML.White + "Max History Size:", false, false );
				AddItem( x, y+3, 0x186A );
				AddImageTiled( x+160, y, 25, 20, 0xBBA );
				AddTextField( x+160, y, 25, 20, 0x480, 31, ChatInfo.MaxPmHistory.ToString() );
				AddButton( x+160-15, y+3, 0x93A, 0x93A, "Max Pm History", new TimerCallback( MaxPmHistory ) );
			}
		}

		private void System()
		{
			AddHtml( 0, 10, Width, 25, HTML.White + "<CENTER>System Admin Options", false, false );
			AddItem( 0, 13, 0x186A );
			AddBackground( 20, 30, Width-40, 3, 0x13BE );

			AddHtml( 0, 50, Width/2, 25, HTML.White + "<CENTER>Public Chat", false, false );
			AddBackground( 40, 70, Width/2-80, 3, 0x13BE );

			int x = 50;
			int y = 75;

			AddHtml( x, y, 70, 25, HTML.White + "<CENTER>Full", false, false );
			AddButton( x-20, y, ChatInfo.Full ? 0xD3 : 0xD2, ChatInfo.Full ? 0xD3 : 0xD2, "Full Chat", new TimerStateCallback( PublicChat ), 0 );

			AddHtml( x, y+=20, 70, 25, HTML.White + "<CENTER>Regional", false, false );
			AddButton( x-20, y, ChatInfo.Regional ? 0xD3 : 0xD2, ChatInfo.Regional ? 0xD3 : 0xD2, "Regional Chat", new TimerStateCallback( PublicChat ), 1 );

			AddHtml( x, y+=20, 70, 25, HTML.White + "<CENTER>Off", false, false );
			AddButton( x-20, y, ChatInfo.NoPublic ? 0xD3 : 0xD2, ChatInfo.NoPublic ? 0xD3 : 0xD2, "No Public Chat", new TimerStateCallback( PublicChat ), 2 );

			AddHtml( Width/2, 50, Width/2, 25, HTML.White + "<CENTER>General", false, false );
			AddBackground( Width/2+40, 70, Width/2-80, 3, 0x13BE );

			y = 75;
			x = Width/2+40;

			AddHtml( x, y, 90, 25, HTML.White + "<CENTER>Show Staff", false, false );
			AddButton( x-30, y, ChatInfo.ShowStaff ? 0xD3 : 0xD2, ChatInfo.ShowStaff ? 0xD3 : 0xD2, "Show Staff", new TimerCallback( ShowStaff ) );

			AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>Show Location", false, false );
			AddButton( x-30, y, ChatInfo.ShowLocation ? 0xD3 : 0xD2, ChatInfo.ShowLocation ? 0xD3 : 0xD2, "Show Location", new TimerCallback( ShowLocation ) );

			AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>Guild Access", false, false );
			AddButton( x-30, y, ChatInfo.GuildMenuAccess ? 0xD3 : 0xD2, ChatInfo.GuildMenuAccess ? 0xD3 : 0xD2, "Guild Menu Access", new TimerCallback( GuildMenuAccess ) );

			AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>Allow Faction", false, false );
			AddButton( x-30, y, ChatInfo.AllowFaction ? 0xD3 : 0xD2, ChatInfo.AllowFaction ? 0xD3 : 0xD2, "Allow Faction", new TimerCallback( AllowFaction ) );

			AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>Alliance Chat", false, false );
			AddButton( x-30, y, ChatInfo.AllianceChat ? 0xD3 : 0xD2, ChatInfo.AllianceChat ? 0xD3 : 0xD2, "Alliance Chat", new TimerCallback( AllianceChat ) );

			AddHtml( x, y+=20, 90, 25, HTML.White + "<CENTER>Public + IRC", false, false );
			AddButton( x-30, y, ChatInfo.PublicPlusIRC ? 0xD3 : 0xD2, ChatInfo.PublicPlusIRC ? 0xD3 : 0xD2, "Public Plus IRC", new TimerCallback( PublicPlusIRC ) );

			x = Width/2+10;

			AddHtml( 0, y+=50, Width/2-10, 25, HTML.White + "<DIV ALIGN=RIGHT>Spam Limiter:", false, false );
			AddImageTiled( x, y, 25, 21, 0xBBA );
			AddTextField( x, y, 25, 21, 0x480, 0, ChatInfo.SpamLimiter.ToString() );
			AddHtml( x+25, y, 10, 25, HTML.White + "s", false, false );
			AddButton( Width/2-5, y+3, 0x93A, 0x93A, "Spam Limiter", new TimerCallback( SpamLimiter ) );
		}

		private void Filter()
		{try{

			int x = Width/2;
			int y = 10;

			AddHtml( 0, y, Width, 25, HTML.White + "<CENTER>Filter Options", false, false );
			AddItem( 0, y+3, 0x186A );
			AddBackground( 20, y+=20, Width-40, 3, 0x13BE );

			AddHtml( 0, y+=20, Width, 25, HTML.White + "<CENTER>Violation Penalty", false, false );
			AddHtml( 50, y+=25, 60, 25, HTML.White + "None", false, false );
			AddButton( 25, y, ChatInfo.NoFilterPenalty ? 0xD3 : 0xD2, ChatInfo.NoFilterPenalty ? 0xD3 : 0xD2, "Filter None", new TimerStateCallback( Filter ), (int)FilterPenalty.None );
			AddHtml( 110, y, 60, 25, HTML.White + "Ban", false, false );
			AddButton( 85, y, ChatInfo.FilterBan ? 0xD3 : 0xD2, ChatInfo.FilterBan ? 0xD3 : 0xD2, "Filter Ban", new TimerStateCallback( Filter ), (int)FilterPenalty.ChatBan );
			AddHtml( 170, y, 60, 25, HTML.White + "Kick", false, false );
			AddButton( 145, y, ChatInfo.FilterKick ? 0xD3 : 0xD2, ChatInfo.FilterKick ? 0xD3 : 0xD2, "Filter Kick", new TimerStateCallback( Filter ), (int)FilterPenalty.Kick );
			AddHtml( 230, y, 60, 25, HTML.White + "Criminal", false, false );
			AddButton( 205, y, ChatInfo.FilterCriminal ? 0xD3 : 0xD2, ChatInfo.FilterCriminal ? 0xD3 : 0xD2, "Filter Criminal", new TimerStateCallback( Filter ), (int)FilterPenalty.Criminal );

			if ( ChatInfo.FilterBan )
			{
				AddHtml( 0, y+=25, Width/2-10, 25, HTML.White + "<DIV ALIGN=RIGHT>Ban Time:", false, false );
				AddImageTiled( x+10, y, 25, 21, 0xBBA );
				AddTextField( x+10, y, 25, 21, 0x480, 10, ChatInfo.FilterBanLength.ToString() );
				AddHtml( x+35, y, 10, 25, HTML.White + "m", false, false );
				AddButton( Width/2-5, y+3, 0x93A, 0x93A, "Filter Ban Length", new TimerCallback( FilterBanLength ) );
			}

			AddHtml( 0, y+=30, Width/2-10, 25, HTML.White + "<DIV ALIGN=RIGHT>Filter Add/Remove:", false, false );
			AddImageTiled( x+10, y, 100, 21, 0xBBA );
			AddTextField( x+10, y, 100, 21, 0x480, 11, "" );
			AddButton( Width/2-5, y+3, 0x93A, 0x93A, "Add Filter", new TimerCallback( ModifyFiltered ) );

			string text = "";
			foreach( string str in ChatInfo.Filters )
			{
				if ( str == ChatInfo.Filters[0].ToString() )
					text += str;
				else
					text += ", " + str;
			}

			AddHtml( 20, y+=25, Width-40, Height-y-20, HTML.White + "Currently Filtered: " + text, false, false );

		}catch{ Errors.Report( String.Format( "OptionsGump-> Filter-> |{0}|", Owner ) ); } }

		private void IRC()
		{
			int y = 10;
			int x = Width/2;

			AddHtml( 0, y, Width, 25, HTML.White + "<CENTER>IRC Options", false, false );
			AddItem( 0, y+3, 0x186A );
			AddBackground( 20, y+=20, Width-40, 3, 0x13BE );

			AddHtml( 50, y+=20, 70, 25, HTML.White + "Enable IRC", false, false );
			AddButton( 20, y, ChatInfo.IrcEnabled ? 0xD3 : 0xD2, ChatInfo.IrcEnabled ? 0xD3 : 0xD2, "IRC Enabled", new TimerCallback( IrcEnabled ) );

			if ( ChatInfo.IrcEnabled )
			{
				AddHtml( x+30, y, 70, 25, HTML.White + "Raw IRC", false, false );
				AddButton( x, y, c_Info.IrcRaw ? 0xD3 : 0xD2, c_Info.IrcRaw ? 0xD3 : 0xD2, "IRC Raw", new TimerCallback( IrcRaw ) );

				AddHtml( 50, y+=25, 90, 25, HTML.White + "Auto Connect", false, false );
				AddButton( 20, y, ChatInfo.IrcAutoConnect ? 0xD3 : 0xD2, ChatInfo.IrcAutoConnect ? 0xD3 : 0xD2, "Auto Connect", new TimerCallback( IrcAutoConnect ) );

				AddHtml( x+30, y, 100, 25, HTML.White + "Auto Reconnect", false, false );
				AddButton( x, y, ChatInfo.IrcAutoReconnect ? 0xD3 : 0xD2, ChatInfo.IrcAutoReconnect ? 0xD3 : 0xD2, "Auto Reconnect", new TimerCallback( IrcAutoReconnect ) );

				AddHtml( 70, y+=25, Width, 25, HTML.White + "Connect Attempts:", false, false );
				AddImageTiled( x+60, y, 25, 20, 0xBBA );
				AddTextField( x+60, y, 25, 20, 0x480, 24, ChatInfo.IrcMaxAttempts.ToString() );
				AddButton( x+60-15, y+3, 0x93A, 0x93A, "Max Attempts", new TimerCallback( IrcMaxAttempts ) );

				AddHtml( 80, y+=25, 200, 25, HTML.White + "Staff Irc Color: " + ChatInfo.IrcStaffColor, false, false );
				AddButton( 65, y, 0x983, 0x983, "IRC Color Up", new TimerCallback( IrcColorUp ) );
				AddButton( 65, y+10, 0x985, 0x985, "IRC Color Down", new TimerCallback( IrcColorDown ) );

				AddHtml( 0, y+=25, Width/2-40, 25, HTML.White + "<DIV ALIGN=RIGHT>Server:", false, false );
				AddImageTiled( x-20, y, 130, 21, 0xBBA );
				AddTextField( x-20, y, 130, 21, 0x480, 20, ChatInfo.IrcServer );
				AddButton( x-37, y+3, 0x93A, 0x93A, "Irc Server", new TimerCallback( IrcServer ) );

				AddHtml( 0, y+=20, Width/2-40, 25, HTML.White + "<DIV ALIGN=RIGHT>Port:", false, false );
				AddImageTiled( x-20, y, 70, 21, 0xBBA );
				AddTextField( x-20, y, 70, 21, 0x480, 21, ChatInfo.IrcPort.ToString() );
				AddButton( x-37, y+3, 0x93A, 0x93A, "Irc Port", new TimerCallback( IrcPort ) );

				AddHtml( 0, y+=20, Width/2-40, 25, HTML.White + "<DIV ALIGN=RIGHT>Room:", false, false );
				AddImageTiled( x-20, y, 100, 21, 0xBBA );
				AddTextField( x-20, y, 100, 21, 0x480, 22, ChatInfo.IrcRoom );
				AddButton( x-37, y+3, 0x93A, 0x93A, "Irc Room", new TimerCallback( IrcRoom ) );

				AddHtml( 0, y+=20, Width/2-40, 25, HTML.White + "<DIV ALIGN=RIGHT>Nick:", false, false );
				AddImageTiled( x-20, y, 100, 21, 0xBBA );
				AddTextField( x-20, y, 100, 21, 0x480, 23, ChatInfo.IrcNick );
				AddButton( x-37, y+3, 0x93A, 0x93A, "Irc Nick", new TimerCallback( IrcNick ) );

				if ( !IrcConnection.Connection.Connecting )
					AddTemplateButton( Width/2-35, y+=30, 70, Template.RedSquare, "Connect", HTML.White + "<CENTER>" + (IrcConnection.Connection.Connected ? "Disconnect" : "Connect"), new TimerCallback( IrcConnect ) );
				else if ( IrcConnection.Connection.Connecting && IrcConnection.Connection.HasMoreAttempts )
					AddTemplateButton( Width/2-35, y+=30, 70, Template.RedSquare, "Cancel Connect", HTML.White + "<CENTER>Cancel", new TimerCallback( IrcCancelConnect ) );
			}
		}

		private void Help()
		{
			NewGump();

			string str = "";

			switch( c_Page )
			{
				case Page.General:
					str = s_GeneralHelp + (Owner.AccessLevel != AccessLevel.Player ? "<BR><BR>" + s_StaffGeneral : "");
					break;
				case Page.Pm:
					str = s_PmHelp+ (Owner.AccessLevel != AccessLevel.Player ? "<BR><BR>" + s_StaffPm : "");
					break;
				case Page.System:
					str = s_SystemHelp;
					break;
				case Page.Filter:
					str = s_FilterHelp;
					break;
				case Page.IRC:
					str = s_IrcHelp;
					break;
			}

			InfoGump.SendTo( Owner, 300, 300, HTML.White + str, true );
		}

		private void ChangePage( object obj )
		{
			if ( !(obj is int) )
				return;

			c_Page = (Page)obj;
			NewGump();
		}

		private void Color( object obj )
		{
			if ( !(obj is int) )
				return;

			switch( (int)obj )
			{
				case 0:
					Owner.SendHuePicker( new InternalPicker( new TimerStateCallback( PublicColor ) ) );
					break;
				case 1:
					Owner.SendHuePicker( new InternalPicker( new TimerStateCallback( GuildColor ) ) );
					break;
				case 2:
					Owner.SendHuePicker( new InternalPicker( new TimerStateCallback( FactionColor ) ) );
					break;
				case 3:
					Owner.SendHuePicker( new InternalPicker( new TimerStateCallback( SystemColor ) ) );
					break;
				case 4:
					Owner.SendHuePicker( new InternalPicker( new TimerStateCallback( IrcColor ) ) );
					break;
				case 5:
					Owner.SendHuePicker( new InternalPicker( new TimerStateCallback( StaffColor ) ) );
					break;
				case 6:
					Owner.SendHuePicker( new InternalPicker( new TimerStateCallback( WorldColor ) ) );
					break;
				case 7:
					Owner.SendHuePicker( new InternalPicker( new TimerStateCallback( PmColor ) ) );
					break;
			}
		}

		private void About()
		{
			NewGump();
			InfoGump.SendTo( Owner, 200, 170, String.Format( "<CENTER>Version: {0}<BR>Release Date: {1}<BR>Author: Knives<BR>kmwill23@hotmail.com<BR><BR>Please provide suggestions and report bugs!", ChatInfo.Version, ChatInfo.ReleaseDate.ToShortDateString() ), false );
		}

		private void PublicColor( object obj )
		{
			if ( !(obj is int) )
				return;

			c_Info.PublicColor = (int)obj;

			NewGump();
		}

		private void GuildColor( object obj )
		{
			if ( !(obj is int) )
				return;

			c_Info.GuildColor = (int)obj;

			NewGump();
		}

		private void FactionColor( object obj )
		{
			if ( !(obj is int) )
				return;

			c_Info.FactionColor = (int)obj;

			NewGump();
		}

		private void SystemColor( object obj )
		{
			if ( !(obj is int) )
				return;

			c_Info.SystemColor = (int)obj;

			NewGump();
		}

		private void StaffColor( object obj )
		{
			if ( !(obj is int) )
				return;

			c_Info.StaffColor = (int)obj;

			NewGump();
		}

		private void WorldColor( object obj )
		{
			if ( !(obj is int) )
				return;

			c_Info.WorldColor = (int)obj;

			NewGump();
		}

		private void PmColor( object obj )
		{
			if ( !(obj is int ) )
				return;

			c_Info.PmColor = (int)obj;

			NewGump();
		}

		private void IrcColor( object obj )
		{
			if ( !(obj is int ) )
				return;

			c_Info.IrcColor = (int)obj;

			NewGump();
		}

		private void PlaySound()
		{
			c_Info.DefaultSound = Utility.ToInt32( GetTextField( 30 ) );

			if ( c_Info.DefaultSound != 0 )
				Owner.SendSound( c_Info.DefaultSound );

			NewGump();
		}

		private void SoundUp()
		{
			c_Info.DefaultSound = c_Info.DefaultSound+1;
			if ( c_Info.DefaultSound != 0 )
				Owner.SendSound( c_Info.DefaultSound );

			NewGump();
		}

		private void SoundDown()
		{
			c_Info.DefaultSound = c_Info.DefaultSound-1;
			if ( c_Info.DefaultSound != 0 )
				Owner.SendSound( c_Info.DefaultSound );

			NewGump();
		}

		private void MaxPmHistory()
		{
			ChatInfo.MaxPmHistory = Utility.ToInt32( GetTextField( 31 ) );

			NewGump();
		}

		private void PublicDisabled()
		{
			c_Info.PublicDisabled = !c_Info.PublicDisabled;
			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void Hidden()
		{
			c_Info.Hidden = !c_Info.Hidden;
			NewGump();
		}

		private void Away()
		{
			c_Info.Away = !c_Info.Away;

			Owner.CloseGump( typeof( ListGump ), -5 );

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

		private void IrcOn()
		{
			c_Info.IrcOn = !c_Info.IrcOn;
			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void Quickbar()
		{
			c_Info.Quickbar = !c_Info.Quickbar;
			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void GlobalWorld()
		{
			c_Info.GlobalWorld = !c_Info.GlobalWorld;
			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void GlobalPm()
		{
			c_Info.GlobalPm = !c_Info.GlobalPm;
			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void GlobalGuild()
		{
			c_Info.GlobalGuild = !c_Info.GlobalGuild;
			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void GlobalFaction()
		{
			c_Info.GlobalFaction = !c_Info.GlobalFaction;
			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void PmDisabled()
		{
			c_Info.PmDisabled = !c_Info.PmDisabled;
			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void PmFriends()
		{
			c_Info.PmFriends = !c_Info.PmFriends;
			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void PmReceipt()
		{
			c_Info.PmReceipt = !c_Info.PmReceipt;
			NewGump();
		}

		private void PublicChat( object obj )
		{
			if ( !(obj is int) )
				return;

			ChatInfo.Style = (ChatInfo.PublicStyle)obj;
			NewGump();
		}

		private void ShowStaff()
		{
			ChatInfo.ShowStaff = !ChatInfo.ShowStaff;

			NewGump();
		}

		private void GuildMenuAccess()
		{
			ChatInfo.GuildMenuAccess = !ChatInfo.GuildMenuAccess;

			NewGump();
		}

		private void AllowFaction()
		{
			ChatInfo.AllowFaction = !ChatInfo.AllowFaction;

			NewGump();
		}

		private void AllianceChat()
		{
			ChatInfo.AllianceChat = !ChatInfo.AllianceChat;

			NewGump();
		}

		private void PublicPlusIRC()
		{
			ChatInfo.PublicPlusIRC = !ChatInfo.PublicPlusIRC;

			NewGump();
		}

		private void ShowLocation()
		{
			ChatInfo.ShowLocation = !ChatInfo.ShowLocation;
			NewGump();
		}

		private void SpamLimiter()
		{
			ChatInfo.SpamLimiter = (double)Utility.ToInt32( GetTextField( 0 ) );
			NewGump();
		}

		private void FilterBan()
		{
		//	ChatInfo.FilterBan = !ChatInfo.FilterBan;
			NewGump();
		}

		private void Filter( object obj )
		{
			if ( !(obj is int ) )
				return;

			ChatInfo.FilterPenalty = (FilterPenalty)obj;
			NewGump();
		}

		private void FilterBanLength()
		{
			ChatInfo.FilterBanLength = (double)Utility.ToInt32( GetTextField( 10 ) );
			NewGump();
		}

		private void ModifyFiltered()
		{
			string text = GetTextField( 11 );

			if ( text != "" )
			{
				if ( ChatInfo.Filters.Contains( text ) )
					ChatInfo.Filters.Remove( text );
				else
					ChatInfo.Filters.Add( text );
			}

			NewGump();
		}

		private void IrcEnabled()
		{
			ChatInfo.IrcEnabled = !ChatInfo.IrcEnabled;
			NewGump();
		}

		private void IrcRaw()
		{
			c_Info.IrcRaw = !c_Info.IrcRaw;
			NewGump();
		}

		private void IrcAutoConnect()
		{
			ChatInfo.IrcAutoConnect = !ChatInfo.IrcAutoConnect;
			NewGump();
		}

		private void IrcAutoReconnect()
		{
			ChatInfo.IrcAutoReconnect = !ChatInfo.IrcAutoReconnect;
			NewGump();
		}

		private void IrcMaxAttempts()
		{
			ChatInfo.IrcMaxAttempts = Utility.ToInt32( GetTextField( 24 ) );

			if ( ChatInfo.IrcMaxAttempts < 1 )
				ChatInfo.IrcMaxAttempts = 1;

			NewGump();
		}

		private void IrcColorUp()
		{
			ChatInfo.IrcStaffColor = ChatInfo.IrcStaffColor + 1;
			NewGump();
		}


		private void IrcColorDown()
		{
			ChatInfo.IrcStaffColor = ChatInfo.IrcStaffColor - 1;
			NewGump();
		}

		private void IrcServer()
		{
			ChatInfo.IrcServer = GetTextField( 20 );

			if ( IrcConnection.Connection.Connected || IrcConnection.Connection.Connecting )
				Owner.SendMessage( c_Info.SystemColor, "This change will not take effect until a new Irc connection is made." );

			NewGump();
		}

		private void IrcPort()
		{
			ChatInfo.IrcPort = Utility.ToInt32( GetTextField( 21 ) );

			if ( IrcConnection.Connection.Connected || IrcConnection.Connection.Connecting )
				Owner.SendMessage( c_Info.SystemColor, "This change will not take effect until a new Irc connection is made." );

			NewGump();
		}

		private void IrcRoom()
		{
			string text = GetTextField( 22 );

			if ( !text.StartsWith( "#" ) )
				text = "#" + text;

			if ( ChatInfo.IrcRoom.ToLower() == text.ToLower() )
			{
				NewGump();
				return;
			}

			ChatInfo.IrcRoom = text;

			if ( IrcConnection.Connection.Connected || IrcConnection.Connection.Connecting )
			{
				IrcConnection.Connection.SendMessage( "JOIN " + text );
				Owner.SendMessage( c_Info.SystemColor, "Changing rooms." );
			}

			NewGump();
		}

		private void IrcNick()
		{

			if ( ChatInfo.IrcNick == GetTextField( 23 ) )
			{
				NewGump();
				return;
			}

			ChatInfo.IrcNick = GetTextField( 23 );

			if ( IrcConnection.Connection.Connected || IrcConnection.Connection.Connecting )
			{
				IrcConnection.Connection.SendMessage( "NICK " + ChatInfo.IrcNick );
				Owner.SendMessage( c_Info.SystemColor, "Server Nick changed to {0}.", ChatInfo.IrcNick );
			}

			NewGump();
		}

		private void IrcConnect()
		{
			ChatInfo.IrcServer = GetTextField( 20 );
			ChatInfo.IrcPort = Utility.ToInt32( GetTextField( 21 ) );
			ChatInfo.IrcRoom = GetTextField( 22 );
			ChatInfo.IrcNick = GetTextField( 23 );

			if ( !ChatInfo.IrcRoom.StartsWith( "#" ) )
				ChatInfo.IrcRoom = "#" + ChatInfo.IrcRoom;

			if ( IrcConnection.Connection.Connected )
				IrcConnection.Connection.Disconnect( false );
			else
				IrcConnection.Connection.Connect( Owner );

			Timer.DelayCall( TimeSpan.FromSeconds( 2 ), new TimerCallback( NewGump ) );

		}

		private void IrcCancelConnect()
		{
			IrcConnection.Connection.CancelConnect();
			NewGump();
		}


		private class InternalPicker : HuePicker
		{
			private TimerStateCallback c_Callback;

			public InternalPicker( TimerStateCallback callback ) : base( 0x1018 )
			{
				c_Callback = callback;
			}

			public override void OnResponse( int hue )
			{
				c_Callback.Invoke( hue );
			}
		}
	}
}