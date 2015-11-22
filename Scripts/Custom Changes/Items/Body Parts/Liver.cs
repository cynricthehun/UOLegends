using System;
using Server;

namespace Server.Items
{
	public class Liver : Item
	{

		private string m_Name;

		[Constructable]
		public Liver() : this( null )
		{
		}

		[Constructable]
		public Liver( string name ) : base( 0x1CEE )
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
					LabelTo( from, "a liver" );
				}
				else
				{
					LabelTo( from, "the liver of " + m_Name );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Liver( Serial serial ) : base( serial )
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