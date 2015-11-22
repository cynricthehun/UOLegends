using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Mobiles
{
	public class WarriorGuard : BaseGuard
	{
		private Timer m_AttackTimer, m_IdleTimer;

		private Mobile m_Focus;

		[Constructable]
		public WarriorGuard() : this( null )
		{
		}

		public WarriorGuard( Mobile target ) : base( target )
		{
			InitStats( 1000, 1000, 1000 );
			Title = "the guard";

			SpeechHue = Utility.RandomDyedHue();

			Hue = Utility.RandomSkinHue();

			if ( Female = Utility.RandomBool() )
			{
				Body = 0x191;
				Name = NameList.RandomName( "female" );

				switch( Utility.Random( 2 ) )
				{
					case 0: AddItem( new LeatherSkirt() ); break;
					case 1: AddItem( new LeatherShorts() ); break;
				}

				switch( Utility.Random( 5 ) )
				{
					case 0: AddItem( new FemaleLeatherChest() ); break;
					case 1: AddItem( new FemaleStuddedChest() ); break;
					case 2: AddItem( new LeatherBustierArms() ); break;
					case 3: AddItem( new StuddedBustierArms() ); break;
					case 4: AddItem( new FemalePlateChest() ); break;
				}
			}
			else
			{
				Body = 0x190;
				Name = NameList.RandomName( "male" );

				AddItem( new PlateChest() );
				AddItem( new PlateArms() );
				AddItem( new PlateLegs() );

				switch( Utility.Random( 3 ) )
				{
					case 0: AddItem( new Doublet( Utility.RandomNondyedHue() ) ); break;
					case 1: AddItem( new Tunic( Utility.RandomNondyedHue() ) ); break;
					case 2: AddItem( new BodySash( Utility.RandomNondyedHue() ) ); break;
				}
			}

			Item hair = new Item( Utility.RandomList( 0x203B, 0x203C, 0x203D, 0x2044, 0x2045, 0x2047, 0x2049, 0x204A ) );

			hair.Hue = Utility.RandomHairHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;

			AddItem( hair );

			if( Utility.RandomBool() && !this.Female )
			{
				Item beard = new Item( Utility.RandomList( 0x203E, 0x203F, 0x2040, 0x2041, 0x204B, 0x204C, 0x204D ) );

				beard.Hue = hair.Hue;
				beard.Layer = Layer.FacialHair;
				beard.Movable = false;

				AddItem( beard );
			}

			Halberd weapon = new Halberd();

			weapon.Movable = false;
			weapon.Crafter = this;
			weapon.Quality = WeaponQuality.Exceptional;

			AddItem( weapon );

			Container pack = new Backpack();

			pack.Movable = false;

			pack.DropItem( new Gold( 10, 25 ) );

			AddItem( pack );

			Skills[SkillName.Anatomy].Base = 120.0;
			Skills[SkillName.Tactics].Base = 120.0;
			Skills[SkillName.Swords].Base = 120.0;
			Skills[SkillName.MagicResist].Base = 120.0;
			Skills[SkillName.DetectHidden].Base = 100.0;

			this.NextCombatTime = DateTime.Now + TimeSpan.FromSeconds( 0.5 );
			this.Focus = target;
		}

public override void OnSpeech( SpeechEventArgs e ) 
{ 
			
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
		this.Say( 1014499 ); // I am a guard.
	     	}
//Job End
           

}


                     	           
            base.OnSpeech( e ); 


 }
		public WarriorGuard( Serial serial ) : base( serial )
		{
		}

		public override bool OnBeforeDeath()
		{
			if ( m_Focus != null && m_Focus.Alive )
				new AvengeTimer( m_Focus ).Start(); // If a guard dies, three more guards will spawn

			return base.OnBeforeDeath();
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public override Mobile Focus
		{
			get
			{
				return m_Focus;
			}
			set
			{
				if ( Deleted )
					return;

				Mobile oldFocus = m_Focus;

				if ( oldFocus != value )
				{
					m_Focus = value;

					if ( value != null )
						this.AggressiveAction( value );

					Combatant = value;

					if ( oldFocus != null && !oldFocus.Alive )
						Say( "Thou hast suffered thy punishment, scoundrel." );

					if ( value != null )
						Say( 500131 ); // Thou wilt regret thine actions, swine!

					if ( m_AttackTimer != null )
					{
						m_AttackTimer.Stop();
						m_AttackTimer = null;
					}

					if ( m_IdleTimer != null )
					{
						m_IdleTimer.Stop();
						m_IdleTimer = null;
					}

					if ( m_Focus != null )
					{
						m_AttackTimer = new AttackTimer( this );
						m_AttackTimer.Start();
						((AttackTimer)m_AttackTimer).DoOnTick();
					}
					else
					{
						m_IdleTimer = new IdleTimer( this );
						m_IdleTimer.Start();
					}
				}
				else if ( m_Focus == null && m_IdleTimer == null )
				{
					m_IdleTimer = new IdleTimer( this );
					m_IdleTimer.Start();
				}
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Focus );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Focus = reader.ReadMobile();

					if ( m_Focus != null )
					{
						m_AttackTimer = new AttackTimer( this );
						m_AttackTimer.Start();
					}
					else
					{
						m_IdleTimer = new IdleTimer( this );
						m_IdleTimer.Start();
					}

					break;
				}
			}
		}

		public override void OnAfterDelete()
		{
			if ( m_AttackTimer != null )
			{
				m_AttackTimer.Stop();
				m_AttackTimer = null;
			}

			if ( m_IdleTimer != null )
			{
				m_IdleTimer.Stop();
				m_IdleTimer = null;
			}

			base.OnAfterDelete();
		}

		private class AvengeTimer : Timer
		{
			private Mobile m_Focus;

			public AvengeTimer( Mobile focus ) : base( TimeSpan.FromSeconds( 2.5 ), TimeSpan.FromSeconds( 1.0 ), 3 )
			{
				m_Focus = focus;
			}

			protected override void OnTick()
			{
				BaseGuard.Spawn( m_Focus, m_Focus, 1, true );
			}
		}

		private class AttackTimer : Timer
		{
			private WarriorGuard m_Owner;

			public AttackTimer( WarriorGuard owner ) : base( TimeSpan.FromSeconds( 0.25 ), TimeSpan.FromSeconds( 0.1 ) )
			{
				m_Owner = owner;
			}

			public void DoOnTick()
			{
				OnTick();
			}

			protected override void OnTick()
			{
				if ( m_Owner.Deleted )
				{
					Stop();
					return;
				}

				m_Owner.Criminal = false;
				m_Owner.Kills = 0;
				m_Owner.Stam = m_Owner.StamMax;

				Mobile target = m_Owner.Focus;

				if ( target != null && (target.Deleted || !target.Alive || !m_Owner.CanBeHarmful( target )) )	
				{
					m_Owner.Focus = null;
					Stop();
					return;
				}
				else if ( m_Owner.Weapon is Fists )
				{
					m_Owner.Kill();
					Stop();
					return;
				}

				if ( target != null && m_Owner.Combatant != target )
					m_Owner.Combatant = target;

				if ( target == null )
				{
					Stop();
				}
				else
				{// <instakill>
					TeleportTo( target );
					target.BoltEffect( 0 );

					if ( target is BaseCreature )
						((BaseCreature)target).NoKillAwards = true;

					target.Damage( target.HitsMax, m_Owner );
					target.Kill(); // just in case, maybe Damage is overriden on some shard

					if ( target.Corpse != null && !target.Player )
						target.Corpse.Delete();

					m_Owner.Focus = null;
					Stop();
				}// </instakill>
				/*else if ( !m_Owner.InRange( target, 20 ) )
				{
					m_Owner.Focus = null;
				}
				else if ( !m_Owner.InRange( target, 10 ) || !m_Owner.InLOS( target ) )
				{
					TeleportTo( target );
				}
				else if ( !m_Owner.InRange( target, 1 ) )
				{
					if ( !m_Owner.Move( m_Owner.GetDirectionTo( target ) | Direction.Running ) )
						TeleportTo( target );
				}
				else if ( !m_Owner.CanSee( target ) )
				{
					if ( !m_Owner.UseSkill( SkillName.DetectHidden ) && Utility.Random( 50 ) == 0 )
						m_Owner.Say( "Reveal!" );
				}*/
			}

			private void TeleportTo( Mobile target )
			{
				Point3D from = m_Owner.Location;
				Point3D to = target.Location;

				m_Owner.Location = to;

				Effects.SendLocationParticles( EffectItem.Create( from, m_Owner.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
				Effects.SendLocationParticles( EffectItem.Create(   to, m_Owner.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );

				m_Owner.PlaySound( 0x1FE );
			}
		}

		private class IdleTimer : Timer
		{
			private WarriorGuard m_Owner;
			private int m_Stage;

			public IdleTimer( WarriorGuard owner ) : base( TimeSpan.FromSeconds( 2.0 ), TimeSpan.FromSeconds( 2.5 ) )
			{
				m_Owner = owner;
			}

			protected override void OnTick()
			{
				if ( m_Owner.Deleted )
				{
					Stop();
					return;
				}

				if ( (m_Stage++ % 4) == 0 || !m_Owner.Move( m_Owner.Direction ) )
					m_Owner.Direction = (Direction)Utility.Random( 8 );

				if ( m_Stage > 16 )
				{
					Effects.SendLocationParticles( EffectItem.Create( m_Owner.Location, m_Owner.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
					m_Owner.PlaySound( 0x1FE );

					m_Owner.Delete();
				}
			}
		}
	}
}