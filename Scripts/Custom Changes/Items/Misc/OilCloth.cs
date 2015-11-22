using System;
using Server;
using Server.Mobiles;
using Server.Targeting;

namespace Server.Items
{
	public class OilCloth : Item, IScissorable, IDyable
	{
		public override int LabelNumber{ get{ return 1041498; } } // oil cloth

		[Constructable]
		public OilCloth() : this( 1 )
		{
		}

		[Constructable]
		public OilCloth( int amount ) : base( 0x175D )
		{
			Weight = 1.0;
			Hue = 2001;
			Stackable = true;
			Amount = amount;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.Amount > 1 )
				{
					LabelTo( from, this.Amount + " oil cloths" );
				}
				else
				{
					LabelTo( from, "an oil cloth" );
				}

			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public override Item Dupe( int amount )
		{
			return base.Dupe( new OilCloth( amount ), amount );
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public bool Scissor( Mobile from, Scissors scissors )
		{
			if ( Deleted || !from.CanSee( this ) )
				return false;

			base.ScissorHelper( from, new Bandage(), 1 );

			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.BeginTarget( -1, false, TargetFlags.None, new TargetCallback( OnTarget ) );
				from.SendAsciiMessage( "Select the weapon or armor you wish to use the cloth on." );
			}
			else
			{
				from.SendAsciiMessage( "That must be in your pack for you to use it." );
			}
		}

		public void OnTarget( Mobile from, object obj )
		{
			// TODO: Need details on how oil cloths should get consumed here

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendAsciiMessage( "That must be in your pack for you to use it." );
			}
			else if ( obj is BaseWeapon )
			{
				BaseWeapon weapon = (BaseWeapon)obj;

				if ( weapon.RootParent != from )
				{
					from.SendAsciiMessage( "You may only wipe down items you are holding or carrying." );
				}
				else if ( weapon.Poison == null || weapon.PoisonCharges <= 0 )
				{
					from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1005422 ); // Hmmmm... this does not need to be cleaned.
				}
				else
				{
					if ( weapon.PoisonCharges < 2 )
						weapon.PoisonCharges = 0;
					else
						weapon.PoisonCharges -= 2;

					if ( weapon.PoisonCharges > 0 )
						from.SendAsciiMessage( "You have removed some of the caustic substance, but not all." );
					else
						from.SendAsciiMessage( "You have cleaned the item." );
				}
			}
			else if ( obj == from && obj is PlayerMobile )
			{
				PlayerMobile pm = (PlayerMobile)obj;

				if ( pm.BodyMod == 183 || pm.BodyMod == 184 )
				{
					pm.SavagePaintExpiration = TimeSpan.Zero;

					pm.BodyMod = 0;
					pm.HueMod = -1;

					from.SendAsciiMessage( "You wipe away all of your body paint." );

					Consume();
				}
				else
				{
					from.LocalOverheadMessage( Network.MessageType.Regular, 0x3B2, 1005422 ); // Hmmmm... this does not need to be cleaned.
				}
			}
			else
			{
				from.SendAsciiMessage( "The cloth will not work on that." );
			}
		}

		public OilCloth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
