using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x144f, 0x1454 )]
	public class BoneChest : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 2; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		public override int InitMinHits{ get{ return 26; } }
		public override int InitMaxHits{ get{ return 30; } }

		public override int AosStrReq{ get{ return 60; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int OldDexBonus{ get{ return -6; } }

		public override int ArmorBase{ get{ return 30; } }
		public override int RevertArmorBase{ get{ return 11; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public BoneChest() : base( 0x144F )
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
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "bone armor" ) );
					}
					else if ( this.Quality != ArmorQuality.Exceptional && this.LootType == LootType.Blessed )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "blessed bone armor" ) );
					}
					else if ( this.Quality == ArmorQuality.Exceptional )
					{
						if ( this.LootType != LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "exceptional bone armor" ) );
							}
							else
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "exceptional bone armor (crafted by:" + this.Crafter.Name + ")" ) );
							}
						}
						if ( this.LootType == LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "blessed, exceptional bone armor" ) );
							}
							else
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "blessed, exceptional bone armor (crafted by:" + this.Crafter.Name + ")" ) );
							}
						}
					}
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "magic bone armor" ) );
				}
				base.OnSingleClick( from );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BoneChest( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );

			if ( Weight == 1.0 )
				Weight = 6.0;
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}