using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Engines.Quests;
using Server.Engines.Quests.Collector;



namespace Server.Engines.Harvest
{
	public class Fishing : HarvestSystem
	{
		private static Fishing m_System;

		public static Fishing System
		{
			get
			{
				if ( m_System == null )
					m_System = new Fishing();

				return m_System;
			}
		}

		private HarvestDefinition m_Definition;

		public HarvestDefinition Definition
		{
			get{ return m_Definition; }
		}

		private Fishing()
		{
			HarvestResource[] res;
			HarvestVein[] veins;

			#region Fishing
			HarvestDefinition fish = new HarvestDefinition();

			// Resource banks are every 8x8 tiles
			fish.BankWidth = 8;
			fish.BankHeight = 8;

			// Every bank holds from 5 to 15 fish
			fish.MinTotal = 5;
			fish.MaxTotal = 15;

			// A resource bank will respawn its content every 10 to 20 minutes
			fish.MinRespawn = TimeSpan.FromMinutes( 10.0 );
			fish.MaxRespawn = TimeSpan.FromMinutes( 20.0 );

			// Skill checking is done on the Fishing skill
			fish.Skill = SkillName.Fishing;

			// Set the list of harvestable tiles
			fish.Tiles = m_WaterTiles;
			fish.RangedTiles = true;

			// Players must be within 4 tiles to harvest
			fish.MaxRange = 4;

			// One fish per harvest action
			fish.ConsumedPerHarvest = 1;
			fish.ConsumedPerFeluccaHarvest = 1;

			// The fishing
			fish.EffectActions = new int[]{ 12 };
			fish.EffectSounds = new int[0];
			fish.EffectCounts = new int[]{ 1 };
			fish.EffectDelay = TimeSpan.Zero;
			fish.EffectSoundDelay = TimeSpan.FromSeconds( 8.0 );

			fish.NoResourcesMessage = 503172; // The fish don't seem to be biting here.
			fish.FailMessage = 503171; // You fish a while, but fail to catch anything.
			fish.TimedOutOfRangeMessage = 500976; // You need to be closer to the water to fish!
			fish.OutOfRangeMessage = 500976; // You need to be closer to the water to fish!
			fish.PackFullMessage = 503176; // You do not have room in your backpack for a fish.
			fish.ToolBrokeMessage = 503174; // You broke your fishing pole.

			res = new HarvestResource[]
				{
					new HarvestResource( 00.0, 00.0, 100.0, 1043297, typeof( Fish ) )
				};

			veins = new HarvestVein[]
				{
					new HarvestVein( 100.0, 0.0, res[0], null )
				};

			fish.Resources = res;
			fish.Veins = veins;

			m_Definition = fish;
			Definitions.Add( fish );
			#endregion
		}

		public override void OnConcurrentHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			from.SendLocalizedMessage( 500972 ); // You are already fishing.
		}

		private class MutateEntry
		{
			public double m_ReqSkill, m_MinSkill, m_MaxSkill;
			public bool m_DeepWater;
			public Type[] m_Types;

			public MutateEntry( double reqSkill, double minSkill, double maxSkill, bool deepWater, params Type[] types )
			{
				m_ReqSkill = reqSkill;
				m_MinSkill = minSkill;
				m_MaxSkill = maxSkill;
				m_DeepWater = deepWater;
				m_Types = types;
			}
		}

