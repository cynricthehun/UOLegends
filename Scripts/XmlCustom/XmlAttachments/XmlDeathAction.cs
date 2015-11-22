using System;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using System.Collections;

namespace Server.Engines.XmlSpawner2
{
	public class XmlDeathAction : XmlAttachment
	{
		private string m_Action;    // action string

		[CommandProperty( AccessLevel.GameMaster )]
		public string Action { get { return m_Action; } set { m_Action = value; } }

		// These are the various ways in which the message attachment can be constructed.  
		// These can be called via the [addatt interface, via scripts, via the spawner ATTACH keyword.
		// Other overloads could be defined to handle other types of arguments
       
		// a serial constructor is REQUIRED
		public XmlDeathAction(ASerial serial) : base(serial)
		{
		}

		[Attachable]
		public XmlDeathAction(string action)
		{
			Action = action;
		}

		[Attachable]
		public XmlDeathAction()
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize(writer);

			writer.Write( (int) 0 );
			// version 0
			writer.Write(m_Action);

		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
			// version 0
			m_Action = reader.ReadString();
		}
		
		public override void OnAttach()
		{
			base.OnAttach();
		    
				if(AttachedTo is Item)
			{
				// dont allow item attachments
				Delete();
			}

		}
		
		public override bool HandlesOnKilled { get { return true; } }
		
		public override void OnKilled(Mobile killed, Mobile killer )
		{
			base.OnKilled(killed, killer);

			if(killed == null) return;

			ExecuteDeathAction(killed.Corpse, killer, Action);
		}

		private static void ExecuteDeathAction(Item corpse, Mobile killer, string action)
		{
			if (action == null || action.Length <= 0 || corpse == null) return;

			string status_str = null;
			Server.Mobiles.XmlSpawner.SpawnObject TheSpawn = new Server.Mobiles.XmlSpawner.SpawnObject(null, 0);

			TheSpawn.TypeName = action;
			string substitutedtypeName = BaseXmlSpawner.ApplySubstitution(null, corpse, killer, action);
			string typeName = BaseXmlSpawner.ParseObjectType(substitutedtypeName);

			Point3D loc = corpse.Location;
			Map map = corpse.Map;

			if (BaseXmlSpawner.IsTypeOrItemKeyword(typeName))
			{
				BaseXmlSpawner.SpawnTypeKeyword(corpse, TheSpawn, typeName, substitutedtypeName, true, killer, loc, map, out status_str);
			}
			else
			{
				// its a regular type descriptor so find out what it is
				Type type = SpawnerType.GetType(typeName);
				try
				{
					string[] arglist = BaseXmlSpawner.ParseString(substitutedtypeName, 3, "/");
					object o = Server.Mobiles.XmlSpawner.CreateObject(type, arglist[0]);

					if (o == null)
					{
						status_str = "invalid type specification: " + arglist[0];
					}
					else
						if (o is Mobile)
						{
							Mobile m = (Mobile)o;
							if (m is BaseCreature)
							{
								BaseCreature c = (BaseCreature)m;
								c.Home = loc; // Spawners location is the home point
							}

							m.Location = loc;
							m.Map = map;

							BaseXmlSpawner.ApplyObjectStringProperties(null, substitutedtypeName, corpse, killer, corpse, out status_str);
						}
						else
							if (o is Item)
							{
								Item item = (Item)o;
								BaseXmlSpawner.AddSpawnItem(null, corpse, TheSpawn, item, loc, map, killer, false, substitutedtypeName, out status_str);
							}
				}
				catch { }
			}
		}

	}
}
