using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Network;

namespace Server.Mobiles
{
	public abstract class BaseGuildmaster : BaseVendor
	{
		private ArrayList m_SBInfos = new ArrayList();
		protected override ArrayList SBInfos{ get { return m_SBInfos; } }

		public override bool IsActiveVendor{ get{ return false; } }

		public override bool ClickTitle{ get{ return false; } }

		public virtual int JoinCost{ get{ return 500; } }

		public virtual TimeSpan JoinAge{ get{ return TimeSpan.FromDays( 0.0 ); } }
		public virtual TimeSpan JoinGameAge{ get{ return TimeSpan.FromDays( 2.0 ); } }
		public virtual TimeSpan QuitAge{ get{ return TimeSpan.FromDays( 7.0 ); } }
		public virtual TimeSpan QuitGameAge{ get{ return TimeSpan.FromDays( 4.0 ); } }

		public override void InitSBInfo()
		{
		}

		public virtual bool CheckCustomReqs( PlayerMobile pm )
		{
			return true;
		}

		public virtual void SayGuildTo( Mobile m )
		{
			SayTo( m, 1008055 + (int)NpcGuild );
		}

		public virtual void SayWelcomeTo( Mobile m )
		{
			SayTo( m, 1008054 ); // Welcome to the guild! Thou shalt find that fellow members shall grant thee lower prices in shops.
		}

		public virtual void SayPriceTo( Mobile m )
		{
			m.Send( new MessageLocalizedAffix( Serial, Body, MessageType.Regular, SpeechHue, 3, 1008052, Name, AffixType.Append, JoinCost.ToString(), "" ) );
		}

		public virtual bool WasNamed( string speech )
		{
			string name = this.Name;

			return ( name != null && Insensitive.StartsWith( speech, name ) );
		}

		public override bool HandlesOnSpeech( Mobile from )
		{
			if ( from.InRange( this.Location, 2 ) )
				return true;

			return base.HandlesOnSpeech( from );
		}

