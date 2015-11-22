using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseIngot : Item, ICommodity
	{
		private CraftResource m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public CraftResource Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; InvalidateProperties(); }
		}
		
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} {1} ingot" : "{0} {1} ingots", Amount, CraftResources.GetName( m_Resource ).ToLower() );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
				case 0:
				{
					OreInfo info;

					switch ( reader.ReadInt() )
					{
						case 0: info = OreInfo.Iron; break;
						case 1: info = OreInfo.DullCopper; break;
						case 2: info = OreInfo.ShadowIron; break;
						case 3: info = OreInfo.Copper; break;
						case 4: info = OreInfo.Bronze; break;
						case 5: info = OreInfo.Gold; break;
						case 6: info = OreInfo.Agapite; break;
						case 7: info = OreInfo.Verite; break;
						case 8: info = OreInfo.Valorite; break;
						default: info = null; break;
					}

					m_Resource = CraftResources.GetFromOreInfo( info );
					break;
				}
			}
		}

		public BaseIngot( CraftResource resource ) : this( resource, 1 )
		{
		}

		public BaseIngot( CraftResource resource, int amount ) : base( 0x1BF2 )
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		public BaseIngot( Serial serial ) : base( serial )
		{
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if ( Amount > 1 )
				list.Add( 1050039, "{0}\t#{1}", Amount, 1027154 ); // ~1_NUMBER~ ~2_ITEMNAME~
			else
				list.Add( 1027154 ); // ingots
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( !CraftResources.IsStandard( m_Resource ) )
			{
				int num = CraftResources.GetLocalizationNumber( m_Resource );

				if ( num > 0 )
					list.Add( num );
				else
					list.Add( CraftResources.GetName( m_Resource ) );
			}
		}

		public override int LabelNumber
		{
			get
			{
				if ( m_Resource >= CraftResource.DullCopper && m_Resource <= CraftResource.Valorite )
					return 1042684 + (int)(m_Resource - CraftResource.DullCopper);

				return 1042692;
			}
		}
	}

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class IronIngot : BaseIngot
	{
		[Constructable]
		public IronIngot() : this( 1 )
		{
		}

		[Constructable]
		public IronIngot( int amount ) : base( CraftResource.Iron, amount )
		{
		}


		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, this.Amount + " iron ingots" ) );
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an iron ingot" ) );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public IronIngot( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new IronIngot( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class DullCopperIngot : BaseIngot
	{
		[Constructable]
		public DullCopperIngot() : this( 1 )
		{
		}

		[Constructable]
		public DullCopperIngot( int amount ) : base( CraftResource.DullCopper, amount )
		{
		}


		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, this.Amount + " dull copper ingots" ) );
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a dull copper ingot" ) );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public DullCopperIngot( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new DullCopperIngot( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class ShadowIronIngot : BaseIngot
	{
		[Constructable]
		public ShadowIronIngot() : this( 1 )
		{
		}

		[Constructable]
		public ShadowIronIngot( int amount ) : base( CraftResource.ShadowIron, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, this.Amount + " shadow iron ingots" ) );
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a shadow iron ingot" ) );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public ShadowIronIngot( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new ShadowIronIngot( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class CopperIngot : BaseIngot
	{
		[Constructable]
		public CopperIngot() : this( 1 )
		{
		}

		[Constructable]
		public CopperIngot( int amount ) : base( CraftResource.Copper, amount )
		{
		}


		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, this.Amount + " copper ingots" ) );
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a copper ingot" ) );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public CopperIngot( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new CopperIngot( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class BronzeIngot : BaseIngot
	{
		[Constructable]
		public BronzeIngot() : this( 1 )
		{
		}

		[Constructable]
		public BronzeIngot( int amount ) : base( CraftResource.Bronze, amount )
		{
		}


		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, this.Amount + " bronze ingots" ) );
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a bronze ingot" ) );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BronzeIngot( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new BronzeIngot( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class GoldIngot : BaseIngot
	{
		[Constructable]
		public GoldIngot() : this( 1 )
		{
		}

		[Constructable]
		public GoldIngot( int amount ) : base( CraftResource.Gold, amount )
		{
		}


		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, this.Amount + " gold ingots" ) );
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a gold ingot" ) );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public GoldIngot( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new GoldIngot( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class AgapiteIngot : BaseIngot
	{
		[Constructable]
		public AgapiteIngot() : this( 1 )
		{
		}

		[Constructable]
		public AgapiteIngot( int amount ) : base( CraftResource.Agapite, amount )
		{
		}


		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, this.Amount + " agapite ingots" ) );
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "an agapite ingot" ) );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public AgapiteIngot( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new AgapiteIngot( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class VeriteIngot : BaseIngot
	{
		[Constructable]
		public VeriteIngot() : this( 1 )
		{
		}

		[Constructable]
		public VeriteIngot( int amount ) : base( CraftResource.Verite, amount )
		{
		}


		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, this.Amount + " verite ingots" ) );
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a verite ingot" ) );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public VeriteIngot( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new VeriteIngot( amount ), amount );
		}
	}

	[FlipableAttribute( 0x1BF2, 0x1BEF )]
	public class ValoriteIngot : BaseIngot
	{
		[Constructable]
		public ValoriteIngot() : this( 1 )
		{
		}

		[Constructable]
		public ValoriteIngot( int amount ) : base( CraftResource.Valorite, amount )
		{
		}


		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, this.Amount + " valorite ingots" ) );
				}
				else
				{
					from.Send( new AsciiMessage( this.Serial, 0, MessageType.Label, 0x3B2, 3, null, "a valorite ingot" ) );
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public ValoriteIngot( Serial serial ) : base( serial )
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

		public override Item Dupe( int amount )
		{
			return base.Dupe( new ValoriteIngot( amount ), amount );
		}
	}
}
