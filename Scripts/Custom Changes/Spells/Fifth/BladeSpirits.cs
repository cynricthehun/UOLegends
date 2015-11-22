using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Fifth
{
	public class BladeSpiritsSpell : Spell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Blade Spirits", "In Jux Hur Ylem", 
				SpellCircle.Fifth,
				266,
				9040,
				false,
				Reagent.BlackPearl,
				Reagent.MandrakeRoot,
				Reagent.Nightshade
			);

		public BladeSpiritsSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override TimeSpan GetCastDelay()
		{
			if ( Core.AOS )
				return TimeSpan.FromTicks( base.GetCastDelay().Ticks * 5 );

			return base.GetCastDelay() + TimeSpan.FromSeconds( 7.5 );
		}

		public override bool CheckCast()
		{
			if ( !base.CheckCast() )
				return false;

			return true;
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( IPoint3D p )
		{
			Map map = Caster.Map;

			SpellHelper.GetSurfaceTop( ref p );

			if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				TimeSpan duration;

				if ( Core.AOS )
					duration = TimeSpan.FromSeconds( 120 );
				else
					duration = TimeSpan.FromSeconds( Utility.Random( 80, 40 ) );

				BaseCreature.Summon( new BladeSpirits(), false, Caster, new Point3D( p ), 0x212, duration );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private BladeSpiritsSpell m_Owner;

			public InternalTarget( BladeSpiritsSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
					m_Owner.Target( (IPoint3D)o );
			}

			protected override void OnTargetOutOfLOS( Mobile from, object o )
			{
				from.SendAsciiMessage( "Target cannot be seen. Try again." );
				from.Target = new InternalTarget( m_Owner );
				from.Target.BeginTimeout( from, TimeoutTime - DateTime.Now );
				m_Owner = null;
			}

			protected override void OnTargetFinish( Mobile from )
			{
				if ( m_Owner != null )
					m_Owner.FinishSequence();
			}
		}
	}
}
