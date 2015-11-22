using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1766, 0x1768 )]
	public class Cloth : Item, IScissorable, IDyable, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} piece of cloth" : "{0} pieces of cloth", Amount );
			}
		}

		[Constructable]
		public Cloth() : this( 1 )
		{
		}

		[Constructable]
		public Cloth( int amount ) : base( 0x1766 )
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public Cloth( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
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

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Amount > 1 )
			{
				LabelTo( from, this.Amount + " yards of cut cloth" );
			}
			else
			{
				LabelTo( from, "a yard of cut cloth" );
			}
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Cloth(), amount );
		}

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) ) return false;

			base.ScissorHelper( from, new Bandage(), 1 );

			return true;
		}
	}
}
