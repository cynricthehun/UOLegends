using Server;
using Server.Items;
using Server.Network;
using System;
using System.Collections;

namespace Server.Items
{
	public abstract class BaseTreasureChest : LockableContainer
	{
		private TreasureLevel m_TreasureLevel;
		private short m_MaxSpawnTime = 60;
		private short m_MinSpawnTime = 10;
		private TreasureResetTimer m_ResetTimer;

		[CommandProperty( AccessLevel.GameMaster )]
		public TreasureLevel Level
		{
			get
			{
				return m_TreasureLevel;
			}
			set
			{
				m_TreasureLevel = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public short MaxSpawnTime
		{
			get
			{
				return m_MaxSpawnTime;
			}
			set
			{
				m_MaxSpawnTime = value;
			}
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public short MinSpawnTime
		{
			get
			{
				return m_MinSpawnTime;
			}
			set
			{
				m_MinSpawnTime = value;
			}
		}

		public override bool CanStore( Mobile m )
		{
			return true; 
		}
		
		public BaseTreasureChest( int itemID ) : base ( itemID )
		{
			m_TreasureLevel = TreasureLevel.Level2;
			Locked = true;
			Movable = false;
			SetLockedName();

			Key key = (Key)FindItemByType( typeof(Key) );

			if( key != null )
				key.Delete();

			SetLockLevel();
			GenerateTreasure();
		}

		public BaseTreasureChest( Serial serial ) : base( serial )
		{
		}

		protected virtual void SetLockedName()
		{
			Name = "a locked treasure chest";
		}

		protected virtual void SetUnlockedName()
		{
			Name = "a treasure chest";
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
			writer.Write( (byte) m_TreasureLevel );
			writer.Write( m_MinSpawnTime );
			writer.Write( m_MaxSpawnTime );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_TreasureLevel = (TreasureLevel)reader.ReadByte();
			m_MinSpawnTime = reader.ReadShort();
			m_MaxSpawnTime = reader.ReadShort();

			if( !Locked )
				StartResetTimer();
		}

		protected virtual void SetLockLevel()
		{

			bool UseFirstItemId = Utility.RandomBool();

			switch( m_TreasureLevel )
			{
				case TreasureLevel.Level1:
				{
					this.RequiredSkill = this.LockLevel = 5;

					switch( Utility.Random( 3 ) )
					{
						case 0:
						{
							this.TrapType = TrapType.ExplosionTrap;
							this.TrapPower = Utility.Random( 1, 20 );
							break;
						}
						case 1:
						{
							this.TrapType = TrapType.PoisonTrap;
							this.TrapPower = Utility.Random( 1, 20 );
							break;
						}
						case 2:
						{
							this.TrapType = TrapType.DartTrap;
							this.TrapPower = Utility.Random( 1, 20 );
							break;
						}
					}

					break;
				}

				case TreasureLevel.Level2:
				{
					this.RequiredSkill = this.LockLevel = 20;

					switch( Utility.Random( 3 ) )
					{
						case 0:
						{
							this.TrapType = TrapType.ExplosionTrap;
							this.TrapPower = Utility.Random( 15, 35 );
							break;
						}
						case 1:
						{
							this.TrapType = TrapType.PoisonTrap;
							this.TrapPower = Utility.Random( 15, 35 );
							break;
						}
						case 2:
						{
							this.TrapType = TrapType.DartTrap;
							this.TrapPower = Utility.Random( 15, 35 );
							break;
						}
					}

					break;
				}

				case TreasureLevel.Level3:
				{
					this.RequiredSkill = this.LockLevel = 50;

					switch( Utility.Random( 3 ) )
					{
						case 0:
						{
							this.TrapType = TrapType.ExplosionTrap;
							this.TrapPower = Utility.Random( 30, 50 );
							break;
						}
						case 1:
						{
							this.TrapType = TrapType.PoisonTrap;
							this.TrapPower = Utility.Random( 30, 50 );
							break;
						}
						case 2:
						{
							this.TrapType = TrapType.DartTrap;
							this.TrapPower = Utility.Random( 30, 50 );
							break;
						}
					}

					break;
				}

				case TreasureLevel.Level4:
				{
					this.RequiredSkill = this.LockLevel = 70;
					switch( Utility.Random( 3 ) )
					{
						case 0:
						{
							this.TrapType = TrapType.ExplosionTrap;
							this.TrapPower = Utility.Random( 45, 75 );
							break;
						}
						case 1:
						{
							this.TrapType = TrapType.PoisonTrap;
							this.TrapPower = Utility.Random( 45, 75 );
							break;
						}
						case 2:
						{
							this.TrapType = TrapType.DartTrap;
							this.TrapPower = Utility.Random( 45, 75 );
							break;
						}
					}

					break;
				}

				case TreasureLevel.Level5:
				{
					this.RequiredSkill = this.LockLevel = 90;
					switch( Utility.Random( 3 ) )
					{
						case 0:
						{
							this.TrapType = TrapType.ExplosionTrap;
							this.TrapPower = Utility.Random( 70, 90 );
							break;
						}
						case 1:
						{
							this.TrapType = TrapType.PoisonTrap;
							this.TrapPower = Utility.Random( 70, 90 );
							break;
						}
						case 2:
						{
							this.TrapType = TrapType.DartTrap;
							this.TrapPower = Utility.Random( 70, 90 );
							break;
						}
					}

					break;
				}

				case TreasureLevel.Level6:
				{
					this.RequiredSkill = this.LockLevel = 100;

					switch( Utility.Random( 3 ) )
					{
						case 0:
						{
							this.TrapType = TrapType.ExplosionTrap;
							this.TrapPower = Utility.Random( 90, 100 );
							break;
						}
						case 1:
						{
							this.TrapType = TrapType.PoisonTrap;
							this.TrapPower = Utility.Random( 90, 100 );
							break;
						}
						case 2:
						{
							this.TrapType = TrapType.DartTrap;
							this.TrapPower = Utility.Random( 90, 100 );
							break;
						}
					}

					break;
				}
			}
		}

		private void StartResetTimer()
		{
			if( m_ResetTimer == null )
				m_ResetTimer = new TreasureResetTimer( this );
			else
				m_ResetTimer.Delay = TimeSpan.FromMinutes( Utility.Random( m_MinSpawnTime, m_MaxSpawnTime ));
				
			m_ResetTimer.Start();
		}

		protected virtual void GenerateTreasure()
		{
			int MinGold = 1;
			int MaxGold = 2;

			switch( m_TreasureLevel )
			{
				case TreasureLevel.Level1:
				{
					MinGold = 25;
					MaxGold = 50;

        			  	// Drop bolts/arrows
					if ( Utility.RandomBool() == true )
					{
						switch( Utility.Random( 2 ) )
						{
							case 0:
           							DropItem( new Bolt( Utility.Random( 2, 10 ) ) );
								break;
							case 1:
								DropItem( new Arrow( Utility.Random( 2, 10 ) ) );
								break;
						}
					}

         				// Gems
         				if( Utility.RandomBool() == true )
					{
           					Item GemLoot = Loot.RandomGem();
            					GemLoot.Amount = Utility.Random( 1, 3 );
               				 	DropItem( GemLoot );
           				}

          				// Weapon
          				if( Utility.RandomBool() == true )
          				     	DropItem( Loot.RandomWeapon() );
            
          				// Armour
          				if( Utility.RandomBool() == true )
          				   	DropItem( Loot.RandomArmorOrShield() );

          				// Clothing
          				if( Utility.RandomBool() == true )
           				   	DropItem( Loot.RandomClothing() );

         				// Jewelry
           				if( Utility.RandomBool() == true )
						DropItem( Loot.RandomJewelry() );

					break;
				}

				case TreasureLevel.Level2:
				{
					MinGold = 80;
					MaxGold = 150;

        			  	// Drop bolts/arrows
					if ( Utility.RandomBool() == true )
					{
						switch( Utility.Random( 2 ) )
						{
							case 0:
           							DropItem( new Bolt( Utility.Random( 6, 25 ) ) );
								break;
							case 1:
								DropItem( new Arrow( Utility.Random( 6, 25 ) ) );
								break;
						}
					}

            				// Reagents 
            				for( int i = Utility.Random( 1, 2 ); i > 1; i-- )
            				{
                				Item ReagentLoot = Loot.RandomReagent();
                				ReagentLoot.Amount = Utility.Random( 1, 2 );
                				DropItem( ReagentLoot );
            				}

            				// Scrolls
            				for( int i = Utility.Random( 1, 2 ); i > 1; i-- )
            				{
                				Item ScrollLoot = Loot.RandomScroll( 0, 39, SpellbookType.Regular );
                				ScrollLoot.Amount = Utility.Random( 1, 8 );
                				DropItem( ScrollLoot );
            				}

            				// Potions
            				for ( int i = Utility.Random( 1, 2 ); i > 1; i-- )
            				{
                				Item PotionLoot = Loot.RandomPotion();
                				DropItem( PotionLoot );
            				}

            				// Gems
            				for( int i = Utility.Random( 1, 2 ); i > 1; i-- )
            				{
                				Item GemLoot = Loot.RandomGem();
                				GemLoot.Amount = Utility.Random( 1, 6 );
                				DropItem( GemLoot );
            				}

					break;
				}

				case TreasureLevel.Level3:
				{
					MinGold = 250;
					MaxGold = 350;

            				// Reagents 
            				for( int i = Utility.Random( 1, 3 ); i > 1; i-- )
            				{
                				Item ReagentLoot = Loot.RandomReagent();
                				ReagentLoot.Amount = Utility.Random( 1, 9 );
                				DropItem( ReagentLoot );
            				}

            				// Scrolls
            				for( int i = Utility.Random( 1, 3 ); i > 1; i-- )
            				{
                				Item ScrollLoot = Loot.RandomScroll( 0, 47, SpellbookType.Regular );
                				ScrollLoot.Amount = Utility.Random( 1, 12 );
                				DropItem( ScrollLoot );
            				}

            				// Potions
            				for ( int i = Utility.Random( 1, 3 ); i > 1; i-- )
            				{
                				Item PotionLoot = Loot.RandomPotion();
                				DropItem( PotionLoot );
            				}

            				// Gems
            				for( int i = Utility.Random( 1, 3 ); i > 1; i-- )
            				{
                				Item GemLoot = Loot.RandomGem();
                				GemLoot.Amount = Utility.Random( 1, 9 );
                				DropItem( GemLoot );
            				}

            				// Magic Wand
            				for( int i = Utility.Random( 1, 3 ); i > 1; i-- )
                				DropItem( Loot.RandomWand() );

            				// Equipment
            				for( int i = Utility.Random( 1, 3 ); i > 1; i-- )
            				{
                				Item item = Loot.RandomArmorOrShieldOrWeapon();

                				if( item is BaseWeapon )
                				{
                    					BaseWeapon weapon = (BaseWeapon)item;
                   					weapon.DamageLevel = (WeaponDamageLevel)Utility.Random( 3 );
                    					weapon.AccuracyLevel = (WeaponAccuracyLevel)Utility.Random( 3 );
                    					weapon.DurabilityLevel = (WeaponDurabilityLevel)Utility.Random( 3 );
                    					weapon.Quality = WeaponQuality.Regular;
                				}
                				else if( item is BaseArmor )
                				{
                    					BaseArmor armor = (BaseArmor)item;
                    					armor.ProtectionLevel = (ArmorProtectionLevel)Utility.Random( 3 );
                    					armor.Durability = (ArmorDurabilityLevel)Utility.Random( 3 );
                    					armor.Quality = ArmorQuality.Regular;
                				}

                			DropItem( item );
            				}

           				// Clothing
            				for( int i = Utility.Random( 1, 2 ); i > 1; i-- )
                				DropItem( Loot.RandomClothing() );

            				// Jewelry
            				for( int i = Utility.Random( 1, 2 ); i > 1; i-- )
               				{
						BaseJewel jewel = Loot.RandomJewelry();

						jewel.Effect = (MagicEffect)Utility.Random( 0, 11 );
						jewel.Uses = Utility.Random( 1, 10 );
						DropItem( jewel );
					}

					break;
				}

				case TreasureLevel.Level4:
				{
					MinGold = 500;
					MaxGold = 900;

					if ( 10 > Utility.Random( 100 ) )
						DropItem( new MagicCrystalBall() );

            				// Reagents 
            				for( int i = Utility.Random( 1, 4 ); i > 1; i-- )
            				{
                				Item ReagentLoot = Loot.RandomReagent();
                				ReagentLoot.Amount = 12;
                				DropItem( ReagentLoot );
            				}

            				// Scrolls
            				for( int i = Utility.Random( 1, 4 ); i > 1; i-- )
            				{
                				Item ScrollLoot = Loot.RandomScroll( 0, 47, SpellbookType.Regular );
                				ScrollLoot.Amount = 16;
                				DropItem( ScrollLoot );
            				}

            				// Drop blank scrolls
            				DropItem( new BlankScroll( Utility.Random( 1, 4 ) ) );

            				// Potions
            				for ( int i = Utility.Random( 1, 4 ); i > 1; i-- )
            				{
                				Item PotionLoot = Loot.RandomPotion();
                				DropItem( PotionLoot );
            				}

            				// Gems
            				for( int i = Utility.Random( 1, 4 ); i > 1; i-- )
            				{
                				Item GemLoot = Loot.RandomGem();
                				GemLoot.Amount = 12;
                				DropItem( GemLoot );
            				}

            				// Magic Wand
            				for( int i = Utility.Random( 1, 4 ); i > 1; i-- )
                				DropItem( Loot.RandomWand() );

            				// Equipment
            				for( int i = Utility.Random( 1, 4 ); i > 1; i-- )
            				{
                				Item item = Loot.RandomArmorOrShieldOrWeapon();

                				if( item is BaseWeapon )
                				{
                    					BaseWeapon weapon = (BaseWeapon)item;
                    					weapon.DamageLevel = (WeaponDamageLevel)Utility.Random( 4 );
                    					weapon.AccuracyLevel = (WeaponAccuracyLevel)Utility.Random( 4 );
                    					weapon.DurabilityLevel = (WeaponDurabilityLevel)Utility.Random( 4 );
                    					weapon.Quality = WeaponQuality.Regular;
                				}
               					else if( item is BaseArmor )
                				{
                    					BaseArmor armor = (BaseArmor)item;
                    					armor.ProtectionLevel = (ArmorProtectionLevel)Utility.Random( 4 );
                    					armor.Durability = (ArmorDurabilityLevel)Utility.Random( 4 );
                    					armor.Quality = ArmorQuality.Regular;
                				}
                				DropItem( item );
            				}

            				// Clothing
            				for( int i = Utility.Random( 1, 2 ); i > 1; i-- )
                				DropItem( Loot.RandomClothing() );

            				// Jewelry
            				for( int i = Utility.Random( 1, 2 ); i > 1; i-- )
               				{
						BaseJewel jewel = Loot.RandomJewelry();

						jewel.Effect = (MagicEffect)Utility.Random( 0, 11 );
						jewel.Uses = Utility.Random( 1, 10 );
						DropItem( jewel );
					}

					break;
				}

				case TreasureLevel.Level5:
				{
					MinGold = 750;
					MaxGold = 1000;
					break;
				}

				case TreasureLevel.Level6:
				{
					MinGold = 1150;
					MaxGold = 1450;
					break;
				}
			}
			DropItem( new Gold( MinGold, MaxGold ) );
		}

		public override void LockPick( Mobile from )
		{
			base.LockPick( from );

			SetUnlockedName();
			StartResetTimer();
		}

		public virtual void UnlockSpell()
		{
			SetUnlockedName();
			StartResetTimer();
		}

		public void ClearContents()
		{
			for ( int i = Items.Count - 1; i >= 0; --i )
				if ( i < Items.Count )
					((Item)Items[i]).Delete();
		}

		public void Reset()
		{
			if( m_ResetTimer != null )
			{
				if( m_ResetTimer.Running )
					m_ResetTimer.Stop();
			}

			Locked = true;
			SetLockedName();
			ClearContents();
			GenerateTreasure();
		}

		public enum TreasureLevel
		{
			Level1, 
			Level2, 
			Level3, 
			Level4, 
			Level5,
			Level6,
		}; 

		private class TreasureResetTimer : Timer
		{
			private BaseTreasureChest m_Chest;
			
			public TreasureResetTimer( BaseTreasureChest chest ) : base ( TimeSpan.FromMinutes( Utility.Random( chest.MinSpawnTime, chest.MaxSpawnTime ) ) )
			{
				m_Chest = chest;
				Priority = TimerPriority.OneMinute;
			}

			protected override void OnTick()
			{
				m_Chest.Reset();
			}
		}
	}
}