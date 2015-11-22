using System;
using Server;

namespace Server.Items
{
	public class DeathShroud : BaseSuit
	{
		[Constructable]
		public DeathShroud() : base( AccessLevel.GameMaster, 0x0, 0x204E )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a death shroud" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public DeathShroud( Serial serial ) : base( serial )
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