		private static MutateEntry[] m_MutateTable = new MutateEntry[]
			{
			    // Here's the stuff for regular fishing...
                new MutateEntry(  80.0,  80.0,  4080.0,  true, typeof( SpecialFishingNet ) ),
				new MutateEntry(  80.0,  80.0,  4080.0,  true, typeof( BigFish ) ),
				new MutateEntry(  90.0,  80.0,  4080.0,  true, typeof( TreasureMap ) ),
				new MutateEntry( 100.0,  80.0,  4080.0,  true, typeof( MessageInABottle ) ),
				new MutateEntry(   0.0, 125.0, -2375.0, false, typeof( PrizedFish ), typeof( WondrousFish ), typeof( TrulyRareFish ), typeof( PeculiarFish ) ),
				new MutateEntry(   0.0, 105.0,  -420.0, false, typeof( Boots ), typeof( Shoes ), typeof( Sandals ), typeof( ThighBoots ), typeof( Bandana ), typeof( FancyShirt ), typeof( ThighBoots ) ),
				new MutateEntry(   0.0, 200.0,  -200.0, false, new Type[1]{ null } ) 
                // Note that Maxskill is HUGE for Net/Big/Map/MIB...this controls the rate! 
                // Folks can simply lower the "4080" to improve the chance at MIBs. I've left it alone.
                // Adding things here (new MutateEntry) does not affect Chance at MIB...see MutateType below
                // But if you add things here, you have to deal with Message I/O below. Ugh!

			};

		public override bool SpecialHarvest( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc )
		{
			// Some special quest stuff...leave untouched unless you're into quests
            PlayerMobile player = from as PlayerMobile;
			if ( player != null )
			{
				QuestSystem qs = player.Quest;
				if ( qs is CollectorQuest )
				{
					QuestObjective obj = qs.FindObjective( typeof( FishPearlsObjective ) );
					if ( obj != null && !obj.Completed )
					{
						if ( Utility.RandomDouble() < 0.5 )
						{
							player.SendLocalizedMessage( 1055086, "", 0x59 ); // You pull a shellfish out of the water, and find a rainbow pearl inside of it.
							obj.CurProgress++;
						}
						else
						{
							player.SendLocalizedMessage( 1055087, "", 0x2C ); // You pull a shellfish out of the water, but it doesn't have a rainbow pearl.
						}
						return true;
					}
				}
			}
			return false;
		}


		public override Type MutateType( Type type, Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource )
		{
			bool deepWater = SpecialFishingNet.FullValidation( map, loc.X, loc.Y );

			double skillBase = from.Skills[SkillName.Fishing].Base;
			double skillValue = from.Skills[SkillName.Fishing].Value;

			for ( int i = 0; i < m_MutateTable.Length; ++i )
			{
                // Determine the chance to fish up the Mutate Table items, and...
                // pick the *first* Type that passes the random chance test
				MutateEntry entry = m_MutateTable[i];

				if ( !deepWater && entry.m_DeepWater )
					continue;

				if ( skillBase >= entry.m_ReqSkill )
				{
					double chance = (skillValue - entry.m_MinSkill) / (entry.m_MaxSkill - entry.m_MinSkill);

					if ( chance > Utility.RandomDouble() )
						return entry.m_Types[Utility.Random( entry.m_Types.Length )];
                        // This checks Net/BF/Map/MIB chances first, before doing easy items
				}
			}

			return type;
		}

		private static Map SafeMap( Map map )
		{
			if ( map == null || map == Map.Internal )
				return Map.Trammel;

			return map;
		}

		public override bool CheckResources( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, bool timed )
		{
			Container pack = from.Backpack;

			if ( pack != null )
			{
				Item[] messages = pack.FindItemsByType( typeof( SOS ) );

				for ( int i = 0; i < messages.Length; ++i )
				{
					SOS sos = (SOS)messages[i];

					if ( from.Map == sos.TargetMap && from.InRange( sos.TargetLocation, 60 ) )
						return true;
				}
			}

			return base.CheckResources( from, tool, def, map, loc, timed );
		}


