using System;
using Server;
using Server.Guilds;
using Knives.Utils;

namespace Knives.Chat
{
	public class GuildChat
	{
		public static void Initialize()
		{
			Server.Commands.Register( "Guild", AccessLevel.Player, new CommandEventHandler( OnChat ) );
			Server.Commands.Register( "G", AccessLevel.Player, new CommandEventHandler( OnChat ) );
		}

		public static bool CanChat( ChatInfo info ){ return CanChat( info, false ); }

		public static bool CanChat( ChatInfo info, bool action )
		{
			if ( info.Mobile.Guild == null )
			{
				if ( action )
					info.Mobile.SendMessage( info.SystemColor, "You are not in a guild." );

				return false;
			}

			if ( info.Banned )
			{
				if ( action )
					info.Mobile.SendMessage( info.SystemColor, "You are banned from chat." );

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

		private static void OnChat( CommandEventArgs e, bool spammsg )
		{try{

			ChatInfo info = ChatInfo.GetInfo( e.Mobile );

			if ( !CanChat( info, true ) )
				return;

			if ( e.ArgString == null || e.ArgString == "" )
				ListGump.SendTo( e.Mobile, Listing.Guild );
			else if ( !TrackSpam.LogSpam( e.Mobile, "chat", ChatInfo.SpamLimiter ) )
			{
				Timer.DelayCall( TrackSpam.NextAllowedIn( e.Mobile, "chat", ChatInfo.SpamLimiter ), new TimerStateCallback( Queued ), e );
				if ( spammsg )
					e.Mobile.SendMessage( info.SystemColor, "Message queued.  Please wait {0} seconds between messages.", ChatInfo.SpamLimiter );
			}
			else
			{
				foreach( ChatInfo ci in ChatInfo.ChatInfos.Values )
				{
					if ( ci.Mobile.NetState == null )
						continue;

					if ( CanChat( ci ) && info.Mobile.Guild == ci.Mobile.Guild && !ci.Ignoring( info.Mobile ) )
						ci.Mobile.SendMessage( ci.GuildColor, "<{0}> {1}: {2}", e.Mobile.Guild.Abbreviation, e.Mobile.Name, e.ArgString );
					else if ( ChatInfo.AllianceChat && CanChat( ci ) && ((Guild)info.Mobile.Guild).Allies.Contains( ci.Mobile.Guild ) && !ci.Ignoring( info.Mobile ) )
						ci.Mobile.SendMessage( ci.GuildColor, "<{0}> {1}: {2}", e.Mobile.Guild.Abbreviation, e.Mobile.Name, e.ArgString );
					else if ( ci.GlobalGuild )
						ci.Mobile.SendMessage( ci.GuildColor, "<{0}> {1}: {2}", e.Mobile.Guild.Abbreviation, e.Mobile.Name, e.ArgString );
				}
			}

		}catch{ Errors.Report( String.Format( "GuildChat-> OnChat-> |{0}|", e.Mobile ) ); } }
	}
}