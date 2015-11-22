using System;
using System.Collections;
using Server.Multis;
using Server.Items;
using Server.Targeting;

namespace Server.Items

{
	public class Keyring : Item
	{
		private ArrayList m_Keys;

		[Constructable]
		public Keyring() : base( 0x1011 )
		{
			Weight = 1.0;
			Stackable = false;
			m_Keys = new ArrayList();
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a key ring" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			string number;

			if ( m_Keys.Count != 0 )
			{
				number = "What shall I use this on?";
				from.Target = new KeyringTarget( this );
			}
			else
			{
				number = "This key ring has no keys in it.";
			}

			from.SendAsciiMessage( number );
		}

		private class KeyringTarget : Target
		{
			private Keyring m_keyring;
			private bool Didit;

			public KeyringTarget( Keyring keyring ) : base( 10, false, TargetFlags.None )
			{
				m_keyring = keyring;
				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted == m_keyring )
				{
					for( int i = 0; i < m_keyring.m_Keys.Count; i++ )
					{
						KeyringEntry entry = m_keyring.m_Keys[i] as KeyringEntry;
						Key key = new Key();
						key.KeyValue = entry.KeyValue;
						key.Link = entry.Link;
						key.MaxRange = entry.MaxRange;
						key.ItemID = entry.ItemID;
						key.LootType = entry.LootType;
						key.Description = entry.Description;
						from.AddToBackpack( key );
						m_keyring.ItemID = 0x1011;
						m_keyring.Weight = 1;
					}
					m_keyring.m_Keys.Clear();
				}
				else if ( targeted is ILockable )
				{
					ILockable o = (ILockable)targeted;

					for( int i = 0; i < m_keyring.m_Keys.Count; i++ )
					{
						KeyringEntry entry = m_keyring.m_Keys[i] as KeyringEntry;
				
						if ( o.KeyValue == entry.KeyValue )
						{
							if ( o is BaseDoor && !((BaseDoor)o).UseLocks() )
							{
								from.SendAsciiMessage( "This key doesn't seem to unlock that." );
							}
							else
							{
								o.Locked = !o.Locked;
								Didit = true;

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
					}

					if ( !Didit )
						from.SendAsciiMessage( "No key seems to unlock that." );
				}
				else
				{
					from.SendAsciiMessage( "You can't unlock that!" );
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Key )
			{
				if ( m_Keys.Count < 16 )
				{
					Key key = (Key)dropped;

					if ( key.KeyValue != 0 )
					{
						m_Keys.Add( new KeyringEntry( key.KeyValue, key.Link, key.MaxRange, key.ItemID, key.LootType, key.Description ) );

						dropped.Delete();

						//from.Send( new PlaySound( 0x42, GetWorldLocation() ) );

						this.Weight = this.Weight + 1;

						switch( m_Keys.Count )
						{
							case 0:
							{
								this.ItemID = 0x1011;
								break;
							}
							case 1:
							{
								this.ItemID = 0x1769;
								break;
							}
							case 3:
							{
								this.ItemID = 0x176A;
								break;
							}
							case 5:
							{
								this.ItemID = 0x176B;
								break;
							}
						}

						return true;
					}
					else
					{
						from.SendAsciiMessage( "This key is blank." );
						return false;
					}
				}
				else
				{
					from.SendAsciiMessage( "This key ring is full" );
					return false;
				}
			}
			return false;
		}

		public Keyring( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			writer.Write( m_Keys.Count );

			for ( int i = 0; i < m_Keys.Count; ++i )
				((KeyringEntry)m_Keys[i]).Serialize( writer );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			int count = reader.ReadInt();

			m_Keys = new ArrayList( count );
			for ( int i = 0; i < count; ++i )
				m_Keys.Add( new KeyringEntry( reader ) );
		}
	}

	public class KeyringEntry
	{
		private uint m_KeyValue;
		private Item m_Link;
		private int m_MaxRange;
		private int m_ItemID;
		private LootType m_LootType;
		private string m_Description;

		public uint KeyValue
		{
			get{ return m_KeyValue; }
		}

		public Item Link
		{
			get{ return m_Link; }
		}

		public int MaxRange
		{
			get{ return m_MaxRange; }
		}

		public int ItemID
		{
			get{ return m_ItemID; }
		}

		public LootType LootType
		{
			get{ return m_LootType; }
		}

		public string Description
		{
			get{ return m_Description; }
		}

		public KeyringEntry( uint value, Item link, int range, int itemid, LootType loot, string desc )
		{
			m_KeyValue = value;
			m_Link = link;
			m_MaxRange = range;
			m_ItemID = itemid;
			m_LootType = loot;
			m_Description = desc;
		}

		public KeyringEntry( GenericReader reader )
		{
			int version = reader.ReadByte();

			switch ( version )
			{
				case 0:
				{
					m_KeyValue = reader.ReadUInt();
					m_Link = reader.ReadItem();
					m_MaxRange = reader.ReadInt();
					m_ItemID = reader.ReadInt();
					m_LootType = (LootType)reader.ReadByte();
					m_Description = reader.ReadString();

					break;
				}
			}
		}

		public void Serialize( GenericWriter writer )
		{
			writer.Write( (byte) 0 ); // version

			writer.Write( m_KeyValue );
			writer.Write( m_Link );
			writer.Write( m_MaxRange );
			writer.Write( m_ItemID );
			writer.Write( (byte) m_LootType );
			writer.Write( m_Description );
		}
	}
}
