using System;
using Server;

namespace Server.Items
{
	public class HeaterShield : BaseShield
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return 50; } }
		public override int InitMaxHits{ get{ return 65; } }

		public override int AosStrReq{ get{ return 90; } }

		public override int ArmorBase{ get{ return 23; } }

		[Constructable]
		public HeaterShield() : base( 0x1B76 )
		{
			Weight = 8.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name  == null )
			{
				if ( this.Identified == true || ( this.Durability == ArmorDurabilityLevel.Regular && this.ProtectionLevel == ArmorProtectionLevel.Regular ) )
				{
					if ( this.Quality != ArmorQuality.Exceptional && this.LootType != LootType.Blessed )
					{
						LabelTo( from, "a heater shield" );
					}
					else if ( this.Quality != ArmorQuality.Exceptional && this.LootType == LootType.Blessed )
					{
						LabelTo( from, "a blessed heater shield" );
					}
					else if ( this.Quality == ArmorQuality.Exceptional )
					{
						if ( this.LootType != LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								LabelTo( from, "an exceptional heater shield" );
							}
							else
							{
								LabelTo( from, "an exceptional heater shield (crafted by:" + this.Crafter.Name + ")" );
							}
						}
						if ( this.LootType == LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								LabelTo( from, "a blessed, exceptional heater shield" );
							}
							else
							{
								LabelTo( from, "a blessed, exceptional heater shield (crafted by:" + this.Crafter.Name + ")" );
							}
						}
					}
				}
				else
				{
					LabelTo( from, "a magic heater shield" );
				}
				base.OnSingleClick( from );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public HeaterShield( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}
