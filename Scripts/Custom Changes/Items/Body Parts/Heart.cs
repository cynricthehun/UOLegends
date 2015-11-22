using System;
using Server;

namespace Server.Items
{
	public class Heart : Item
	{

		private string m_Name;

		[Constructable]
		public Heart() : this( null )
		{
		}

		[Constructable]
		public Heart( string name ) : base( 0x1CED )
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
					LabelTo( from, "a heart" );
				}
				else
				{
					LabelTo( from, "the heart of " + m_Name );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Heart( Serial serial ) : base( serial )
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