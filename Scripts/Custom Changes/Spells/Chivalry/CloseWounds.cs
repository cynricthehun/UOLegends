using System;
using System.Collections;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Spells.Chivalry
{
	public class CloseWoundsSpell : PaladinSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Close Wounds", "Obsu Vulni",
				SpellCircle.Seventh,
				-1,
				9002
			);

		public override double RequiredSkill{ get{ return 0.0; } }
		public override int RequiredMana{ get{ return 10; } }
		public override int RequiredTithing{ get{ return 10; } }
		public override int MantraNumber{ get{ return 1060719; } } // Obsu Vulni

		public CloseWoundsSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.InRange( m, 2 ) )
			{
			}
			else if ( m is BaseCreature && ((BaseCreature)m).IsAnimatedDead )
			{
			}
			else if ( m.IsDeadBondedPet )
			{
			}
			else if ( m.Hits >= m.HitsMax )
			{
			}
			else if ( m.Poisoned || Server.Items.MortalStrike.IsWounded( m ) )
			{
				Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, (Caster == m) ? 1005000 : 1010398 );
			}
			else if ( CheckBSequence( m ) )
			{
				SpellHelper.Turn( Caster, m );

				/* Heals the target for 7 to 39 points of damage.
				 * The caster's Karma affects the amount of damage healed.
				 */

				int toHeal = ComputePowerValue( 6 );

				// TODO: Should caps be applied?
				if ( toHeal < 7 )
					toHeal = 7;
				else if ( toHeal > 39 )
					toHeal = 39;

				if ( (m.Hits + toHeal) > m.HitsMax )
					toHeal = m.HitsMax - m.Hits;

				m.Hits += toHeal;


				m.PlaySound( 0x202 );
				m.FixedParticles( 0x376A, 1, 62, 9923, 3, 3, EffectLayer.Waist );
				m.FixedParticles( 0x3779, 1, 46, 9502, 5, 3, EffectLayer.Waist );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private CloseWoundsSpell m_Owner;

			public InternalTarget( CloseWoundsSpell owner ) : base( 12, false, TargetFlags.Beneficial )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile) o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
