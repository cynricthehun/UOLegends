using System;
using Server;

namespace Server.Items
{
	[Server.Engines.Craft.Anvil]
	public class AnvilComponent : AddonComponent
	{
		[Constructable]
		public AnvilComponent( int itemID ) : base( itemID )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "an anvil" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public AnvilComponent( Serial serial ) : base( serial )
		{
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
	}

	[Server.Engines.Craft.Forge]
	public class ForgeComponent : AddonComponent
	{
		[Constructable]
		public ForgeComponent( int itemID ) : base( itemID )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a forge" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public ForgeComponent( Serial serial ) : base( serial )
		{
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
	}

	public class LocalizedAddonComponent : AddonComponent
	{
		private int m_LabelNumber;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Number
		{
			get{ return m_LabelNumber; }
			set{ m_LabelNumber = value; InvalidateProperties(); }
		}

		public override int LabelNumber{ get{ return m_LabelNumber; } }

		[Constructable]
		public LocalizedAddonComponent( int itemID, int labelNumber ) : base( itemID )
		{
			m_LabelNumber = labelNumber;
		}

		public LocalizedAddonComponent( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_LabelNumber );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_LabelNumber = reader.ReadInt();
					break;
				}
			}
		}
	}

	public class AddonComponent : Item, IChopable
	{
		private Point3D m_Offset;
		private BaseAddon m_Addon;

		[CommandProperty( AccessLevel.GameMaster )]
		public BaseAddon Addon
		{
			get
			{
				return m_Addon;
			}
			set
			{
				m_Addon = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D Offset
		{
			get
			{
				return m_Offset;
			}
			set
			{
				m_Offset = value;
			}
		}

		[Hue, CommandProperty( AccessLevel.GameMaster )]
		public override int Hue
		{
			get
			{
				return base.Hue;
			}
			set
			{
				base.Hue = value;

				if ( m_Addon != null && m_Addon.ShareHue )
					m_Addon.Hue = value;
			}
		}

		public virtual bool NeedsWall{ get{ return false; } }
		public virtual Point3D WallPosition{ get{ return Point3D.Zero; } }

		[Constructable]
		public AddonComponent( int itemID ) : base( itemID )
		{
			Movable = false;
			ApplyLightTo( this );
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.ItemID >= 0x120E && this.ItemID <= 0x1216 )
					LabelTo( from, "an altar" );
				else if ( this.ItemID >= 0x1CF1 && this.ItemID <= 0x1D12 )
					LabelTo( from, "blood" );
				else if ( this.ItemID >= 0x1E36 && this.ItemID <= 0x1E5b )
					LabelTo( from, "a bearskin rug" );
				else if ( ( this.ItemID >= 0x1920 && this.ItemID <=0x1923 ) || ( this.ItemID >= 0x1925 && this.ItemID <= 0x1927 ) || ( this.ItemID >= 0x192C && this.ItemID <= 0x192F ) || ( this.ItemID >= 0x1931 && this.ItemID <= 0x1933 ) )
					LabelTo( from, "a flour mill" );
				else if ( ( this.ItemID >= 0x1928 && this.ItemID <= 0x192B ) || ( this.ItemID >= 0x1934 && this.ItemID <= 0x1937 ) || this.ItemID == 0x1924 || this.ItemID == 0x1930 )
					LabelTo( from, "a crank" );
				else if ( ( this.ItemID >= 0xFD9 && this.ItemID <= 0xFE0 ) )
					LabelTo( from, "a tapestry" );
				else if ( ( this.ItemID >= 0xA5A && this.ItemID <= 0xA91 ) )
					LabelTo( from, "a bed" );
				else if ( ( this.ItemID >= 0x1201 && this.ItemID <= 0x1206 ) )
					LabelTo( from, "a stone table" );
				else if ( ( this.ItemID >= 0x1015 && this.ItemID <= 0x1017 ) || ( this.ItemID >= 0x1019 && this.ItemID <= 0x101E ) || ( this.ItemID >= 0x10A4 && this.ItemID <= 0x10A6 ) )
					LabelTo( from, "a spinning wheel" );
				else if ( this.ItemID >= 0x105F && this.ItemID <= 0x1066 )
					LabelTo( from, "an upright loom" );
				else if ( this.ItemID >= 0xFE6 && this.ItemID <= 0xFEE )
					LabelTo( from, "a pentagram" );
				else if ( this.ItemID >= 0x92B && this.ItemID <= 0x934 )
					LabelTo( from, "a stone oven" );
				else if ( ( this.ItemID >= 0x1069 && this.ItemID <= 0x106F ) || ( this.ItemID >= 0x107A && this.ItemID <= 0x1080 ) )
					LabelTo( from, "a stretched hide" );
				else if ( this.ItemID >= 0xB41 && this.ItemID <= 0xB44 )
					LabelTo( from, "a water trough" );
				else if ( ( this.ItemID >= 0x935 && this.ItemID <= 0x96C ) || ( this.ItemID >= 0x45D && this.ItemID <= 0x48E ) )
					LabelTo( from, "a fireplace" );
				else if ( ( this.ItemID >= 0x19C3 && this.ItemID <= 0x19EC ) || ( this.ItemID >= 0x1731 && this.ItemID <= 0x175A ) )
					LabelTo( from, "a fountain" );
				else if ( this.ItemID >= 0x1459 && this.ItemID <= 0x149A )
					LabelTo( from, "a telescope" );
				else if ( ( this.ItemID >= 0x14D7 && this.ItemID <= 0x14DF ) || ( this.ItemID >= 0x1550 && this.ItemID <= 0x1558 ) )
					LabelTo( from, "a vat" );
				else if ( this.ItemID == 0x1559 )
					LabelTo( from, "water" );
				else
					base.OnSingleClick( from );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public AddonComponent( Serial serial ) : base( serial )
		{
		}

		public void OnChop( Mobile from )
		{
			if ( m_Addon != null && from.InRange( GetWorldLocation(), 3 ) )
				m_Addon.OnChop( from );
			else
				from.SendAsciiMessage( "That is too far away." );
		}

		public override void OnLocationChange( Point3D old )
		{
			if ( m_Addon != null )
				m_Addon.Location = new Point3D( X - m_Offset.X, Y - m_Offset.Y, Z - m_Offset.Z );
		}

		public override void OnMapChange()
		{
			if ( m_Addon != null )
				m_Addon.Map = Map;
		}

		public override void OnAfterDelete()
		{
			base.OnAfterDelete();

			if ( m_Addon != null )
				m_Addon.Delete();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Addon );
			writer.Write( m_Offset );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Addon = reader.ReadItem() as BaseAddon;
					m_Offset = reader.ReadPoint3D();

					if ( m_Addon != null )
						m_Addon.OnComponentLoaded( this );

					ApplyLightTo( this );

					break;
				}
			}
		}

		public static void ApplyLightTo( Item item )
		{
			if ( (item.ItemData.Flags & TileFlag.LightSource) == 0 )
				return; // not a light source

			int itemID = item.ItemID;

			for ( int i = 0; i < m_Entries.Length; ++i )
			{
				LightEntry entry = m_Entries[i];
				int[] toMatch = entry.m_ItemIDs;
				bool contains = false;

				for ( int j = 0; !contains && j < toMatch.Length; ++j )
					contains = ( itemID == toMatch[j] );

				if ( contains )
				{
					item.Light = entry.m_Light;
					return;
				}
			}
		}

		private static LightEntry[] m_Entries = new LightEntry[]
			{
				new LightEntry( LightType.WestSmall, 1122, 1123, 1124, 1141, 1142, 1143, 1144, 1145, 1146, 2347, 2359, 2360, 2361, 2362, 2363, 2364, 2387, 2388, 2389, 2390, 2391, 2392 ),
				new LightEntry( LightType.NorthSmall, 1131, 1133, 1134, 1147, 1148, 1149, 1150, 1151, 1152, 2352, 2373, 2374, 2375, 2376, 2377, 2378, 2401, 2402, 2403, 2404, 2405, 2406 ),
				new LightEntry( LightType.Circle300, 6526, 6538 )
			};

		private class LightEntry
		{
			public LightType m_Light;
			public int[] m_ItemIDs;

			public LightEntry( LightType light, params int[] itemIDs )
			{
				m_Light = light;
				m_ItemIDs = itemIDs;
			}
		}
	}
}
