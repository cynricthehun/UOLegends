using System;
using Server;
using Server.BountySystem;
using Server.Items;
using System.Collections;

namespace Server.Items
{
	public class Head : Item, ICarvable
	{
//bount system here
		private DateTime m_CreationTime;
		private Mobile m_Owner;
		private Mobile m_Killer;
		private bool m_Player;

		[CommandProperty( AccessLevel.GameMaster )]
		public DateTime CreationTime
		{
			get{ return m_CreationTime; }
			set{ m_CreationTime = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_Owner; }
			set{ m_Owner = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Killer
		{
			get{ return m_Killer; }
			set{ m_Killer = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsPlayer
		{
			get{ return m_Player; }
			set{ m_Player = value; }
		}
//end bounty system

		private String m_Name;

		[Constructable]
		public Head() : this( null )
		{
		}

		[Constructable]
		public Head( string name ) : base( 0x1CE9 )
		{
			m_Name = name;
			Weight = 1.0;

//bounty system
			m_Player = false;
			m_Owner = null;
			m_Killer = null;
			m_CreationTime = DateTime.Now;
//end bounty system

		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( m_Name == null )
				{
					LabelTo( from, "a head" );
				}
				else
				{
					LabelTo( from, "the head of " + m_Name );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public void Carve( Mobile from, Item item )
		{
			Container backpack = from.Backpack;
			if ( backpack != null && this.IsChildOf( from.Backpack ) )
			{
				from.AddToBackpack( new Brain( m_Name ) );
				from.AddToBackpack( new Skull( m_Name ) );
			}
			else
			{
				new Blood( 0x122D ).MoveToWorld( Location, Map );
				new Brain( m_Name ).MoveToWorld( Location, Map );
				new Skull( m_Name ).MoveToWorld( Location, Map );
			}
			this.Delete();
		}

		public Head( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

//bounty system
			writer.Write( (int) 2 ); // version

			writer.Write( m_Player );
			writer.Write( m_CreationTime );

			if( m_Player )
			{
				writer.Write(  m_Owner );
				writer.Write(  m_Killer );
			}
			writer.Write( m_Name );
//end bounty system
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
//bounty system
			switch( version )
			{
				case 2:
				{
					m_Player = reader.ReadBool();
					m_CreationTime = reader.ReadDateTime();
					if( m_Player )
					{
						m_Owner = reader.ReadMobile();
						m_Killer = reader.ReadMobile();
					}

					goto case 1;
				}
				case 1:
				{
					if( version < 2 )
					{
						m_Owner = null;
						m_Killer = null;
						m_Player = false;
						m_CreationTime = DateTime.Now - BountyBoardEntry.DefaultDecayRate;
					}
					m_Name = reader.ReadString();
					break;
				}
			}
//end bounty system
		}
	}
}