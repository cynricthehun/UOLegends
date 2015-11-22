using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;
using Knives.Utils;

namespace Knives.Chat
{
	public class ProfileGump : GumpPlus
	{
		private static string s_Help = "     A player's profile reveals a bit of info about them, along with buttons to add them to your friends, ignore them, and send them messages.  The second page of the profile show's that player's profile as they have written in their paperdoll profile!";
		private static string s_StaffHelp = "     In addition, staff members can also goto, view the client of, and ban the player from chat.  Aministrators can grant staff members global access through their profiles.";

		public static void SendTo( Mobile source, Mobile target )
		{
			new ProfileGump( source, target );
		}

		private const int Width = 300;
		private const int Height = 200;

		private Mobile c_Target;
		private ChatInfo c_Info;

		public ProfileGump( Mobile m, Mobile target ) : base( m, 100, 100 )
		{
			c_Target = target;
			c_Info = ChatInfo.GetInfo( Owner );

			NewGump();
		}

		protected override void BuildGump()
		{try{

			Owner.CloseGump( typeof( ProfileGump ) );

			AddPage( 0 );

			ChatInfo otherInfo = ChatInfo.GetInfo( c_Target );

			AddBackground( 0, 0, Width, Height, 0x13BE );

			AddButton( Width-20, Height-20, 0x5689, 0x5689, "Help", new TimerCallback( Help ) );

			AddPage( 1 );

			string text = Server.Misc.Titles.ComputeTitle( Owner, c_Target );

			if ( c_Target.AccessLevel != AccessLevel.Player )
				text += "<BR>" + c_Target.AccessLevel;
			else if ( c_Target.Guild != null )
				text += "<BR>[" + c_Target.Guild.Abbreviation + "] " + c_Target.GuildTitle;

			if ( Owner.AccessLevel > c_Target.AccessLevel
			|| ( ChatInfo.ShowLocation && Owner.AccessLevel == c_Target.AccessLevel ) )
				if ( c_Target.NetState != null )
				{
					text += "<BR>" + c_Target.Map.Name;

					if ( c_Target.Region != null && c_Target.Region.Name != "" )
						text += ", " + c_Target.Region.Name;
				}

			AddHtml( 0, 10, Width, 65, HTML.White + "<CENTER>" + text, false, false );

			AddBackground( 20, 65, Width-40, 3, 0x13BE );

			int y = 90;
			int x = 40;

			if ( c_Target.AccessLevel >= Owner.AccessLevel )
				x = Width/2-35;

			if ( Pm.CanPm( c_Info, otherInfo ) )
				AddTemplateButton( x, y, 70, Template.RedSquare, "New Message", HTML.White + "<CENTER>Message", new TimerCallback( Message ) );

			AddTemplateButton( x, y+20, 70, Template.RedSquare, "Friend", HTML.White + (c_Info.Friends.Contains( c_Target ) ? "<CENTER>Unfriend" : "<CENTER>Friend"), new TimerStateCallback( Friend ), c_Info.Friends.Contains( c_Target ) );

			if ( Owner.AccessLevel >= c_Target.AccessLevel )
				AddTemplateButton( x, y+40, 70, Template.RedSquare, "Ignore", HTML.White + (c_Info.Ignores.Contains( c_Target ) ? "<CENTER>Unignore" : "<CENTER>Ignore"), new TimerStateCallback( Ignore ), c_Info.Ignores.Contains( c_Target ) );

			AddHtml( x-10, y+65, 70, 25, HTML.White + "Pm Sound:", false, false );
			AddImageTiled( x+65, y+65, 35, 21, 0xBBA );
			AddTextField( x+65, y+65, 35, 21, 0x480, 0, c_Info.GetSound( c_Target ).ToString() );
			AddButton( x+100, y+67, 0x15E1, 0x15E5, "Play Sound", new TimerCallback( PlaySound ) );
			AddButton( x+55, y+65, 0x983, 0x983, "Sound Up", new TimerCallback( SoundUp ) );
			AddButton( x+55, y+75, 0x985, 0x985, "Sound Down", new TimerCallback( SoundDown ) );

			if ( Owner.AccessLevel > c_Target.AccessLevel )
			{
				x = Width-110;

				if ( c_Target.NetState != null )
				{
					AddTemplateButton( x, y, 70, Template.RedSquare, "Client", HTML.White + "<CENTER>Client", new TimerCallback( Client ) );
					AddItem( x-30, y+3, 0x186A );

					AddTemplateButton( x, y+20, 70, Template.RedSquare, "Goto", HTML.White + "<CENTER>Goto", new TimerCallback( Goto ) );
					AddItem( x-30, y+23, 0x186A );
				}

				if ( c_Info.GlobalAccess )
				{
					AddTemplateButton( x, y+60, 70, Template.RedSquare, "Global Ignore", HTML.White + (c_Info.GlobalIgnores.Contains( c_Target ) ? "<CENTER>Global Unignore" : "<CENTER>Global Ignore"), new TimerStateCallback( IgnoreGlobal ), c_Info.GlobalIgnores.Contains( c_Target ) );
					AddItem( x-30, y+63, 0x186A );
				}

				if ( c_Target.AccessLevel == AccessLevel.Player )
				{
					AddTemplateButton( x, y+40, 70, Template.RedSquare, "Ban", HTML.White + (otherInfo.Banned ? "<CENTER>Unban" : "<CENTER>Ban"), otherInfo.Banned ? new TimerCallback( Unban ) : new TimerCallback( Ban ) );
					AddItem( x-30, y+43, 0x186A );
				}
				else if ( Owner.AccessLevel == AccessLevel.Administrator )
				{
					AddTemplateButton( x, y+40, 70, Template.RedSquare, "Global Priv", HTML.White + (otherInfo.GlobalAccess ? "<CENTER>Revoke Global" : "<CENTER>Give Global"), new TimerStateCallback( GlobalAccess ), otherInfo.GlobalAccess );
					AddItem( x-30, y+43, 0x186A );
				}
			}

			AddButton( Width-20, Height/2-10, 0x15E1, 0x15E5, 0, GumpButtonType.Page, 2 );

			AddPage( 2 );

			AddButton( 0, Height/2-10, 0x15E3, 0x15E7, 0, GumpButtonType.Page, 1 );

			AddHtml( 0, 10, Width, 25, "<CENTER>" + HTML.White + c_Target.Name + "'s Profile", false, false );
			AddHtml( 20, 30, Width-40, Height-50, c_Target.Profile, true, true );

		}catch{ Errors.Report( String.Format( "ProfileGump-> BuildGump-> |{0}|-> |{1}|", Owner, c_Target ) ); } }

