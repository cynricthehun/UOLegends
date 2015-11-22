using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;
using Server.Engines.Craft;

namespace Server.Items
{
	public class TinkerTrapTarget : Target
	{
		private BaseTinkerTrapDeed m_Deed;

		public TinkerTrapTarget( BaseTinkerTrapDeed deed ) : base( 1, false, TargetFlags.None )
		{
			m_Deed = deed;
		}

		protected override void OnTarget( Mobile from, object target ) // Override the protected OnTarget() for our feature
		{
			if ( m_Deed.Deleted )
				return;

			if ( target is LockableContainer )
			{
				LockableContainer item = (LockableContainer)target;

				if ( item.TrapType != TrapType.None ) // Check if its already trapped
				{
					from.SendAsciiMessage( "You can only place one trap on an object at a time." );
				}
				else if ( item.Locked != false )
				{
					from.SendAsciiMessage( "You can only trap an unlocked object." );
				}
				else
				{
					item.TrapType = m_Deed.TrapType;
					item.TrapPower = m_Deed.TrapPower;
					item.PlayerTrapped = true;
					from.SendAsciiMessage( "You successfully trap the container." );

					m_Deed.Delete(); // Delete the deed
				}
			}
			else
			{
				from.SendAsciiMessage( "You can only trap lockable chests." );
			}
		}
	}

	public class BaseTinkerTrapDeed : Item, ICraftable
	{
		private TrapType m_TrapType;
		private int m_TrapPower;
		private int m_PotionType;

		[CommandProperty( AccessLevel.GameMaster )]
		public int PotionType
		{
			get 
			{ return m_PotionType; }
			set 
			{ m_PotionType = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public TrapType TrapType
		{
			get 
			{
				return m_TrapType;
			}
			set 
			{
				m_TrapType = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int TrapPower
		{
			get
			{
				return m_TrapPower;
			}
			set
			{
				m_TrapPower = value;
			}
		}

		public BaseTinkerTrapDeed() : base( 0x14F0 )
		{
		}

		public BaseTinkerTrapDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_TrapPower );
			writer.Write( (int) m_TrapType );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_TrapPower = reader.ReadInt();

					goto case 0;
				}

				case 0:
				{
					m_TrapType = (TrapType)reader.ReadInt();

					break;
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				 from.SendAsciiMessage( "That must be in your pack for you to use it." );
			}
			else
			{
				from.SendAsciiMessage( "What would you like to set a trap on?" );
				from.Target = new TinkerTrapTarget( this );
			}
		}

		#region ICraftable Members
		public int OnCraft( int quality, bool makersMark, Mobile from, CraftSystem craftSystem, Type typeRes, BaseTool tool, CraftItem craftItem, int resHue )
		{
			int tink = from.Skills[SkillName.Tinkering].Fixed;
			int rand = Utility.RandomMinMax( 8, 17 );
			this.TrapPower =  ( tink ) / rand;
			return tink;
		}

		#endregion	
	}
}
