using System;
using System.Collections;
using Server;

namespace Knives.Chat
{
	public class Alpha : IComparer
	{
		public Alpha()
		{
		}

		public int Compare( object x, object y )
		{
			if ( x == null && y == null )
				return 0;
			else if ( x == null )
				return -1;
			else if ( y == null )
				return 1;

			string a = x as string;
			string b = y as string;

			if ( a == null || b == null )
				return 0;

			return Insensitive.Compare( a, b );
		}
	}

	public class MobileName : IComparer
	{
		public MobileName()
		{
		}

		public int Compare( object x, object y )
		{
			if ( x == null && y == null )
				return 0;
			else if ( x == null )
				return -1;
			else if ( y == null )
				return 1;

			Mobile a = x as Mobile;
			Mobile b = y as Mobile;

			if ( a == null || b == null )
				return 0;

			return Insensitive.Compare( a.Name, b.Name );
		}
	}

	public class MobileAccessLevel : IComparer
	{
		public MobileAccessLevel()
		{
		}

		public int Compare( object x, object y )
		{
			if ( x == null && y == null )
				return 0;
			else if ( x == null )
				return -1;
			else if ( y == null )
				return 1;

			Mobile a = x as Mobile;
			Mobile b = y as Mobile;

			if ( a == null || b == null )
				return 0;

			if ( a.AccessLevel > b.AccessLevel )
				return -1;

			if ( a.AccessLevel < b.AccessLevel )
				return 1;

			return Insensitive.Compare( a.Name, b.Name );
		}
	}

	public class MessageTime : IComparer
	{
		public MessageTime()
		{
		}

		public int Compare( object x, object y )
		{
			if ( x == null && y == null )
				return 0;
			else if ( x == null )
				return -1;
			else if ( y == null )
				return 1;

			Message a = x as Message;
			Message b = y as Message;

			if ( a == null || b == null )
				return 0;

			if ( a.LastReceive > b.LastReceive )
				return 1;

			if ( a.LastReceive < b.LastReceive )
				return -1;

			return 0;
		}
	}
}