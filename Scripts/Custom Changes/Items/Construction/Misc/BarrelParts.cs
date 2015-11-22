using System;

namespace Server.Items
{
	public class BarrelLid : Item
	{
		[Constructable]
		public BarrelLid() : base(0x1DB8)
		{
			Weight = 2;
		}

		public override void OnSingleClick( Mobile from)
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a barrel lid" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BarrelLid(Serial serial) : base(serial)
		{
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

	[FlipableAttribute(0x1EB1, 0x1EB2, 0x1EB3, 0x1EB4)]
	public class BarrelStaves : Item
	{
		[Constructable]
		public BarrelStaves() : base(0x1EB1)
		{
			Weight = 1;
		}

		public override void OnSingleClick( Mobile from)
		{
			if ( this.Name == null )
			{
				LabelTo( from, "barrel staves" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BarrelStaves(Serial serial) : base(serial)
		{
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

	public class BarrelHoops : Item
	{
		[Constructable]
		public BarrelHoops() : base(0x1DB7)
		{
			Weight = 5;
		}

		public override void OnSingleClick( Mobile from)
		{
			if ( this.Name == null )
			{
				LabelTo( from, "barrel hoops" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BarrelHoops(Serial serial) : base(serial)
		{
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

	public class BarrelTap : Item
	{
		[Constructable]
		public BarrelTap() : base(0x1004)
		{
			Weight = 1;
		}

		public override void OnSingleClick( Mobile from)
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a barrel tap" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BarrelTap(Serial serial) : base(serial)
		{
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