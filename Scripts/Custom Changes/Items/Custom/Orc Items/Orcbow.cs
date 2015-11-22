using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x13B2, 0x13B1 )]
	public class OrcBow : BaseRanged
	{
		public override int EffectID{ get{ return 0xF42; } }
		public override Type AmmoType{ get{ return typeof( Arrow ); } }
		public override Item Ammo{ get{ return new Arrow(); } }

		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.MortalStrike; } }

		public override int AosMinDamage{ get{ return 25; } }
		public override int AosMaxDamage{ get{ return 35; } }
		public override int AosSpeed{ get{ return 35; } }

		public override int OldMinDamage{ get{ return 18; } }
		public override int OldMaxDamage{ get{ return 20; } }
		public override int OldSpeed{ get{ return 20; } }

		public override int DefMaxRange{ get{ return 13; } }

		public override int InitMinHits{ get{ return 31; } }
		public override int InitMaxHits{ get{ return 60; } }

		public override WeaponAnimation DefAnimation{ get{ return WeaponAnimation.ShootBow; } }

		[Constructable]
		public OrcBow() : base( 0x13B2 )
		{
			Weight = 6.0;
			Layer = Layer.TwoHanded;
			StrRequirement = 60;
			Hue = 2106;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Identified == true || ( this.Slayer == SlayerName.None && this.DurabilityLevel == WeaponDurabilityLevel.Regular && this.DamageLevel == WeaponDamageLevel.Regular && this.AccuracyLevel == WeaponAccuracyLevel.Regular ) )
				{
					if ( this.Quality != WeaponQuality.Exceptional && this.LootType != LootType.Blessed )
					{
						LabelTo( from, "an orc bow" );
					}
					else if ( this.Quality != WeaponQuality.Exceptional && this.LootType == LootType.Blessed )
					{
						LabelTo( from, "a blessed orc bow" );
					}
					else if ( this.Quality == WeaponQuality.Exceptional )
					{
						if ( this.LootType != LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								LabelTo( from, "an exceptional orc bow" );
							}
							else
							{
								LabelTo( from, "an exceptional orc bow (crafted by:" + this.Crafter.Name + ")" );
							}
						}
						if ( this.LootType == LootType.Blessed )
						{
							if ( this.Crafter == null )
							{
								LabelTo( from, "a blessed, exceptional orc bow" );
							}
							else
							{
								LabelTo( from, "a blessed, exceptional orc bow (crafted by:" + this.Crafter.Name + ")" );
							}
						}
					}
				}
				else
				{
					LabelTo( from, "a magic orc bow" );
				}
				base.OnSingleClick( from );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public override bool OnFired( Mobile attacker, Mobile defender )
		{
			Container pack = attacker.Backpack;

			if ( attacker.Player && (pack == null || !pack.ConsumeTotal( AmmoType, 2 )) )
				return false;

			attacker.MovingEffect( defender, EffectID, 18, 1, false, false );

			return true;
		}

            		public OrcBow( Serial serial ) : base( serial )
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

			if ( Weight == 7.0 )
				Weight = 6.0;
		}
	}
}