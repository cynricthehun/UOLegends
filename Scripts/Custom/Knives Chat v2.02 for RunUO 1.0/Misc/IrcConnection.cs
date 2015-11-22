using System;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Collections;
using Server;
using Knives.Utils;

namespace Knives.Chat
{
	public enum IrcColor
	{
		White = 0,
		Black = 1,
		Blue = 2,
		Green = 3,
		LightRed = 4,
		Brown = 5,
		Purple = 6,
		Orange = 7,
		Yellow = 8,
		LightGreen = 9,
		Cyan = 10,
		LightCyan = 11,
		LightBlue = 12,
		Pink = 13,
		Grey = 14,
		LightGrey = 15,
	};

	public class IrcConnection
	{
		private static IrcConnection s_Connection = new IrcConnection();

		public static IrcConnection Connection{ get{ return s_Connection; } }

		private TcpClient c_Tcp;
		private Thread c_Thread;
		private StreamReader c_Reader;
		private StreamWriter c_Writer;
		private bool c_Connecting, c_Connected;
		private int c_Attempts;
		private DateTime c_LastPong;

		public bool Connecting{ get{ return c_Connecting; } }
		public bool Connected{ get{ return c_Connected; } }
		public bool HasMoreAttempts{ get{ return c_Attempts <= ChatInfo.IrcMaxAttempts; } }

		public IrcConnection()
		{
		}

		public void Connect( Mobile m )
		{
			ChatInfo info = ChatInfo.GetInfo( m );

			if ( c_Connecting )
			{
				m.SendMessage( info.SystemColor, "The server is already attempting to connect." );
				return;
			}

			if ( c_Connected )
			{
				m.SendMessage( info.SystemColor, "The server is already connected." );
				return;
			}

			Connect();
		}

		public void Connect()
		{
			new Thread( new ThreadStart( ConnectTcp ) ).Start();
		}

		public void CancelConnect()
		{
			c_Attempts = ChatInfo.IrcMaxAttempts;
		}

		private void Reconnect()
		{
			c_Attempts++;

			if ( !HasMoreAttempts )
			{
				c_Attempts = 1;
				BroadcastSystem( "Irc Connection failed." );
				return;
			}

			BroadcastSystem( String.Format( "Attempting to connect to IRC... {0}", c_Attempts ) );

			Connect();
		}

		private void ConnectTcp()
		{
			try{ c_Tcp = new TcpClient( ChatInfo.IrcServer, ChatInfo.IrcPort ); }
			catch
			{
				BroadcastSystem( "Connection could not be established." );

				Reconnect();

				return;
			}

			ConnectStream();
		}

		private void ConnectStream()
		{try{

			c_Connecting = true;
			c_LastPong = DateTime.Now;

			c_Reader = new StreamReader( c_Tcp.GetStream() );
			c_Writer = new StreamWriter( c_Tcp.GetStream() );

			BroadcastSystem( "Connecting to IRC Server..." );

			SendMessage( String.Format( "USER {0} 1 * :Hello!", ChatInfo.IrcNick ) );
			SendMessage( String.Format( "NICK {0}", ChatInfo.IrcNick ) );
			SendMessage( String.Format( "JOIN {0}", ChatInfo.IrcRoom ) );

			c_Thread = new Thread( new ThreadStart( ReadStream ) );
			c_Thread.Start();

			Server.Timer.DelayCall( TimeSpan.FromSeconds( 15.0 ), new Server.TimerCallback( Ping ) );

		}catch{ Errors.Report( String.Format( "IrcConnection-> ConnectStream" ) ); } }

		private void Ping()
		{
			if ( c_LastPong < DateTime.Now-TimeSpan.FromMinutes( 1 ) )
				Disconnect();

			if ( !c_Connecting && !c_Connected )
				return;

			SendMessage( "PING " + ChatInfo.IrcServer );

			if ( c_Connected )
				SendMessage( "NAMES " + ChatInfo.IrcRoom );

			Server.Timer.DelayCall( TimeSpan.FromSeconds( 15.0 ), new Server.TimerCallback( Ping ) );
		}

		public void SendMessage( string msg )
		{
			try{

			BroadcastRaw( msg );

			c_Writer.WriteLine( msg );
			c_Writer.Flush();

			}catch{ Disconnect(); }
		}

		public void SendUserMessage( Mobile m, string msg )
		{
			if ( !Connected )
				return;

			if ( !ChatInfo.PublicPlusIRC )
				s_Connection.Broadcast( m, String.Format( "<{0}> {1}: {2}", ChatInfo.IrcRoom, m.Name, msg ) );

			msg = OutParse( m, m.Name + ": " + msg );
			s_Connection.SendMessage( String.Format( "PRIVMSG {0} : {1}", ChatInfo.IrcRoom, msg  ));

			BroadcastRaw( String.Format( "PRIVMSG {0} : {1}", ChatInfo.IrcRoom, msg ) );
		}

