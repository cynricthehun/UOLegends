using System;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class Cotton : Item, IDyable
	{
		[Constructable]
		public Cotton() : this( 1 )
		{
		}

		[Constructable]
		public Cotton( int amount ) : base( 0xDF9 )
		{
			Stackable = true;
			Weight = 4.0;
			Amount = amount;
		}



		public override void OnSingleClick( Mobile from )
		{
			if ( this.Amount > 1 )
			{
				LabelTo( from, this.Amount + " bales of cotton" );
			}
			else
			{
				LabelTo( from, "a bale of cotton" );
			}
		}

		public Cotton( Serial serial ) : base( serial )
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
			return base.Dupe( new Cotton(), amount );
		}

		public bool Dye( Mobile from, DyeTub sender )
		{
			if ( Deleted )
				return false;

			Hue = sender.DyedHue;

			return true;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				from.SendAsciiMessage( "What spinning wheel do you wish to spin this on?" );
				from.Target = new PickWheelTarget( this );
			}
			else
			{
				from.SendAsciiMessage( "That must be in your pack for you to use it." );
			}
		}

		public static void OnSpun( ISpinningWheel wheel, Mobile from, int hue )
		{
			Item item = new SpoolOfThread( 6 );
			item.Hue = hue;

			from.AddToBackpack( item );
			from.SendAsciiMessage( "You put the spools of thread in your backpack." );
		}

		private class PickWheelTarget : Target
		{
			private Cotton m_Cotton;

			public PickWheelTarget( Cotton cotton ) : base( 3, false, TargetFlags.None )
			{
				m_Cotton = cotton;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Cotton.Deleted )
					return;

				ISpinningWheel wheel = targeted as ISpinningWheel;

				if ( wheel == null && targeted is AddonComponent )
					wheel = ((AddonComponent)targeted).Addon as ISpinningWheel;

				if ( wheel is Item )
				{
					Item item = (Item)wheel;

					if ( !m_Cotton.IsChildOf( from.Backpack ) )
					{
						from.SendAsciiMessage( "That must be in your pack for you to use it." );
					}
					else if ( wheel.Spinning )
					{
						from.SendAsciiMessage( "That spinning wheel is being used." );
					}
					else
					{
						m_Cotton.Consume();
						wheel.BeginSpin( new SpinCallback( Cotton.OnSpun ), from, m_Cotton.Hue );
					}
				}
				else
				{
					from.SendAsciiMessage( "Use that on a spinning wheel." );
				}
			}
		}
	}
}
