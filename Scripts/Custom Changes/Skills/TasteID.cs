using System;
using Server.Targeting;
using Server.Items;
using Server.Network;
using Server.Mobiles;

namespace Server.SkillHandlers
{
	public class TasteID
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.TasteID].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.Target = new InternalTarget();

			m.SendAsciiMessage( "What would you like to taste?" );

			return TimeSpan.FromSeconds( 1.0 );
		}

		[PlayerVendorTarget]
		private class InternalTarget : Target
		{
			public InternalTarget() :  base ( 2, false, TargetFlags.None )
			{
				AllowNonlocal = true;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
				{
					from.SendAsciiMessage( "You feel that such an action would be inappropriate." );
				}
				else if ( targeted is Food )
				{
					Food food = (Food) targeted;

					if ( from.CheckTargetSkill( SkillName.TasteID, food, 0, 100 ) )
					{
						if ( food.Poison != null )
						{
							food.SendAsciiMessageTo( from, "It appears to have poison smeared on it." );
						}
						else
						{
							// No poison on the food
							food.SendAsciiMessageTo( from, "You detect nothing unusual about this substance." );
						}
					}
					else
					{
						// Skill check failed
						food.SendAsciiMessageTo( from, "You cannot discern anything about this substance." );
					}
				}
				else if ( targeted is BasePotion )
				{
					BasePotion potion = (BasePotion) targeted;

					if ( potion.CheckTasters( from ) )
					{
						potion.SendAsciiMessageTo( from, "You already know what kind of potion that is." );
					}
					else if ( from.CheckTargetSkill( SkillName.TasteID, potion, 0, 100 ) )
					{
						potion.AddTasters( from );
						potion.OnSingleClick( from );
					}
				}
				else if ( targeted is PotionKeg )
				{
					PotionKeg keg = (PotionKeg) targeted;

					if ( keg.Held <= 0 )
					{
						keg.SendAsciiMessageTo( from, "There is nothing in the keg to taste!" );
					}
					if ( keg.CheckTasters( from ) )
					{
						keg.SendAsciiMessageTo( from, "You are already familiar with this keg's contents." );
					}
					else if ( from.CheckTargetSkill( SkillName.TasteID, keg, 0, 100 ) )
					{
						keg.AddTasters( from );
						keg.OnSingleClick( from );
					}
				}
				else
				{
					// The target is not food or potion or potion keg.
					from.SendAsciiMessage( "That's not something you can taste." );
				}
			}

			protected override void OnTargetOutOfRange( Mobile from, object targeted )
			{
				from.SendAsciiMessage( "You are too far away to taste that." );
			}
		}
	}
}