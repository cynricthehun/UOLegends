using System;

namespace Server.Items
{
	[FlipableAttribute(0x1BD7, 0x1BDA)]
	public class Board : Item, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} board" : "{0} boards", Amount );
			}
		}

		[Constructable]
		public Board() : this(1)
		{
		}

		[Constructable]
		public Board(int amount) : base(0x1BD7)
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " boards" );
				}
				else
				{
					LabelTo( from, "a board" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Board(Serial serial) : base(serial)
		{
		}

		public override Item Dupe(int amount)
		{
			return base.Dupe(new Board(amount), amount);
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}