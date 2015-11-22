using System;
using Server.Targeting;
using Server.Network;
using Server.Items;

namespace Server.Spells.Third
{
	public class MagicLockSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Magic Lock", "An Por",
				SpellCircle.Third,
				215,
				9001,
				Reagent.Garlic,
				Reagent.Bloodmoss,
				Reagent.SulfurousAsh
			);

		public MagicLockSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( LockableContainer targ )
		{
			if ( Multis.BaseHouse.CheckLockedDownOrSecured( targ ) )
			{
				// You cannot cast this on a locked down item.
				Caster.LocalOverheadMessage( MessageType.Regular, 0x22, 501761 );
			}
			else if ( targ.Locked || targ.LockLevel == 0 )
			{
				Caster.SendAsciiMessage( "Target must be an unlocked chest." );
			}
			else if ( CheckSequence() )
			{
				SpellHelper.Turn( Caster, targ );

				Point3D loc = targ.GetWorldLocation();

				Effects.SendLocationParticles(
					EffectItem.Create( loc, targ.Map, EffectItem.DefaultDuration ),
					0x376A, 9, 32, 5020 );

				Effects.PlaySound( loc, targ.Map, 0x1FA );

				// The chest is now locked!
				Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 501763 );

				targ.LockLevel = -255; // signal magic lock
				targ.Locked = true;
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private MagicLockSpell m_Owner;

			public InternalTarget( MagicLockSpell owner ) : base( 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is LockableContainer )
					m_Owner.Target( (LockableContainer)o );
				else
					from.SendAsciiMessage( "Target must be an unlocked chest." );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}

		public override TimeSpan GetCastDelay()
		{
			return base.GetCastDelay() + TimeSpan.FromSeconds( 1.5 );
		}
	}
}
