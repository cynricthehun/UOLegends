using System;
using Server;
using Knives.Utils;

namespace Knives.Chat
{
	public class IRC
	{
		public static void Initialize()
		{
			Server.Commands.Register( "IRC", AccessLevel.Player, new CommandEventHandler( OnIRC ) );
			Server.Commands.Register( "I", AccessLevel.Player, new CommandEventHandler( OnIRC ) );
		}

		public static bool CanIRC( Mobile m ){ return CanIRC( m, false ); }

		public static bool CanIRC( Mobile m, bool action )
		{
			ChatInfo info = ChatInfo.GetInfo( m );

			if ( info.Banned )
			{
				if ( action )
					m.SendMessage( info.SystemColor, "You are banned from chat." );

				return false;
			}

			if ( !info.IrcOn )
			{
				if ( action )
				{
					OptionsGump.SendTo( info.Mobile );
					m.SendMessage( info.SystemColor, "You must first turn on IRC." );
				}

				return false;
			}

			if ( !IrcConnection.Connection.Connected )
			{
				if ( action )
					m.SendMessage( info.SystemColor, "The irc connection is down." );

				return false;
			}

			return true;
		}

		private static void OnIRC( CommandEventArgs e )
		{try{

			if ( ChatInfo.PublicPlusIRC && !( e.ArgString.ToLower().StartsWith( "input " ) && e.Mobile.AccessLevel == AccessLevel.Administrator ) )
			{
				PublicChat.OnChat( e );
				return;
			}

			if ( e.ArgString == null || e.ArgString == "" )
				return;

			if ( !CanIRC( e.Mobile, true ) )
				return;

			if ( e.ArgString.ToLower().StartsWith( "input " ) && e.Mobile.AccessLevel == AccessLevel.Administrator )
				IrcConnection.Connection.SendMessage( e.ArgString.Substring( 6, e.ArgString.Length-6 ) );
			else
				IrcConnection.Connection.SendUserMessage( e.Mobile, e.ArgString );

		}catch{ Errors.Report( String.Format( "IRC-> OnIRC-> |{0}|", e.Mobile ) ); } }
	}
}