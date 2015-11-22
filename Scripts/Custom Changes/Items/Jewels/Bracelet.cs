using System;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseBracelet : BaseJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044221; } } // star sapphire bracelet

		public BaseBracelet( int itemID ) : base( itemID, Layer.Bracelet )
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
						LabelTo( from, this.GemType == GemType.None ? "a bracelet of " + Effect : Gem + " bracelet of " + Effect );
						LabelTo( from, "(charges:" + this.Uses + ")" );
					}
					else
					{
						LabelTo( from, this.GemType == GemType.None ? "a magic bracelet" : Gem + " bracelet ring" );
					}
				}
				else
				{
					LabelTo( from, this.GemType == GemType.None ? "a bracelet" : Gem + " bracelet" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BaseBracelet( Serial serial ) : base( serial )
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

	public class GoldBracelet : BaseBracelet
	{
		[Constructable]
		public GoldBracelet() : base( 0x1086 )
		{
			Weight = 0.1;
		}

		public GoldBracelet( Serial serial ) : base( serial )
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

	public class SilverBracelet : BaseBracelet
	{
		[Constructable]
		public SilverBracelet() : base( 0x1F06 )
		{
			Weight = 0.1;
		}

		public SilverBracelet( Serial serial ) : base( serial )
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