		public override Item Construct( Type type, Mobile from )
		{
			if ( type == typeof( TreasureMap ) )
			{
				int level;
				if ( from is PlayerMobile && ((PlayerMobile)from).Young && from.Map == Map.Trammel && TreasureMap.IsInHavenIsland( from ) )
					level = 0;
				else
					level = 1;

				return new TreasureMap( level, from.Map == Map.Felucca ? Map.Felucca : Map.Trammel );
			}
			else if ( type == typeof( MessageInABottle ) )
			{
				return new MessageInABottle( SafeMap( from.Map ) );
			}

			Container pack = from.Backpack;

			if ( pack != null )
			{
				Item[] messages = pack.FindItemsByType( typeof( SOS ) );

				for ( int i = 0; i < messages.Length; ++i )
				{
					SOS sos = (SOS)messages[i];

					if ( from.Map == sos.TargetMap && from.InRange( sos.TargetLocation, 60 ) )
					{
						Item preLoot = null;

						switch ( Utility.Random( 7 ) )
						{
							case 0: // Body parts
							{
								int[] list = new int[]
									{
										0x1CDD, 0x1CE5, // arm
										0x1CE0, 0x1CE8, // torso
										0x1CE1, 0x1CE9, // head
										0x1CE2, 0x1CEC // leg
									};

								preLoot = new ShipwreckedItem( Utility.RandomList( list ) );
								break;
							}
							case 1: // Bone parts
							{
								int[] list = new int[]
									{
										0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3, 0x1AE4, // skulls
										0x1B09, 0x1B0A, 0x1B0B, 0x1B0C, 0x1B0D, 0x1B0E, 0x1B0F, 0x1B10, // bone piles
										0x1B15, 0x1B16 // pelvis bones
									};

								preLoot = new ShipwreckedItem( Utility.RandomList( list ) );
								break;
							}
							case 2: // Paintings and portraits
							{
								preLoot = new ShipwreckedItem( Utility.Random( 0xE9F, 10 ) );
								break;
							}
							case 3: // Pillows
							{
								preLoot = new ShipwreckedItem( Utility.Random( 0x13A4, 11 ) );
								break;
							}
							case 4: // Shells
							{
								preLoot = new ShipwreckedItem( Utility.Random( 0xFC4, 9 ) );
								break;
							}
							case 5: // Misc
							{
								int[] list = new int[]
									{
										0x1EB5, // unfinished barrel
										0xA2A, // stool
										0xC1F, // broken clock
										0x1047, 0x1048, // globe
										0x1EB1, 0x1EB2, 0x1EB3, 0x1EB4, // barrel staves
                                        0xB5B, 0xB4F, 0xB5E, 0xB57, 0xB53, // chairs
                                        0x1560, 0x1562, 0x155D, 0x155E, 0x1582, 0x1634, //Deco Bows/Swords
                                        0x156C, 0x157E, 0x1580, 0x1565, 0x1578, 0x1569, //Deco Shields/Axe
                                        0xC1B, 0xC10, 0xC19, 0xC2C, 0xC2D, 0xB95, // Ruined things/Sign
									    0xE31, 0x19AA, 0x1853, 0x9FB, 0xA05, 0xA26, // Light sources
                                        0x1EA7, 0x26AC, 0x1879, 0x1005, 0x185B, 0xEFB, // Misc items
                                        0x1810, 0xEFC, 0X14F8, 0x166E // More Misc Items
                                        //
                                        // *** Can easily add more stuff to fish up while doing chest!!
                                        //
                                    };

								preLoot = new ShipwreckedItem( Utility.RandomList( list ) );
                                // This statement converts to Item and tags "... from shipwreck"
								break;
							}
						}

                        if (preLoot != null)  // Drew a "0-5" in above Case Stmt and have preLoot
                        {
                            if (0.143 > Utility.RandomDouble()) // Spawn weak monster ~ every 7 tries
                            {
                                BaseCreature serp1;

                                switch (Utility.Random(5))
                                {
                                    case 0:  { serp1 = new IcebergSnake(); break; }
                                    case 1:  { serp1 = new SeaSlime(); break; }
                                    case 2:  { serp1 = new SeaWorm(); break; }
                                    case 3:  { serp1 = new ToxicSlime(); break; }
                                    default: { serp1 = new OilSlime(); break; }
                                }

                                int x = from.X, y = from.Y;
                                Map map = from.Map;

                                for (int ii = 0; map != null && ii < 20; ++ii)
                                {
                                    int tx = from.X - 10 + Utility.Random(21);
                                    int ty = from.Y - 10 + Utility.Random(21);
                                    Tile t = map.Tiles.GetLandTile(tx, ty);

                                    if (t.Z == -5 && ((t.ID >= 0xA8 && t.ID <= 0xAB) || (t.ID >= 0x136 && t.ID <= 0x137)) && !Spells.SpellHelper.CheckMulti(new Point3D(tx, ty, -5), map))
                                    {
                                        x = tx;
                                        y = ty;
                                        break;
                                    }
                                }

                                serp1.MoveToWorld(new Point3D(x, y, -5), map);
                                serp1.Home = serp1.Location;
                                serp1.RangeHome = 10;
                                from.SendLocalizedMessage(503170); // Uh oh! That doesn't look like a fish!

                            }  
                            
                            return preLoot;
                        }

						sos.Delete();

						WoodenChest chest = new WoodenChest();

						TreasureMapChest.Fill( chest, Utility.RandomMinMax( 1, 4 ) );

						// TODO: Are there chances on this? All MIB's I've done had nets..
						chest.DropItem( new SpecialFishingNet() );

						chest.Movable = true;
						chest.Locked = false;
						chest.TrapType = TrapType.None;
						chest.TrapPower = 0;

						return chest;
					}
				}
			}

			return base.Construct( type, from );
		}

