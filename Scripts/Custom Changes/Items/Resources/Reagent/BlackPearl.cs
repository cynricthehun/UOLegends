using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BlackPearl : BaseReagent, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( "{0} black pearl", Amount );
			}
		}

		[Constructable]
		public BlackPearl() : this( 1 )
		{
		}

		[Constructable]
		public BlackPearl( int amount ) : base( 0xF7A, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " black pearls" );
				}
				else
				{
					LabelTo( from, "black pearl" );	
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BlackPearl( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new BlackPearl( amount ), amount );
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