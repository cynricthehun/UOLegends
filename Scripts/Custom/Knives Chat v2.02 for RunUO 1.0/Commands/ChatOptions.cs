using System;
using Server;

namespace Knives.Chat
{
	public class ChatOptions
	{
		public static void Initialize()
		{
			Server.Commands.Register( "ChatOptions", AccessLevel.Player, new CommandEventHandler( OnOptions ) );
			Server.Commands.Register( "CO", AccessLevel.Player, new CommandEventHandler( OnOptions ) );
		}

		private static void OnOptions( CommandEventArgs e )
		{
			OptionsGump.SendTo( e.Mobile );
		}
	}
}