		public override bool Give( Mobile m, Item item, bool placeAtFeet )
		{
            // This code is triggered when you've harvested a Map/MIB/Net. You first
            // spawn a critter and then add the special item to his/her pack for loot

			if ( item is TreasureMap || item is MessageInABottle || item is SpecialFishingNet )
			{
				BaseCreature serp;

                switch ( Utility.Random(11) )
				{  
                    case 0:  { serp = new DeepSeaSnake(); break; }
                    case 1:  { serp = new DeepWaterElemental(); break; }
                    case 2:  { serp = new FlameShark(); break; }
                    case 3:  { serp = new HurricaneElemental(); break; }
                    case 4:  { serp = new IcebergElemental(); break; }
                    case 5:  { serp = new RoamingKraken(); break; }
                    case 6:  { serp = new StormSerpent(); break; }
                    case 7:  { serp = new TsunamiSerpent(); break; }
                    case 8:  { serp = new GiantWaterStrider(); break; }
                    case 9:  { serp = new DeepSeaSerpent(); break; }
                    default: { serp = new SeaSerpent(); break; }
                }

				int x = m.X, y = m.Y;
				Map map = m.Map;

				for ( int i = 0; map != null && i < 20; ++i )
				{
					int tx = m.X - 10 + Utility.Random( 21 );
					int ty = m.Y - 10 + Utility.Random( 21 );

					Tile t = map.Tiles.GetLandTile( tx, ty );

					if ( t.Z == -5 && ( (t.ID >= 0xA8 && t.ID <= 0xAB) || (t.ID >= 0x136 && t.ID <= 0x137) ) && !Spells.SpellHelper.CheckMulti( new Point3D( tx, ty, -5 ), map ) )
					{
						x = tx;
						y = ty;
						break;
					}
				}

				serp.MoveToWorld( new Point3D( x, y, -5 ), map );

				serp.Home = serp.Location;
				serp.RangeHome = 10;

				serp.PackItem( item );

				m.SendLocalizedMessage( 503170 ); // Uh oh! That doesn't look like a fish!

				return true; // we don't want to give the item to the player, it's on the serpent
			}

            // BigFish go into the pack, but why does it look like they are put at feet? R12?
			if ( item is BigFish || item is WoodenChest ) 
				placeAtFeet = true;

            // Here's where we sometimes replace the Big fish with a Special Item.
            // Note that we have to trigger on the Big Fish to stop folks from
            // macroing on the shore (shallow water) for Magic Weps. This method
            // does not have the right variables for deepwater check

            if ( item is BigFish ) // We know we're in Deepwater, and it's a rare event
            {
                
                double leet_loot_chance = 0.1;
                
                if ( leet_loot_chance > Utility.RandomDouble() )
                {
                    switch (Utility.Random(2))
                    {
                        case 0:
                            {
                                BaseWeapon temp_weapon = Loot.RandomWeapon();
                                temp_weapon.DamageLevel = (WeaponDamageLevel)Utility.Random(6);
                                temp_weapon.AccuracyLevel = (WeaponAccuracyLevel)Utility.Random(6);
                                temp_weapon.DurabilityLevel = (WeaponDurabilityLevel)Utility.Random(1);
                                // Item was in the water forever...shouldn't be very durable
                                item = (Item)temp_weapon;
                                break;
                            }
                        default:
                            {
                                BaseArmor temp_armor = Loot.RandomArmor();
                                temp_armor.ProtectionLevel = (ArmorProtectionLevel)Utility.Random(4);
                                temp_armor.Durability = (ArmorDurabilityLevel)Utility.Random(1);
                                // Item was in the water forever...shouldn't be very durable
                                item = (Item)temp_armor;
                                break;
                            }
                    }
                }
                // else just give the fisher a Big Fish

            }
            // Could also easily add variations on the following...but I've left it at Weps/Armor
            // item = Loot.RandomShield();
            // item = Loot.RandomJewelry();
            // item = Loot.RandomInstrument();
            // item = Loot.RandomScroll(2, 8, SpellbookType.Regular);
            // item = Loot.RandomPotion();
			// item = new BankCheck( 1000000 );
            // item = new ShipwreckedItem(5055); // Chain tunic with Shipwreck Label

			return base.Give( m, item, placeAtFeet );  // Here's where you GIVE player the "item"
		}


