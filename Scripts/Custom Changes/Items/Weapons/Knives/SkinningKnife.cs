using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0xEC4, 0xEC5 )]
	public class SkinningKnife : BaseKnife
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ShadowStrike; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 5; } }
		public override int AosMinDamage{ get{ return 9; } }
		public override int AosMaxDamage{ get{ return 11; } }
		public override int AosSpeed{ get{ return 49; } }

		public override int OldStrengthReq{ get{ return 5; } }
		public override int OldMinDamage{ get{ return 1; } }
		public override int OldMaxDamage{ get{ return 10; } }
		public override int OldSpeed{ get{ return 40; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 40; } }

		[Constructable]
		public SkinningKnife() : base( 0xEC4 )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Identified == true || ( this.Slayer == SlayerName.None && this.DurabilityLevel == WeaponDurabilityLevel.Regular && this.DamageLevel == WeaponDamageLevel.Regular && this.AccuracyLevel == WeaponAccuracyLevel.Regular ) )
			{
				if ( this.Quality != WeaponQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					if ( this.PoisonCharges < 1 )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a skinning knife" ) );
					}
					else
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a poisoned skinning knife" ) );
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "(poison charges:" + this.PoisonCharges + ")" ) );
					}
				}
				else if ( this.Quality != WeaponQuality.Exceptional && this.LootType == LootType.Blessed )
				{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed skinning knife" ) );
					if ( this.PoisonCharges > 0 )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "(poison charges:" + this.PoisonCharges + ")" ) );
					}
				}
				else if ( this.Quality == WeaponQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional skinning knife" ) );
								if ( this.PoisonCharges > 0 )
								{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "(poison charges:" + this.PoisonCharges + ")" ) );
								}
						}
						else
						{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional skinning knife (crafted by:" + this.Crafter.Name + ")" ) );
								if ( this.PoisonCharges > 0 )
								{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "(poison charges:" + this.PoisonCharges + ")" ) );
								}
						}
					}
					if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional skinning knife" ) );
							if ( this.PoisonCharges > 0 )
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "(poison charges:" + this.PoisonCharges + ")" ) );
							}
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional skinning knife (crafted by:" + this.Crafter.Name + ")" ) );
							if ( this.PoisonCharges > 0 )
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "(poison charges:" + this.PoisonCharges + ")" ) );
							}
						}
					}
				}
			}
			else
			{
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a magic skinning knife" ) );
				if ( this.PoisonCharges > 0 )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "(poison charges:" + this.PoisonCharges + ")" ) );
				}
			}
			base.OnSingleClick( from );
		}

		public SkinningKnife( Serial serial ) : base( serial )
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
