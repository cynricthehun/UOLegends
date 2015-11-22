using System;
using Server;
using Server.Items;
using Server.Guilds;
using Server.Mobiles;

namespace Server.Dueling
{
	public class Keywords
	{
		public static void Initialize()
		{
			// Register our speech handler
			EventSink.Speech += new SpeechEventHandler( EventSink_Speech );
		}

		public static void EventSink_Speech( SpeechEventArgs args )
		{
			string said = args.Speech.ToLower();
			Mobile from = args.Mobile;

			switch( said )
			{
				case( "i wish to duel" ):
				{
					if ( from.Region is DuelRegion || DuelGump.Exists( from ) || ((PlayerMobile)from).SentRequest || ((PlayerMobile)from).RecievedRequest )
					{
						from.SendAsciiMessage( "You are too busy to duel someone else." );
					}
					else if ( !from.Alive )
					{
						from.SendMessage( "You cannot duel while dead." );
					}
					else if ( from.Aggressed.Count != 0 || from.Aggressors.Count != 0 )
					{
						from.SendMessage( "You cannot duel while in combat." );
					}
					else if ( from.Hits != from.HitsMax )
					{
						from.SendMessage( "You need to heal before offering a duel." );
					}
					else
					{
						from.Target = new DuelTarget( from );
						((PlayerMobile)from).SentRequest = true;
					}
					break;
				}
			}
		}
	}
}