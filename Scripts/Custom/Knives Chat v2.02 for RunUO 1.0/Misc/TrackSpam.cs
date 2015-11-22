using System;
using System.Collections;
using Server;
using Knives.Utils;

namespace Knives.Chat
{
	public class TrackSpam
	{
		private static Hashtable s_Log = new Hashtable();

		public static bool LogSpam( Mobile m, string type, double limit )
		{try{

			if ( s_Log.Contains( m ) )
			{
				Hashtable table = (Hashtable)s_Log[m];

				if ( table.Contains( type ) )
				{
					if ( (DateTime)table[type] > DateTime.Now-TimeSpan.FromSeconds( limit ) )
						return false;

					table[type] = DateTime.Now;
				}
			}
			else
			{
				Hashtable table = new Hashtable();
				table[type] = DateTime.Now;
				s_Log[m] = table;
			}

			return true;

			}catch{ Errors.Report( String.Format( "TrackSpam-> LogSpam-> {0}", m ) ); }

			return false;
		}

		public static TimeSpan NextAllowedIn( Mobile m, string type, double limit )
		{
			if ( s_Log[m] == null )
				return TimeSpan.FromSeconds( 1 );

			Hashtable table = (Hashtable)s_Log[m];

			if ( table[type] == null || (DateTime)table[type]+TimeSpan.FromSeconds( limit ) < DateTime.Now )
				return TimeSpan.FromSeconds( 1 );

			return (DateTime)table[type]+TimeSpan.FromSeconds( limit )-DateTime.Now;
		}
	}
}