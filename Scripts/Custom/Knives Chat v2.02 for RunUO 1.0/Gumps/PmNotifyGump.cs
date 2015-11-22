using System;
using Server;
using Server.Gumps;
using Server.Network;
using Knives.Utils;

namespace Knives.Chat
{
	public class PmNotifyGump : GumpPlus
	{
		public static void SendTo( Mobile m )
		{
			new PmNotifyGump( m );
		}

		public PmNotifyGump( Mobile m ) : base( m, 100, 100 )
		{
			m.CloseGump( typeof( PmNotifyGump ) );

			Override = false;

			NewGump();
		}

		protected override void BuildGump()
		{
			ChatInfo info = ChatInfo.GetInfo( Owner );

			AddButton( 30, 10, 0x82E, 0x82E, "Message", new TimerCallback( Message ) );
			AddImage( 0, 0, 0x9CB );
			AddImageTiled( 35, 7, 20, 8, 0x9DC );
			AddHtml( 23, 1, 50, 20, "<CENTER>" + HTML.Black + info.NextMessage().LastSender.Name, false, false );
		}

		private void Message()
		{
			PmGump.SendMostRecent( Owner );
		}
	}
}