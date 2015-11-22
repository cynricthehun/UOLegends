using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1405, 0x1404 )]
	public class WarFork : BaseSpear
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.BleedAttack; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 45; } }
		public override int AosMinDamage{ get{ return 12; } }
		public override int AosMaxDamage{ get{ return 13; } }
		public override int AosSpeed{ get{ return 43; } }

		public override int OldStrengthReq{ get{ return 35; } }
		public override int OldMinDamage{ get{ return 4; } }
		public override int OldMaxDamage{ get{ return 32; } }
		public override int OldSpeed{ get{ return 45; } }

		public override int DefHitSound{ get{ return 0x236; } }
		public override int DefMissSound{ get{ return 0x238; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 110; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.Pierce1H; } }

		[Constructable]
		public WarFork() : base( 0x1405 )
		{
			Weight = 9.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Identified == true || ( this.Slayer == SlayerName.None && this.DurabilityLevel == WeaponDurabilityLevel.Regular && this.DamageLevel == WeaponDamageLevel.Regular && this.AccuracyLevel == WeaponAccuracyLevel.Regular ) )
			{
				if ( this.Quality != WeaponQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					if ( this.PoisonCharges < 1 )
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a war fork" ) );
					}
					else
					{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a poisoned war fork" ) );
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "(poison charges:" + this.PoisonCharges + ")" ) );
					}
				}
				else if ( this.Quality != WeaponQuality.Exceptional && this.LootType == LootType.Blessed )
				{
						from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed war fork" ) );
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
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional war fork" ) );
								if ( this.PoisonCharges > 0 )
								{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "(poison charges:" + this.PoisonCharges + ")" ) );
								}
						}
						else
						{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional war fork (crafted by:" + this.Crafter.Name + ")" ) );
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
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional war fork" ) );
							if ( this.PoisonCharges > 0 )
							{
								from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "(poison charges:" + this.PoisonCharges + ")" ) );
							}
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional war fork (crafted by:" + this.Crafter.Name + ")" ) );
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
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a magic war fork" ) );
				if ( this.PoisonCharges > 0 )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "(poison charges:" + this.PoisonCharges + ")" ) );
				}
			}
			base.OnSingleClick( from );
		}

		public WarFork( Serial serial ) : base( serial )
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