		public override void OnSpeech( SpeechEventArgs e )
		{
			Mobile from = e.Mobile;

			if ( !e.Handled && from is PlayerMobile && from.InRange( this.Location, 2 ) && WasNamed( e.Speech ) )
			{
				PlayerMobile pm = (PlayerMobile)from;

				if ( e.HasKeyword( 0x0004 ) ) // *join* | *member*
				{
					if ( pm.NpcGuild == this.NpcGuild )
						SayTo( from, 501047 ); // Thou art already a member of our guild.
					else if ( pm.NpcGuild != NpcGuild.None )
						SayTo( from, 501046 ); // Thou must resign from thy other guild first.
					else if ( pm.GameTime < JoinGameAge || (pm.CreationTime + JoinAge) > DateTime.Now )
						SayTo( from, 501048 ); // You are too young to join my guild...
					else if ( CheckCustomReqs( pm ) )
						SayPriceTo( from );

					e.Handled = true;
				}
				else if ( e.HasKeyword( 0x0005 ) ) // *resign* | *quit*
				{
					if ( pm.NpcGuild != this.NpcGuild )
					{
						SayTo( from, 501052 ); // Thou dost not belong to my guild!
					}
					else if ( (pm.NpcGuildJoinTime + QuitAge) > DateTime.Now || (pm.NpcGuildGameTime + QuitGameAge) > pm.GameTime )
					{
						SayTo( from, 501053 ); // You just joined my guild! You must wait a week to resign.
					}
					else
					{
						SayTo( from, 501054 ); // I accept thy resignation.
						pm.NpcGuild = NpcGuild.None;
					}

					e.Handled = true;
				}
			}

if( !e.Handled && e.Mobile.InRange( this, 6 ) ) 
   {
                int[] keywords = e.Keywords; 
                string speech = e.Speech; 

		if( e.HasKeyword( 0x00FC ) )// how art thou
	{
		switch( Utility.Random( 4 ) )
		 {
		case 0: this.Say( 502002 ); break; //Very well.
		case 1: this.Say( 1014089 ); break; //I am well.
		case 2: this.Say( 1014115 ); break; //Just fine.
		case 3: this.Say( 1014110 ); break; //I'm doing relatively well.
		 }
		}
		if( e.HasKeyword( 0x003B ) )// Hail, Hello etc
	     {
		switch( Utility.Random( 6 ) )
		 {
		case 0: this.Say( 1007104 ); break; //Greetings, my friend.
		case 1: this.Say( 1007105 ); break; //Hail, my friend.
		case 2: this.Say( 1014085 ); break; //Greetings
		case 3: this.Say( 1014495 ); break; //Greetings. What might I help thee with?
		case 4: this.Say( 1014497 ); break; //Hello, my friend! How may I assist thee?
		case 5: this.Say( 1014116 ); break; // Let's see... I remember the names of Shame...
		 }
	     	}
//Where am I? Start
		if( e.HasKeyword( 0x0122 ) && (Region.Name == "Britain"))// Where am i?
	     {
		this.Say( 1014071 ); // Thou art in Britain
	     	}             
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Jhelom"))// Where am i?
	     {
		this.Say( "Thou art in Jhelom." ); // Thou art in Jhelom
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Trinsic"))// Where am i?
	     {
		this.Say( 1014359 ); // Thou'rt in Trinsic,near the Cape of Heros we are.
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Cove"))// Where am i?
	     {
		this.Say( 1014182 ); // This is Cove, friend.
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Buccaneer's Den"))// Where am i?
	     {
		this.Say( 1014171 ); // Thou'rt in Buccaneer's Den.
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Moonglow"))// Where am i?
	     {
		this.Say( "Thou art in Moonglow." ); // Thou'rt in Moonglow
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Vesper"))// Where am i?
	     {
		this.Say( "Thou'rt in Vesper, wonderful town, aye?" ); // Thou'rt in Vesper, wonderful town, aye?
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Minoc"))// Where am i?
	     {
		this.Say( "Thou'rt in Minoc, friend." ); // Thou'rt in Minoc, friend.
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Nujel'm"))// Where am i?
	     {
		this.Say( "Thou art in Nujel'm." ); // Thou art in Nujel'm.
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Ocllo"))// Where am i?
	     {
		this.Say( "Thou'rt in the wonderful town of Ocllo!" ); // Thou'rt in the wonderful town of Ocllo!
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Serpent's Hold"))// Where am i?
	     {
		this.Say( "Thou'rt in Serpent's Hold." ); // Thou'rt in Serpent's Hold
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Skara Brae"))// Where am i?
	     {
		this.Say( "Thou'rt in Skara Brae." ); // Thou'rt in Skara Brae
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Papua"))// Where am i?
	     {
		this.Say( "Thou art in Papua." ); // Thou art in Papua.
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Delucia"))// Where am i?
	     {
		this.Say( "Thou'rt in Delucia." ); // Thou'rt Delucia.
	     	}
		else if( e.HasKeyword( 0x0122 ) && (Region.Name == "Haven"))// Where am i?
	     {
		this.Say( "Thou'rt in Haven, young friend." ); // Thou'rt in Haven, young friend.
	     	}
//Where am I? End
//Inns Start

		if( e.HasKeyword( 0x00CC ) && (Region.Name == "Britain"))// Where is the inn
	     {
		switch( Utility.Random( 3 ) )
		 {
		case 0: this.Say( 1014003 ); break; // An inn called Sweet Deams lies next ...
		case 1: this.Say( 1014032 ); break; // The Northside Inn is situated...
		case 2: this.Say( 1014007 ); break; // Despite it's name...
		 }
	     	}
//Inns end

//Moongates? Start
		if( e.HasKeyword( 0x0136 ))// Moongates?
	     {
		this.Say( 1014130 ); // The moongates? They are...
	     	}
//Moongates? End

//Moons Start
		if( e.HasKeyword( 0x0132 ))// Moons
	     {
		this.Say( 1014121 ); // Our moons are...
	     	}
//Moons End 
//Where is Start here.
		if( e.HasKeyword( 0x00A1 ) && (Region.Name != "Britain"))// Where is Britain?
	     {
		this.Say( 1014006 ); // Britain is bounded by two rivers. ...
		}
		else if( e.HasKeyword( 0x00AC ) && (Region.Name != "Trinsic"))// Where is Trinsic?
	     {
		this.Say( 1014157 ); // If thou'rt looking for Trinsic,...
		}
		else if( e.HasKeyword( 0x00A3 ) && (Region.Name != "Jhelom"))// Where is Jhelom?
	     {
		this.Say( 1014159 );  // Jhelom? 'Tis to be found...
		}
		else if( e.HasKeyword( 0x00A8 ) && (Region.Name != "Nujel'm"))// Where is Nujel'm?
	     {
		this.Say( 1014160 );  // Nujel'm is an island city. Part of ...
		}
		else if( e.HasKeyword( 0x00A9 ) && (Region.Name != "Ocllo"))// Where is Ocllo?
	     {
		this.Say( 1014161 );  // Ocllo... hmmm. 'Tis an island, I believe,
		}
		else if( e.HasKeyword( 0x00AB ) && (Region.Name != "Skara Brae"))// Where is Skara Brae?
	     {
		this.Say( 1014165 );  // Skara Brae? Thou canst not get more west than that!
		}
		else if( e.HasKeyword( 0x00A7 ) && (Region.Name != "Moonglow"))// Where is Moonglow?
	     {
		this.Say( 1014173 );  // Verity Isle is Moonglow's whereabouts. ...
		}
		else if( e.HasKeyword( 0x00A5 ) && (Region.Name != "Vesper"))// Where is Vesper?
	     {
		this.Say( 1014174 );  // Vesper can be found on the eastern coast of...
		}
		else if( e.HasKeyword( 0x00AD ) && (Region.Name != "Yew"))// Where is Yew?
	     {
		this.Say( 1014175 );  // Yew.. ahh, that is near to Empath Abbey. ...
		}
//Where is End

//Time Start
		if( e.HasKeyword( 0x009E ))// Time
	     {
		this.Say( "I seem to lose track of time nowa days." );
	     	}
//Time End

//Job 
		if( e.HasKeyword( 0x00F8 ))// Job
	     {
		this.Say( 1014519 ); //If thou hast guild business, ...
	     	}
//Job End
           

}
			base.OnSpeech( e );
		}

		public override bool OnGoldGiven( Mobile from, Gold dropped )
		{
			if ( from is PlayerMobile && dropped.Amount == JoinCost )
			{
				PlayerMobile pm = (PlayerMobile)from;

				if ( pm.NpcGuild == this.NpcGuild )
				{
					SayTo( from, 501047 ); // Thou art already a member of our guild.
				}
				else if ( pm.NpcGuild != NpcGuild.None )
				{
					SayTo( from, 501046 ); // Thou must resign from thy other guild first.
				}
				else if ( pm.GameTime < JoinGameAge || (pm.CreationTime + JoinAge) > DateTime.Now )
				{
					SayTo( from, 501048 ); // You are too young to join my guild...
				}
				else if ( CheckCustomReqs( pm ) )
				{
					SayWelcomeTo( from );

					pm.NpcGuild = this.NpcGuild;
					pm.NpcGuildJoinTime = DateTime.Now;
					pm.NpcGuildGameTime = pm.GameTime;

					dropped.Delete();
					return true;
				}

				return false;
			}

			return base.OnGoldGiven( from, dropped );
		}

		public BaseGuildmaster( string title ) : base( title )
		{
			Title = String.Format( "the {0} {1}", title, Female ? "guildmistress" : "guildmaster" );
		}

		public BaseGuildmaster( Serial serial ) : base( serial )
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