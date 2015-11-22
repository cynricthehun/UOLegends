using System;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
	public class Flax : Item
	{
		[Constructable]
		public Flax() : this( 1 )
		{
		}

		[Constructable]
		public Flax( int amount ) : base( 0x1A9C )
		{
			Stackable = true;
			Weight = 1.0;
			Amount = amount;
		}



		public override void OnSingleClick( Mobile from )
		{
			if ( this.Amount > 1 )
			{
				LabelTo( from, this.Amount + " flax bundles" );
			}
			else
			{
				LabelTo( from, "a flax bundle" );
			}
		}

		public Flax( Serial serial ) : base( serial )
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
			return base.Dupe( new Flax(), amount );
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
			private Flax m_Flax;

			public PickWheelTarget( Flax flax ) : base( 3, false, TargetFlags.None )
			{
				m_Flax = flax;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Flax.Deleted )
					return;

				ISpinningWheel wheel = targeted as ISpinningWheel;

				if ( wheel == null && targeted is AddonComponent )
					wheel = ((AddonComponent)targeted).Addon as ISpinningWheel;

				if ( wheel is Item )
				{
					Item item = (Item)wheel;

					if ( !m_Flax.IsChildOf( from.Backpack ) )
					{
						from.SendAsciiMessage( "That must be in your pack for you to use it." );
					}
					else if ( wheel.Spinning )
					{
						from.SendAsciiMessage( "That spinning wheel is being used." );
					}
					else
					{
						m_Flax.Consume();
						wheel.BeginSpin( new SpinCallback( Flax.OnSpun ), from, m_Flax.Hue );
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
