using System;
using System.Collections;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.First
{
	public class ReactiveArmorSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Reactive Armor", "Flam Sanct",
				SpellCircle.First,
				236,
				9011,
				Reagent.Garlic,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public ReactiveArmorSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast()
		{
			if ( Core.AOS )
				return true;

			if ( Caster.MeleeDamageAbsorb > 0 )
			{
				Caster.SendAsciiMessage( "This spell is already in effect." );
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendAsciiMessage( "The spell will not adhere to you at this time." );
				return false;
			}

			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( Core.AOS )
			{
				if ( CheckSequence() )
				{
					Mobile targ = Caster;

					ResistanceMod[] mods = (ResistanceMod[])m_Table[targ];

					if ( mods == null )
					{
						targ.PlaySound( 0x1E9 );
						targ.FixedParticles( 0x376A, 9, 32, 5008, EffectLayer.Waist );

						mods = new ResistanceMod[5]
							{
								new ResistanceMod( ResistanceType.Physical, 15 + (int)(targ.Skills[SkillName.Inscribe].Value / 20) ),
								new ResistanceMod( ResistanceType.Fire, -5 ),
								new ResistanceMod( ResistanceType.Cold, -5 ),
								new ResistanceMod( ResistanceType.Poison, -5 ),
								new ResistanceMod( ResistanceType.Energy, -5 )
							};

						m_Table[targ] = mods;

						for ( int i = 0; i < mods.Length; ++i )
							targ.AddResistanceMod( mods[i] );
					}
					else
					{
						targ.PlaySound( 0x1ED );
						targ.FixedParticles( 0x376A, 9, 32, 5008, EffectLayer.Waist );

						m_Table.Remove( targ );

						for ( int i = 0; i < mods.Length; ++i )
							targ.RemoveResistanceMod( mods[i] );
					}
				}

				FinishSequence();
			}
			else
			{
				if ( Caster.MeleeDamageAbsorb > 0 )
				{
					Caster.SendAsciiMessage( "This spell is already in effect." );
				}
				else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
				{
					Caster.SendAsciiMessage( "The spell will not adhere to you at this time." );
				}
				else if ( CheckSequence() )
				{
					if ( Caster.BeginAction( typeof( DefensiveSpell ) ) )
					{
						int value = (int)(Caster.Skills[SkillName.Magery].Value + Caster.Skills[SkillName.Meditation].Value + Caster.Skills[SkillName.Inscribe].Value);
						value /= 3;

						if ( value < 0 )
							value = 1;
						else if ( value > 75 )
							value = 75;

						Caster.MeleeDamageAbsorb = value;

						Caster.FixedParticles( 0x376A, 9, 32, 5008, EffectLayer.Waist );
						Caster.PlaySound( 0x1F2 );
					}
					else
					{
						Caster.SendAsciiMessage( "The spell will not adhere to you at this time." );
					}
				}

				FinishSequence();
			}
		}

		public override TimeSpan GetCastDelay()
		{
			return base.GetCastDelay() + TimeSpan.FromSeconds( 0.5 );
		}
	}
}
