using System;
using System.Collections;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Gumps;

namespace Server.Items
{
	public class Bandage : Item, IDyable
	{
		[Constructable]
		public Bandage() : this( 1 )
		{
		}

		[Constructable]
		public Bandage( int amount ) : base( 0xE21 )
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " clean bandages" );
				}
				else
				{
					LabelTo( from, "a clean bandage" );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Bandage( Serial serial ) : base( serial )
		{
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( GetWorldLocation(), Core.AOS ? 2 : 1 ) )
			{
				from.RevealingAction();

				from.SendAsciiMessage( "Who will you use the bandages on?" );

				from.Target = new InternalTarget( this );
			}
			else
			{
				from.SendAsciiMessage( "You are too far away to do that." );
			}
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new Bandage(), amount );
		}

		private class InternalTarget : Target
		{
			private Bandage m_Bandage;

			public InternalTarget( Bandage bandage ) : base( 1, false, TargetFlags.Beneficial )
			{
				m_Bandage = bandage;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Bandage.Deleted )
					return;

				if ( targeted is Mobile )
				{
					if ( from.InRange( m_Bandage.GetWorldLocation(), Core.AOS ? 2 : 1 ) )
					{
						if ( BandageContext.BeginHeal( from, (Mobile)targeted ) != null )
							m_Bandage.Consume();
					}
					else
					{
						from.SendAsciiMessage( "You are too far away to do that." ); // 
					}
				}
				else
				{
					from.SendAsciiMessage( "Bandages can not be used on that." ); // 
				}
			}
		}
	}

	public class BandageContext
	{
		private Mobile m_Healer;
		private Mobile m_Patient;
		private int m_Slips;
		private Timer m_Timer;

		public Mobile Healer{ get{ return m_Healer; } }
		public Mobile Patient{ get{ return m_Patient; } }
		public int Slips{ get{ return m_Slips; } set{ m_Slips = value; } }
		public Timer Timer{ get{ return m_Timer; } }

		public void Slip()
		{
			m_Healer.SendAsciiMessage( "Your fingers slip!" );
			++m_Slips;
		}

		public BandageContext( Mobile healer, Mobile patient, TimeSpan delay )
		{
			m_Healer = healer;
			m_Patient = patient;

			m_Timer = new InternalTimer( this, delay );
			m_Timer.Start();
		}

		public void StopHeal()
		{
			m_Table.Remove( m_Healer );

			if ( m_Timer != null )
				m_Timer.Stop();

			m_Timer = null;
		}

		private static Hashtable m_Table = new Hashtable();

		public static BandageContext GetContext( Mobile healer )
		{
			return (BandageContext)m_Table[healer];
		}

		public static SkillName GetPrimarySkill( Mobile m )
		{
			if ( !m.Player && (m.Body.IsMonster || m.Body.IsAnimal) )
				return SkillName.Veterinary;
			else
				return SkillName.Healing;
		}

		public static SkillName GetSecondarySkill( Mobile m )
		{
			if ( !m.Player && (m.Body.IsMonster || m.Body.IsAnimal) )
				return SkillName.AnimalLore;
			else
				return SkillName.Anatomy;
		}

		public void EndHeal()
		{
			StopHeal();

			string healerstring = "";
			string patientstring = "";
			bool playSound = true;
			bool checkSkills = false;

			SkillName primarySkill = GetPrimarySkill( m_Patient );
			SkillName secondarySkill = GetSecondarySkill( m_Patient );

			BaseCreature petPatient = m_Patient as BaseCreature;

			if ( !m_Healer.Alive )
			{
				healerstring = "You were unable to finish your work before you died.";
				patientstring = "";
				playSound = false;
			}
			else if ( !m_Healer.InRange( m_Patient, Core.AOS ? 2 : 1 ) )
			{
				healerstring = "You did not stay close enough to heal your target.";
				patientstring = "";
				playSound = false;
			}
			else if ( !m_Patient.Alive || (petPatient != null && petPatient.IsDeadPet) )
			{
				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = ((healing - 68.0) / 50.0) - (m_Slips * 0.02);

				if ( (checkSkills = (healing >= 80.0 && anatomy >= 80.0)) && chance > Utility.RandomDouble() )
				{
					if ( m_Patient.Map == null || !m_Patient.Map.CanFit( m_Patient.Location, 16, false, false ) )
					{
						healerstring = "Target can not be resurrected at that location."; 
						patientstring = "Thou can not be resurrected there!"; // 
					}
					else if ( m_Patient.Region != null && m_Patient.Region.Name == "Khaldun" )
					{
						healerstring = "The veil of death in this area is too strong and resists thy efforts to restore life."; 
						patientstring = "";
					}
					else
					{
						healerstring = "You are able to resurrect your patient."; // 
						patientstring = "";

						m_Patient.PlaySound( 0x214 );
						m_Patient.FixedEffect( 0x376A, 10, 16 );

						if ( petPatient != null && petPatient.IsDeadPet )
						{
							Mobile master = petPatient.ControlMaster;

							if ( master != null && master.InRange( petPatient, 3 ) )
							{
								healerstring = "You are able to resurrect the creature.";

								master.CloseGump( typeof( PetResurrectGump ) );
								master.SendGump( new PetResurrectGump( m_Healer, petPatient ) );
							}
							else
							{
								bool found = false;

								ArrayList friends = petPatient.Friends;

								for ( int i = 0; friends != null && i < friends.Count; ++i )
								{
									Mobile friend = (Mobile) friends[i];

									if ( friend.InRange( petPatient, 3 ) )
									{
										healerstring = "You are able to resurrect the creature."; // 

										friend.CloseGump( typeof( PetResurrectGump ) );
										friend.SendGump( new PetResurrectGump( m_Healer, petPatient ) );

										found = true;
										break;
									}
								}

								if ( !found )
									healerstring = "The pet's owner must be nearby to attempt resurrection."; // 
							}
						}
						else
						{
							m_Patient.CloseGump( typeof( ResurrectGump ) );
							m_Patient.SendGump( new ResurrectGump( m_Patient, m_Healer ) );
						}
					}
				}
				else
				{
					if ( petPatient != null && petPatient.IsDeadPet )
						healerstring = "You fail to resurrect the creature."; // 
					else
						healerstring = "You are unable to resurrect your patient."; // 

					patientstring = "";
				}
			}
			else if ( m_Patient.Poisoned )
			{
				m_Healer.SendAsciiMessage( "You finish applying the bandages." ); // 

				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = ((healing - 30.0) / 50.0) - (m_Patient.Poison.Level * 0.1) - (m_Slips * 0.02);

				if ( (checkSkills = (healing >= 60.0 && anatomy >= 60.0)) && chance > Utility.RandomDouble() )
				{
					if ( m_Patient.CurePoison( m_Healer ) )
					{
						healerstring = (m_Healer == m_Patient) ? "" : "You have cured the target of all poisons.";
						patientstring = "You have been cured of all poisons."; // 
					}
					else
					{
						healerstring = "";
						patientstring = "";
					}
				}
				else
				{
					healerstring = "You have failed to cure your target!";
					patientstring = "";
				}
			}
			else if ( BleedAttack.IsBleeding( m_Patient ) )
			{
				healerstring = "";
				patientstring = "The bleeding wounds have healed, you are no longer bleeding!";

				BleedAttack.EndBleed( m_Patient, true );
			}
			else if ( MortalStrike.IsWounded( m_Patient ) )
			{
				healerstring = ( m_Healer == m_Patient ? "" : "" );
				patientstring = "";
				playSound = false;
			}
			else if ( m_Patient.Hits == m_Patient.HitsMax )
			{
				healerstring = "You heal what little damage your patient had."; // 
				patientstring = "";
			}
			else
			{
				checkSkills = true;
				patientstring = "";

				double healing = m_Healer.Skills[primarySkill].Value;
				double anatomy = m_Healer.Skills[secondarySkill].Value;
				double chance = ((healing + 10.0) / 100.0) - (m_Slips * 0.02);

				if ( chance > Utility.RandomDouble() )
				{
					healerstring = "You finish applying the bandages.";

					double min, max;

					if ( Core.AOS )
					{
						min = (anatomy / 8.0) + (healing / 5.0) + 4.0;
						max = (anatomy / 6.0) + (healing / 2.5) + 4.0;
					}
					else
					{
						min = (anatomy / 5.0) + (healing / 5.0) + 3.0;
						max = (anatomy / 5.0) + (healing / 2.0) + 10.0;
					}

					double toHeal = min + (Utility.RandomDouble() * (max - min));

					if ( m_Patient.Body.IsMonster || m_Patient.Body.IsAnimal )
						toHeal += m_Patient.HitsMax / 100;

					if ( Core.AOS )
						toHeal -= toHeal * m_Slips * 0.35; // TODO: Verify algorithm
					else
						toHeal -= m_Slips * 4;

					if ( toHeal < 1 )
					{
						toHeal = 1;
						healerstring = "You apply the bandages, but they barely help.";
					}

					m_Patient.Heal( (int) toHeal );
				}
				else
				{
					healerstring = "You apply the bandages, but they barely help.";
					playSound = false;
				}
			}

			if ( healerstring != "" )
				m_Healer.SendAsciiMessage( healerstring );

			if ( patientstring != "" )
				m_Patient.SendAsciiMessage( patientstring );

			if ( playSound )
				m_Patient.PlaySound( 0x57 );

			if ( checkSkills )
			{
				m_Healer.CheckSkill( secondarySkill, 0.0, 120.0 );
				m_Healer.CheckSkill( primarySkill, 0.0, 120.0 );
			}
		}

		private class InternalTimer : Timer
		{
			private BandageContext m_Context;

			public InternalTimer( BandageContext context, TimeSpan delay ) : base( delay )
			{
				m_Context = context;
				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				m_Context.EndHeal();
			}
		}

		public static BandageContext BeginHeal( Mobile healer, Mobile patient )
		{
			bool isDeadPet = ( patient is BaseCreature && ((BaseCreature)patient).IsDeadPet );

			if ( patient is Golem )
			{
				healer.SendAsciiMessage( "Bandages cannot be used on that." );
			}
			else if ( patient is BaseCreature && ((BaseCreature)patient).IsAnimatedDead )
			{
				healer.SendAsciiMessage( "You cannot heal that." );
			}
			else if ( !patient.Poisoned && patient.Hits == patient.HitsMax && !BleedAttack.IsBleeding( patient ) && !isDeadPet )
			{
				healer.SendAsciiMessage( "That being is not damaged!" );
			}
			else if ( !patient.Alive && (patient.Map == null || !patient.Map.CanFit( patient.Location, 16, false, false )) )
			{
				healer.SendAsciiMessage( "Target cannot be resurrected at that location." );
			}
			else if ( healer.CanBeBeneficial( patient, true, true ) )
			{
				healer.DoBeneficial( patient );

				bool onSelf = ( healer == patient );
				int dex = healer.Dex;

				double seconds;
				double resDelay = ( patient.Alive ? 0.0 : 5.0 );

				if ( onSelf )
				{
					if ( Core.AOS )
						seconds = 5.0 + (0.5 * ((double)(120 - dex) / 10)); // TODO: Verify algorithm
					else
						seconds = 9.4 + (0.6 * ((double)(120 - dex) / 10));
				}
				else
				{
					if ( Core.AOS && GetPrimarySkill( patient ) == SkillName.Veterinary )
					{
						//if ( dex >= 40 )
							seconds = 2.0;
						//else
						//	seconds = 3.0;
					}
					else
					{
						if ( dex >= 100 )
							seconds = 3.0 + resDelay;
						else if ( dex >= 40 )
							seconds = 4.0 + resDelay;
						else
							seconds = 5.0 + resDelay;
					}
				}

				BandageContext context = GetContext( healer );

				if ( context != null )
					context.StopHeal();

				context = new BandageContext( healer, patient, TimeSpan.FromSeconds( seconds ) );

				m_Table[healer] = context;

				if ( !onSelf )
					patient.SendAsciiMessage( healer.Name + " : Attempting to heal you." );

				healer.SendAsciiMessage( "You begin applying the bandages." );
				return context;
			}

			return null;
		}
	}
}
