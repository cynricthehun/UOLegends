using System;
using System.Collections;
using Server;

namespace Knives.Chat
{
	public class Message
	{
		private ArrayList c_History;
		private bool c_Saved;

		public ArrayList History{ get{ return c_History; } set{ c_History = value; } }
		public bool Saved{ get{ return c_Saved; } set{ c_Saved = value; } }

		public Mobile LastSender
		{
			get
			{
				if ( c_History.Count == 0 )
					return null;

				return ((object[])c_History[c_History.Count-1])[0] as Mobile;
			}
		}

		public DateTime LastReceive
		{
			get
			{
				if ( c_History.Count == 0 || !(((object[])c_History[c_History.Count-1])[2] is DateTime) )
					return DateTime.Now;

				return (DateTime)((object[])c_History[c_History.Count-1])[2];
			}
		}

		public string LastText
		{
			get
			{
				if ( c_History.Count == 0 || !(((object[])c_History[c_History.Count-1])[1] is string) )
					return "None";

				return ((object[])c_History[c_History.Count-1])[1].ToString();
			}
		}

		public Message()
		{
			c_History = new ArrayList();
		}

		public void AddMessage( Mobile m, string text )
		{
			c_History.Add( new object[3]{ m, text, DateTime.Now } );

			while( c_History.Count > ChatInfo.MaxPmHistory )
				c_History.RemoveAt( 0 );
		}

		public void Save( GenericWriter writer )
		{
			writer.Write( 0 ); // version

			writer.Write( c_Saved );

			writer.Write( c_History.Count );
			foreach( object[] obj in c_History )
			{
				writer.Write( (Mobile)obj[0] );
				writer.Write( obj[1].ToString() );
				writer.Write( (DateTime)obj[2] );
			}
		}

		public void Load( GenericReader reader )
		{
			int version = reader.ReadInt();

			c_Saved = reader.ReadBool();

			int count = reader.ReadInt();
			Mobile m;
			string str;
			DateTime date;

			for( int i = 0; i < count; ++i )
			{
				m = reader.ReadMobile();
				str = reader.ReadString();
				date = reader.ReadDateTime();

				c_History.Add( new object[3]{ m, str, date } );
			}
		}
	}
}