using System;
using Server.Items;
using System.Collections;
using Server;

namespace Server.Mobiles
{
	public class Jeweler : BaseVendor
	{
		private ArrayList m_SBInfos = new ArrayList();
		protected override ArrayList SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public Jeweler() : base( "the jeweler" )
		{
			SetSkill( SkillName.ItemID, 64.0, 100.0 );
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBJewel() );
		}

		public override void InitOutfit()
		{

			Item hair = new Item( Utility.RandomList( 0x203B, 0x2049, 0x2048, 0x204A ) );
			hair.Hue = Utility.RandomHairHue();
			hair.Layer = Layer.Hair;
			hair.Movable = false;
			AddItem( hair );

			if ( this.Female == true )
			{
				switch ( Utility.Random( 5 ) )
				{
					case 0:
					{
						AddItem( new Server.Items.Skirt( Utility.RandomNeutralHue() ) );
						AddItem( new Server.Items.FancyShirt( Utility.RandomNeutralHue() ) );
						break;
					}
					case 1:
					{
						AddItem( new Server.Items.Kilt( Utility.RandomNeutralHue() ) );
						AddItem( new Server.Items.Shirt( Utility.RandomNeutralHue() ) );
						AddItem( new Server.Items.Sandals( 1 ) );
						break;
					}

					case 2:
					{
						AddItem( new Server.Items.Kilt( Utility.RandomNeutralHue() ) );
						AddItem( new Server.Items.FancyShirt( Utility.RandomNeutralHue() ) );
						break;
					}

					case 3:
					{
						AddItem( new Server.Items.Skirt( Utility.RandomNeutralHue() ) );
						AddItem( new Server.Items.Shirt( Utility.RandomNeutralHue() ) );
						break;
					}

					case 4:
					{
						AddItem( new Server.Items.Kilt( Utility.RandomNeutralHue() ) );
						AddItem( new Server.Items.FancyShirt( Utility.RandomNeutralHue() ) );
						break;
					}
				}
			}
			else
			{
				switch ( Utility.Random( 4 ) )
				{
					case 0:
					{
						AddItem( new Server.Items.LongPants( Utility.RandomNeutralHue() ) );
						AddItem( new Server.Items.FancyShirt( Utility.RandomNeutralHue() ) );
						break;
					}

					case 1:
					{
						AddItem( new Server.Items.ShortPants( Utility.RandomNeutralHue() ) );
						AddItem( new Server.Items.FancyShirt( Utility.RandomNeutralHue() ) );
						break;
					}

					case 2:
					{
						AddItem( new Server.Items.LongPants( Utility.RandomNeutralHue() ) );
						AddItem( new Server.Items.Shirt( Utility.RandomNeutralHue() ) );
						break;
					}

					case 3:
					{
						AddItem( new Server.Items.ShortPants( Utility.RandomNeutralHue() ) );
						AddItem( new Server.Items.Shirt( Utility.RandomNeutralHue() ) );
						break;
					}
				}
			}
	                switch ( Utility.Random( 5 ) )  
			{  
				case 0:
				{
					AddItem( new Server.Items.Sandals( Utility.RandomNeutralHue() ) );
					break;
				}
				case 1:
				{
					AddItem( new Server.Items.Shoes( Utility.RandomNeutralHue() ) );
					break;
				}
				case 2:
				{
					AddItem( new Server.Items.Sandals() );
					break;
				}
				case 3:
				{
					AddItem( new Server.Items.Shoes( 1721 ) );
					break;
				}
				case 4:
				{
					AddItem( new Server.Items.Shoes( 1 ) );
					break;
				}
        		 }
		}

		public Jeweler( Serial serial ) : base( serial )
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
