using System;
using Server;
using Knives.Utils;

namespace Knives.Chat
{
	public enum FilterPenalty{ None, ChatBan, Kick, Criminal }

	public class Filter
	{
		public static string FilterText( Mobile m, string s )
		{try{

			string filter = "";
			string subOne = "";
			string subTwo = "";
			string subThree = "";
			int index = 0;

			for( int i = 0; i < ChatInfo.Filters.Count; ++i )
			{
				filter = ChatInfo.Filters[i].ToString();

				if ( filter == "" )
				{
					ChatInfo.Filters.Remove( filter );
					continue;
				}

				index = s.ToLower().IndexOf( filter );

				if ( index >= 0 )
				{
					if ( m.AccessLevel == AccessLevel.Player )
					{
						if ( !ChatInfo.NoFilterPenalty )
							m.SendMessage( ChatInfo.GetInfo( m ).SystemColor, "Filter violation detected: " + filter );

						if ( ChatInfo.FilterBan )
							ChatInfo.GetInfo( m ).Ban( ChatInfo.FilterBanLength );
						else if ( ChatInfo.FilterKick )
						{
							m.Say( "I've been kicked!" );
							if ( m.NetState != null ) m.NetState.Dispose();
						}
						else if ( ChatInfo.FilterCriminal )
							m.CriminalAction( false );

						if ( !ChatInfo.NoFilterPenalty )
							return "";
					}

					subOne = s.Substring( 0, index );
					subTwo = "";

					for( int ii = 0; ii < filter.Length; ++ii )
						subTwo+="*";

					subThree = s.Substring( index+filter.Length, s.Length-filter.Length-index );

					s = subOne + subTwo + subThree;

					i--;
				}
			}

			}catch{ Errors.Report( String.Format( "Filter-> FilterText-> |{0}|", m ) ); }

			return s;
		}
	}
}