		private void Help()
		{
			NewGump();
			InfoGump.SendTo( Owner, 300, 300, s_Help + (Owner.AccessLevel != AccessLevel.Player ? "<BR><BR>" + s_StaffHelp : ""), true );
		}

		private void Message()
		{
			NewGump();
			PmGump.SendTo( Owner, c_Target );
		}

		private void Friend( object obj )
		{
			if ( !(obj is bool) )
				return;

			if ( (bool)obj )
				c_Info.RemoveFriend( c_Target );
			else
				c_Info.AddFriend( c_Target );

			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void Ignore( object obj )
		{
			if ( !(obj is bool) )
				return;

			if ( (bool)obj )
				c_Info.RemoveIgnore( c_Target );
			else
				c_Info.AddIgnore( c_Target );

			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void PlaySound()
		{
			c_Info.SetSound( c_Target, Utility.ToInt32( GetTextField( 0 ) ) );
			if ( c_Info.GetSound( c_Target ) != 0 )
				Owner.SendSound( c_Info.GetSound( c_Target ) );

			NewGump();
		}

		private void SoundUp()
		{
			c_Info.SetSound( c_Target, c_Info.GetSound( c_Target )+1 );
			if ( c_Info.GetSound( c_Target ) != 0 )
				Owner.SendSound( c_Info.GetSound( c_Target ) );

			NewGump();
		}

		private void SoundDown()
		{
			c_Info.SetSound( c_Target, c_Info.GetSound( c_Target )-1 );
			if ( c_Info.GetSound( c_Target ) != 0 )
				Owner.SendSound( c_Info.GetSound( c_Target ) );

			NewGump();
		}

		private void Client()
		{
			NewGump();

			if ( c_Target.NetState == null )
				Owner.SendMessage( "{0} is no longer online.", c_Target.Name );
			else
				Owner.SendGump( new ClientGump( Owner, c_Target.NetState ) );
		}

		private void Goto()
		{try{

			NewGump();

			if ( c_Target.NetState == null )
				Owner.SendMessage( "{0} is no longer online.", c_Target.Name );
			else
			{
				Owner.Location = c_Target.Location;
				Owner.Map = c_Target.Map;
			}

		}catch{ Errors.Report( String.Format( "ProfileGump-> Goto-> |{0}|-> |{1}|", Owner, c_Target ) ); } }

		private void Ban()
		{
			Hashtable table = new Hashtable();

			table.Add( TimeSpan.FromMinutes( 5 ), "Five Minutes" );
			table.Add( TimeSpan.FromMinutes( 30 ), "Thirty Minutes" );
			table.Add( TimeSpan.FromHours( 5 ), "Five Hours" );
			table.Add( TimeSpan.FromDays( 1 ), "One Day" );
			table.Add( TimeSpan.FromDays( 7 ), "One Week" );
			table.Add( TimeSpan.FromDays( 30 ), "One Month" );
			table.Add( TimeSpan.FromDays( 365 ), "One Year" );

			ChoiceGump.SendTo( Owner, "Select Duration", 200, new TimerStateCallback( BanCallback ), table );
		}

		private void BanCallback( object obj )
		{
			if ( !(obj is TimeSpan) )
				return;

			ChatInfo.GetInfo( c_Target ).Ban( (TimeSpan)obj );

			NewGump();
		}

		private void Unban()
		{
			ChatInfo.GetInfo( c_Target ).RemoveBan();

			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}

		private void GlobalAccess( object obj )
		{
			if ( !(obj is bool) )
				return;

			ChatInfo.GetInfo( c_Target ).GlobalAccess = !(bool)obj;
			NewGump();
		}

		private void IgnoreGlobal( object obj )
		{
			if ( !(obj is bool) )
				return;

			if ( (bool)obj )
				c_Info.RemoveGlobalIgnore( c_Target );
			else
				c_Info.AddGlobalIgnore( c_Target );

			Owner.CloseGump( typeof( ListGump ), -5 );
			NewGump();
		}
	}
}