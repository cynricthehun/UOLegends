using System;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class TribalPaint : Item
	{
		public override int LabelNumber{ get{ return 1040000; } } // savage kin paint

		[Constructable]
		public TribalPaint() : base( 0x9EC )
		{
			Hue = 2101;
			Weight = 2.0;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				LabelTo( from, "savage kin paint" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public TribalPaint( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				if ( !from.CanBeginAction( typeof( Spells.Fifth.IncognitoSpell ) ) )
				{
					from.SendAsciiMessage( "You cannot disguise yourself while incognitoed." );
				}
				else if ( !from.CanBeginAction( typeof( Spells.Seventh.PolymorphSpell ) ) )
				{
					from.SendAsciiMessage( "You cannot disguise yourself while polymorphed." );
				}
				else if ( Spells.Necromancy.TransformationSpell.UnderTransformation( from ) )
				{
					from.SendAsciiMessage( "You cannot disguise yourself while polymorphed." );
				}
				else if ( from.IsBodyMod || from.FindItemOnLayer( Layer.Helm ) is OrcishKinMask )
				{
					from.SendAsciiMessage( "You are already disguised." );
				}
				else
				{
					from.BodyMod = ( from.Female ? 184 : 183 );
					from.HueMod = 0;

					if ( from is PlayerMobile )
						((PlayerMobile)from).SavagePaintExpiration = TimeSpan.FromDays( 7.0 );

					from.SendAsciiMessage( "You now bear the markings of the savage tribe.  Your body paint will last about a week or you can remove it with an oil cloth." );

					Consume();
				}
			}
			else
			{
				from.SendAsciiMessage( "That must be in your pack for you to use it." );
			}
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
