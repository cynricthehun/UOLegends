using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	public abstract class BaseScales : Item, ICommodity
	{
		public override int LabelNumber{ get{ return 1053139; } } // dragon scales

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
				return String.Format( Amount == 1 ? "{1}" : "{0} {1}", Amount, CraftResources.GetName( m_Resource ).ToLower() );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (int) m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_Resource = (CraftResource)reader.ReadInt();
					break;
				}
			}
		}

		public BaseScales( CraftResource resource ) : this( resource, 1 )
		{
		}

		public BaseScales( CraftResource resource, int amount ) : base( 0x26B4 )
		{
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
			Hue = CraftResources.GetHue( resource );

			m_Resource = resource;
		}

		public BaseScales( Serial serial ) : base( serial )
		{
		}
	}

	public class RedScales : BaseScales
	{
		[Constructable]
		public RedScales() : this( 1 )
		{
		}

		[Constructable]
		public RedScales( int amount ) : base( CraftResource.RedScales, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " dragon scales" );
				}
				else
				{
					LabelTo( from, "dragon scales" );	
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public RedScales( Serial serial ) : base( serial )
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
			return base.Dupe( new RedScales( amount ), amount );
		}
	}

	public class YellowScales : BaseScales
	{
		[Constructable]
		public YellowScales() : this( 1 )
		{
		}

		[Constructable]
		public YellowScales( int amount ) : base( CraftResource.YellowScales, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " dragon scales" );
				}
				else
				{
					LabelTo( from, "dragon scales" );	
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public YellowScales( Serial serial ) : base( serial )
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
			return base.Dupe( new YellowScales( amount ), amount );
		}
	}

	public class BlackScales : BaseScales
	{
		[Constructable]
		public BlackScales() : this( 1 )
		{
		}

		[Constructable]
		public BlackScales( int amount ) : base( CraftResource.BlackScales, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " dragon scales" );
				}
				else
				{
					LabelTo( from, "dragon scales" );	
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BlackScales( Serial serial ) : base( serial )
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
			return base.Dupe( new BlackScales( amount ), amount );
		}
	}

	public class GreenScales : BaseScales
	{
		[Constructable]
		public GreenScales() : this( 1 )
		{
		}

		[Constructable]
		public GreenScales( int amount ) : base( CraftResource.GreenScales, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " dragon scales" );
				}
				else
				{
					LabelTo( from, "dragon scales" );	
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public GreenScales( Serial serial ) : base( serial )
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
			return base.Dupe( new GreenScales( amount ), amount );
		}
	}

	public class WhiteScales : BaseScales
	{
		[Constructable]
		public WhiteScales() : this( 1 )
		{
		}

		[Constructable]
		public WhiteScales( int amount ) : base( CraftResource.WhiteScales, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " dragon scales" );
				}
				else
				{
					LabelTo( from, "dragon scales" );	
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public WhiteScales( Serial serial ) : base( serial )
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
			return base.Dupe( new WhiteScales( amount ), amount );
		}
	}

	public class BlueScales : BaseScales
	{
		public override int LabelNumber{ get{ return 1053140; } } // sea serpent scales

		[Constructable]
		public BlueScales() : this( 1 )
		{
		}

		[Constructable]
		public BlueScales( int amount ) : base( CraftResource.BlueScales, amount )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " dragon scales" );
				}
				else
				{
					LabelTo( from, "dragon scales" );	
				}
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public BlueScales( Serial serial ) : base( serial )
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
			return base.Dupe( new BlueScales( amount ), amount );
		}
	}
}