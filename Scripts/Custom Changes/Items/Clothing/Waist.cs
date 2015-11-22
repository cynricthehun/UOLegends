using System;
using Server.Network;

namespace Server.Items
{

	public abstract class BaseWaist : BaseClothing
	{
		public BaseWaist( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseWaist( int itemID, int hue ) : base( itemID, Layer.Waist, hue )
		{
		}

		public BaseWaist( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x153b, 0x153c )]
	public class HalfApron : BaseWaist
	{
		[Constructable]
		public HalfApron() : this( 0 )
		{
		}

		[Constructable]
		public HalfApron( int hue ) : base( 0x153b, hue )
		{
			Weight = 2.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a half apron" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed half apron" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional half apron" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional half apron (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional half apron" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional half apron (crafted by:" + this.Crafter.Name + ")" ) );
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

		public HalfApron( Serial serial ) : base( serial )
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
