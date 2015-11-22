using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Sixth
{
	public class ExplosionSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Explosion", "Vas Ort Flam",
				SpellCircle.Sixth,
				230,
				9041,
				Reagent.Bloodmoss,
				Reagent.MandrakeRoot,
				Reagent.SulfurousAsh
			);

		public ExplosionSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return false; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendAsciiMessage( "Target can not be seen." );
			}
			else if ( Caster.CanBeHarmful( m ) && CheckSequence() )
			{
				Mobile attacker = Caster, defender = m;

				SpellHelper.Turn( Caster, m );

				SpellHelper.CheckReflect( (int)this.Circle, Caster, ref m );

				InternalTimer t = new InternalTimer( this, attacker, defender, m );
				t.Start();
			}

			FinishSequence();
		}

		private class InternalTimer : Timer
		{
			private Spell m_Spell;
			private Mobile m_Target;
			private Mobile m_Attacker, m_Defender;

			public InternalTimer( Spell spell, Mobile attacker, Mobile defender, Mobile target ) : base( TimeSpan.FromSeconds( Core.AOS ? 3.3 : 3.3 ) )
			{
				m_Spell = spell;
				m_Attacker = attacker;
				m_Defender = defender;
				m_Target = target;

				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				if ( m_Attacker.HarmfulCheck( m_Defender ) )
				{
					double damage;

					if ( Core.AOS )
					{
						damage = m_Spell.GetNewAosDamage( 38, 1, 5, m_Attacker.Player && m_Defender.Player );
					}
					else
					{
						damage = Utility.Random( 20, 17 );

						if ( m_Spell.CheckResisted( m_Target ) )
						{
							damage *= 0.75;

							m_Target.SendAsciiMessage( "You feel yourself resisting magical energy." );
						}

						damage *= m_Spell.GetDamageScalar( m_Target );
					}

					m_Target.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
					m_Target.PlaySound( 0x307 );

					SpellHelper.Damage( m_Spell, m_Target, damage, 0, 100, 0, 0, 0 );
				}
			}
		}

		private class InternalTarget : Target
		{
			private ExplosionSpell m_Owner;

			public InternalTarget( ExplosionSpell owner ) : base( 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		public override TimeSpan GetCastDelay()
		{
			return base.GetCastDelay() + TimeSpan.FromSeconds( 3.0 );
		}
	}
}
