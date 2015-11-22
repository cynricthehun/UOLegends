using System;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseHat : BaseClothing
	{
		public BaseHat( int itemID ) : this( itemID, 0 )
		{
		}

		public BaseHat( int itemID, int hue ) : base( itemID, Layer.Helm, hue )
		{
		}

		public BaseHat( Serial serial ) : base( serial )
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
	[Flipable( 0x2798, 0x27E3 )]
	public class Kasa : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		//TODO: MinHits = 20, MaxHits = 30

		[Constructable]
		public Kasa() : this( 0 )
		{
		}

		[Constructable]
		public Kasa( int hue ) : base( 0x2798, hue )
		{
			Weight = 3.0;
		}

		public Kasa( Serial serial ) : base( serial )
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

	[Flipable( 0x278F, 0x27DA )]
	public class ClothNinjaHood : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 9; } }

		//TODO: MinHits = 20, MaxHits = 30

		[Constructable]
		public ClothNinjaHood() : this( 0 )
		{
		}

		[Constructable]
		public ClothNinjaHood( int hue ) : base( 0x278F, hue )
		{
			Weight = 2.0;
		}

		public ClothNinjaHood( Serial serial ) : base( serial )
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

	[Flipable( 0x2306, 0x2305 )]
	public class FlowerGarland : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 9; } }

		[Constructable]
		public FlowerGarland() : this( 0 )
		{
		}

		[Constructable]
		public FlowerGarland( int hue ) : base( 0x2306, hue )
		{
			Weight = 1.0;
		}

		public FlowerGarland( Serial serial ) : base( serial )
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

	public class FloppyHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public FloppyHat() : this( 0 )
		{
		}

		[Constructable]
		public FloppyHat( int hue ) : base( 0x1713, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a floppy hat" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed floppy hat" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional floppy hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional floppy hat (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional floppy hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional floppy hat (crafted by:" + this.Crafter.Name + ")" ) );
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

		public FloppyHat( Serial serial ) : base( serial )
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

	public class WideBrimHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public WideBrimHat() : this( 0 )
		{
		}

		[Constructable]
		public WideBrimHat( int hue ) : base( 0x1714, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a wide-brim hat" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed wide-brim hat" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional wide-brim hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional wide-brim hat (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional wide-brim hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional wide-brim hat (crafted by:" + this.Crafter.Name + ")" ) );
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

		public WideBrimHat( Serial serial ) : base( serial )
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

	public class Cap : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public Cap() : this( 0 )
		{
		}

		[Constructable]
		public Cap( int hue ) : base( 0x1715, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a cap" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed cap" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional cap" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional cap (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional cap" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional cap (crafted by:" + this.Crafter.Name + ")" ) );
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

		public Cap( Serial serial ) : base( serial )
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

	public class SkullCap : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		[Constructable]
		public SkullCap() : this( 0 )
		{
		}

		[Constructable]
		public SkullCap( int hue ) : base( 0x1544, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a skullcap" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed skullcap" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional skullcap" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional skullcap (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional skullcap" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional skullcap (crafted by:" + this.Crafter.Name + ")" ) );
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

		public SkullCap( Serial serial ) : base( serial )
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

	public class Bandana : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 8; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		[Constructable]
		public Bandana() : this( 0 )
		{
		}

		[Constructable]
		public Bandana( int hue ) : base( 0x1540, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a bandana" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed bandana" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional bandana" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional bandana (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional bandana" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional bandana (crafted by:" + this.Crafter.Name + ")" ) );
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

		public Bandana( Serial serial ) : base( serial )
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

	public class BearMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 3; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 4; } }

		[Constructable]
		public BearMask() : this( 0 )
		{
		}

		[Constructable]
		public BearMask( int hue ) : base( 0x1545, hue )
		{
			Weight = 1.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a bear mask" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed bear mask" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional bear mask" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional bear mask (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional bear mask" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional bear mask (crafted by:" + this.Crafter.Name + ")" ) );
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

		public BearMask( Serial serial ) : base( serial )
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

	public class DeerMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 2; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 1; } }
		public override int BaseEnergyResistance{ get{ return 7; } }

		[Constructable]
		public DeerMask() : this( 0 )
		{
		}

		[Constructable]
		public DeerMask( int hue ) : base( 0x1547, hue )
		{
			Weight = 1.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a deer mask" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed deer mask" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional deer mask" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional deer mask (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional deer mask" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional deer mask (crafted by:" + this.Crafter.Name + ")" ) );
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

		public DeerMask( Serial serial ) : base( serial )
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

	public class HornedTribalMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 6; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 4; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public HornedTribalMask() : this( 0 )
		{
		}

		[Constructable]
		public HornedTribalMask( int hue ) : base( 0x1549, hue )
		{
			Weight = 1.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a tribal mask" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed tribal mask" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tribal mask" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tribal mask (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tribal mask" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tribal mask (crafted by:" + this.Crafter.Name + ")" ) );
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

		public HornedTribalMask( Serial serial ) : base( serial )
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

	public class TribalMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public TribalMask() : this( 0 )
		{
		}

		[Constructable]
		public TribalMask( int hue ) : base( 0x154B, hue )
		{
			Weight = 1.0;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a tribal mask" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed tribal mask" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tribal mask" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tribal mask (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tribal mask" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tribal mask (crafted by:" + this.Crafter.Name + ")" ) );
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

		public TribalMask( Serial serial ) : base( serial )
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

	public class TallStrawHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public TallStrawHat() : this( 0 )
		{
		}

		[Constructable]
		public TallStrawHat( int hue ) : base( 0x1716, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a tall straw hat" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed tall straw hat" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tall straw hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tall straw hat (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tall straw hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tall straw hat (crafted by:" + this.Crafter.Name + ")" ) );
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

		public TallStrawHat( Serial serial ) : base( serial )
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

	public class StrawHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public StrawHat() : this( 0 )
		{
		}

		[Constructable]
		public StrawHat( int hue ) : base( 0x1717, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a straw hat" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed straw hat" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional straw hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional straw hat (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional straw hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional straw hat (crafted by:" + this.Crafter.Name + ")" ) );
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

		public StrawHat( Serial serial ) : base( serial )
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

	public class OrcishKinMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 1; } }
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 7; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		[Constructable]
		public OrcishKinMask() : this( 0x8A4 )
		{
		}

		[Constructable]
		public OrcishKinMask( int hue ) : base( 0x141B, hue )
		{
			Weight = 2.0;
			Name = "a mask of orcish kin";
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a mask of orcish kin" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed mask of orcish kin" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional mask of orcish kin" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional mask of orcish kin (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional mask of orcish kin" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional mask of orcish kin (crafted by:" + this.Crafter.Name + ")" ) );
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

		public override bool CanEquip( Mobile m )
		{
			if ( !base.CanEquip( m ) )
				return false;

			if ( m.BodyMod == 183 || m.BodyMod == 184 )
			{
				m.SendAsciiMessage( "You can't do that while wearing savage kin paint." );
				return false;
			}

			return true;
		}

		public override void OnAdded( object parent )
		{
			base.OnAdded( parent );

			if ( parent is Mobile )
				Misc.Titles.AwardKarma( (Mobile)parent, -20, true );
		}

		public OrcishKinMask( Serial serial ) : base( serial )
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

			if ( Hue != 0x8A4 )
				Hue = 0x8A4;
		}
	}

	public class SavageMask : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 3; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public static int GetRandomHue()
		{
			int v = Utility.RandomBirdHue();

			if ( v == 2101 )
				v = 0;

			return v;
		}

		public override bool Dye( Mobile from, DyeTub sender )
		{
			from.SendAsciiMessage( sender.FailMessage );
			return false;
		}

		[Constructable]
		public SavageMask() : this( GetRandomHue() )
		{
		}

		[Constructable]
		public SavageMask( int hue ) : base( 0x154B, hue )
		{
			Weight = 2.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a tribal mask" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed tribal mask" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tribal mask" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tribal mask (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tribal mask" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tribal mask (crafted by:" + this.Crafter.Name + ")" ) );
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

		public SavageMask( Serial serial ) : base( serial )
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

			if ( Hue != 0 && (Hue < 2101 || Hue > 2130) )
				Hue = GetRandomHue();
		}
	}

	public class WizardsHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public WizardsHat() : this( 0 )
		{
		}

		[Constructable]
		public WizardsHat( int hue ) : base( 0x1718, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a wizard's hat" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed wizard's hat" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional wizard's hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional wizard's hat (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional wizard's hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional wizard's hat (crafted by:" + this.Crafter.Name + ")" ) );
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

		public WizardsHat( Serial serial ) : base( serial )
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

	public class MagicWizardsHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int LabelNumber{ get{ return 1041072; } } // a magical wizard's hat

		public override int BaseStrBonus{ get{ return -5; } }
		public override int BaseDexBonus{ get{ return -5; } }
		public override int BaseIntBonus{ get{ return +5; } }

		[Constructable]
		public MagicWizardsHat() : this( 0 )
		{
		}

		[Constructable]
		public MagicWizardsHat( int hue ) : base( 0x1718, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a magical wizard's hat" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed magical wizard's hat" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional magical wizard's hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional magical wizard's hat (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional magical wizard's hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional magical wizard's hat (crafted by:" + this.Crafter.Name + ")" ) );
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

		public MagicWizardsHat( Serial serial ) : base( serial )
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

	public class Bonnet : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public Bonnet() : this( 0 )
		{
		}

		[Constructable]
		public Bonnet( int hue ) : base( 0x1719, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a bonnet" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed bonnet" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional bonnet" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional bonnet (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional bonnet" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional bonnet (crafted by:" + this.Crafter.Name + ")" ) );
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

		public Bonnet( Serial serial ) : base( serial )
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

	public class FeatheredHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public FeatheredHat() : this( 0 )
		{
		}

		[Constructable]
		public FeatheredHat( int hue ) : base( 0x171A, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a feathered hat" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed feathered hat" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional feathered hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional feathered hat (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional feathered hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional feathered hat (crafted by:" + this.Crafter.Name + ")" ) );
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

		public FeatheredHat( Serial serial ) : base( serial )
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

	public class TricorneHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public TricorneHat() : this( 0 )
		{
		}

		[Constructable]
		public TricorneHat( int hue ) : base( 0x171B, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a tricorne hat" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed tricorne hat" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tricorne hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional tricorne hat (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tricorne hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional tricorne hat (crafted by:" + this.Crafter.Name + ")" ) );
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

		public TricorneHat( Serial serial ) : base( serial )
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

	public class JesterHat : BaseHat
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 9; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public JesterHat() : this( 0 )
		{
		}

		[Constructable]
		public JesterHat( int hue ) : base( 0x171C, hue )
		{
			Weight = 1.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Quality != ClothingQuality.Exceptional && this.LootType != LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a jester hat" ) );
				}
				else if ( this.Quality != ClothingQuality.Exceptional && this.LootType == LootType.Blessed )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed jester hat" ) );
				}
				else if ( this.Quality == ClothingQuality.Exceptional )
				{
					if ( this.LootType != LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional jester hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an exceptional jester hat (crafted by:" + this.Crafter.Name + ")" ) );
						}
					}	
					else if ( this.LootType == LootType.Blessed )
					{
						if ( this.Crafter == null )
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional jester hat" ) );
						}
						else
						{
							from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a blessed, exceptional jester hat (crafted by:" + this.Crafter.Name + ")" ) );
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

		public JesterHat( Serial serial ) : base( serial )
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
