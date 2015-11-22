using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MindBlastScroll : SpellScroll
	{
		[Constructable]
		public MindBlastScroll() : this( 1 )
		{
		}

		[Constructable]
		public MindBlastScroll( int amount ) : base( 36, 0x1F51, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " mind blast scrolls" );
				}
				else
				{
					LabelTo( from, "a mind blast scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public MindBlastScroll( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new MindBlastScroll( amount ), amount );
		}
	}
}