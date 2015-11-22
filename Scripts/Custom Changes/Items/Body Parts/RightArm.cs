using System;
using Server;

namespace Server.Items
{
	public class RightArm : Item, ICarvable
	{
		private String m_Name;

		[Constructable]
		public RightArm() : base( 0x1DA2 )
		{
		}

		[Constructable]
		public RightArm( String name ) : base( 0x1DA2 )
		{
			m_Name = name;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a right arm" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public void Carve( Mobile from, Item item )
		{
			new Blood ( 0x122D ).MoveToWorld( Location, Map );
			Container backpack = from.Backpack;
			if ( backpack != null )
			{
				from.AddToBackpack( new Jerky( m_Name ) );
			}
			this.Delete();
		}

		public RightArm( Serial serial ) : base( serial )
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