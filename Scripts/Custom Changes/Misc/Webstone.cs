using System;
using System.Text;
using Server.Gumps;
using Server.Network;
using Server.Items;

namespace Server.Gumps
{
	public class WebStone : Item
	{
		[Constructable]
		public WebStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 0;
			Name = "Website Stone"; //name this whatever you want, like "Click here for our websites" Or whatever :P
		}

		public override void OnDoubleClick( Mobile from )
		{
		from.SendGump( new WebstoneGump() );	
		}

		public WebStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		

	}
}

namespace Server.Gumps
{

public class WebstoneGump : Gump
	{


		public  WebstoneGump() : base( 40, 40 )
		{
			AddPage( 0 );
			AddBackground( 0, 0, 160, 120, 5054 );

			AddButton( 10, 10, 0xFB7, 0xFB9, 1, GumpButtonType.Reply, 0 );
			AddLabel( 45, 10, 0x34, "Legends of Ultima Forums" ); //#1 Site's Name

			AddButton( 10, 35, 0xFB7, 0xFB9, 2, GumpButtonType.Reply, 0 );
			AddLabel( 45, 35, 0x34, "Vote Top200" );  //#2 Site's Name

			AddButton( 10, 60, 0xFB7, 0xFB9, 3, GumpButtonType.Reply, 0 );
			AddLabel( 45, 60, 0x34, "Donation List" );  //#3 Site's Name

			AddButton( 10, 85, 0xFB1, 0xFB3, 0, GumpButtonType.Reply, 0 );
			AddLabel( 45, 85, 0x34, "Close" );
		}

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			switch ( info.ButtonID )
			{
				case 1: // #1 Site's Url
				{
					sender.LaunchBrowser( "http://11.freebb.com/?freebb=legends" );
					break;
				}
				case 2: // #2 Site's url
				{
					sender.LaunchBrowser( "http://www.gamesites200.com/ultimaonline/vote.php?id=4432" );
					break;
				}
				case 3: // #3 Site's url
				{
					sender.LaunchBrowser( "http://11.freebb.com/viewtopic.php?t=26&freebb=legends" );
					break;
				}
			}
          }
  }
}