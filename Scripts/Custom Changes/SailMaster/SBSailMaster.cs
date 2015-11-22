using System;
using System.Collections;
using Server.Items;
using Server.Multis;

namespace Server.Mobiles
{
	public class SBSailMaster : SBInfo
	{
		private ArrayList m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBSailMaster()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override ArrayList BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : ArrayList
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( "Sailboat Membershipcard", typeof( SailboatMembershipcard ), 10000, 5, 0x14F0, 0x4F1 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( SailboatMembershipcard ), 5000 );
			}
		}
	}
}