using System;
using Server;

namespace Server.Items
{
	public class Static : Item
	{
		[Constructable]
		public Static( int itemID ) : base( itemID )
		{
			Movable = false;
		}

		[Constructable]
		public Static( int itemID, string name ) : base( itemID )
		{
			Movable = false;
			Name = name;
		}

		[Constructable]
		public Static( int itemID, int count ) : this( Utility.Random( itemID, count ) )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( this.ItemID >= 0x120E && this.ItemID <= 0x1216 )
					LabelTo( from, "an altar" );
				else if ( this.ItemID >= 0x1E36 && this.ItemID <= 0x1E5b )
					LabelTo( from, "a bearskin rug" );
				else if ( ( this.ItemID >= 0x1920 && this.ItemID <=0x1923 ) || ( this.ItemID >= 0x1925 && this.ItemID <= 0x1927 ) || ( this.ItemID >= 0x192C && this.ItemID <= 0x192F ) || ( this.ItemID >= 0x1931 && this.ItemID <= 0x1933 ) )
					LabelTo( from, "a flour mill" );
				else if ( ( this.ItemID >= 0x1928 && this.ItemID <= 0x192B ) || ( this.ItemID >= 0x1934 && this.ItemID <= 0x1937 ) || this.ItemID == 0x1924 || this.ItemID == 0x1930 )
					LabelTo( from, "a crank" );
				else if ( ( this.ItemID >= 0xFD9 && this.ItemID <= 0xFE0 ) )
					LabelTo( from, "a tapestry" );
				else if ( this.ItemID >= 0xFE6 && this.ItemID <= 0xFEE )
					LabelTo( from, "a pentagram" );
				else if ( this.ItemID >= 0x92B && this.ItemID <= 0x934 )
					LabelTo( from, "a stone oven" );
				else if ( ( this.ItemID >= 0x1069 && this.ItemID <= 0x106F ) || ( this.ItemID >= 0x107A && this.ItemID <= 0x1080 ) )
					LabelTo( from, "a stretched hide" );
				else if ( this.ItemID >= 0xB41 && this.ItemID <= 0xB44 )
					LabelTo( from, "a water trough" );
				else if ( ( this.ItemID >= 0x935 && this.ItemID <= 0x96C ) || ( this.ItemID >= 0x45D && this.ItemID <= 0x48E ) )
					LabelTo( from, "a fireplace" );
				else if ( ( this.ItemID >= 0x19C3 && this.ItemID <= 0x19EC ) || ( this.ItemID >= 0x1731 && this.ItemID <= 0x175A ) )
					LabelTo( from, "a fountain" );
				else if ( this.ItemID >= 0x1459 && this.ItemID <= 0x149A )
					LabelTo( from, "a telescope" );
				else if ( ( this.ItemID >= 0x14D7 && this.ItemID <= 0x14DF ) || ( this.ItemID >= 0x1550 && this.ItemID <= 0x1558 ) )
					LabelTo( from, "a vat" );
				else if ( this.ItemID == 0x1559 )
					LabelTo( from, "water" );
				else if ( ( this.ItemID >= 0x1771 && this.ItemID <= 0x177C ) || ( this.ItemID >= 0x1363 && this.ItemID <= 0x1366 ) || this.ItemID == 0x1368 || this.ItemID == 0x1369 || this.ItemID == 0x136B || this.ItemID == 0x136C )
					LabelTo( from, "a rock" );
				else if ( this.ItemID == 0x1367 || this.ItemID == 0x136A || this.ItemID == 0x136D )
					LabelTo( from, "rocks" );
				else if ( this.ItemID == 0xA92 || this.ItemID == 0xA93 )
					LabelTo( from, "a folded sheet" );
				else if ( this.ItemID == 0xB45 || this.ItemID == 0xB47 )
					LabelTo( from, "a large vase" );
				else if ( this.ItemID == 0xB46 || this.ItemID == 0xB48 )
					LabelTo( from, "a vase" );
				else if ( this.ItemID == 0xE20 || this.ItemID == 0xE22 )
					LabelTo( from, "a bloody bandage" );
				else if ( this.ItemID == 0xE23 )
					LabelTo( from, "bloody water" );
				else if ( this.ItemID >= 0x1370 && this.ItemID <= 0x1373 )
					LabelTo( from, "a brush" );
				else if ( this.ItemID >= 0x1EA8 && this.ItemID <= 0x1EAF )
					LabelTo( from, "a winch" );
				else if ( this.ItemID >= 0xA55 && this.ItemID <= 0xA59 )
					LabelTo( from, "a bedroll" );
				else if ( this.ItemID >= 0xA6C && this.ItemID <= 0xA6F )
					LabelTo( from, "a folded blanket" );
				else if ( this.ItemID >= 0xA92 && this.ItemID <= 0xA95 )
					LabelTo( from, "a folded sheet" );
				else if ( this.ItemID == 0x1397 || ( this.ItemID >=0x13A4 && this.ItemID <= 0x13AE ) || ( this.ItemID >=0x163A && this.ItemID <= 0x163C ) || this.ItemID == 0x1915 )
					LabelTo( from, "a pillow" );
				else if ( ( this.ItemID >= 0xA9F && this.ItemID <= 0xAA5 ) || ( this.ItemID >= 0xAFD && this.ItemID <= 0xB18 ) )
					LabelTo( from, "a display case" );
				else if ( this.ItemID >= 0x10AC && this.ItemID <= 0x10B1 )
					LabelTo( from, "a hanging pole" );
				else if ( this.ItemID >= 0xDE3 && this.ItemID <= 0xDE8 )
					LabelTo( from, "a campfire" );
				else if ( this.ItemID == 0xE21 || this.ItemID == 0xEE9 )
					LabelTo( from, "a clean bandage" );
				else if ( this.ItemID == 0xFAC )
					LabelTo( from, "a fire pit" );	
				else if ( this.ItemID == 0x1011 )
					LabelTo( from, "a keyring" );
				else if ( this.ItemID == 0x821 )
					LabelTo( from, "an iron gate" );
				else if ( this.ItemID >= 0xE5C && this.ItemID <= 0xE6A )
					LabelTo( from, "a glowing rune" );
				else if ( ( this.ItemID >= 0x134F && this.ItemID <= 0x1362 ) || ( this.ItemID >= 0x11B6 && this.ItemID <= 0x11BB ) )
					LabelTo( from, "a boulder" );
				else if ( ( this.ItemID >= 0x1586 && this.ItemID <= 0x15F5 ) || ( this.ItemID >= 0x160F && this.ItemID <= 0x1633 ) )
					LabelTo( from, "a banner" );
				else if ( this.ItemID >= 0x114F && this.ItemID <= 0x115E )
					LabelTo( from, "magical sparkles" );
				else if ( this.ItemID >= 0x3725 && this.ItemID <= 0x3739 )
					LabelTo( from, "a fizzle" );
				else if ( this.ItemID == 0x14E0 )
					LabelTo( from, "a bucket" );
				else if ( this.ItemID == 0xFFA )
					LabelTo( from, "a bucket of water" );
				else if ( this.ItemID == 0xDFC || this.ItemID == 0xDFD )
					LabelTo( from, "barber scissors" );
				else if ( this.ItemID == 0x1013 )
					LabelTo( from, "a rusty iron key" );
				else if ( this.ItemID >= 0x1810 && this.ItemID <= 0x181C )
					LabelTo( from, "an hourglass" );
				else if ( this.ItemID == 0x13F5	|| this.ItemID == 0x13F4 )
					LabelTo( from, "a crook" );
				else if ( this.ItemID >= 0x3789 && this.ItemID <= 0x379D )
					LabelTo( from, "a death vortex" );
				else if ( this.ItemID == 0x1865 || this.ItemID == 0x1866 )
					LabelTo( from, "a dishing stump" );
				else if ( this.ItemID == 0x1E9A || this.ItemID == 0x1E9B )
					LabelTo( from, "a hook" );
				else if ( this.ItemID == 0x1E9C || this.ItemID == 0x1E9D )
					LabelTo( from, "pullies" );
				else if ( this.ItemID == 0x1E9E || this.ItemID == 0x1E9F )
					LabelTo( from, "a pulley" );
				else if ( this.ItemID == 0x433 || this.ItemID == 0x434 )
					LabelTo( from, "a curtain rod" );
				else if ( this.ItemID >= 0x3490 && this.ItemID == 0x3493 )
					LabelTo( from, "a whirlpool" );
				else if ( this.ItemID >= 0x899 && this.ItemID <= 0x8A4 )
					LabelTo( from, "a wooden ladder" );
				else if ( this.ItemID >= 0x19AB && this.ItemID <= 0x19B6 )
					LabelTo( from, "fire" );
				/* walls */
				else if ( ( this.ItemID >= 0x8EB && this.ItemID <=0x8F2 ) || ( this.ItemID >= 0x8F7 && this.ItemID <= 0x8FC ) )
					LabelTo( from, "a stone rail" );
				else if ( this.ItemID >= 0x34ED && this.ItemID <= 0x3528 )
					LabelTo( from, "a waterfall" );
				else if ( ( this.ItemID >= 0x21A9 && this.ItemID <= 0x21AE ) || ( this.ItemID >= 0x21CD && this.ItemID <= 0x21DC ) || ( this.ItemID >= 0x226 && this.ItemID <= 0x228 ) || this.ItemID == 0x22A || this.ItemID == 0x22B || ( this.ItemID >= 0x334 && this.ItemID <= 0x353 ) || ( this.ItemID >= 0x6 && this.ItemID <= 0x8 ) || ( this.ItemID >= 0xA && this.ItemID <= 0xD ) || ( this.ItemID >= 0x10 && this.ItemID <= 0x12 ) || ( this.ItemID >= 0x14 && this.ItemID <= 0x16 ) || this.ItemID == 0x18 || this.ItemID == 0x19 || this.ItemID == 0xC1 || this.ItemID == 0xC2 || ( this.ItemID >= 0xA6 && this.ItemID <= 0xA8 ) || ( this.ItemID >= 0xAA && this.ItemID <= 0xB0 ) || ( this.ItemID >= 0xB2 && this.ItemID <= 0xB8 ) || ( this.ItemID >= 0xBD && this.ItemID <= 0xBF ) )
					LabelTo( from, "a wooden wall" );
				else if ( ( this.ItemID >= 0x90 && this.ItemID <= 0x92 ) || ( this.ItemID >= 0x94 && this.ItemID <= 0x97 ) || ( this.ItemID >= 0x9A && this.ItemID <= 0x9C ) || ( this.ItemID >= 0x9E && this.ItemID <= 0xA5 ) || ( this.ItemID >= 0x21B1 && this.ItemID <= 0x21B8 ) || ( this.ItemID >= 0x21BD && this.ItemID <= 0x21CC ) )
					LabelTo( from, "a log wall" );
				else if ( this.ItemID == 0x93 || this.ItemID == 0x9D || ( this.ItemID >= 0x145 && this.ItemID <= 0x148 ) )
					LabelTo( from, "a log post" );
				else if ( ( this.ItemID >= 0x3B3 && this.ItemID <= 0x3B5 ) || ( this.ItemID >= 0x835 && this.ItemID <= 0x837 ) || ( this.ItemID >= 0x85C && this.ItemID <= 0x866 ) || this.ItemID == 0x878 || this.ItemID == 0x87A || this.ItemID == 0x88A || this.ItemID == 0x88B || ( this.ItemID >= 0x21D1 && this.ItemID <= 0x21D4 ) )
					LabelTo( from, "a wooden fence" );
				else if ( ( this.ItemID >= 0x21D && this.ItemID <= 0x223 ) || ( this.ItemID >= 0x421 && this.ItemID <= 0x425 ) || ( this.ItemID >= 0x2188 && this.ItemID <= 0x2197 ) || ( this.ItemID >= 0x21A5 && this.ItemID <= 0x21A8 ) || this.ItemID == 0x430 || this.ItemID == 0x431 )
					LabelTo( from, "a palisade wall" );
				/* gy */
				else if ( this.ItemID == 0xED3 || this.ItemID == 0xEE8 || ( this.ItemID >= 0xEDF && this.ItemID <= 0xEE2 ) )
					LabelTo( from, "a grave" );
				else if ( ( this.ItemID >= 0xED4 && this.ItemID <= 0xEDE ) || ( this.ItemID >= 0x1165 && this.ItemID <= 0x1184 ) )
					LabelTo( from, "a gravestone" );
				/* plants */
				else if ( this.ItemID >= 0xCF3 && this.ItemID <= 0xCF7 )
					LabelTo( from, "a fallen log" );
				else if ( this.ItemID == 0xCC8 )
					LabelTo( from, "a juniper bush" );
				else if ( this.ItemID >= 0xE56 && this.ItemID <= 0xE59 )
					LabelTo( from, "a stump" );
				else if ( this.ItemID == 0x1B7E )
					LabelTo( from, "a tuscany pine" );
				else if ( this.ItemID == 0xC85 || this.ItemID == 0xCC0 || this.ItemID == 0xCC1 )
					LabelTo( from, "orfluer flowers" );
				else if ( this.ItemID == 0xC95 )
					LabelTo( from, "a coconut palm" );
				else if ( this.ItemID == 0xC8F || this.ItemID == 0xC90 || this.ItemID == 0xDB8 || this.ItemID == 0xDB9 )
					LabelTo( from, "a hedge" );
				else if ( this.ItemID == 0xC91 || this.ItemID == 0xC92 )
					LabelTo( from, "an untrimmed hedge" );
				else if ( this.ItemID >= 0xC99 && this.ItemID <= 0xC9D )
					LabelTo( from, "a small palm" );
				else if ( this.ItemID >= 0x1B1F && this.ItemID <= 0x1B26 )
					LabelTo( from, "leaves" );
				else if ( this.ItemID >= 0xD06 && this.ItemID <= 0xD0A )
					LabelTo( from, "lilypads" );
				else if ( this.ItemID == 0xD0B )
					LabelTo( from, "lilypads" );
				else if ( ( this.ItemID >= 0x911 && this.ItemID <= 0x914 ) || ( this.ItemID >= 0x1B27 && this.ItemID <= 0x1B3E ) || ( this.ItemID >= 0x1DFD && this.ItemID <= 0x1E00 ) )
					LabelTo( from, "dirt" );
				else if ( this.ItemID == 0x1E02 || this.ItemID == 0x1E02 )
					LabelTo( from, "mud" );
				else if ( this.ItemID == 0xCF8 || this.ItemID == 0xCFB || this.ItemID == 0xCFE || this.ItemID == 0xD01 )
					LabelTo( from, "a cypress tree" );
				else if ( ( this.ItemID >= 0xD25 && this.ItemID <= 0xD28 ) || this.ItemID == 0xD2A || this.ItemID == 0xD2C || this.ItemID == 0xD2E || this.ItemID == 0xD35 )
					LabelTo( from, "a cactus" );
				else if ( this.ItemID == 0xD9C || this.ItemID == 0xD9E || this.ItemID == 0xDA0 || this.ItemID == 0xDA2 )
					LabelTo( from, "a peach tree" );
				else if ( this.ItemID == 0xDA4 || this.ItemID == 0xDA6 || this.ItemID == 0xDA8 || this.ItemID == 0xDAA )
					LabelTo( from, "a pear tree" );
				else if ( this.ItemID == 0xD94 || this.ItemID == 0xD96 || this.ItemID == 0xD98 || this.ItemID == 0xD9A )
					LabelTo( from, "an apple tree" );
				else if ( this.ItemID == 0xD30 || this.ItemID == 0xD31 )
					LabelTo( from, "a century plant" );
				else if ( this.ItemID >= 0xCEB && this.ItemID <= 0xCF2 )
					LabelTo( from, "vines" );
				else if ( ( this.ItemID >= 0xCAB && this.ItemID <= 0xCB6  ) || ( this.ItemID >=0xCB9 && this.ItemID <= 0xCBD ) || this.ItemID == 0xCC5 || this.ItemID == 0xCC6 || this.ItemID == 0xD32 || this.ItemID == 0xD33 )
					LabelTo( from, "grasses" );
				/* rares */
				else if ( this.ItemID >= 0xE48 && this.ItemID <= 0xE4B )
					LabelTo( from, "full jars" );
				else if ( this.ItemID == 0x1006 )
					LabelTo( from, "a full jar" );
				else if ( this.ItemID >= 0xE44 && this.ItemID <= 0xE47 )
					LabelTo( from, "empty jars" );
				else if ( this.ItemID == 0x1005 )
					LabelTo( from, "an empty jar" );
				else if ( this.ItemID == 0x1007 )
					LabelTo( from, "a half empty jar" );
				/* blood */
				else if ( ( this.ItemID >= 0x1CF1 && this.ItemID <= 0x1D12 ) || ( this.ItemID >= 0x122A && this.ItemID <= 0x122E ) || ( this.ItemID >= 0x1CC7 && this.ItemID <= 0x1CDC ) || ( this.ItemID >= 0x1D92 && this.ItemID <= 0x1D96 ) || this.ItemID == 0x1645 )
					LabelTo( from, "blood" );
				else if ( this.ItemID == 0x122F )
					LabelTo( from, "a blood smear" );
				/* ankh */
				else if ( this.ItemID >= 0x2 && this.ItemID <= 0x5 )
					LabelTo( from, "an ankh" );
				/* garbage */
				else if ( this.ItemID >= 0x10EE && this.ItemID <= 0x10F3 )
					LabelTo( from, "garbage" );
				/* orc */
				else if ( this.ItemID == 0x41F || this.ItemID == 0x420 || this.ItemID == 0x428 || this.ItemID == 0x429 )
					LabelTo( from, "a gruesome standard" );
				/* lights */
				else if ( ( this.ItemID >= 0xA0F && this.ItemID <= 0xA11 ) || this.ItemID == 0xA26 || this.ItemID == 0xA28 || ( this.ItemID >= 0xB1A && this.ItemID <= 0xB1C ) || ( this.ItemID >= 0x142C && this.ItemID <= 0x1437 ) )
					LabelTo( from, "a candle" );
				else if ( ( this.ItemID >= 0xE31 && this.ItemID == 0xE33 ) || this.ItemID == 0x19AA || this.ItemID == 0x19BB )
					LabelTo( from, "a brazier" );
				else if ( ( this.ItemID >= 0xA12 && this.ItemID <= 0xa14 ) || this.ItemID == 0xF64 || this.ItemID == 0xF6B )
					LabelTo( from, "a torch" );
				else if ( ( this.ItemID >= 0xB1D && this.ItemID <= 0xB1F ) || ( this.ItemID >= 0xB26 && this.ItemID <= 0xB28 ) )
					LabelTo( from, "a candelabra" );
				else if ( this.ItemID >= 0x1849 && this.ItemID <= 0x1850 )
					LabelTo( from, "a heating stand" );
				else if ( this.ItemID >= 0x1853 && this.ItemID <= 0x185A )
					LabelTo( from, "a skull with candle" );
				else if ( this.ItemID == 0xA05 || ( this.ItemID >= 0xA07 && this.ItemID <= 0xA0A ) || ( this.ItemID >= 0xA0C && this.ItemID <= 0xA0E ) )
					LabelTo( from, "a wall torch" );
				else if ( this.ItemID == 0x9FB || ( this.ItemID >= 0x9FD && this.ItemID <= 0xA00 ) || ( this.ItemID >= 0xA02 && this.ItemID <= 0xA04 ) )
					LabelTo( from, "a wall torch" );
				else if ( ( this.ItemID >= 0xA15 && this.ItemID <= 0xA18 ) || ( this.ItemID >= 0xA22 && this.ItemID <= 0xA25 ) )
					LabelTo( from, "a lantern" );
				else if ( this.ItemID >= 0xA1A && this.ItemID <= 0xA1D )
					LabelTo( from, "a hanging lantern" );
				/* clothes */
				else if ( this.ItemID >= 0x1549 && this.ItemID <= 0x154C )
					LabelTo( from, "a tribal mask" );
				else if ( this.ItemID == 0x1535 || this.ItemID == 0x1536 )
					LabelTo( from, "a gold belt" );
				else if ( this.ItemID == 0x1712 || this.ItemID == 0x1711 )
					LabelTo( from, "thigh boots" );
				else if ( this.ItemID == 0x1718 )
					LabelTo( from, "a wizard's hat" );
				else if ( this.ItemID == 0x171B )
					LabelTo( from, "a tricorne hat" );
				else if ( this.ItemID == 0x1517 || this.ItemID == 0x1518 )
					LabelTo( from, "a shirt" );
				else if ( this.ItemID == 0x170F || this.ItemID == 0x1710 )
					LabelTo( from, "shoes" );
				else if ( this.ItemID == 0x1547 || this.ItemID == 0x1548 )
					LabelTo( from, "a deer mask" );
				else if ( this.ItemID == 0x171A )
					LabelTo( from, "a feathered hat" );
				else if ( this.ItemID == 0x1516 || this.ItemID == 0x1531 )
					LabelTo( from, "a skirt" );
				else if ( this.ItemID == 0x1539 || this.ItemID == 0x153A )
					LabelTo( from, "long pants" );
				/* docks */
				else if ( ( this.ItemID >= 0x3A6 && this.ItemID <= 0x3A9 ) || this.ItemID == 0x1EA0 || this.ItemID == 0x1EA1 || this.ItemID == 0x1EA2 || this.ItemID == 0x14F8 || this.ItemID == 0x14FA )
					LabelTo( from, "a rope" );
				else if ( this.ItemID == 0x154D )
					LabelTo( from, "a water barrel" );
				else if ( this.ItemID >= 0xDD6 && this.ItemID <= 0xDD9 )
					LabelTo( from, "small fish" );
				/* smith */
				else if ( this.ItemID == 0x1BF6 || this.ItemID == 0x1BF7 || this.ItemID == 0x1BF9 || this.ItemID == 0x1BFA )
					LabelTo( from, "silver ingots" );
				else if ( this.ItemID == 0x1BF5 || this.ItemID == 0x1BF8 )
					LabelTo( from, "a silver ingot" );
				else if ( this.ItemID == 0x1BE4 || this.ItemID == 0x1BE5 || this.ItemID == 0x1BE7 || this.ItemID == 0x1BE8 )
					LabelTo( from, "copper ingots" );
				else if ( this.ItemID == 0x1BE3 || this.ItemID == 0x1BE6 )
					LabelTo( from, "a copper ingot" );
				else if ( this.ItemID == 0x1BEA || this.ItemID == 0x1BEB || this.ItemID == 0x1BED || this.ItemID == 0x1BEE )
					LabelTo( from, "gold ingots" );
				else if ( this.ItemID == 0x1BE9 || this.ItemID == 0x1BEC )
					LabelTo( from, "a gold ingot" );
				else if ( this.ItemID == 0x1BF0 || this.ItemID == 0x1BF1 || this.ItemID == 0x1BF3 || this.ItemID == 0x1BF4 )
					LabelTo( from, "iron ingots" );
				else if ( this.ItemID == 0x1BEF || this.ItemID == 0x1BF2 )
					LabelTo( from, "an iron ingot" );
				else if ( this.ItemID == 0x1876 )
					LabelTo( from, "iron wire" );
				else if ( this.ItemID == 0x1877 )
					LabelTo( from, "silver wire" );
				else if ( this.ItemID == 0x1878 )
					LabelTo( from, "gold wire" );
				else if ( this.ItemID == 0x1879 )
					LabelTo( from, "copper wire" );
				else if ( this.ItemID == 0xFB7 || this.ItemID == 0xFB8 )
					LabelTo( from, "forged metal" );
				else if ( this.ItemID == 0xFB9 || this.ItemID == 0xFBC )
					LabelTo( from, "tongs" );
				/* stable */
				else if ( this.ItemID == 0xF3B || this.ItemID == 0xF3C )
					LabelTo( from, "horse dung" );
				else if ( this.ItemID == 0x1374 || this.ItemID == 0x1375 )
					LabelTo( from, "a bridle" );
				else if ( this.ItemID == 0xFB6 )
					LabelTo( from, "horse shoes" );
				else if ( this.ItemID == 0xE7B )
					LabelTo( from, "a water tub" );
				else if ( this.ItemID == 0xE83 )
					LabelTo( from, "an empty tub" );
				else if ( this.ItemID == 0xF37 || this.ItemID == 0xF38 )
					LabelTo( from, "a saddle" );
				/* library */
				else if ( this.ItemID == 0x1851 || this.ItemID == 0x1852 )
					LabelTo( from, "scales" );
				else if ( this.ItemID == 0x1047 || this.ItemID == 0x1048 )
					LabelTo( from, "a globe" );
				else if ( this.ItemID == 0xFBF || this.ItemID == 0xFC0 )
					LabelTo( from, "pen and ink" );
				else if ( this.ItemID == 0xC16 )
					LabelTo( from, "ruined books" );
				else if ( this.ItemID == 0xFBD || this.ItemID == 0xFBE || ( this.ItemID >= 0xFEF && this.ItemID <= 0xFF4 ) || this.ItemID == 0x1E20 )
					LabelTo( from, "a book" );
				else if ( this.ItemID >= 0x1E21 && this.ItemID <= 0x1E25 )
					LabelTo( from, "books" );
				/* tavern */
				else if ( this.ItemID >= 0x1F8D && this.ItemID <= 0x1F90 )
					LabelTo( from, "a glass of wine" );
				else if ( this.ItemID >= 0x1F91 && this.ItemID <= 0x1F94 )
					LabelTo( from, "a glass of water" );
				else if ( this.ItemID == 0xFF8 || this.ItemID == 0xFF9 )
					LabelTo( from, "a pitcher of water" );
				else if ( this.ItemID == 0x1F9B || this.ItemID == 0x1F9C )
					LabelTo( from, "a pitcher of wine" );
				else if ( this.ItemID >= 0x1F81 && this.ItemID <= 0x1F84 )
					LabelTo( from, "a glass mug" );
				else if ( this.ItemID == 0x99A || this.ItemID == 0x9B3 || this.ItemID == 0x9BF || this.ItemID == 0x9CB )
					LabelTo( from, "a goblet" );
				else if ( this.ItemID == 0x1003 )
					LabelTo( from, "a spittoon" );
				else if ( this.ItemID >= 0xFFB && this.ItemID <= 0xFFE )
					LabelTo( from, "a skull mug" );
				else if ( this.ItemID == 0x98E || this.ItemID == 0x98D )
					LabelTo( from, "jugs of cider" );
				else if ( this.ItemID == 0x991 || this.ItemID == 0x992 )
					LabelTo( from, "a tray" );
				else if ( this.ItemID >= 0x9A0 && this.ItemID <= 0x9A2 )
					LabelTo( from, "bottles of ale" );
				else if ( this.ItemID == 0x98D || this.ItemID == 0x98E )
					LabelTo( from, "bottles of cider" );
				else if ( this.ItemID >= 0x9C4 && this.ItemID <= 0x9C6 )
					LabelTo( from, "bottles of wine" );
				else if ( this.ItemID >= 0x99C && this.ItemID <= 0x99E )
					LabelTo( from, "bottles of liquor" );
				else if ( ( this.ItemID >= 0xE25 && this.ItemID <= 0xE2C ) || ( this.ItemID >= 0xEFB && this.ItemID <= 0xF04 ) )
					LabelTo( from, "a bottle" );
				/* games */
				else if ( this.ItemID == 0xFA8 )
					LabelTo( from, "chess pieces" );
				else if ( this.ItemID == 0xFA2 || this.ItemID == 0xFA3 )
					LabelTo( from, "playing cards" );
				else if ( this.ItemID >= 0xE12 && this.ItemID <= 0xE14 ) 
					LabelTo( from, "chessmen" );
				else if ( this.ItemID >= 0xE15 && this.ItemID <= 0xE19 )
					LabelTo( from, "cards" );
				else if ( this.ItemID == 0xE1A || this.ItemID == 0xE1B || this.ItemID == 0xFA4 || this.ItemID == 0xFA5 )
					LabelTo( from, "checkers" );
				/* thief */
				else if ( this.ItemID == 0x14FB || this.ItemID == 0x14FC )
					LabelTo( from, "a lockpick" );
				else if ( this.ItemID == 0x14FD || this.ItemID == 0x14FE )
					LabelTo( from, "lockpicks" );
				/* broken furniture */
				else if ( this.ItemID == 0xC17 || this.ItemID == 0xC18 )
					LabelTo( from , "a covered chair" );
				else if ( this.ItemID >= 0xC31 && this.ItemID <= 0xC36 )
					LabelTo( from, "sheets" );
				else if ( this.ItemID == 0xC12 || this.ItemID == 0xC13 )
					LabelTo( from, "a broken armoire" );
				else if ( this.ItemID == 0xC16 )
					LabelTo( from, "damaged books" );
				else if ( this.ItemID == 0xC24 || this.ItemID == 0xC25 )
					LabelTo( from, "broken furniture" );
				else if ( this.ItemID == 0xC14 || this.ItemID == 0xC15 )
					LabelTo( from, "a ruined bookcase" );
				else if ( this.ItemID == 0xC2C )
					LabelTo( from, "a ruined painting" );
				else if ( this.ItemID == 0x426 || this.ItemID == 0x427 || ( this.ItemID >= 0x42A && this.ItemID <= 0x42F ) )
					LabelTo( from, "a tattered banner" );
				else if ( this.ItemID >= 0xC2D && this.ItemID <= 0xC30 )
					LabelTo( from, "debris" );
				else if ( this.ItemID == 0xC10 || this.ItemID == 0xC11 || ( this.ItemID >= 0xC19 && this.ItemID <= 0xC1E ) )
					LabelTo( from, "a broken chair" );
				/* body parts */
				else if ( this.ItemID >= 0x1AE0 && this.ItemID <= 0x1AE4 )
					LabelTo( from, "a skull" );
				else if ( this.ItemID == 0x1CDE || this.ItemID == 0x1CE6 || ( this.ItemID >= 0x1D13 && this.ItemID <= 0x1D4B ) )
					LabelTo( from, "a body" );
				else if ( this.ItemID == 0x1CE0 || this.ItemID == 0x1CE8 || this.ItemID == 0x1D9F )
					LabelTo( from, "a torso" );
				else if ( this.ItemID == 0x1CF0 )
					LabelTo( from, "a brain" );
				else if ( this.ItemID == 0x1CEF )
					LabelTo( from, "entrails" );	
				else if ( this.ItemID == 0x1CDD || this.ItemID == 0x1CE5 )
					LabelTo( from, "an arm" ); 
				else if ( this.ItemID == 0x1CE3 || this.ItemID == 0x1CEA )
					LabelTo( from, "a body part" );
				else if ( this.ItemID == 0x1CDF || this.ItemID == 0x1CE4 || this.ItemID == 0x1CE7 || this.ItemID ==0x1CEB )
					LabelTo( from, "legs" );
				else if ( this.ItemID == 0x1CE2 || this.ItemID == 0x1CEC )
					LabelTo( from, "a leg" );
				else if ( this.ItemID == 0x1CED )
					LabelTo( from, "a heart" );	
				else if ( this.ItemID == 0x1CE1 || this.ItemID == 0x1CE9 || this.ItemID == 0x1DA0 || this.ItemID == 0x1DAE )
					LabelTo( from, "a head" );
				/* dungeon */
				else if ( this.ItemID >= 0x1AD8 && this.ItemID <= 0x1ADF )
					LabelTo( from, "a pile of skulls" );
				else if ( this.ItemID >= 0x1B9f && this.ItemID <= 0x1BBD )
					LabelTo( from, "refuse" );
				else if ( this.ItemID == 0x1B1D || this.ItemID == 0x1B1E )
					LabelTo( from, "a skeleton with meant" );
				else if ( ( this.ItemID >= 0xEE3 && this.ItemID <= 0xEE6 ) || ( this.ItemID >= 0x10B8 && this.ItemID <= 0x10D1 ) || this.ItemID == 0x10DD )
					LabelTo( from, "a spiderweb" );
				else if ( this.ItemID >= 0x10D2 && this.ItemID <= 0x10D7 )
					LabelTo( from, "a small spiderweb" );
				else if ( this.ItemID == 0x10D8 )
					LabelTo( from, "an egg case web" );
				else if ( this.ItemID >= 0x10DA && this.ItemID <= 0x10DC )
					LabelTo( from, "a cocoon" );
				else if ( this.ItemID == 0x10D9 )
					LabelTo( from, "an egg case" );
				else if ( this.ItemID >= 0x112B && this.ItemID <= 0x1132 )
					LabelTo( from, "a fitting" );
				else if ( ( this.ItemID >= 0xA01 && this.ItemID <= 0xA0E ) || this.ItemID == 0x1B7C || this.ItemID == 0x1B7D || this.ItemID == 0x1B7F || this.ItemID == 0x1B80 || ( this.ItemID >= 0x1D4C && this.ItemID <= 0x1D89 ) || ( this.ItemID >= 0x1D8E && this.ItemID <= 0x1D91 ) )
					LabelTo( from, "a skeleton" );
				else if ( ( this.ItemID >= 0xD0C && this.ItemID <= 0xD19 ) || ( this.ItemID >= 0x1125 && this.ItemID <= 0x112B ) )
					LabelTo( from, "a mushroom" );
				else if ( this.ItemID == 0xF7E || this.ItemID == 0x1B11 || this.ItemID == 0x1B12 )
					LabelTo( from, "a bone" );
				else if ( this.ItemID >= 0x1B09 && this.ItemID <= 0x1B10 )
					LabelTo( from, "a pile of bones" );
				else if ( this.ItemID == 0x1B13 || this.ItemID == 0x1B14 )
					LabelTo( from, "a jaw bone" );
				else if ( this.ItemID == 0x1B15 || this.ItemID == 0x1B16 )
					LabelTo( from, "a pelvis bone" );
				else if ( this.ItemID == 0x1B17 || this.ItemID == 0x1B18 )
					LabelTo( from, "a rib cage" );
				else if ( this.ItemID == 0x1B1B || this.ItemID == 0x1B1C )
					LabelTo( from, "a spine" );
				else if ( ( this.ItemID >= 0x1224 && this.ItemID <= 0x1228 ) || ( this.ItemID >= 0x129F && this.ItemID <= 0x12A4 ) || ( this.ItemID >= 0x12AE && this.ItemID <= 0x12B0 ) || ( this.ItemID >= 0x12D5 && this.ItemID <= 0x12D9 ) || ( this.ItemID >= 0x139A && this.ItemID <= 0x13A3 ) || ( this.ItemID >= 0x2A9E && this.ItemID <= 0x2AA0 ) )
					LabelTo( from, "a statue" );
				else if ( this.ItemID >= 0x126B && this.ItemID <= 0x127C )
					LabelTo( from, "a rack" );
				else if ( this.ItemID >= 0x108F && this.ItemID <= 0x1092 )
					LabelTo( from, "a switch" );
				else if ( ( this.ItemID >= 0x108C && this.ItemID <= 0x108E ) || ( this.ItemID >= 0x1093 && this.ItemID <= 0x1095 ) )
					LabelTo( from, "a lever" );
				else if ( this.ItemID >= 0x124E && this.ItemID <= 0x125D )
					LabelTo( from, "a rack" );
				else if ( this.ItemID == 0x1262 || this.ItemID == 0x1263 || this.ItemID == 0x1640 )
					LabelTo( from, "shackles" );
				else if ( this.ItemID == 0x1B19 || this.ItemID == 0x1B1A )
					LabelTo( from, "bone shards" );
				else if ( this.ItemID >= 0xECA && this.ItemID <= 0xED2 )
					LabelTo( from, "bones" );
				else if ( this.ItemID == 0x9DC || this.ItemID == 0x9DD || this.ItemID == 0x9DF || this.ItemID == 0x9E6 || this.ItemID == 0x9E7 )
					LabelTo( from, "a dirty pot" );
				else if ( this.ItemID == 0x9E8 )
					LabelTo( from, "a dirty pan" );
				else if ( this.ItemID == 0x9AE || this.ItemID == 0x9DA || this.ItemID == 0xA19 )
					LabelTo( from, "a dirty plate" );
				else if ( this.ItemID == 0x9DE )
					LabelTo( from, "a dirty frypan" );
				else if ( this.ItemID >= 0x1249 && this.ItemID <= 0x124D )
					LabelTo( from, "an iron maiden" );
				else if ( this.ItemID == 0x1230 || ( this.ItemID >= 0x1245 && this.ItemID <= 0x1247 ) || ( this.ItemID >= 0x125E && this.ItemID <= 0x1261 ) || this.ItemID == 0x1268 || this.ItemID == 0x1269 )
					LabelTo( from, "a guillotine" );
				else if ( this.ItemID >= 0x11E6 && this.ItemID <= 0x11E9 )
					LabelTo( from, "a woven mat" );
				else if ( this.ItemID == 0x11EA || this.ItemID == 0x11EB )
					LabelTo( from, "a straw pillow" );
				else if ( this.ItemID >= 0x181D && this.ItemID <= 0x1828 )
					LabelTo( from, "an alchemical symbol" );
				/* furniture */
				else if ( ( this.ItemID >= 0xB4E && this.ItemID <= 0xB5D ) || this.ItemID == 0x1E6F || this.ItemID ==0x1E78 )
					LabelTo( from, "a chair" );
				else if ( ( this.ItemID >= 0x1218 && this.ItemID <= 0x121B ) )
					LabelTo( from, "a stone chair" );
				else if ( ( this.ItemID >= 0x1201 && this.ItemID <= 0x1206 ) )
					LabelTo( from, "a stone table" );
				else if ( ( this.ItemID >= 0x11FD && this.ItemID <= 0x1200 ) )
					LabelTo( from, "a cot" );
				else if ( ( this.ItemID >= 0xA5A && this.ItemID <= 0xA91 ) )
					LabelTo( from, "a bed" );
				else if ( this.ItemID >= 0x11DC && this.ItemID <= 0x11E1 )
					LabelTo( from, "a log table" );
				else if ( this.ItemID >= 0xB3D && this.ItemID <= 0xB40 )
					LabelTo( from, "a counter" );
				else if ( this.ItemID == 0xA2A || this.ItemID == 0xA2B || this.ItemID == 0x1E7F )
					LabelTo( from, "a stool" );
				else if ( this.ItemID == 0xA3C || this.ItemID == 0xA3D || this.ItemID == 0xA44 || this.ItemID == 0xA45 )
					LabelTo( from, "a dresser" );
				else if ( ( this.ItemID >= 0xB6B && this.ItemID <= 0xB90 ) || ( this.ItemID >= 0xB34 && this.ItemID <= 0xB3C ) || ( this.ItemID >= 0x118B && this.ItemID <= 0x1192 ) || ( this.ItemID >= 0x1667 && this.ItemID <= 0x166C ) || ( this.ItemID >= 0x1DA5 && this.ItemID <= 0x1DAC ) || this.ItemID == 0x1E72 || this.ItemID == 0x1E7B )
					LabelTo( from, "a table" );
				else if ( ( this.ItemID >= 0xB5F && this.ItemID <= 0xB6A ) || ( this.ItemID >= 0xB91 && this.ItemID <= 0xB94 ) )
					LabelTo( from, "a bench" );
				/* mining */
				else if ( this.ItemID == 0xF39 || this.ItemID == 0xF3A )
					LabelTo( from, "a shovel" );
				else if ( this.ItemID >= 0x1A82 && this.ItemID <= 0x1A8B )
					LabelTo( from, "an ore cart" );
				else if ( this.ItemID >= 0x19B7 && this.ItemID <= 0x19BA )
					LabelTo( from, "iron ore" );
				/* forges */
				else if ( this.ItemID == 0xFB1 || ( this.ItemID >= 0x197E && this.ItemID <= 0x1985 ) || ( this.ItemID >= 0x198A && this.ItemID <= 0x1991 ) || ( this.ItemID >= 0x1996 && this.ItemID <= 0x199D ) || ( this.ItemID >= 0x19A2 && this.ItemID <= 0x19A9 ) )
					LabelTo( from, "a forge" );
				else if ( ( this.ItemID >= 0x179A && this.ItemID <= 0x179D ) || ( this.ItemID >= 0x1986 && this.ItemID <= 0x1989 ) || ( this.ItemID >= 0x1992 && this.ItemID <= 0x1995 ) || ( this.ItemID >= 0x199E && this.ItemID <= 0x19A1 ) )
					LabelTo( from, "bellows" );
				/* food */
				else if ( this.ItemID == 0xC5C || this.ItemID == 0xC5D )
					LabelTo( from, "a watermelon" );	
				else if ( this.ItemID == 0x103B || this.ItemID == 0x103C )
					LabelTo( from, "a bread loaf" );
				else if ( this.ItemID == 0x9BB || this.ItemID == 0x9BC )
					LabelTo( from, "a roast pig" );
				else if ( this.ItemID == 0xC77 || this.ItemID == 0xC78 )
					LabelTo( from, "a carrot" );
				else if ( this.ItemID == 0x9C9 || this.ItemID == 0x9D3 )
					LabelTo( from, "a ham" );
				else if ( this.ItemID == 0x160B )
					LabelTo( from, "a pan of cookies" );
				else if ( this.ItemID == 0x1040 )
					LabelTo( from, "a pizza" );
				else if ( this.ItemID == 0x1727 )
					LabelTo( from, "a bunch of dates" );
				else if ( this.ItemID >= 0x171D && this.ItemID <= 0x1720 )
					LabelTo( from, "a banana" );
				else if ( this.ItemID == 0x1721 || this.ItemID == 0x1722 )
					LabelTo( from, "a bunch of bananas" );
				else if ( this.ItemID >= 0x97C && this.ItemID <= 0x97D )
					LabelTo( from, "a wedge of cheese" );
				else if ( this.ItemID == 0x97E )
					LabelTo( from, "a wheel of cheese" );
				else if ( this.ItemID == 0x9D0 )
					LabelTo( from, "an apple" );
				else if ( this.ItemID == 0x9D2 )
					LabelTo( from, "a peach" );
				else if ( this.ItemID == 0x944 )
					LabelTo( from, "a peach" );
				else if ( this.ItemID >= 0x1723 && this.ItemID <= 0x1726 )
					LabelTo( from, "coconut" );
				else if ( this.ItemID == 0x9AF || this.ItemID == 0x9D8 || this.ItemID == 0x9D9 || this.ItemID == 0x9DB )
					LabelTo( from, "a plate of food" );
				else if ( this.ItemID == 0x976 || this.ItemID == 0x977 )
					LabelTo(from, "a slab of bacon" );
				/* cooking */
				else if ( this.ItemID == 0x974 || this.ItemID == 0x975 )
					LabelTo( from, "a cauldron" );
				else if ( this.ItemID == 0x9B9 || this.ItemID == 0x9BA )
					LabelTo( from, "a raw bird" );
				else if ( this.ItemID == 0x103E )
					LabelTo( from, "a flour sifter" );
				else if ( this.ItemID >= 0x187E && this.ItemID <= 0x1881 )
					LabelTo( from, "spilled flour" );
				else if ( this.ItemID == 0x9F1 )
					LabelTo( from, "a cut of raw ribs" );
				else if ( this.ItemID == 0x974 || this.ItemID == 0x975 )
					LabelTo( from, "a caulron" );
				else if ( this.ItemID == 0x9BD || this.ItemID == 0x9BE || this.ItemID == 0x9D4 || this.ItemID == 0x9D4 )
					LabelTo( from, "silverware" );
				else if ( this.ItemID == 0x9A7 || this.ItemID == 0xFF6 || this.ItemID == 0xFF7 )
					LabelTo( from, "a glass pitcher" );
				else if ( this.ItemID == 0x9D6 )
					LabelTo( from, "a pitcher" );
				else if ( this.ItemID == 0x9D7 )
					LabelTo( from, "a plate" );
				else if ( this.ItemID == 0x15F8 )
					LabelTo( from, "a wooden bowl" );
				else if ( this.ItemID == 0x15FD )
					LabelTo( from, "a pewter bowl" );
				else if ( this.ItemID == 0x1603 )
					LabelTo( from, "a large pewter bowl" );
				else if ( this.ItemID == 0x1605 )
					LabelTo( from, "a large wooden bowl" );
				else if ( this.ItemID == 0x15FB || this.ItemID == 0x1600 )
					LabelTo( from, "a bowl of lettuce" );
				else if ( this.ItemID == 0x103D )
					LabelTo( from, "dough" );
				else if ( this.ItemID == 0x103F )
					LabelTo( from, "cookie mix" );
				else if ( this.ItemID == 0x1043	)
					LabelTo( from, "a rolling pin" );
				else if ( this.ItemID == 0x1039 || this.ItemID == 0x1045 )
					LabelTo( from, "a sack of flour" );
				else if ( this.ItemID == 0x103A || this.ItemID == 0x1046 )
					LabelTo( from, "an open sack of flour" );
				else if ( this.ItemID == 0x9E0 || this.ItemID == 0x9E1 || this.ItemID == 0x9E3 || this.ItemID == 0x9E4 || this.ItemID == 0x9E5 )
					LabelTo( from, "a pot" );
				else if ( this.ItemID == 0x97F || this.ItemID == 0x9E2 )
					LabelTo( from, "a frypan" );
				else if ( this.ItemID == 0x9ED )
					LabelTo( from, "a kettle" );
				else if ( this.ItemID == 0x9F3 )
					LabelTo( from, "a pan" );
				else if ( this.ItemID == 0xA1E )
					LabelTo( from, "a bowl of flour" );
				/* tanner */
				else if ( this.ItemID >= 0x11F4 && this.ItemID <= 0x11FB )
					LabelTo( from, "a fur" );
				else if ( this.ItemID == 0x1078 || this.ItemID == 0x1079 )
					LabelTo( from, "a pile of hides" );
				else if ( this.ItemID == 0x1067 || this.ItemID == 0x1068 || this.ItemID == 0x1081 || this.ItemID == 0x1082 )
					LabelTo( from, "cut leather" );
				/* tailor */
				else if ( this.ItemID == 0xEC7 || this.ItemID == 0xEC6 )
					LabelTo( from, "a dress form" );
				else if ( this.ItemID >= 0x175D && this.ItemID <= 0x1764 )
					LabelTo( from, "folded cloth" );
				else if ( this.ItemID == 0x1765 || this.ItemID == 0x1767 )
					LabelTo( from, "cloth" );
				else if ( this.ItemID == 0x1766 || this.ItemID == 0x1768 )
					LabelTo( from, "cut cloth" );
				else if ( this.ItemID == 0x1049 || this.ItemID == 0x104A )
					LabelTo( from, "a loom bench" );
				else if ( this.ItemID == 0xFA0 || this.ItemID == 0xFA1 )
					LabelTo( from, "a spool of thread" );
				else if ( this.ItemID == 0xF9D )
					LabelTo( from, "a sewing kit" );
				else if ( this.ItemID >= 0xF95 && this.ItemID <= 0xF9C )
					LabelTo( from, "a bolt of cloth" );
				else if ( ( this.ItemID >= 0x1015 && this.ItemID <= 0x1017 ) || ( this.ItemID >= 0x1019 && this.ItemID <= 0x101E ) || ( this.ItemID >= 0x10A4 && this.ItemID <= 0x10A6 ) )
					LabelTo( from, "a spinning wheel" );
				else if ( this.ItemID >= 0x105F && this.ItemID <= 0x1066 )
					LabelTo( from, "an upright loom" );
				else if ( this.ItemID == 0xDF6 || this.ItemID == 0xDF7 )
					LabelTo( from, "a knitting" );
				else if ( this.ItemID == 0xDF8 || this.ItemID == 0x101F )
					LabelTo( from, "a pile of wool" );
				else if ( this.ItemID == 0xDEF || this.ItemID == 0xDF9 )
					LabelTo( from, "a bale of cotton" );
				else if ( this.ItemID >= 0xE1D && this.ItemID <= 0xE1F )
					LabelTo( from, "a ball of yarn" );
				/* cartography */
				else if ( this.ItemID == 0x14EC || this.ItemID == 0x14EB )
					LabelTo( from, "a map" );
				else if ( this.ItemID == 0x14F3 || this.ItemID == 0x14F4 )
					LabelTo( from, "a ship model" );
				else if ( this.ItemID == 0x14F5 || this.ItemID == 0x14F6 )
					LabelTo( from, "a spyglass" );
				/* masonry */
				else if ( this.ItemID == 0x1026 || this.ItemID == 0x1027 )
					LabelTo( from, "chisels" );
				/* tinkering */
				else if ( this.ItemID == 0x1EB5 )
					LabelTo( from, "an unfinished barrel" );
				else if ( this.ItemID >= 0x1EB1 && this.ItemID <= 0x1EB4 )
					LabelTo( from, "barrel staves" );
				else if ( this.ItemID == 0x10E1 || this.ItemID == 0x10E2 || this.ItemID == 0x1DB7 )
					LabelTo( from, "barrel hoops" );
				else if ( this.ItemID == 0x1EBC )
					LabelTo( from, "tinker's tools" );
				else if ( this.ItemID == 0x1050 || this.ItemID == 0x1051 )
					LabelTo( from, "an axle with gears" );
				else if ( this.ItemID == 0x104F || this.ItemID == 0x1050 )
					LabelTo( from, "clock parts" );
				else if ( this.ItemID == 0x1053 || this.ItemID == 0x1054 )
					LabelTo( from, "gears" );
				else if ( this.ItemID >= 0x1EB8 && this.ItemID <= 0x1EBB )
					LabelTo( from, "a tool kit" );
				else if ( this.ItemID == 0x104B || this.ItemID == 0x104C )
					LabelTo( from, "a clock" );
				else if ( this.ItemID == 0x104D || this.ItemID == 0x104E )
					LabelTo( from, "a clock frame" );
				/* carpentry */
				else if ( this.ItemID == 0x1028 || this.ItemID == 0x1029 )
					LabelTo( from, "a dovetail saw" );
				else if ( this.ItemID == 0x102A || this.ItemID == 0x102B )
					LabelTo( from, "a hammer" );
				else if ( this.ItemID == 0x102E || this.ItemID == 0x102F )
					LabelTo( from, "nails" );
				else if ( this.ItemID == 0x1030 || this.ItemID == 0x1031 )
					LabelTo( from, "a jointing plane" );
				else if ( this.ItemID == 0x1034 || this.ItemID == 0x1035 )
					LabelTo( from, "a saw" );
				else if ( this.ItemID == 0x102C || this.ItemID == 0x102D )
					LabelTo( from, "moulding planes" );
				else if ( this.ItemID == 0x1032 || this.ItemID == 0x1033 )
					LabelTo( from, "smoothing planes" );
				else if ( this.ItemID == 0x1038 )
					LabelTo( from, "wood curls" );
				/* containers */
				else if ( this.ItemID == 0xE7F )
					LabelTo( from, "a keg" );
				else if ( this.ItemID == 0xE77 || this.ItemID == 0xFAE )
					LabelTo( from, "a barrel" );
				else if ( this.ItemID == 0xE7D || this.ItemID == 0x9AA )
					LabelTo( from, "a wooden box" );
				else if ( this.ItemID == 0x9B0 || this.ItemID == 0xE79 )
					LabelTo( from, "a pouch" );
				/* magery */
				else if ( this.ItemID == 0x0F87 )
					LabelTo( from, "eye of newt" );
				else if ( this.ItemID == 0xFC7 )
					LabelTo( from, "bloodspawn" );
				else if ( this.ItemID == 0xF7A )
					LabelTo( from, "a black pearl" );
				else if ( this.ItemID == 0xF8D )
					LabelTo( from, "spider's silk" );
				else if ( this.ItemID == 0xF8C )
					LabelTo( from, "sulfurous ash" );
				else if ( this.ItemID == 0xF88 )
					LabelTo( from, "nightshade" );
				else if ( this.ItemID == 0xF84 )
					LabelTo( from, "garlic" );
				else if ( this.ItemID == 0xF85 )
					LabelTo( from, "ginseng" );
				else if ( this.ItemID == 0xF86 )
					LabelTo( from, "a mandrake root" );
				else if ( this.ItemID == 0xF7B )
					LabelTo( from, "blood moss" );
				else if ( this.ItemID == 0xF78 )
					LabelTo( from, "a batwing" );
				else if ( this.ItemID >= 0xE35 && this.ItemID <= 0xE3A )
					LabelTo( from, "a scroll" );
				else if ( this.ItemID >= 0xDF2 && this.ItemID <= 0xDF5 )
					LabelTo( from, "a wand" );
				else if ( this.ItemID >= 0x12A5 && this.ItemID <= 0x12AA )
					LabelTo( from, "tarot" );
				else if ( this.ItemID == 0x12AB || this.ItemID == 0x12AC )
					LabelTo( from, "a deck of tarot" );
				else if ( this.ItemID >= 0xE2D && this.ItemID <= 0xE30 )
					LabelTo( from, "a crystal ball" );
				else if ( this.ItemID == 0xE34 || this.ItemID == 0xEF3 )
					LabelTo( from, "a blank scroll" );
				/* weapons */
				else if ( this.ItemID == 0xF4D || this.ItemID == 0xF4E )
					LabelTo( from, "a bardiche" );
				else if ( this.ItemID == 0xF4B || this.ItemID == 0xF4C )
					LabelTo( from, "a double axe" );
				else if ( this.ItemID == 0xF49 || this.ItemID == 0xF4A )
					LabelTo( from, "an axe" );
				else if ( this.ItemID == 0x1438 || this.ItemID == 0x1439 )
					LabelTo( from, "a war hammer" );
				else if ( this.ItemID == 0x1406 || this.ItemID == 0x1407 )
					LabelTo( from, "a war mace" );
				else if ( this.ItemID == 0x1442 || this.ItemID == 0x1443 )
					LabelTo( from, "a two handed axe" );
				else if ( this.ItemID == 0xF43 || this.ItemID == 0xF44 )
					LabelTo( from, "a hatchet" );
				else if ( this.ItemID == 0x13FA || this.ItemID == 0x13FB )
					LabelTo( from, "a large battle axe" );
				else if ( this.ItemID == 0x13B3 || this.ItemID == 0x13B4 )
					LabelTo( from, "a club" );
				else if ( this.ItemID == 0xE85 || this.ItemID == 0xE86 )
					LabelTo( from, "a pickaxe" );
				else if ( this.ItemID == 0xF4F || this.ItemID == 0xF50 )
					LabelTo( from, "a crossbow" );
				else if ( this.ItemID == 0xE89 || this.ItemID == 0xE8A )
					LabelTo( from, "a quarter staff" );
				else if ( this.ItemID == 0x13D8 || this.ItemID == 0x13D9 )
					LabelTo( from, "a gnarled staff" );
				else if ( this.ItemID == 0x0F5C || this.ItemID == 0x0F5D )
					LabelTo( from, "a mace" );
				else if ( this.ItemID == 0x13B1 || this.ItemID == 0x13B2 )
					LabelTo( from, "a bow" );
				else if ( this.ItemID == 0x13FC || this.ItemID == 0x13FD )
					LabelTo( from, "a heavy crossbow" );
				else if ( this.ItemID == 0xF51 || this.ItemID == 0xF52 )
					LabelTo( from, "a dagger" );
				else if ( this.ItemID == 0x13B9 || this.ItemID == 0x13BA )
					LabelTo( from, "a viking sword" );
				else if ( this.ItemID == 0xF5E || this.ItemID == 0xF5F )
					LabelTo( from, "a broadsword" );
				else if ( this.ItemID == 0x13B5 || this.ItemID == 0x13B6 )
					LabelTo( from, "a scimitar" );
				else if ( this.ItemID == 0x13F6 || this.ItemID == 0x13F7 )
					LabelTo( from, "a butcher's knife" );
				else if ( this.ItemID == 0xEC4 || this.ItemID == 0xEC5 )
					LabelTo( from, "a skinning knife" );
				else if ( this.ItemID == 0xEC2 || this.ItemID == 0xEC3 )
					LabelTo( from, "a cleaver" );
				else if ( this.ItemID == 0x143E || this.ItemID == 0x143F )
					LabelTo( from, "a halberd" );
				else if ( this.ItemID == 0x1402 || this.ItemID == 0x1403 )
					LabelTo( from, "a short spear" );
				else if ( this.ItemID == 0x0FB5 || this.ItemID == 0x0FB4 )
					LabelTo( from, "a sledge hammer" );
				else if ( this.ItemID == 0x0F45 || this.ItemID == 0x0F46 )
					LabelTo( from, "an executioner's axe" );
				else if ( this.ItemID == 0xE87 || this.ItemID == 0xE88 )
					LabelTo( from, "a pitchfork" );
				/* armor */
				else if ( this.ItemID == 0x1F0B || this.ItemID == 0x1F0C )
					LabelTo( from, "an orc helm" );
				else if ( this.ItemID == 0x140C || this.ItemID == 0x140D )
					LabelTo( from, "a bascinet" );
				else if ( this.ItemID == 0x13E7 || this.ItemID == 0x13E8 || this.ItemID == 0x13EC || this.ItemID == 0x13ED )
					LabelTo( from, "a ringmail tunic" );
				else if ( this.ItemID == 0x13EB || this.ItemID == 0x13F2 )
					LabelTo( from, "ringmail gloves" );
				else if ( this.ItemID == 0x13E5 || this.ItemID == 0x13E6 || this.ItemID == 0x13F0 || this.ItemID == 0x13F1 )
					LabelTo( from, "ringmail leggings" );
				else if ( this.ItemID == 0x144E || this.ItemID == 0x1453 )
					LabelTo( from, "bone arms" );
				else if ( this.ItemID == 0x144F || this.ItemID == 0x1454 )
					LabelTo( from, "bone armor" );
				else if ( this.ItemID == 0x13BB || this.ItemID == 0x13C0 )
					LabelTo( from, "a chainmail coif" );
				else if ( this.ItemID == 0x13BC || this.ItemID == 0x13BE || this.ItemID == 0x13C1 || this.ItemID == 0x13C3 )
					LabelTo( from, "chainmail leggings" );
				else if ( this.ItemID == 0x1412 || this.ItemID == 0x1419 )
					LabelTo( from, "a plate helm" );
				else if ( this.ItemID == 0x1415 || this.ItemID == 0x1416 )
					LabelTo( from, "a platemail tunic" );
				else if ( this.ItemID == 0x1420 || this.ItemID == 0x1417 )
					LabelTo( from, "platemail arms" );
				else if ( this.ItemID == 0x1414 || this.ItemID == 0x1418 )
					LabelTo( from, "platemail gloves" );
				else if ( this.ItemID == 0x1413 )
					LabelTo( from, "a platemail gorget" );	
				else if ( this.ItemID == 0x1408 || this.ItemID == 0x1409 )
					LabelTo( from, "a close helm" );
				else if ( this.ItemID == 0x13C5 || this.ItemID == 0x13C8 || this.ItemID == 0x13CD || this.ItemID ==0x13CF )
					LabelTo( from, "leather sleeves" );
				else if ( this.ItemID == 0x13C7 )
					LabelTo( from, "a leather gorget" );
				else if ( this.ItemID == 0x13D9 || this.ItemID == 0x13DB || this.ItemID == 0x13E0 || this.ItemID == 0x13E2 )
					LabelTo( from, "a studded tunic" );
				else if ( this.ItemID == 0x13D4	|| this.ItemID == 0x13D7 || this.ItemID == 0x13DC || this.ItemID == 0x13DE )
					LabelTo( from, "studded sleeves" );
				else if ( this.ItemID == 0x13D5 || this.ItemID == 0x13DD )
					LabelTo( from, "studded gloves" );
				else if ( this.ItemID == 0x13D6 )
					LabelTo( from, "a studded gorget" );
				/* shields */
				else if ( this.ItemID == 0x1B73 )
					LabelTo( from, "a buckler" );
				else if ( this.ItemID == 0x1B78 || this.ItemID == 0x1B79 )
					LabelTo( from, "a wooden kite shield" );
				else if ( this.ItemID == 0x1B74 || this.ItemID == 0x1B75 )
					LabelTo( from, "a metal kite shield" );
				else if ( this.ItemID == 0x1B7A )
					LabelTo( from, "a wooden shield" );
				else if ( this.ItemID == 0x1B76 || this.ItemID == 0x1B77 )
					LabelTo( from, "a heater shield" );
				/* butcher */
				else if ( this.ItemID >= 0x1E83 && this.ItemID <= 0x1E86 )
					LabelTo( from, "a bird" );
				else if ( this.ItemID == 0x1E87 )
					LabelTo( from, "a chicken" );
				else if ( this.ItemID == 0x1E8E || this.ItemID == 0x1E8F )
					LabelTo( from, "a pig's head" );
				else if ( this.ItemID == 0x1E8C || this.ItemID == 0x1E8D )
					LabelTo( from, "pigs feet" );
				else if ( this.ItemID == 0x1871 || this.ItemID == 0x1872 )
					LabelTo( from, "a beef carcass" );
				else if ( this.ItemID == 0x1873 || this.ItemID == 0x1874 )
					LabelTo( from, "a sheep carcass" );
				else if ( this.ItemID == 0x1E88 || this.ItemID == 0x1E89 )
					LabelTo( from, "skinned goat" );
				else if ( this.ItemID == 0x1E90 || this.ItemID == 0x1E91 )
					LabelTo( from, "skinned deer" );
				else if ( this.ItemID == 0x1E92 || this.ItemID == 0x1E93 )
					LabelTo( from, "skinned rabbit" );
				/* fletcher */
				else if ( this.ItemID == 0xF40 || this.ItemID == 0xF41 )
					LabelTo( from, "arrows" );
				else if ( this.ItemID == 0xF3F || this.ItemID == 0xF42 )
					LabelTo( from, "an arrow" );
				else if ( this.ItemID == 0xDFA || this.ItemID == 0xDFB || this.ItemID == 0x1021 || this.ItemID == 0x1BD2 || this.ItemID == 0x1BD3 )
					LabelTo( from, "feathers" );
				else if ( this.ItemID == 0x1BD1 || this.ItemID == 0x1020 )
					LabelTo( from, "a feather" );
				else if ( this.ItemID == 0x1024 || this.ItemID == 0x1025 )
					LabelTo( from, "arrow shafts" );
				else if ( this.ItemID == 0x1BD4	 )
					LabelTo( from, "a shaft" );
				else if ( this.ItemID == 0x1BD5 || this.ItemID == 0x1BD6 )
					LabelTo( from, "shafts" );
				/* art */
				else if ( this.ItemID == 0xFC1 )
					LabelTo( from, "paints and brush" );
				else if ( this.ItemID == 0xF65 || this.ItemID == 0xF67 || this.ItemID == 0xF69 )
					LabelTo( from, "an easel" );
				else if ( this.ItemID == 0xF66 || this.ItemID == 0xF68 || this.ItemID == 0xF6A )
					LabelTo( from, "an easel with canvas" );
				else if ( this.ItemID >= 0xF71 && this.ItemID <= 0xF76 )
					LabelTo( from, "a rack of canvases" );
				/* instruments */
				else if ( this.ItemID == 0xE9C )
					LabelTo( from, "a drum" );
				else if ( this.ItemID == 0xE9D || this.ItemID == 0xE9E )
					LabelTo( from, "a tambourine" );
				else if ( this.ItemID == 0xEB1 )
					LabelTo( from, "a standin harp" );
				else if ( this.ItemID == 0xEB2 )
					LabelTo( from, "a lap harp" );
				else if ( this.ItemID == 0xEB3 || this.ItemID == 0xEB4 )
					LabelTo( from, "a lute" );
				else if ( this.ItemID >= 0xEB5 && this.ItemID <= 0xEBC )
					LabelTo( from, "a music stand" );
				else if ( this.ItemID >= 0xEBD && this.ItemID <= 0xEC0 )
					LabelTo( from, "sheet music" );
				/* alchemy */
				else if ( this.ItemID >= 0x182A && this.ItemID <= 0x1848 )
					LabelTo( from, "a flask" );
				else if ( this.ItemID == 0x1829 )
					LabelTo( from, "a flask stand" );
				else if ( this.ItemID == 0xC41 || this.ItemID == 0xC42 )
					LabelTo( from, "dried herbs" );
				else if ( this.ItemID >= 0xC3B && this.ItemID <= 0xC3E )
					LabelTo( from, "dried flowers" );
				else if ( this.ItemID == 0x0C3F || this.ItemID == 0x0C40 )
					LabelTo( from, "dried onions" );
				else if ( this.ItemID == 0x185B || this.ItemID == 0x185C )
					LabelTo( from, "empty vials" );
				else if ( this.ItemID == 0x185D || this.ItemID == 0x185E )
					LabelTo( from, "full vials" );
				else if ( this.ItemID == 0x0E24 )
					LabelTo( from, "an empty vial" );
				else if ( this.ItemID == 0xE9B )
					LabelTo( from, "a mortar and pestle" );
				else if ( this.ItemID == 0xF06 )
					LabelTo( from, "a black potion" );
				else if ( this.ItemID == 0xF07 )
					LabelTo ( from, "an orange potion" );
				else if ( this.ItemID == 0xF08 )
					LabelTo( from, "a blue potion" );
				else if ( this.ItemID == 0xF0A )
					LabelTo( from, "a green potion" );
				else if ( this.ItemID == 0xF0B )
					LabelTo( from, "a red potion" );
				else if ( this.ItemID == 0xF0C )
					LabelTo( from, "a yellow potion" );
				else if ( this.ItemID == 0xF0D )
					LabelTo( from, "a purple potion" );
				else if ( this.ItemID == 0xF0E )
					LabelTo( from, "an empty bottle" );
				else if ( this.ItemID == 0xF09 )
					LabelTo( from, "a white potion" );
				/* jewelry */
				else if ( this.ItemID == 0x1085 || this.ItemID == 0x1088 || this.ItemID == 0x1089 || this.ItemID == 0x1F05 || this.ItemID == 0x1F08 || this.ItemID == 0x1F0A )
					LabelTo( from, "a necklace" );
				else if ( this.ItemID == 0x1087 || this.ItemID == 0x1F07 )
					LabelTo( from, "earrings" );
				else if ( this.ItemID == 0x1086 || this.ItemID == 0x1F06 )
					LabelTo( from, "a bracelet" );
				else if ( this.ItemID == 0x108A || this.ItemID == 0x1F09 )
					LabelTo( from, "a ring" );
				/* jewels */
				else if ( this.ItemID == 0xF16 || this.ItemID == 0xF17 || this.ItemID == 0xF22 || this.ItemID == 0xF2E )
					LabelTo( from, "an amethyst" );
				else if ( this.ItemID == 0xF13 || this.ItemID == 0xF14 || this.ItemID == 0xF1A || this.ItemID == 0xF1C || this.ItemID == 0xF1D || this.ItemID == 0xF2A || this.ItemID == 0xF2B )
					LabelTo( from, "a ruby" );
				else if ( ( this.ItemID >= 0xF26 && this.ItemID <= 0xF29 ) || this.ItemID == 0xF30 )
					LabelTo( from, "a diamond" );
				else if ( this.ItemID == 0xF11 || this.ItemID == 0xF12 || this.ItemID == 0xF19 || this.ItemID == 0xF1F )
					LabelTo( from, "a sapphire" );
				else if ( this.ItemID == 0xF15 || this.ItemID == 0xF23 || this.ItemID == 0xF24 || this.ItemID == 0xF2C )
					LabelTo( from, "a citrine" );
				/* hay */
				else if ( this.ItemID == 0xF36 )
					LabelTo( from, "a sheaf of hay" );
				else if ( this.ItemID == 0x100C || this.ItemID == 0x100D )
					LabelTo( from, "a hay sheave" );
				else if ( this.ItemID == 0xF34 || this.ItemID == 0xF35 || this.ItemID == 0x1036 || this.ItemID == 0x1037 )
					LabelTo( from, "hay" );
				else
					base.OnSingleClick( from );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public Static( Serial serial ) : base( serial )
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

	public class LocalizedStatic : Static
	{
		private int m_LabelNumber;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Number
		{
			get{ return m_LabelNumber; }
			set{ m_LabelNumber = value; InvalidateProperties(); }
		}

		public override int LabelNumber{ get{ return m_LabelNumber; } }

		[Constructable]
		public LocalizedStatic( int itemID ) : this( itemID, 1020000 + itemID )
		{
		}

		[Constructable]
		public LocalizedStatic( int itemID, int labelNumber ) : base( itemID )
		{
			m_LabelNumber = labelNumber;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( m_LabelNumber == 1016017 )
					LabelTo( from, "a summon daemon scroll" );
			}
			else
			{
			}
		}

		public LocalizedStatic( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (byte) 0 ); // version
			writer.WriteEncodedInt( (int) m_LabelNumber );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadByte();

			switch ( version )
			{
				case 0:
				{
					m_LabelNumber = reader.ReadEncodedInt();
					break;
				}
			}
		}
	}
}