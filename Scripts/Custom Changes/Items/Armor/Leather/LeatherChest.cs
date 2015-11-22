using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x13cc, 0x13d3 )]
	public class LeatherChest : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 3; } }
		public override int BasePoisonResistance{ get{ return 3; } }
		public override int BaseEnergyResistance{ get{ return 3; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 37; } }

		public override int AosStrReq{ get{ return 25; } }
		public override int OldStrReq{ get{ return 15; } }

		public override int ArmorBase{ get{ return 13; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public LeatherChest() : base( 0x13CC )
		{
			Weight = 6.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name  == null )
			{
				if ( this.Identified == true || ( this.Durability == ArmorDurabilityLevel.Regular && this.ProtectionLevel == ArmorProtectionLevel.Regular ) )
				{
					if ( this.Quality != ArmorQuality.Exceptional && this.LootType != LootType.Blessed )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a leather tunic" ) );
					}
					else if ( this.Quality != ArmorQuality.Exceptional && this.LootType == LootType.Blessed )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed leather tunic" ) );
					}
					else if ( this.Quality == ArmorQuality.Exceptional )
					{
						if ( this.LootType != LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional leather tunic" ) );
							}
							else
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional leather tunic (crafted by:" + this.Crafter.Name + ")" ) );
							}
						}
						if ( this.LootType == LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional leather tunic" ) );
							}
							else
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional leather tunic (crafted by:" + this.Crafter.Name + ")" ) );
							}
						}
					}
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a magic leather tunic" ) );
				}
				base.OnSingleClick( from );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public LeatherChest( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 6.0;
		}
	}
}