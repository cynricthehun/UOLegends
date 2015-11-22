using System;

namespace Server.SkillHandlers
{
	public class Stealth
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Stealth].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			if ( !m.Hidden )
			{
				m.SendAsciiMessage( "You must hide first." );
			}
			else if ( m.Skills[SkillName.Hiding].Base < ((Core.SE) ? 50.0 : 80.0) )
			{
				m.SendAsciiMessage( "You are not hidden well enough. Become better at hiding." );
			}
			else
			{
				int armorRating = (int) m.ArmorRating;

				if ( armorRating > 25 )
				{
					m.SendAsciiMessage( "You could not hope to move quietly wearing this much armor." );
				}
				else if ( m.CheckSkill( SkillName.Stealth, -20.0 + (armorRating * 2), 80.0 + (armorRating * 2) ) )
				{
					int steps = (int)(m.Skills[SkillName.Stealth].Value / (Core.AOS ? 5.0 : 10.0));

					if ( steps < 1 )
						steps = 1;

					m.AllowedStealthSteps = steps;

					m.SendAsciiMessage( "You begin to move quietly." );

					return TimeSpan.FromSeconds( 10.0 );
				}
				else
				{
					m.SendAsciiMessage( "You fail in your attempt to move unnoticed." );
					m.RevealingAction();
				}
			}

			return TimeSpan.FromSeconds( 10.0 );
		}
	}
}