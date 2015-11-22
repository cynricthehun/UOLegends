using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MagicReflectScroll : SpellScroll
	{
		[Constructable]
		public MagicReflectScroll() : this( 1 )
		{
		}

		[Constructable]
		public MagicReflectScroll( int amount ) : base( 35, 0x1F50, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " magic reflection scrolls" );
				}
				else
				{
					LabelTo( from, "a magic reflection scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public MagicReflectScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new MagicReflectScroll( amount ), amount );
		}
	}
}