using System;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseCloak : BaseClothing
	{
		public BaseCloak( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseCloak( int itemID, int hue ) : base( itemID, Layer.Cloak, hue )
		{
		}

		public BaseCloak( Serial serial ) : base( serial )
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

	[Flipable]
	public class Cloak : BaseCloak, IArcaneEquip
	{
		#region Arcane Impl
		private int m_MaxArcaneCharges, m_CurArcaneCharges;

		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxArcaneCharges
		{
			get{ return m_MaxArcaneCharges; }
			set{ m_MaxArcaneCharges = value; InvalidateProperties(); Update(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int CurArcaneCharges
		{
			get{ return m_CurArcaneCharges; }
			set{ m_CurArcaneCharges = value; InvalidateProperties(); Update(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsArcane
		{
			get{ return ( m_MaxArcaneCharges > 0 && m_CurArcaneCharges >= 0 ); }
		}

		public void Update()
		{
			if ( IsArcane )
				ItemID = 0x26AD;
			else if ( ItemID == 0x26AD )
				ItemID = 0x1515;

			if ( IsArcane && CurArcaneCharges == 0 )
				Hue = 0;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( IsArcane )
				list.Add( 1061837, "{0}\t{1}", m_CurArcaneCharges, m_MaxArcaneCharges ); // arcane charges: ~1_val~ / ~2_val~
		}

		public void Flip()
		{
			if ( ItemID == 0x1515 )
				ItemID = 0x1530;
			else if ( ItemID == 0x1530 )
				ItemID = 0x1515;
		}
		#endregion

		[Constructable]
		public Cloak() : this( 0 )
		{
		}

		[Constructable]
		public Cloak( int hue ) : base( 0x1515, hue )
		{
			Weight = 5.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a cloak" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed cloak" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional cloak" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional cloak (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional cloak" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional cloak (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}
				}
				base.OnSingleClick( from );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Cloak( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			if ( IsArcane )
			{
				writer.Write( true );
				writer.Write( (int) m_CurArcaneCharges );
				writer.Write( (int) m_MaxArcaneCharges );
			}
			else
			{
				writer.Write( false );
			}
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					if ( reader.ReadBool() )
					{
						m_CurArcaneCharges = reader.ReadInt();
						m_MaxArcaneCharges = reader.ReadInt();

						if ( Hue == 2118 )
							Hue = ArcaneGem.DefaultArcaneHue;
					}

					break;
				}
			}

			if ( Weight == 4.0 )
				Weight = 5.0;
		}
	}

	[Flipable]
	public class RewardCloak : BaseCloak, Engines.VeteranRewards.IRewardItem
	{
		private int m_LabelNumber;
		private bool m_IsRewardItem;

		[CommandProperty( AccessLevel.GameMaster )]
		public bool IsRewardItem
		{
			get{ return m_IsRewardItem; }
			set{ m_IsRewardItem = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Number
		{
			get{ return m_LabelNumber; }
			set{ m_LabelNumber = value; InvalidateProperties(); }
		}

		public override int LabelNumber
		{
			get
			{
				if ( m_LabelNumber > 0 )
					return m_LabelNumber;

				return base.LabelNumber;
			}
		}

		public override int BasePhysicalResistance{ get{ return 3; } }

		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( parent is Mobile )
				((Mobile)parent).VirtualArmorMod += 2;
		}

		public override void OnRemoved(object parent)
		{
			base.OnRemoved( parent );

			if ( parent is Mobile )
				((Mobile)parent).VirtualArmorMod -= 2;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		public override bool CanEquip( Mobile m )
		{
			if ( !base.CanEquip( m ) )
				return false;

			return !m_IsRewardItem || Engines.VeteranRewards.RewardSystem.CheckIsUsableBy( m, this, new object[]{ Hue, m_LabelNumber } );
		}

		[Constructable]
		public RewardCloak() : this( 0 )
		{
		}

		[Constructable]
		public RewardCloak( int hue ) : this( hue, 0 )
		{
		}

		[Constructable]
		public RewardCloak( int hue, int labelNumber ) : base( 0x1515, hue )
		{
			Weight = 5.0;
			LootType = LootType.Blessed;

			m_LabelNumber = labelNumber;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( m_LabelNumber == 1041286 )
					LabelTo( from, "a bronze cloak" );
				if ( m_LabelNumber == 1041288 )
					LabelTo( from, "a copper cloak" );
				if ( m_LabelNumber == 1041290 )
					LabelTo( from, "an agapite cloak" );
				if ( m_LabelNumber == 1041292 )
					LabelTo( from, "a golden cloak" );
				if ( m_LabelNumber == 1041294 )
					LabelTo( from, "a verite cloak" );
				if ( m_LabelNumber == 1041296 )
					LabelTo( from, "a valorite cloak" );
				if ( m_LabelNumber == 1049725 )
					LabelTo( from, "a dark gray cloak" );
				if ( m_LabelNumber == 1049727 )
					LabelTo( from, "an ice green cloak" );
				if ( m_LabelNumber == 1049729 )
					LabelTo( from, "an ice blue cloak" );
				if ( m_LabelNumber == 1049731 )
					LabelTo( from, "a jet black cloak" );
				if ( m_LabelNumber == 1049733 )
					LabelTo( from, "an ice whitek cloak" );
				if ( m_LabelNumber == 1049735 )
					LabelTo( from, "a fire cloak" );
				else if ( m_LabelNumber != 1049735 && 
						m_LabelNumber != 1049733 && 
						m_LabelNumber != 1049731 && 
						m_LabelNumber != 1049729 && 
						m_LabelNumber != 1049727 && 
						m_LabelNumber != 1049725 && 
						m_LabelNumber != 1041296 && 
						m_LabelNumber != 1041294 && 
						m_LabelNumber != 1041292 && 
						m_LabelNumber != 1041290 && 
						m_LabelNumber != 1041288 && 
						m_LabelNumber != 1041286 )
					LabelTo( from, "a reward cloak" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public RewardCloak( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_LabelNumber );
			writer.Write( (bool) m_IsRewardItem );
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
					m_IsRewardItem = reader.ReadBool();
					break;
				}
			}

			if ( Parent is Mobile )
				((Mobile)Parent).VirtualArmorMod += 2;
		}
	}

	[Flipable( 0x230A, 0x2309 )]
	public class FurCape : BaseCloak
	{
		[Constructable]
		public FurCape() : this( 0 )
		{
		}

		[Constructable]
		public FurCape( int hue ) : base( 0x230A, hue )
		{
			Weight = 4.0;
		}

		public FurCape( Serial serial ) : base( serial )
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
}
