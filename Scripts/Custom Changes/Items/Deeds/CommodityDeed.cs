using System;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{
	public interface ICommodity
	{
		string Description{ get; }
	}

	public class CommodityDeed : Item
	{
		private Item m_Commodity;

		[CommandProperty( AccessLevel.GameMaster )]
		public Item Commodity
		{
			get
			{
				return m_Commodity;
			}
		}

		public bool SetCommodity( Item item )
		{
			InvalidateProperties();

			if ( m_Commodity == null && item is ICommodity )
			{
				m_Commodity = item;
				m_Commodity.Internalize();
				InvalidateProperties();

				return true;
			}
			else
			{
				return false;
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( m_Commodity );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Commodity = reader.ReadItem();

					break;
				}
			}
		}

		public CommodityDeed( Item commodity ) : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 0x47;

			m_Commodity = commodity;

			LootType = LootType.Blessed;
		}

		[Constructable]
		public CommodityDeed() : this( null )
		{
		}

		public CommodityDeed( Serial serial ) : base( serial )
		{
		}

		public override void OnDelete()
		{
			if ( m_Commodity != null )
				m_Commodity.Delete();

			base.OnDelete();
		}

		public override int LabelNumber{ get{ return m_Commodity == null ? 1047016 : 1047017; } }

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties( list );

			if ( m_Commodity != null )
				list.Add( 1060658, "#{0}\t{1}", m_Commodity.LabelNumber, m_Commodity.Amount ); // ~1_val~: ~2_val~
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, " a comoddity deed" );
			}
			else
			{
				LabelTo( from, this.Name );
			}

			if ( m_Commodity != null )
				from.Send( new AsciiMessage( Serial, ItemID, MessageType.Label, 0x3B2, 3, null, ((ICommodity)m_Commodity).Description ) );
		}

		public override void OnDoubleClick( Mobile from )
		{
			string number;

			BankBox box = from.BankBox;

			if ( m_Commodity != null )
			{
				if ( box != null && IsChildOf( box ) )
				{
					number = "The commodity has been redeemed.";

					box.DropItem( m_Commodity );

					m_Commodity = null;
					Delete();
				}
				else
				{
					number = "To claim the resources ....";
				}
			}
			else if ( box == null || !IsChildOf( box ) )
			{
				number = "That must be in your bank box to use it.";
			}
			else
			{
				number = "Target the commodity to fill this deed with.";

				from.Target = new InternalTarget( this );
			}

			from.SendAsciiMessage( number );
		}

		private class InternalTarget : Target
		{
			private CommodityDeed m_Deed;

			public InternalTarget( CommodityDeed deed ) : base( 3, false, TargetFlags.None )
			{
				m_Deed = deed;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Deed.Deleted )
					return;

				string number;

				if ( m_Deed.Commodity != null )
				{
					number = "The commodity deed has already been filled.";
				}
				else if ( targeted is Item )
				{
					BankBox box = from.BankBox;

					if ( box != null && m_Deed.IsChildOf( box ) && ((Item)targeted).IsChildOf( box ) )
					{
						if ( m_Deed.SetCommodity( (Item) targeted ) )
						{
							number = "The commodity deed has been filled.";
						}
						else
						{
							number = "That is not a commodity the bankers will fill a commodity deed with.";
						}
					}
					else
					{
						number = "That must be in your bank box to use it.";
					}
				}
				else
				{
					number = "That is not a commodity the bankers will fill a commodity deed with.";
				}

				from.SendAsciiMessage( number );
			}
		}
	}
}