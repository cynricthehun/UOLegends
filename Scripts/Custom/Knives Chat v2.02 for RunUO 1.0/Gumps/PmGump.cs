using System;
using Server;
using Server.Gumps;
using Server.Network;
using Knives.Utils;

namespace Knives.Chat
{
	public class PmGump : GumpPlus
	{
		public enum Page { Reply, Send }

		public static string s_Help = "    Read and reply to messages sent to you, send new ones, or save them for the future!"
			+ "<BR><BR>     When you save messages, you can find them through the messages menu and view them at your leasure."
			+ "<BR><BR>     Using the arrows on the left and right side, you can view previous messages in the current conversation.";
		public static string s_StaffHelp = "     Staff members may also goto players from Pm messages for speed when help is needed.";

		public static void SendTo( Mobile source, Mobile target )
		{
			SendTo( source, target, "" );
		}

		public static void SendTo( Mobile source, Mobile target, string str )
		{
			new PmGump( source, target, new Message(), str );
		}

		public static void SendTo( Mobile m, Message msg )
		{
			if ( m != null && msg.LastSender != null )
				new PmGump( m, msg.LastSender, msg );

			ChatInfo.GetInfo( m ).RemoveMessage( msg );
		}

		public static void SendMostRecent( Mobile m )
		{
			Message msg = ChatInfo.GetInfo( m ).GetNextMessage();

			SendTo( m, msg );
		}

		private const int Width = 300;
		private const int Height = 200;

		private Mobile c_Other;
		private Message c_Message;
		private Page c_Page;
		private string c_StartText;

		public PmGump( Mobile m, Mobile other, Message msg ) : this( m, other, msg, "" ){}

		public PmGump( Mobile m, Mobile other, Message message, string str ) : base( m, 100, 100 )
		{
			c_Other = other;
			c_Message = message;
			c_StartText = str;

			if ( c_Message.History.Count == 0 )
				c_Page = Page.Send;
			else if ( c_Other.AccessLevel >= Owner.AccessLevel
			&& ChatInfo.GetInfo( c_Other ).PmReceipt )
				c_Other.SendMessage( "{0} has opened your message.", Owner.Name );

			NewGump();
		}

		protected override void BuildGump()
		{try{

			ChatInfo info = ChatInfo.GetInfo( Owner );
			ChatInfo otherInfo = ChatInfo.GetInfo( c_Other );

			AddPage( 0 );

			AddBackground( 0, 0, Width, Height, 0x13BE );

			AddButton( Width-20, Height-20, 0x5689, 0x5689, "Help", new TimerCallback( Help ) );

			AddPage( 1 );

			if ( Owner.AccessLevel > c_Other.AccessLevel
			&& c_Other.NetState != null )
				AddTemplateButton( 30, Height-30, 70, Template.RedSquare, "Goto", HTML.White + "<CENTER>Goto", new TimerCallback( Goto ) );
			
			if ( c_Other.AccessLevel == AccessLevel.Player )
				AddTemplateButton( 80, Height-30, 70, Template.RedSquare, "Ignore", HTML.White + "<CENTER>Ignore", new TimerCallback( Ignore ) );

			int currentMessage = c_Message.History.Count-1;

			object[] obj;

			if ( c_Page == Page.Reply )
			{
				if ( currentMessage < 0 )
					return;

				obj = (object[])c_Message.History[currentMessage];

				AddHtml( 0, 10, Width, 25, "<CENTER>" + HTML.White + (DateTime)obj[2], false, false );

				if ( !c_Message.Saved )
					AddTemplateButton( 130, Height-30, 70, Template.RedSquare, "Save", HTML.White + "<CENTER>Save", new TimerCallback( Save ) );

				if ( !c_Message.Saved && Pm.CanReply( info, otherInfo ) )
					AddTemplateButton( 180, Height-30, 70, Template.RedSquare, "Reply", HTML.White + "<CENTER>Reply", new TimerCallback( Reply ) );

				AddHtml( 20, 30, Width-40, Height-73, String.Format( "<B>{0} says:</B><BR>{1}", c_Message.LastSender.Name, obj[1] ), true, true );

				currentMessage--;
			}
			else
			{
				AddHtml( 0, 10, Width, 25, "<CENTER>" + HTML.White + "Message to " + c_Other.Name + (c_Other.NetState == null || (otherInfo.Hidden && c_Other.AccessLevel >= Owner.AccessLevel) ? " (Offline)" : ""), false, false );

				if ( !c_Message.Saved && Pm.CanReply( info, otherInfo ) )
					AddTemplateButton( 180, Height-30, 70, Template.RedSquare, "Send", HTML.White + "<CENTER>Send", new TimerCallback( Send ) );

				AddImageTiled( 20, 30, Width-40, Height-73, 0xBBC );
				AddTextField( 20, 30, Width-40, Height-73, 0x480, 0, GetTextField( 0 ) == "" ? c_StartText : GetTextField( 0 ) );
			}

			int tabPage = 1;

			while( currentMessage >= 0 )
			{
				AddButton( 0, Height/2-10, 0x15E3, 0x15E7, 0, GumpButtonType.Page, tabPage+1 );

				AddPage( ++tabPage );

				obj = (object[])c_Message.History[currentMessage--];

				AddButton( Width-20, Height/2-10, 0x15E1, 0x15E5, 0, GumpButtonType.Page, tabPage-1 );

				AddHtml( 0, 10, Width, 25, "<CENTER>" + HTML.White + (DateTime)obj[2], false, false );
				AddHtml( 20, 30, Width-40, Height-50, String.Format( "<B>{0} says:</B><BR>{1}", ((Mobile)obj[0]).Name, obj[1] ), true, true );
			}

		}catch{ Errors.Report( String.Format( "PmGump-> BuildGump-> |{0}|-> |{1}|", Owner, c_Other ) ); } }