		private string OutParse( Mobile m, string str )
		{
			if ( m.AccessLevel != AccessLevel.Player )
				return str = '\x0003' + ((int)ChatInfo.IrcStaffColor).ToString() + str;

			return str;
		}

		private void BroadcastSystem( string msg )
		{
			foreach( ChatInfo info in ChatInfo.ChatInfos.Values )
				if ( info.IrcOn && !info.Banned )
					info.Mobile.SendMessage( info.SystemColor, msg );
		}

		private void Broadcast( string msg )
		{
			foreach( ChatInfo info in ChatInfo.ChatInfos.Values )
				if ( info.IrcOn && !info.Banned )
					info.Mobile.SendMessage( info.IrcColor, msg );
		}

		private void Broadcast( Mobile m, string msg )
		{
			ChatInfo info = ChatInfo.GetInfo( m );

			foreach( ChatInfo otherInfo in ChatInfo.ChatInfos.Values )
				if ( otherInfo.IrcOn && !otherInfo.Banned )
					otherInfo.Mobile.SendMessage( m.AccessLevel == AccessLevel.Player ? otherInfo.IrcColor : info.StaffColor, msg );
		}

		private void BroadcastRaw( string msg )
		{
			foreach( ChatInfo info in ChatInfo.ChatInfos.Values )
				if ( info.IrcRaw )
					info.Mobile.SendMessage( info.SystemColor, "RAW: " + msg );
		}

		private void ReadStream()
		{try{

			string input = "";

			while( c_Thread.IsAlive )
			{
				input = c_Reader.ReadLine();

				if ( input == null )
					break;

				HandleInput( input );
			}

			Disconnect();

		}catch{ Disconnect(); } }

		private void HandleInput( string str )
		{try{

			BroadcastRaw( str );

			if ( str.IndexOf( "PONG" ) != -1 )
			{
				if ( !c_Connected )
				{
					c_Connected = true;
					c_Connecting = false;
					BroadcastSystem( String.Format( "Server is now connected to IRC channel." ) );
					c_Attempts = 1;
				}

				c_LastPong = DateTime.Now;
				return;
			}

			if ( str.Length > 300 )
				return;

			if ( str.IndexOf( "353" ) != -1 )
			{
				BroadcastRaw( "IRC names list updating." );

				int index = str.ToLower().IndexOf( ChatInfo.IrcRoom.ToLower() ) + ChatInfo.IrcRoom.Length + 2;

				if ( index == 1 )
					return;

				string strList = str.Substring( index, str.Length-index );

				string[] strs = strList.Trim().Split( ' ' );

				ChatInfo.IrcList.Clear();
				ChatInfo.IrcList.AddRange( strs );
				ChatInfo.IrcList.Remove( ChatInfo.IrcNick );
			}

			if ( str.IndexOf( "PRIVMSG" ) != -1 )
			{
				string parOne = str.Substring( 1, str.IndexOf( "!" )-1 );

				int index = 0;

				index = str.ToLower().IndexOf( ChatInfo.IrcRoom.ToLower() ) + ChatInfo.IrcRoom.Length + 2;

				if ( index == 1 )
					return;

				string parTwo = str.Substring( index, str.Length-index );

				if ( parTwo.IndexOf( "ACTION" ) != -1 )
				{
					index = parTwo.IndexOf( "ACTION" ) + 7;
					parTwo = parTwo.Substring( index, parTwo.Length-index );
					str = String.Format( "<{0}> {1} {2}", ChatInfo.IrcRoom, parOne, parTwo );
				}
				else
					str = String.Format( "<{0}> {1}: {2}", ChatInfo.IrcRoom, parOne, parTwo );

				Broadcast( str );
			}

		}catch{ Errors.Report( String.Format( "IrcConnection-> HandleInput" ) ); } }

		public void Disconnect()
		{
			Disconnect( true );
		}

		public void Disconnect( bool reconn )
		{try{

			if ( c_Connected )
				BroadcastSystem( "IRC connection down." );

			c_Connected = false;
			c_Connecting = false;

			if ( c_Thread != null )
				c_Thread.Suspend();
			if ( c_Reader != null )
				c_Reader.Close();
			if ( c_Writer != null )
				c_Writer.Close();
			if ( c_Tcp != null )
				c_Tcp.Close();

			if ( reconn )
				Reconnect();

		}catch{ Errors.Report( String.Format( "IrcConnection-> Disconnect" ) ); } }
	}
}