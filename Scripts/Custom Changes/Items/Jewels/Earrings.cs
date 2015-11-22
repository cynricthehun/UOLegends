using System;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseEarrings : BaseJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044203; } } // star sapphire earrings

		public BaseEarrings( int itemID ) : base( itemID, Layer.Earrings )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			string Gem = "";
			string Effect = "";
			switch( this.GemType )
			{
				case GemType.None:{ Gem = ""; break; }
				case GemType.StarSapphire:{ Gem = "star sapphire"; break; }
				case GemType.Emerald:{ Gem = "aemerald"; break; }
				case GemType.Sapphire:{ Gem = "sapphire"; break; }
				case GemType.Ruby:{ Gem = "ruby"; break; }
				case GemType.Citrine:{ Gem = "citrine"; break; }
				case GemType.Amethyst:{ Gem = "amethyst"; break; }
				case GemType.Tourmaline:{ Gem = "tourmaline"; break; }
				case GemType.Amber:{ Gem = "amber"; break; }
				case GemType.Diamond:{ Gem = "diamond"; break; }
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
						LabelTo( from, this.GemType == GemType.None ? "earrings of " + Effect : Gem + " earrings of " + Effect );
						LabelTo( from, "(charges:" + this.Uses + ")" );
					}
					else
					{
						LabelTo( from, this.GemType == GemType.None ? "magic earrings" : Gem + " magic earrings" );
					}
				}
				else
				{
					LabelTo( from, this.GemType == GemType.None ? "earrings" : Gem + " earrings" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BaseEarrings( Serial serial ) : base( serial )
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

	public class GoldEarrings : BaseEarrings
	{
		[Constructable]
		public GoldEarrings() : base( 0x1087 )
		{
			Weight = 0.1;
		}

		public GoldEarrings( Serial serial ) : base( serial )
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

	public class SilverEarrings : BaseEarrings
	{
		[Constructable]
		public SilverEarrings() : base( 0x1F07 )
		{
			Weight = 0.1;
		}

		public SilverEarrings( Serial serial ) : base( serial )
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