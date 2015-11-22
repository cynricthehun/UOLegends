using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class NorseHelm : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 4; } }
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 4; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 2; } }

		public override int InitMinHits{ get{ return 46; } }
		public override int InitMaxHits{ get{ return 58; } }

		public override int AosStrReq{ get{ return 55; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int ArmorBase{ get{ return 30; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Plate; } }

		[Constructable]
		public NorseHelm() : base( 0x140E )
		{
			Weight = 5.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name  == null )
			{
				if ( this.Identified == true || ( this.Durability == ArmorDurabilityLevel.Regular && this.ProtectionLevel == ArmorProtectionLevel.Regular ) )
				{
					if ( this.Quality != ArmorQuality.Exceptional && this.LootType != LootType.Blessed )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a norse helm" ) );
					}
					else if ( this.Quality != ArmorQuality.Exceptional && this.LootType == LootType.Blessed )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed norse helm" ) );
					}
					else if ( this.Quality == ArmorQuality.Exceptional )
					{
						if ( this.LootType != LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional norse helm" ) );
							}
							else
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional norse helm (crafted by:" + this.Crafter.Name + ")" ) );
							}
						}
						if ( this.LootType == LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional norse helm" ) );
							}
							else
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional norse helm (crafted by:" + this.Crafter.Name + ")" ) );
							}
						}
					}
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a magic norse helm" ) );
				}
				base.OnSingleClick( from );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public NorseHelm( Serial serial ) : base( serial )
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
				Weight = 5.0;
		}
	}
}