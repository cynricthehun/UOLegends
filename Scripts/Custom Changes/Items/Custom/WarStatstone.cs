using System; 
using Server.Items;

namespace Server.Items
{ 
public class Statstone2 : Item 
{ 
[Constructable] 
public Statstone2() : base( 0xED4 ) 
{ 
Movable = false; 
Hue = 0; 
Name = "Warrior Stat Stone"; 
} 

public override void OnDoubleClick( Mobile m ) 
{ 
m.Str = 100;
m.Int = 25;
m.Dex = 100;
} 

public Statstone2( Serial serial ) : base( serial ) 
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