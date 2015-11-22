using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class CreateFoodScroll : SpellScroll
	{
		[Constructable]
		public CreateFoodScroll() : this( 1 )
		{
		}

		[Constructable]
		public CreateFoodScroll( int amount ) : base( 1, 0x1F2F, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " create food scrolls" );
				}
				else
				{
					LabelTo( from, "a create food scroll" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public CreateFoodScroll( Serial serial ) : base( serial )
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
			return base.Dupe( new CreateFoodScroll( amount ), amount );
		}
	}
}