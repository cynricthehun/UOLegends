using System;
using Server;
using Knives.Utils;

namespace Knives.Chat
{
	public class PublicChat
	{
		public static void Initialize()
		{
			Server.Commands.Register( "Chat", AccessLevel.Player, new CommandEventHandler( OnChat ) );
			Server.Commands.Register( "C", AccessLevel.Player, new CommandEventHandler( OnChat ) );
		}

		public static bool CanChat( ChatInfo info ){ return CanChat( info, false ); }

		public static bool CanChat( ChatInfo info, bool action )
		{
			if ( info.Banned )
			{
				if ( action )
					info.Mobile.SendMessage( info.SystemColor, "You are banned from chat." );

				return false;
			}

			if ( info.PublicDisabled )
			{
				if ( action )
				{
					OptionsGump.SendTo( info.Mobile );
					info.Mobile.SendMessage( info.SystemColor, "You must first enable public chat." );
				}

				return false;
			}

			return true;
		}

		public static void OnChat( CommandEventArgs e )
		{
			OnChat( e, true );
		}

		private static void Queued( object obj )
		{
			if ( !(obj is CommandEventArgs) )
				return;

			OnChat( (CommandEventArgs)obj, false );
		}

		public static void OnChat( CommandEventArgs e, bool spammsg )
		{try{

			ChatInfo info = ChatInfo.GetInfo( e.Mobile );

			if ( !CanChat( info, true ) )
				return;

			if ( e.ArgString == null || e.ArgString == "" || ChatInfo.NoPublic )
				ListGump.SendTo( e.Mobile, Listing.Public );
			else if ( ChatInfo.Regional && ( e.Mobile.Region == null || e.Mobile.Region.Name == "" ) )
				e.Mobile.SendMessage( info.SystemColor, "You are not in a region." );
			else if ( !TrackSpam.LogSpam( e.Mobile, "chat", ChatInfo.SpamLimiter ) )
			{
				Timer.DelayCall( TrackSpam.NextAllowedIn( e.Mobile, "chat", ChatInfo.SpamLimiter ), new TimerStateCallback( Queued ), e );
				if ( spammsg )
					e.Mobile.SendMessage( info.SystemColor, "Message queued.  Please wait {0} seconds between messages.", ChatInfo.SpamLimiter );
			}
			else
			{
				string text = Filter.FilterText( e.Mobile, e.ArgString );
				if ( text == "" )
					return;

				foreach( ChatInfo ci in ChatInfo.ChatInfos.Values )
				{
					if ( ci.Mobile == null || ci.Mobile.NetState == null )
						continue;

					if ( CanChat( ci ) && !ci.Ignoring( info.Mobile ) )
					{
						if ( ci.Mobile.AccessLevel == AccessLevel.Player
						&& ChatInfo.Regional
						&& info.Mobile.Region != ci.Mobile.Region )
							continue;

						ci.Mobile.SendMessage( e.Mobile.AccessLevel == AccessLevel.Player ? ci.PublicColor : info.StaffColor, "<{0}> {1}: {2}", ChatInfo.Regional ? e.Mobile.Region.Name : "Public", e.Mobile.Name, text );
					}
				}

				if ( ChatInfo.PublicPlusIRC )
					IrcConnection.Connection.SendUserMessage( e.Mobile, e.ArgString );
			}

		}catch{ Errors.Report( String.Format( "PublicChat-> OnChat-> |{0}|", e.Mobile ) ); } }
	}
}