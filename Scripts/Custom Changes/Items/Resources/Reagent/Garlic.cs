using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class Garlic : BaseReagent, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( "{0} garlic", Amount );
			}
		}

		[Constructable]
		public Garlic() : this( 1 )
		{
		}

		[Constructable]
		public Garlic( int amount ) : base( 0xF84, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " garlic" );
				}
				else
				{
					LabelTo( from, "garlic" );	
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Garlic( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Garlic( amount ), amount );
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