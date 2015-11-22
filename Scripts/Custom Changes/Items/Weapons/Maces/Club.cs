using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13b4, 0x13b3 )]
	public class Club : BaseBashing
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ShadowStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Dismount; } }

		public override int AosStrengthReq{ get{ return 40; } }
		public override int AosMinDamage{ get{ return 11; } }
		public override int AosMaxDamage{ get{ return 13; } }
		public override int AosSpeed{ get{ return 44; } }

		public override int OldStrengthReq{ get{ return 10; } }
		public override int OldMinDamage{ get{ return 8; } }
		public override int OldMaxDamage{ get{ return 24; } }
		public override int OldSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		[Constructable]
		public Club() : base( 0x13B4 )
		{
			Weight = 9.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Identified == true || ( this.Slayer == SlayerName.None && this.DurabilityLevel == WeaponDurabilityLevel.Regular && this.DamageLevel == WeaponDamageLevel.Regular && this.AccuracyLevel == WeaponAccuracyLevel.Regular ) )
			{
				if ( this.Quality != WeaponQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a club" ) );
				}
				else if ( this.Quality != WeaponQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed club" ) );
				}
				else if ( this.Quality == WeaponQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional club" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional club (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}
					if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional club" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional club (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}
				}
			}
			else
			{
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a magic club" ) );
			}
			base.OnSingleClick( from );
		}

		public Club( Serial serial ) : base( serial )
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
