using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x141B, 0x141C )]
	public class OrcMask : BaseHat
	{
		[Constructable]
		public OrcMask() : base( 0x141B )
		{
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					LabelTo( from, "an orc mask" );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					LabelTo( from, "a blessed orc mask" );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							LabelTo( from, "an exceptional orc mask" );
						}
						else
						{
							LabelTo( from, "an exceptional orc mask (crafted by:" + this.Crafter.Name + ")" );
						}
					}
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							LabelTo( from, "a blessed, exceptional orc mask" );
						}
						else
						{
							LabelTo( from, "a blessed, exceptional orc mask (crafted by:" + this.Crafter.Name + ")" );
						}
					}
				}
				base.OnSingleClick( from );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public OrcMask( Serial serial ) : base ( serial ) 
		{
		}

		public override void Serialize( GenericWriter writer ) 
		{
		base.Serialize( writer );
		writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
		base.Deserialize( reader );
		int version = reader.ReadInt();
		}
	}
} 
