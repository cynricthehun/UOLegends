using System; 
using Server.Items;

namespace Server.Items
{ 
public class Statstone : Item 
{ 
[Constructable] 
public Statstone() : base( 0xED4 ) 
{ 
Movable = false; 
Hue = 0; 
Name = "Mage Stat Stone"; 
} 

public override void OnDoubleClick( Mobile m ) 
{ 
m.Str = 100;
m.Int = 100;
m.Dex = 25;
} 

public Statstone( Serial serial ) : base( serial ) 
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