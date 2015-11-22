using System;
using System.Collections;
using Server;
using Knives.Utils;

namespace Knives.Chat
{
	public class Pm
	{
		public static void Initialize()
		{
			Server.Commands.Register( "Pm", AccessLevel.Player, new CommandEventHandler( OnPm ) );
		}

		public static bool CanPm( ChatInfo info ){ return CanPm( info, false ); }

		public static bool CanPm( ChatInfo info, bool action )
		{
			if ( info.Banned )
			{
				if ( action )
					info.Mobile.SendMessage( info.SystemColor, "You are banned from chat." );

				return false;
			}

			if ( info.PmDisabled && info.Mobile.AccessLevel == AccessLevel.Player )
			{
				if ( action )
				{
					OptionsGump.SendTo( info.Mobile );
					info.Mobile.SendMessage( info.SystemColor, "You must first enable private messages." );
				}

				return false;
			}

			return true;
		}

		public static bool CanPm( ChatInfo source, ChatInfo target )
		{
			if ( source == target )
				return false;

			if ( source.Mobile.AccessLevel > target.Mobile.AccessLevel )
				return true;

			if ( target.PmDisabled && source.Mobile.AccessLevel == AccessLevel.Player )
				return false;

			if ( !ChatInfo.ShowStaff
			&& source.Mobile.AccessLevel == AccessLevel.Player
			&& target.Mobile.AccessLevel > AccessLevel.Player )
				return false;

			if ( source.PmFriends || target.PmFriends )
			{
				if ( !source.Friends.Contains( target.Mobile ) )
					return false;
				if ( !target.Friends.Contains( source.Mobile ) )
					return false;
			}

			if ( !CanPm( target ) || !CanPm( source ) )
				return false;

			if ( source.Ignoring( target.Mobile )
			|| target.Ignoring( source.Mobile ) )
				return false;

			return true;
		}

		public static bool CanReply( ChatInfo source, ChatInfo target )
		{
			if ( target.Mobile.AccessLevel > source.Mobile.AccessLevel )
				return true;

			if ( !CanPm( source, target ) )
				return false;

			return true;
		}

		private static void OnPm( CommandEventArgs e )
		{try{

			ChatInfo info = ChatInfo.GetInfo( e.Mobile );

			if ( !CanPm( info, true ) )
				return;

			if ( e.ArgString == null || e.ArgString == "" )
				ListGump.SendTo( e.Mobile, Listing.Messages );
			else
			{
				string name = e.GetString( 0 );
				string text = "";

				if ( e.Arguments.Length > 1 )
					text = e.ArgString.Substring( name.Length+1, e.ArgString.Length-name.Length-1 );

				ArrayList list = GetPmCanidates( info, name );

				if ( list.Count > 10 )
					e.Mobile.SendMessage( info.SystemColor, "Too many name search results, please be more specific." );
				else if ( list.Count == 0 )
					e.Mobile.SendMessage( info.SystemColor, "No name containing '{0}' was found.", name );
				else if ( list.Count == 1 )
					PmGump.SendTo( info.Mobile, (Mobile)list[0], text );
				else
				{
					Hashtable table = new Hashtable();
					foreach( Mobile m in list )
						table.Add( new object[3]{ e.Mobile, m, text }, m.Name );

					ChoiceGump.SendTo( info.Mobile, "", 200, new TimerStateCallback( ChoiceCallback ), table );
				}
			}

		}catch{ Errors.Report( String.Format( "Pm-> OnPm-> |{0}|", e.Mobile ) ); } }

		private static void ChoiceCallback( object obj )
		{
			if ( !(obj is object[]) )
				return;

			object[] info = (object[])obj;

			if ( info.Length != 3
			|| !(info[0] is Mobile )
			|| !(info[1] is Mobile )
			|| !(info[2] is string ) )
				return;

			PmGump.SendTo( (Mobile)info[0], (Mobile)info[1], info[2].ToString() );
		}

		private static ArrayList GetPmCanidates( ChatInfo info, string name )
		{
			ArrayList list = new ArrayList();

			try{

			foreach( ChatInfo l_info in ChatInfo.ChatInfos.Values )
				if ( l_info.Mobile.Name.ToLower().IndexOf( name.ToLower() ) != -1
				&& CanPm( info, l_info ) )
					list.Add( l_info.Mobile );

			}catch{ Errors.Report( String.Format( "Pm-> GetPmCanidates-> |{0}|", info.Mobile ) ); }

			return list;
		}
	}
}