using System;
using Server.Network;
using Server.Prompts;
using Server.Guilds;
using Server.Multis;
using Server.Regions;

namespace Server.Items
{
	public class GuildTeleporter : Item
	{
		private Item m_Stone;

		public override int LabelNumber{ get{ return 1041054; } } // guildstone teleporter

		[Constructable]
		public GuildTeleporter() : this( null )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a guildstone teleporter");
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public GuildTeleporter( Item stone ) : base( 0x1869 )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;

			m_Stone = stone;
		}

		public GuildTeleporter( Serial serial ) : base( serial )
		{
		}

		public override bool DisplayLootType{ get{ return false; } }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Stone );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			LootType = LootType.Blessed;

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Stone = reader.ReadItem();

					break;
				}
			}

			if ( Weight == 0.0 )
				Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Guildstone stone = m_Stone as Guildstone;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendAsciiMessage( "That must be in your pack for you to use it." );
			}
			else if ( stone == null || stone.Deleted || stone.Guild == null || stone.Guild.Teleporter != this )
			{
				from.SendAsciiMessage( "This teleporting object can not determine what guildstone to teleport." );
			}
			else
			{
				BaseHouse house = BaseHouse.FindHouseAt( from );

				if ( house == null )
				{
					from.SendAsciiMessage( "You can only place a guildstone in a house." );
				}
				else if ( !house.IsOwner( from ) )
				{
					from.SendAsciiMessage( "You can only place a guildstone in a house you own!" );
				}
				else
				{
					m_Stone.MoveToWorld( from.Location, from.Map );
					Delete();
					stone.Guild.Teleporter = null;
				}
			}
		}
	}
}
