using System;
using Server;

namespace Server.Items
{
	public class Buckler : BaseShield
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 1; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 50; } }

		public override int AosStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 7; } }

		[Constructable]
		public Buckler() : base( 0x1B73 )
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
						LabelTo( from, "a buckler" );
					}
					else if ( this.Quality != ArmorQuality.Exceptional && this.LootType == LootType.Blessed )
					{
						LabelTo( from, "a blessed buckler" );
					}
					else if ( this.Quality == ArmorQuality.Exceptional )
					{
						if ( this.LootType != LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								LabelTo( from, "an exceptional buckler" );
							}
							else
							{
								LabelTo( from, "an exceptional buckler (crafted by:" + this.Crafter.Name + ")" );
							}
						}
						if ( this.LootType == LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								LabelTo( from, "a blessed, exceptional buckler" );
							}
							else
							{
								LabelTo( from, "a blessed, exceptional buckler (crafted by:" + this.Crafter.Name + ")" );
							}
						}
					}
				}
				else
				{
					LabelTo( from, "a magic buckler" );
				}
				base.OnSingleClick( from );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Buckler( Serial serial ) : base(serial)
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
