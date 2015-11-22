using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Engines.VeteranRewards;
using Server.Dueling;

namespace Server.Items 
{
	public class SendGump : Item
	{
		[Constructable]
		public SendGump() : base( 0x12AB )
		{
			Name = "a gump sender";
		}

		public SendGump( Serial serial ) : base( serial ) 
		{
		}
	
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
	
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	
		public override void OnDoubleClick( Mobile from )
		{
			from.SendGump ( new DuelRankGump( from ) );
		}
	}
}
