using System;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Multis;
using Server.Targeting;

namespace Server.Items
{
	public class BannerDeed : Item
	{
		[Constructable]
		public BannerDeed() : base(0x14F0)
		{
			LootType = LootType.Blessed;
			Weight = 1.0;
		}

		public BannerDeed( Serial serial ) : base( serial )
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
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a banner deed" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public override void OnDoubleClick( Mobile from ) // Override double click of the deed to call our target
		{
			if ( !IsChildOf( from.Backpack ) ) // Make sure its in their pack
			{
				 from.SendAsciiMessage( "That must be in your pack for you to use it." );
			}
			else
			{
				from.SendAsciiMessage( "Where would you like to place this banner?" );
				from.Target = new BannerTarget( this ); // Call our target
			 }
		}
		private class BannerTarget : Target
		{
			private Item m_Deed;
			public BannerTarget( Item item ) : base( -1, true, TargetFlags.None)
			{
				m_Deed = item;

				CheckLOS = false;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				IPoint3D p = targeted as IPoint3D;

				if ( p == null || m_Deed.Deleted )
					return;
				if ( m_Deed.IsChildOf( from.Backpack ) )
				{
					BaseHouse house = BaseHouse.FindHouseAt( from );

					if ( !( house == null || !house.IsOwner( from ) || !house.IsCoOwner( from ) ) )
					{
						from.SendGump( new ChooseBannerGump( p ) );
						m_Deed.Delete();
					}
					else
						from.SendAsciiMessage( "You can only place this in a house that you own!" );
				}
				else
					from.SendAsciiMessage( "That must be in your pack for you to use it." );
			}
		}
	}
	public class Banner : Item, IDyable
	{
		[Constructable]
		public Banner(int id) : base(id)
		{
			Movable = false;
		}

		public Banner( Serial serial ) : base( serial )
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
			LootType = LootType.Blessed;

			int version = reader.ReadInt();
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "a banner" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			BaseHouse house = BaseHouse.FindHouseAt( this );
			if ( !( house == null || !house.IsOwner( from ) || !house.IsCoOwner( from ) ) )
			{
				from.SendGump( new BannerCloseGump( this ) );
			}
		}

		public virtual bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;
			else if ( RootParent is Mobile && from != RootParent )
				return false;

			Hue = sender.DyedHue;

			return true;
		}
	}
}

namespace Server.Gumps
{
	public class ChooseBannerGump : Gump
	{
		private IPoint3D m_Loc;
		public ChooseBannerGump(IPoint3D loc) : base(0,0)
		{
			m_Loc = loc;
			Closable = false;
			Dragable = true;

			AddPage(0);

			AddBackground( 0, 0, 500, 230, 2600);
			AddLabel( 45, 15, 1152, "Choose a Banner:");
			int id = 5550;

			for ( int i = 1; i <= 4; i++ )
			{
				AddPage(i);
				int aux = 0;

				for ( int j = 0; aux != 8; j++ )
				{
					if ( (i == 1 &&  j == 4 ) || (i == 4 && j == 6 ))
						j = j + 2;

					AddButton( 30 + 60*aux, 50, 2117, 2118, 8*(i-1) + aux, GumpButtonType.Reply, 0 );
					AddItem( 15 + 60*aux, 70, id + 2*j);

					if ( i < 4 )
						AddButton( 430, 200, 2224, 2224, 0, GumpButtonType.Page, i + 1 );
					if ( i > 1 )
						AddButton( 50, 200, 2223, 2223, 0, GumpButtonType.Page, i - 1 );
					if ( aux == 7 )
						id = id + 2*(j+1);
					aux++;
				}
			}
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Mobile m = sender.Mobile;
			
			int id = 0;
			if ( info.ButtonID > 3 )
				if ( info.ButtonID > 27 )
					id = ( ( info.ButtonID+4 )*2 ) + 5550;
				else
					id = ( ( info.ButtonID+2 )*2 ) + 5550;
			else
				id = ( info.ButtonID*2) + 5550;

			m.CloseGump( typeof ( ChooseBannerGump ) );
			m.SendGump( new ChooseBannerFaceGump(id, m_Loc) );
		}
	}

	public class ChooseBannerFaceGump : Gump
	{
		private int m_Id;
		private IPoint3D m_Loc;
		public ChooseBannerFaceGump(int id, IPoint3D loc) : base(0,0)
		{
			m_Id = id;
			m_Loc = loc;
			Closable = false;
			Dragable = true;

			AddPage(0);

			AddBackground( 0, 2, 300, 150, 2600);
			AddButton( 50, 40, 2151, 2153, 1, GumpButtonType.Reply, 0 );
			AddItem( 90, 35, id+1);
			AddButton( 150, 40, 2151, 2153, 0, GumpButtonType.Reply, 0 );
			AddItem( 180, 35, id);
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			Item banner = new Banner(m_Id+info.ButtonID);
			banner.Map = from.Map;
			banner.Location = new Point3D( m_Loc );
		}
	}

	public class BannerCloseGump : Gump
	{
		private Item m_Item;

		public void AddButtonLabel( int x, int y, int buttonID, string text )
		{
			AddButton( x, y - 1, 4005, 4007, buttonID, GumpButtonType.Reply, 0 );
			AddLabel( x + 35, y, 0, text );
		}

		public BannerCloseGump( Item item ) : base(0,0)
		{
			m_Item = item;
			Closable = false;
			Dragable = true;

			AddPage(0);

			AddBackground( 0, 0, 215, 180, 5054);
			AddBackground( 10, 10, 195, 160, 3000);
			AddLabel( 20, 40, 0, "Do you wish to re-deed this");
			AddLabel( 20, 60, 0, "banner?");
			AddButtonLabel( 20, 110, 1, "CONTINUE" );
			AddButtonLabel( 20, 135, 0, "CANCEL" );
		}

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info )
		{
			if (info.ButtonID == 1)
			{
				m_Item.Delete();
				sender.Mobile.AddToBackpack( new BannerDeed() );
			}
		}
	}
}

