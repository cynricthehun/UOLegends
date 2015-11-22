using System;
using Server;
using Server.Mobiles;
using Knives.Utils;

namespace Knives.Chat
{
	public class FactionChat
	{
		public static void Initialize()
		{
			Server.Commands.Register( "Faction", AccessLevel.Player, new CommandEventHandler( OnChat ) );
			Server.Commands.Register( "F", AccessLevel.Player, new CommandEventHandler( OnChat ) );
		}

		public static bool CanChat( ChatInfo info ){ return CanChat( info, false ); }

		public static bool CanChat( ChatInfo info, bool action )
		{
			if ( !ChatInfo.AllowFaction )
			{
				if ( action )
					info.Mobile.SendMessage( info.SystemColor, "Faction chat is not enabled." );

				return false;
			}

			if ( !IsInFaction( info.Mobile ) )
			{
				if ( action )
					info.Mobile.SendMessage( info.SystemColor, "You are not in a faction." );

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

		public static bool IsInFaction( Mobile m )
		{
			if ( !m.Player )
				return false;

			return ( ((PlayerMobile)m).FactionPlayerState != null && ((PlayerMobile)m).FactionPlayerState.Faction != null );
		}

		public static bool SameFaction( Mobile a, Mobile b )
		{
			if ( !a.Player || !b.Player )
				return false;

			return ( IsInFaction( a ) && IsInFaction( b ) && ((PlayerMobile)a).FactionPlayerState.Faction == ((PlayerMobile)b).FactionPlayerState.Faction );
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
				ListGump.SendTo( e.Mobile, Listing.Faction );
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

					if ( CanChat( ci ) && SameFaction( info.Mobile, ci.Mobile ) && !ci.Ignoring( info.Mobile ) )
						ci.Mobile.SendMessage( ci.FactionColor, "<{0}> {1}: {2}", ((PlayerMobile)e.Mobile).FactionPlayerState.Faction.Definition.FriendlyName, e.Mobile.Name, e.ArgString );
					else if ( ci.GlobalFaction )
						ci.Mobile.SendMessage( ci.FactionColor, "<{0}> {1}: {2}", ((PlayerMobile)e.Mobile).FactionPlayerState.Faction.Definition.FriendlyName, e.Mobile.Name, e.ArgString );
				}
			}

		}catch{ Errors.Report( String.Format( "FactionChat-> OnChat-> |{0}|", e.Mobile ) ); } }
	}
}