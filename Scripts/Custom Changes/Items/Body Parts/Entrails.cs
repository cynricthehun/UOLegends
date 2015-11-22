using System;
using Server;

namespace Server.Items
{
	public class Entrails : Item
	{

		private string m_Name;

		[Constructable]
		public Entrails() : this( null )
		{
		}

		[Constructable]
		public Entrails( string name ) : base( 0x1CEF )
		{
			m_Name = name;
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( m_Name == null )
				{
					LabelTo( from, "entrails" );
				}
				else
				{
					LabelTo( from, "the entrails of " + m_Name );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Entrails( Serial serial ) : base( serial )
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