		public override void SendSuccessTo( Mobile from, Item item, HarvestResource resource )
		{
			if ( item is BigFish )
			{
				// Deleted this message since you can now get loot, which is notta fish
                // I wish I knew how to send "custom" message to the screen like...
                // "Whoa! You just reeled in something very interesting"

                // from.SendLocalizedMessage( 1042635 ); // Your fishing pole bends as you pull a big fish from the depths!
                
				((BigFish)item).Fisher = from;
			}
			else if ( item is WoodenChest )
			{
				from.SendLocalizedMessage( 503175 ); // You pull up a heavy chest from the depths of the ocean!
			}
			else
			{
				int number;
				string name;

				if ( item is BaseMagicFish )
				{
					number = 1008124;
					name = "a mess of small fish";
				}
				else if ( item is Fish )
				{
					number = 1008124;
					name = "a fish";
				}
				else if ( item is BaseShoes )
				{
					number = 1008124;
					name = item.ItemData.Name;
				}
				else if ( item is TreasureMap )
				{
					number = 1008125;
					name = "a sodden piece of parchment";
				}
				else if ( item is MessageInABottle )
				{
					number = 1008125;
					name = "a bottle, with a message in it";
				}
                else if (item is SpecialFishingNet)
                {
                    number = 1008125;
                    name = "a special fishing net"; // TODO: this is just a guess--what should it really be named?
                }
                else
                {
                    number = 1043297; // It's Some Other Stuff...

                    // Console.WriteLine(item.ItemData.Name);

                    if ((item.ItemData.Flags & TileFlag.ArticleA) != 0)
                        name = "a " + item.ItemData.Name;
                    else if ((item.ItemData.Flags & TileFlag.ArticleAn) != 0)
                        name = "an " + item.ItemData.Name;
                    else
                        name = item.ItemData.Name;

                    if (number == 1043297)
                        from.SendLocalizedMessage(number, name);
                    else
                        from.SendLocalizedMessage(number, true, name);
                
                }

                double temp_random_dbl = Utility.RandomDouble();
                // Console.WriteLine("Trying to spawn Random Monster when 0.05 > {0}", temp_random_dbl);

                if ( 0.05 > temp_random_dbl )  // Spawn a monster ~ every 20 tries
                {
                     BaseCreature serp2;

                     switch (Utility.Random(11))
                     {
                         case 0:  { serp2 = new DeepSeaSnake(); break; }
                         case 1:  { serp2 = new DeepWaterElemental(); break; }
                         case 2:  { serp2 = new FlameShark(); break; }
                         case 3:  { serp2 = new HurricaneElemental(); break; }
                         case 4:  { serp2 = new IcebergElemental(); break; }
                         case 5:  { serp2 = new RoamingKraken(); break; }
                         case 6:  { serp2 = new StormSerpent(); break; }
                         case 7:  { serp2 = new TsunamiSerpent(); break; }
                         case 8:  { serp2 = new GiantWaterStrider(); break; }
                         case 9:  { serp2 = new DeepSeaSerpent(); break; }
                         default: { serp2 = new SeaSerpent(); break; }
                         
                     }

                     int x = from.X, y = from.Y;
                     Map map = from.Map;

                     for (int i = 0; map != null && i < 20; ++i)
                     {
                         int tx = from.X - 10 + Utility.Random(21);
                         int ty = from.Y - 10 + Utility.Random(21);
                         Tile t = map.Tiles.GetLandTile(tx, ty);

                         if (t.Z == -5 && ((t.ID >= 0xA8 && t.ID <= 0xAB) || (t.ID >= 0x136 && t.ID <= 0x137)) && !Spells.SpellHelper.CheckMulti(new Point3D(tx, ty, -5), map))
                         {
                             x = tx;
                             y = ty;
                             break;
                         }
                     }

                     serp2.MoveToWorld(new Point3D(x, y, -5), map);
                     serp2.Home = serp2.Location;
                     serp2.RangeHome = 10;
                     from.SendLocalizedMessage(503170); // Uh oh! That doesn't look like a fish!
                 
				}
			}
		}

