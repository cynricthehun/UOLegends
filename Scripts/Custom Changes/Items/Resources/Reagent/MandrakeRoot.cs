using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MandrakeRoot : BaseReagent, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( "{0} mandrake root", Amount );
			}
		}

		[Constructable]
		public MandrakeRoot() : this( 1 )
		{
		}

		[Constructable]
		public MandrakeRoot( int amount ) : base( 0xF86, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " mandrake roots" );
				}
				else
				{
					LabelTo( from, "mandrake root" );	
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public MandrakeRoot( Serial serial ) : base( serial )
		{
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new MandrakeRoot( amount ), amount );
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