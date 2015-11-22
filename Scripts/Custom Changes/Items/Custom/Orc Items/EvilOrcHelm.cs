using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13B2, 0x13B1 )]
	public class EvilOrcHelm : BaseArmor
	{
		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }
		public override int OldStrReq{ get{ return 10; } }
		public override int ArmorBase{ get{ return 20; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }
		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		[Constructable]
		public EvilOrcHelm() : base( 0x1F0B )
		{
			Weight = 6.0;
			Layer = Layer.Helm;
			StrRequirement = 60;
			Hue = 2413;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name  == null )
			{
				if ( this.Identified == true || ( this.Durability == ArmorDurabilityLevel.Regular && this.ProtectionLevel == ArmorProtectionLevel.Regular ) )
				{
					if ( this.Quality != ArmorQuality.Exceptional && this.LootType != LootType.Blessed )
					{
						LabelTo( from, "an evil orc helm" );
					}
					else if ( this.Quality != ArmorQuality.Exceptional && this.LootType == LootType.Blessed )
					{
						LabelTo( from, "a blessed evil orc helm" );
					}
					else if ( this.Quality == ArmorQuality.Exceptional )
					{
						if ( this.LootType != LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								LabelTo( from, "an exceptional evil orc helm" );
							}
							else
							{
								LabelTo( from, "an exceptional evil orc helm (crafted by:" + this.Crafter.Name + ")" );
							}
						}
						if ( this.LootType == LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								LabelTo( from, "a blessed, exceptional evil orc helm" );
							}
							else
							{
								LabelTo( from, "a blessed, exceptional evil orc helm (crafted by:" + this.Crafter.Name + ")" );
							}
						}
					}
				}
				else
				{
					LabelTo( from, "a magic evil orc helm" );
				}
				base.OnSingleClick( from );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public override bool CanEquip( Mobile m )
		{
			if ( !base.CanEquip( m ) )
				return false;

			if ( m.BodyMod == 183 || m.BodyMod == 184 )
			{
				m.SendAsciiMessage( "You can't do that while wearing savage kin paint." );
				return false;
			}

			return true;
		}

		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( parent is Mobile )
				Misc.Titles.AwardKarma( (Mobile)parent, -20, true );
		}

            		public EvilOrcHelm( Serial serial ) : base( serial )
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