		public override void OnHarvestStarted( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			base.OnHarvestStarted( from, tool, def, toHarvest );

			int tileID;
			Map map;
			Point3D loc;

			if ( GetHarvestDetails( from, tool, toHarvest, out tileID, out map, out loc ) )
				Timer.DelayCall( TimeSpan.FromSeconds( 1.5 ), new TimerStateCallback( Splash_Callback ), new object[]{ loc, map } );
		}

		private void Splash_Callback( object state )
		{
			object[] args = (object[])state;
			Point3D loc = (Point3D)args[0];
			Map map = (Map)args[1];

			Effects.SendLocationEffect( loc, map, 0x352D, 16, 4 );
			Effects.PlaySound( loc, map, 0x364 );
		}

		public override object GetLock( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			return this;
		}

		public override bool BeginHarvesting( Mobile from, Item tool )
		{
			if ( !base.BeginHarvesting( from, tool ) )
				return false;

			from.SendLocalizedMessage( 500974 ); // What water do you want to fish in?
			return true;
		}

		public override bool CheckHarvest( Mobile from, Item tool )
		{
			if ( !base.CheckHarvest( from, tool ) )
				return false;

			if ( from.Mounted )
			{
				from.SendLocalizedMessage( 500971 ); // You can't fish while riding!
				return false;
			}

			return true;
		}

		public override bool CheckHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			if ( !base.CheckHarvest( from, tool, def, toHarvest ) )
				return false;

			if ( from.Mounted )
			{
				from.SendLocalizedMessage( 500971 ); // You can't fish while riding!
				return false;
			}

			return true;
		}

		private static int[] m_WaterTiles = new int[]
			{
				0x00A8, 0x00AB,
				0x0136, 0x0137,
				0x5797, 0x579C,
				0x746E, 0x7485,
				0x7490, 0x74AB,
				0x74B5, 0x75D5
			};
	}
}