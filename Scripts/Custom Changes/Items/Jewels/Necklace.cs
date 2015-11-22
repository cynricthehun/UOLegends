using System;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseNecklace : BaseJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044241; } } // star sapphire necklace

		public BaseNecklace( int itemID ) : base( itemID, Layer.Neck )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			string Gem = "";
			string Effect = "";
			switch( this.GemType )
			{
				case GemType.None:{ Gem = ""; break; }
				case GemType.StarSapphire:{ Gem = "a star sapphire"; break; }
				case GemType.Emerald:{ Gem = "an emerald"; break; }
				case GemType.Sapphire:{ Gem = "a sapphire"; break; }
				case GemType.Ruby:{ Gem = "a ruby"; break; }
				case GemType.Citrine:{ Gem = "a citrine"; break; }
				case GemType.Amethyst:{ Gem = "an amethyst"; break; }
				case GemType.Tourmaline:{ Gem = "a tourmaline"; break; }
				case GemType.Amber:{ Gem = "a amber"; break; }
				case GemType.Diamond:{ Gem = "a diamond"; break; }
			}
			switch( this.Effect )
			{
				case MagicEffect.None:{ Effect = ""; break;}
				case MagicEffect.Clumsy:{ Effect = "clumsy"; break;}
				case MagicEffect.Feeblemind:{ Effect = "feeblemind"; break;}
				case MagicEffect.Nightsight:{ Effect = "night sight"; break;}
				case MagicEffect.Weaken:{ Effect = "weaken"; break;}
				case MagicEffect.Agility:{ Effect = "agility"; break;}
				case MagicEffect.Cunning:{ Effect = "cunning"; break;}
				case MagicEffect.Protection:{ Effect = "protection"; break;}
				case MagicEffect.Stength:{ Effect = "strength"; break;}
				case MagicEffect.Bless:{ Effect = "bless"; break;}
				case MagicEffect.Invisibility:{ Effect = "invisibility"; break;}
				case MagicEffect.MagicReflection: { Effect = "magic reflection"; break;}
			}
			if ( this.Name == null )
			{
				if ( this.Effect != MagicEffect.None && this.Uses > 0 )
				{
					if ( this.Identified == true )
					{
						LabelTo( from, this.GemType == GemType.None ? "a necklace of " + Effect : Gem + " necklace of " + Effect );
						LabelTo( from, "(charges:" + this.Uses + ")" );
					}
					else
					{
						LabelTo( from, this.GemType == GemType.None ? "a magic necklace" : Gem + " magic necklace" );
					}
				}
				else
				{
					LabelTo( from, this.GemType == GemType.None ? "a necklace" : Gem + " necklace" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BaseNecklace( Serial serial ) : base( serial )
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
	}

	public class Necklace : BaseNecklace
	{
		[Constructable]
		public Necklace() : base( 0x1085 )
		{
			Weight = 0.1;
		}

		public Necklace( Serial serial ) : base( serial )
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
	}

	public class GoldNecklace : BaseNecklace
	{
		[Constructable]
		public GoldNecklace() : base( 0x1088 )
		{
			Weight = 0.1;
		}

		public GoldNecklace( Serial serial ) : base( serial )
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
	}

	public class GoldBeadNecklace : BaseNecklace
	{
		[Constructable]
		public GoldBeadNecklace() : base( 0x1089 )
		{
			Weight = 0.1;
		}

		public GoldBeadNecklace( Serial serial ) : base( serial )
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
	}


	public class SilverNecklace : BaseNecklace
	{
		[Constructable]
		public SilverNecklace() : base( 0x1F08 )
		{
			Weight = 0.1;
		}

		public SilverNecklace( Serial serial ) : base( serial )
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
	}

	public class SilverBeadNecklace : BaseNecklace
	{
		[Constructable]
		public SilverBeadNecklace() : base( 0x1F05 )
		{
			Weight = 0.1;
		}

		public SilverBeadNecklace( Serial serial ) : base( serial )
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
	}
}