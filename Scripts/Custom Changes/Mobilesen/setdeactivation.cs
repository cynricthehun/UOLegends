using System;
using System.Text;
using Server;

namespace Server.Mobiles
{
	public class SetDeactivation
	{
		public static double DefaultDeactivationDelay = 2.0;

		public static void Initialize()
		{
			Server.Commands.Register( "SetDeactivation", AccessLevel.Administrator, new CommandEventHandler( SetDeactivation_OnCommand ) );
		}

        [Usage( "SetDeactivation [minutes]" )]
        [Description( "Sets/reports the default deactivation delay for the PlayerRangeSensitive mod in minutes" )]
        public static void SetDeactivation_OnCommand( CommandEventArgs e )
        {
                if( e.Arguments.Length > 0 ){
                  try{
                    DefaultDeactivationDelay = double.Parse(e.Arguments[0]);
                  } catch{}
                }
                e.Mobile.SendMessage("Default deactivation delay set to {0} minutes",DefaultDeactivationDelay);
        }
    }
}
