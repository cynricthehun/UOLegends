using System;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Regions;
using System.Collections;

namespace Server.Engines.XmlSpawner2
{

	public class MobFactionRegionStone : Item
	{

        private Map m_MobFactionMap;
        private string m_MobFactionRegionName;
        private MobFactionRegion m_MobFactionRegion;                    // MobFaction region
        private MusicName m_Music;
        private int m_Priority;
        private Rectangle2D m_MobFactionArea;
        private string m_CopiedRegion;
        private XmlMobFactions.GroupTypes m_FactionType;
        private int m_FactionLevel;
        private Point3D m_EjectLocation;
        private Map m_EjectMap;

        [CommandProperty( AccessLevel.GameMaster )]
        public Point3D EjectLocation 
        {
            get { return m_EjectLocation; } 
            set {
                m_EjectLocation = value;
                if(m_MobFactionRegion != null)
                {
                    m_MobFactionRegion.EjectLocation = m_EjectLocation;
                }
                RefreshRegions();
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public Map EjectMap 
        {
            get { return m_EjectMap; }
            set {
                m_EjectMap = value;
                if(m_MobFactionRegion != null)
                {
                    m_MobFactionRegion.EjectMap = m_EjectMap;
                }
                RefreshRegions();
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public Rectangle2D MobFactionArea
        {
            get { return m_MobFactionArea; }
            set { m_MobFactionArea = value;}
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public string MobFactionRegionName
        {
            get { return m_MobFactionRegionName; }
            set
            { 
                m_MobFactionRegionName = value; 

                if(m_MobFactionRegion != null)
                {
                    m_MobFactionRegion.Name = m_MobFactionRegionName;
                }
                RefreshRegions();
            }
        }

        [CommandProperty( AccessLevel.GameMaster )]
        public MobFactionRegion MobFactionRegion
        {
            get { return m_MobFactionRegion; }
            set { m_MobFactionRegion = value; }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public Map MobFactionMap
        {
            get { return m_MobFactionMap; }
            set { 
                m_MobFactionMap = value; 
                if(m_MobFactionRegion != null)
                {
                    m_MobFactionRegion.Map = m_MobFactionMap;
                }
                RefreshRegions();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public MusicName MobFactionMusic
        {
            get { return m_Music; }
            set {
                m_Music = value;
                if(m_MobFactionRegion != null)
                {
                    m_MobFactionRegion.Music = m_Music;
                }
                RefreshRegions();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public int MobFactionPriority
        {
            get { return m_Priority; }
            set {
                m_Priority = value;
                if(m_MobFactionRegion != null)
                {
                    m_MobFactionRegion.Priority = m_Priority;
                }

                RefreshRegions();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public XmlMobFactions.GroupTypes FactionType
        {
            get { return m_FactionType; }
            set {
                m_FactionType = value;
                if(m_MobFactionRegion != null)
                {
                    m_MobFactionRegion.FactionType = m_FactionType;
                }
                
                RefreshRegions();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public int FactionLevel
        {
            get { return m_FactionLevel; }
            set {
                m_FactionLevel = value;
                if(m_MobFactionRegion != null)
                {
                    m_MobFactionRegion.FactionLevel = m_FactionLevel;
                }
                
                RefreshRegions();
            }
        }
        
        [CommandProperty( AccessLevel.GameMaster )]
        public string CopyRegion
        {
            get {
                return m_CopiedRegion;
            }
            set {
                if(value == null)
                {
                    m_CopiedRegion = null;
                } else
                {

                    // find the named region
                    Region r = FindRegion(value);
                    
                    if(r != null)
                    {
                        // if no region exists, then make one
                        if(m_MobFactionRegion == null)
                        {
                            if(m_MobFactionMap == null )
                            {
                                m_MobFactionMap = this.Map;
                            }
                            m_MobFactionRegion = new MobFactionRegion(MobFactionRegionName, m_MobFactionMap);
                            Region.AddRegion( m_MobFactionRegion );
                        }
    
                        m_CopiedRegion = value;
    
                        // copy the coords, map, and music from that region
                        MobFactionMap = r.Map;
                        m_MobFactionArea = new Rectangle2D( 0, 0, 0, 0 );
                        MobFactionMusic = r.Music;
    
                        m_MobFactionRegion.Coords = new ArrayList();
                        m_MobFactionRegion.Coords = (ArrayList)r.Coords.Clone();

                        RefreshRegions();
    
                    }
                }
            }
        }
        
        public override void OnLocationChange( Point3D oldLocation )
        {
            base.OnLocationChange(oldLocation);

            // assign the eject location to the new location
            EjectLocation = Location;

        }
        
        public override void OnMapChange()
        {
            base.OnMapChange();

            // assign the eject map to the new map
            EjectMap = Map;
        }

        public void RefreshRegions()
        {
            if(m_MobFactionRegion != null)
            {
                Region.RemoveRegion(m_MobFactionRegion);
                Region.AddRegion(m_MobFactionRegion);
            }
        }

		[Constructable]
		public MobFactionRegionStone() : base ( 0x161D )
		{
			Visible = false;
			Movable = false;
			Name = "MobFaction Region Controller";

			MobFactionRegionName = "MobFaction Game Region";

			MobFactionPriority = 0x90;             // high priority
			
			if(m_MobFactionRegion == null)
            {
                if(m_MobFactionMap == null )
                {
                    m_MobFactionMap = this.Map;
                }
                m_MobFactionRegion = new MobFactionRegion(MobFactionRegionName, m_MobFactionMap);
                Region.AddRegion( m_MobFactionRegion );
            }
		}

		public MobFactionRegionStone( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

            // version 0
			writer.Write( (int)m_Music );
			writer.Write( m_Priority );
			writer.Write( m_MobFactionArea );
			writer.Write( m_MobFactionRegionName );
			writer.Write( m_MobFactionMap.Name );
			writer.Write( m_CopiedRegion );
			
			// do the coord list
			if(m_MobFactionRegion != null && m_MobFactionRegion.Coords != null && m_MobFactionRegion.Coords.Count > 0)
			{
                writer.Write( m_MobFactionRegion.Coords.Count );
                
                for(int i = 0;i < m_MobFactionRegion.Coords.Count; i++)
                {
                    writer.Write( (Rectangle2D) m_MobFactionRegion.Coords[i] );
                }
			} else
			{
                writer.Write( (int)0 );
			}
		}
		
		public override void GetProperties(ObjectPropertyList list)
		{
            base.GetProperties(list);
            
            list.Add( 1062613, m_MobFactionRegionName);
		}



		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{

				case 0:
				{
                    m_Music = (MusicName)reader.ReadInt();
                    m_Priority = reader.ReadInt();
                    m_MobFactionArea = reader.ReadRect2D();
                    m_MobFactionRegionName = reader.ReadString();
                    string mapname = reader.ReadString();
                    try{
					   m_MobFactionMap = Map.Parse(mapname);
					} catch {}
					m_CopiedRegion = reader.ReadString();

					m_MobFactionRegion = new MobFactionRegion(m_MobFactionRegionName, m_MobFactionMap);

					// do the coord list
					int count = reader.ReadInt();
					m_MobFactionRegion.Coords = new ArrayList();
					for(int i =0;i<count;i++)
					{
                        m_MobFactionRegion.Coords.Add(reader.ReadRect2D());
					}

					m_MobFactionRegion.Priority = m_Priority;
                    m_MobFactionRegion.Music = m_Music;
                    m_MobFactionRegion.Map = m_MobFactionMap;

					break;
				}
			}

			// refresh the region
			RefreshRegions();
		}

		public static Region FindRegion(string name)
		{
            if (Region.Regions == null)	return null;

        	foreach (Region region in Region.Regions)
        	{
        		if (string.Compare(region.Name, name, true) == 0)
        		{
        			return region;
        		}
        	}

        	return null;
        }

        public override void OnDoubleClick( Mobile m )
		{
			if( m != null && m.AccessLevel >= AccessLevel.GameMaster)
			{
                m.SendMessage("Define the MobFaction area");
                DefineMobFactionArea ( m );
			}
		}


		public void DefineMobFactionArea( Mobile m )
		{
			BoundingBoxPicker.Begin( m, new BoundingBoxCallback( MobFactionRegionArea_Callback ), this );
		}


		private static void MobFactionRegionArea_Callback( Mobile from, Map map, Point3D start, Point3D end, object state )
		{
            // assign these coords to the region
            MobFactionRegionStone s = state as MobFactionRegionStone;
            
            if(s != null && s.MobFactionRegion != null && from != null)
            {

    			s.MobFactionArea = new Rectangle2D( start.X, start.Y, end.X - start.X + 1, end.Y - start.Y + 1 );
    			s.MobFactionMap = map;

    			ArrayList coords = new ArrayList();
    			coords.Add( s.MobFactionArea );

    			s.MobFactionRegion.Coords = coords;

    			s.MobFactionRegion.Map = map;

    			s.CopyRegion = null;
    			
    			s.RefreshRegions();

			}
		}

		public override void OnDelete()
		{
			if( m_MobFactionRegion != null )
				Region.RemoveRegion( m_MobFactionRegion );

			base.OnDelete();
		}
	}
}
