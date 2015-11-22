using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MeteorSwarmScroll : SpellScroll
	{
		[Constructable]
		public MeteorSwarmScroll() : this( 1 )
		{
		}

		[Constructable]
		public MeteorSwarmScroll( int amount ) : base( 54, 0x1F63, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " meteor storm scrolls" );
				}
				else
				{
					LabelTo( from, "a meteor storm scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public MeteorSwarmScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new MeteorSwarmScroll( amount ), amount );
		}
	}
}