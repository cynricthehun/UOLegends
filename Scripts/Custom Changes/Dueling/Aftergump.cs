using System;
using Server;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Dueling
{
	public class AfterGump : Gump
	{
		public AfterGump( Mobile from, bool won ): base( 0, 0 )
		{
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;
			AddPage(0);
			AddBackground(0, 0, 200, 150, 9200);
			if ( won )
				AddHtml( 25, 25, 150, 40, "You have won the duel.", true, false);
			else
				AddHtml( 25, 25, 150, 40, "You have lost the duel.", true, false);
			AddHtml( 25, 75, 150, 20, "Your wins: " + ((PlayerMobile)from).DuelWins.ToString(), true, false);
			AddHtml( 25, 100, 150, 20, "Your losses: " + ((PlayerMobile)from).DuelLosses.ToString(), true, false);
			//AddHtml( 25, 125, 150, 20, "Your rank: ", true, false);

		}
	}
}