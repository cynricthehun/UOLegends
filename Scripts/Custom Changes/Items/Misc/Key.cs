using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public enum KeyType
	{
		Copper = 0x100E,
		Gold   = 0x100F,
		Iron   = 0x1010,
		Rusty  = 0x1013
	}

	public interface ILockable
	{
		bool Locked{ get; set; }
		uint KeyValue{ get; set; }
	}

	public class Key : Item
	{
		private string m_Description;
		private uint m_KeyVal;
		private Item m_Link;
		private int m_MaxRange;

		public static uint RandomValue()
		{
			return (uint)(0xFFFFFFFE * Utility.RandomDouble()) + 1;
		}

		public static void RemoveKeys( Mobile m, uint keyValue )
		{
			if ( keyValue == 0 )
				return;

			Container pack = m.Backpack;

			if ( pack != null )
			{
				Item[] keys = pack.FindItemsByType( typeof( Key ), true );

				foreach ( Key key in keys )
					if ( key.KeyValue == keyValue )
						key.Delete();
			}

			BankBox box = m.BankBox;

			if ( box != null )
			{
				Item[] keys = box.FindItemsByType( typeof( Key ), true );

				foreach ( Key key in keys )
					if ( key.KeyValue == keyValue )
						key.Delete();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string Description
		{
			get
			{
				return m_Description;
			}
			set
			{
				m_Description = value;
				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxRange
		{
			get
			{
				return m_MaxRange;
			}

			set
			{
				m_MaxRange = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public uint KeyValue
		{
			get
			{
				return m_KeyVal;
			}

			set
			{
				m_KeyVal = value;
				InvalidateProperties();
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Item Link
		{
			get
			{
				return m_Link;
			}

			set
			{
				m_Link = value;
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			writer.Write( (int) m_MaxRange );

			writer.Write( (Item) m_Link );

			writer.Write( (string) m_Description );
			writer.Write( (uint) m_KeyVal );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				{
					m_MaxRange = reader.ReadInt();

					goto case 1;
				}
				case 1:
				{
					m_Link = reader.ReadItem();

					goto case 0;
				}
				case 0:
				{
					if ( version < 2 || m_MaxRange == 0 )
						m_MaxRange = 3;

					m_Description = reader.ReadString();

					m_KeyVal = reader.ReadUInt();

					break;
				}
			}
		}

		[Constructable]
		public Key() : this( KeyType.Copper, 0 )
		{
		}

		[Constructable]
		public Key( KeyType type ) : this( type, 0 )
		{
		}

		[Constructable]
		public Key( uint val ) : this ( KeyType.Copper, val )
		{
		}

		[Constructable]
		public Key( KeyType type, uint LockVal ) : this( type, LockVal, null )
		{
			m_KeyVal = LockVal;
		}

		public Key( KeyType type, uint LockVal, Item link ) : base( (int)type )
		{
			Weight = 1.0;

			m_MaxRange = 3;
			m_KeyVal = LockVal;
			m_Link = link;
		}

		public Key( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;
			string number;

			if ( m_KeyVal != 0 )
			{
				number = "What shall I use this key on?";
				t = new UnlockTarget( this );
			}
			else
			{
				number = "This key is a key blank. Which key would you like to make a copy of?";
				t = new CopyTarget( this );
			}

			from.SendAsciiMessage( number );
			from.Target = t;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			string desc;

			if ( m_KeyVal == 0 )
				desc = "(blank)";
			else if ( (desc = m_Description) == null || (desc = desc.Trim()).Length <= 0 )
				desc = null;

			if ( desc != null )
				list.Add( desc );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.ItemID == 0x100E )
				LabelTo( from, "a copper key" );
			if ( this.ItemID == 0x100F )
				LabelTo( from, "a gold key" );
			if ( this.ItemID == 0x1010 )
				LabelTo( from, "an iron key" );
			if ( this.ItemID == 0x1013 )
				LabelTo( from, "a rusty key" );

			string desc;

			if ( m_KeyVal == 0 )
				desc = "(blank)";
			else if ( (desc = m_Description) == null || (desc = desc.Trim()).Length <= 0 )
				desc = "";

			if ( desc.Length > 0 )
				from.Send( new AsciiMessage( Serial, ItemID, MessageType.Regular, 0x3B2, 3, null, desc ) );
		}

		private class RenamePrompt : Prompt
		{
			private Key m_Key;

			public RenamePrompt( Key key )
			{
				m_Key = key;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Key.Description = Utility.FixHtml( text );
			}
		}

		private class UnlockTarget : Target
		{
			private Key m_Key;

			public UnlockTarget( Key key ) : base( key.MaxRange, false, TargetFlags.None )
			{
				m_Key = key;
				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				string number;

				if ( targeted == m_Key )
				{
					number = "Enter a description for this key.";

					from.Prompt = new RenamePrompt( m_Key );
				}
				else if ( targeted is ILockable )
				{
					number = "";

					ILockable o = (ILockable)targeted;

					if ( o.KeyValue == m_Key.KeyValue )
					{
						if ( o is BaseDoor && !((BaseDoor)o).UseLocks() )
						{
							number = "This key doesn't seem to unlock that.";
						}
						else
						{
							o.Locked = !o.Locked;

							if ( o is LockableContainer )
							{
								LockableContainer cont = (LockableContainer)o;

								if ( cont.LockLevel == -255 )
									cont.LockLevel = cont.RequiredSkill - 10;
							}

							if ( targeted is Item )
							{
								Item item = (Item)targeted;

								if ( o.Locked )
									item.SendAsciiMessageTo( from, "You lock it." );
								else
									item.SendAsciiMessageTo( from, "You unlock it." );
							}
						}
					}
					else
					{
						number = "This key doesn't seem to unlock that.";
					}
				}
				else
				{
					number = "You can't unlock that!";
				}

				if ( number != "" )
				{
					from.SendAsciiMessage( number );
				}
			}
		}

		private class CopyTarget : Target
		{
			private Key m_Key;

			public CopyTarget( Key key ) : base( 3, false, TargetFlags.None )
			{
				m_Key = key;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				string number;

				if ( targeted is Key )
				{
					Key k = (Key)targeted;

					if ( k.m_KeyVal == 0 )
					{
						number = "This key is also blank.";
					}
					else if ( from.CheckTargetSkill( SkillName.Tinkering, k, 0, 75.0 ) )
					{
						number = "You make a copy of the key.";

						m_Key.Description = k.Description;
						m_Key.KeyValue = k.KeyValue;
						m_Key.Link = k.Link;
						m_Key.MaxRange = k.MaxRange;
					}
					else if ( Utility.RandomDouble() <= 0.1 ) // 10% chance to destroy the key
					{
						from.SendAsciiMessage( "You fail to make a copy of the key." );

						number = "The key was destroyed in the attempt.";

						m_Key.Delete();
					}
					else
					{
						number = "You fail to make a copy of the key.";
					}
				}
				else
				{
					number = "Not a key."; 
				}

				from.SendAsciiMessage( number );
			}
		}
	}
}