		private void Help()
		{
			NewGump();

			ChatInfo info = ChatInfo.GetInfo( Owner );

			InfoGump.SendTo( Owner, 300, 300, s_Help + (Owner.AccessLevel != AccessLevel.Player ? "<BR><BR>" + s_StaffHelp : ""), true );
		}

		private void Goto()
		{try{

			NewGump();
			if ( c_Other.NetState == null )
				Owner.SendMessage( "{0} is no longer online.", c_Other.Name );
			else
			{
				Owner.Location = c_Other.Location;
				Owner.Map = c_Other.Map;
			}

		}catch{ Errors.Report( String.Format( "PmGump-> Goto-> |{0}|-> |{1}|", Owner, c_Other ) ); } }

		private void Ignore()
		{
			ChatInfo.GetInfo( Owner ).AddIgnore( c_Other );
			NewGump();
		}

		private void Save()
		{
			ChatInfo.GetInfo( Owner ).Save( c_Message );
			NewGump();
		}

		private void Reply()
		{
			c_Page = Page.Send;
			NewGump();
		}

		private void Send()
		{try{

			if ( !Pm.CanReply( ChatInfo.GetInfo( Owner ), ChatInfo.GetInfo( c_Other ) ) )
			{
				Owner.SendMessage( ChatInfo.GetInfo( Owner ).SystemColor, "Your message could not be delivered." );
				return;
			}

			if ( GetTextField( 0 ) == "" )
			{
				NewGump();
				return;
			}

			c_Message.AddMessage( Owner, GetTextField( 0 ) );

			Server.Scripts.Commands.CommandLogging.WriteLine( Owner, String.Format( "<PM> {0} to {1}: {2}", Owner.Name, c_Other.Name, GetTextField( 0 ) ) );

			foreach( ChatInfo ci in ChatInfo.ChatInfos.Values )
				if ( ci.GlobalPm && ci.Mobile.AccessLevel > Owner.AccessLevel )
					ci.Mobile.SendMessage( ci.PmColor, String.Format( "<PM> {0} to {1}: {2}", Owner.Name, c_Other.Name, GetTextField( 0 ) ) );

			ChatInfo.GetInfo( c_Other ).AddMessage( Owner, c_Message );

		}catch{ Errors.Report( String.Format( "PmGump-> Send-> |{0}|-> |{1}|", Owner, c_Other ) ); } }
	}
}