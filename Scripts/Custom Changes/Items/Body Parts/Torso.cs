using System;
using Server;

namespace Server.Items
{
	public class Torso : Item, ICarvable
	{
		private String m_Name;
		[Constructable]
		public Torso() : base( 0x1D9F )
		{
			Weight = 2.0;
		}

		[Constructable]
		public Torso( String name ) : base( 0x1D9F )
		{
			m_Name = name;
			Weight = 2.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( m_Name == null )
				{
					LabelTo( from, "a torso" );
				}
				else
				{
					LabelTo( from, "the torso of " + m_Name );
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
				from.AddToBackpack( new RibCage( m_Name ) );
				from.AddToBackpack( new Heart( m_Name ) );
				from.AddToBackpack( new Liver( m_Name ) );
				from.AddToBackpack( new Entrails( m_Name ) );
			}
			else
			{
				new Blood( 0x122D ).MoveToWorld( Location, Map );
				new RibCage( m_Name ).MoveToWorld( Location, Map );
				new Heart( m_Name ).MoveToWorld( Location, Map );
				new Liver( m_Name ).MoveToWorld( Location, Map );
				new Entrails( m_Name ).MoveToWorld( Location, Map );
			}
			this.Delete();
		}

		public Torso( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
			writer.Write( m_Name );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Name = reader.ReadString();
					goto case 0;
				}
				case 0:
				{
					break;
				}
			}
		}
	}
}