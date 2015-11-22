using System;
using Server;
using Knives.Utils;

namespace Knives.Chat
{
	public class WorldListen
	{
		public static void Initialize()
		{
			EventSink.Speech += new SpeechEventHandler( OnSpeech );
		}

		private static void OnSpeech( SpeechEventArgs e )
		{try{

			foreach( ChatInfo info in ChatInfo.ChatInfos.Values )
			{
				if ( info.Mobile.Map == e.Mobile.Map && info.Mobile.InRange( e.Mobile.Location, 10 ) )
					continue;

				if ( info.GlobalWorld && !info.GlobalIgnoring( e.Mobile ) && info.Mobile.AccessLevel > e.Mobile.AccessLevel )
					info.Mobile.SendMessage( info.WorldColor, String.Format( "<World> {0}: {1}", e.Mobile.Name, e.Speech ) );
			}

		}catch{ Errors.Report( String.Format( "WorldListen-> OnSpeech-> |{0}|", e.Mobile ) ); } }
	}
}