using System;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
	[FlipableAttribute( 0xE81, 0xE82 )]
	public class ShepherdsCrook : BaseStaff
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.Disarm; } }

		public override int AosStrengthReq{ get{ return 20; } }
		public override int AosMinDamage{ get{ return 13; } }
		public override int AosMaxDamage{ get{ return 15; } }
		public override int AosSpeed{ get{ return 40; } }

		public override int OldStrengthReq{ get{ return 10; } }
		public override int OldMinDamage{ get{ return 3; } }
		public override int OldMaxDamage{ get{ return 12; } }
		public override int OldSpeed{ get{ return 30; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 50; } }

		[Constructable]
		public ShepherdsCrook() : base( 0xE81 )
		{
			Weight = 4.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Identified == true || ( this.Slayer == SlayerName.None && this.DurabilityLevel == WeaponDurabilityLevel.Regular && this.DamageLevel == WeaponDamageLevel.Regular && this.AccuracyLevel == WeaponAccuracyLevel.Regular ) )
			{
				if ( this.Quality != WeaponQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a shepherd's crook" ) );
				}
				else if ( this.Quality != WeaponQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed shepherd's crook" ) );
				}
				else if ( this.Quality == WeaponQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional shepherd's crook" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional shepherd's crook (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}
					if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional shepherd's crook" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional shepherd's crook (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}
				}
			}
			else
			{
				from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a magic shepherd's crook" ) );
			}
			base.OnSingleClick( from );
		}

		public ShepherdsCrook( Serial serial ) : base( serial )
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

			if ( Weight == 2.0 )
				Weight = 4.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendAsciiMessage( "Target the animal you wish to herd." );
			from.Target = new HerdingTarget();
		}

		private class HerdingTarget : Target
		{
			public HerdingTarget() : base( 10, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targ )
			{
				if ( targ is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)targ;
					if ( bc.Body.IsAnimal )
					{
						from.SendAsciiMessage( "Click where you wish the animal to go." );
						from.Target = new InternalTarget( bc );
					}
					else
					{
						from.SendAsciiMessage( "That is not a herdable animal." );
					}
				}
				else
				{
					from.SendAsciiMessage( "You don't seem to be able to persuade that to move." );
				}
			}

			private class InternalTarget : Target
			{
				private BaseCreature m_Creature;

				public InternalTarget( BaseCreature c ) : base( 10, true, TargetFlags.None )
				{
					m_Creature = c;
				}

				protected override void OnTarget( Mobile from, object targ )
				{
					if ( targ is IPoint2D )
					{
						if ( from.CheckTargetSkill( SkillName.Herding, m_Creature, 0, 100 ) )
						{
							m_Creature.TargetLocation = new Point2D( (IPoint2D)targ );
							from.SendAsciiMessage( "The animal walks where it was instructed to." );
						}
						else
						{
							from.SendAsciiMessage( "You don't seem to be able to persuade that to move." );
						}
					}
				}
			}
		}
	}
}
