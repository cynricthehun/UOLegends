using System;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseRing : BaseJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044176; } } // star sapphire ring

		public BaseRing( int itemID ) : base( itemID, Layer.Ring )
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
						LabelTo( from, this.GemType == GemType.None ? "a ring of " + Effect : Gem + " ring of " + Effect );
						LabelTo( from, "(charges:" + this.Uses + ")" );
					}
					else
					{
						LabelTo( from, this.GemType == GemType.None ? "a magic ring" : Gem + " magic ring" );
					}
				}
				else
				{
					LabelTo( from, this.GemType == GemType.None ? "a ring" : Gem + " ring" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BaseRing( Serial serial ) : base( serial )
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

	public class GoldRing : BaseRing
	{
		[Constructable]
		public GoldRing() : base( 0x108a )
		{
			Weight = 0.1;
		}

		public GoldRing( Serial serial ) : base( serial )
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

	public class SilverRing : BaseRing
	{
		[Constructable]
		public SilverRing() : base( 0x1F09 )
		{
			Weight = 0.1;
		}

		public SilverRing( Serial serial ) : base( serial )
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
