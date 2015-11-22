using System;
using Server;

namespace Server.Items
{
	public class Jerky : Food
	{

		private string m_Name;

		[Constructable]
		public Jerky() : this( null )
		{
		}

		[Constructable]
		public Jerky( string name ) : base( 1, 0x978 )
		{
			m_Name = name;
			FillFactor = 1;
			Stackable = false;
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( m_Name == null )
				{
					LabelTo( from, "jerky" );
				}
				else
				{
					LabelTo( from, "jerky from " + m_Name );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Jerky( Serial serial ) : base( serial )
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