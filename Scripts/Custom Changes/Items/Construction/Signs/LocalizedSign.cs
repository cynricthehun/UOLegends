using System;
using Server;

namespace Server.Items
{
	public class LocalizedSign : Sign
	{
		private int m_LabelNumber;

		public override int LabelNumber{ get{ return m_LabelNumber; } }

		[CommandProperty( AccessLevel.GameMaster )]
		public int Number{ get{ return m_LabelNumber; } set{ m_LabelNumber = value; InvalidateProperties(); } }

		[Constructable]
		public LocalizedSign( SignType type, SignFacing facing, int labelNumber ) : base( ( 0xB95 + (2 * (int)type) ) + (int)facing )
		{
			m_LabelNumber = labelNumber;
		}

		[Constructable]
		public LocalizedSign( int itemID, int labelNumber ) : base( itemID )
		{
			m_LabelNumber = labelNumber;
		}

		public override void OnSingleClick( Mobile from )
		{
			if ( this.Name == null )
			{
				if ( m_LabelNumber == 1016018 )
					LabelTo( from, "A Girl's Best Friend" );
				else if ( m_LabelNumber == 1016022 )
					LabelTo( from, "A Stitch in Time" );
				else if ( m_LabelNumber == 1016036 )
					LabelTo( from, "Adventurer Outfitters" );
				else if ( m_LabelNumber == 1016037 )
					LabelTo( from, "Adventurer's Clothing" );
				else if ( m_LabelNumber == 1016038 )
					LabelTo( from, "Adventurer's Needle" );
				else if ( m_LabelNumber == 1016042 )
					LabelTo( from, "Anchors Aweigh" );
				else if ( m_LabelNumber == 1016045 )
					LabelTo( from, "Artistic Armor" );
				else if ( m_LabelNumber == 1016046 )
					LabelTo( from, "Artists' Guild" );
				else if ( m_LabelNumber == 1016047 )
					LabelTo( from, "Audience granted by appointment only," );
				else if ( m_LabelNumber == 1016048 )
					LabelTo( from, "Baked Delights" );
				else if ( m_LabelNumber == 1016049 )
					LabelTo( from, "Baked Dozen" );
				else if ( m_LabelNumber == 1016050 )
					LabelTo( from, "Bank of Britannia: Trinsic Branch" );
				else if ( m_LabelNumber == 1016051 )
					LabelTo( from, "Bank of Magincia" );
				else if ( m_LabelNumber == 1016052 )
					LabelTo( from, "Bank of Minoc" );
				else if ( m_LabelNumber == 1016053 )
					LabelTo( from, "Bank of Nujel'm" );
				else if ( m_LabelNumber == 1016054 )
					LabelTo( from, "Bank of Occlo" );
				else if ( m_LabelNumber == 1016055 )
					LabelTo( from, "The Bank of Skara Brae" );
				else if ( m_LabelNumber == 1016057 )
					LabelTo( from, "Bardic Guild" );
				else if ( m_LabelNumber == 1016058 )
					LabelTo( from, "Barracks" );
				else if ( m_LabelNumber == 1016059 )
					LabelTo( from, "Bay Side Road" );
				else if ( m_LabelNumber == 1016060 )
					LabelTo( from, "Beasts of Burden" );
				else if ( m_LabelNumber == 1016061 )
					LabelTo( from, "Better Leather Tannery" );
				else if ( m_LabelNumber == 1016066 )
					LabelTo( from, "Bloody Bowman" );
				else if ( m_LabelNumber == 1016067 )
					LabelTo( from, "Bloody Thumb Woodworks" );
				else if ( m_LabelNumber == 1016069 )
					LabelTo( from, "Bountiful Meats" );
				else if ( m_LabelNumber == 1016071 )
					LabelTo( from, "Britain Public Library" );
				else if ( m_LabelNumber == 1016072 )
					LabelTo( from, "Britain's Blacksmith Guild" );
				else if ( m_LabelNumber == 1016073 )
					LabelTo( from, "Britain's Premier Provisioners and Fish Shoppe" );
				else if ( m_LabelNumber == 1016074 )
					LabelTo( from, "Britannia Animal Care" );
				else if ( m_LabelNumber == 1023075 )
					LabelTo( from, "Britannia Prison" );
				else if ( m_LabelNumber == 1016076 )
					LabelTo( from, "Britainnia Royal Zoo" );
				else if ( m_LabelNumber == 1016077 )
					LabelTo( from, "Britannian Herbs" );
				else if ( m_LabelNumber == 1016078 )
					LabelTo( from, "Britannia Provisions" );
				else if ( m_LabelNumber == 1016079 )
					LabelTo( from, "Brotherhood of Trinsic" );
				else if ( m_LabelNumber == 1016080 )
					LabelTo( from, "Brothers in Arms" );
				else if ( m_LabelNumber == 1016082 )
					LabelTo( from, "Buccaneer's Bath" );
				else if ( m_LabelNumber == 1016083 )
					LabelTo( from, "Buccaneer's Den Leatherworks" );
				else if ( m_LabelNumber == 1016084 )
					LabelTo( from, "Builder's Delight" );
				else if ( m_LabelNumber == 1016085 )
					LabelTo( from, "Call to Arms" );
				else if ( m_LabelNumber == 1016088 )
					LabelTo( from, "Castle Britannia" );
				else if ( m_LabelNumber == 1016089 )
					LabelTo( from, "Cavalry Guild" );
				else if ( m_LabelNumber == 1016091 )
					LabelTo( from, "Counselor's Guild" );
				else if ( m_LabelNumber == 1016093 )
					LabelTo( from, "Court of  Truth" );
				else if ( m_LabelNumber == 1016095 )
					LabelTo( from, "Cutlass Smithing" );
				else if ( m_LabelNumber == 1016098 )
					LabelTo( from, "Deadly Intentions" );
				else if ( m_LabelNumber == 1016099 )
					LabelTo( from, "Debtor's Prison" );
				else if ( m_LabelNumber == 1016104 )
					LabelTo( from, "East Side Park" );
				else if ( m_LabelNumber == 1016106 )
					LabelTo( from, "Empath Abbey" );
				else if ( m_LabelNumber == 1016107 )
					LabelTo( from, "Encyclopedia Magicka" );
				else if ( m_LabelNumber == 1016110 )
					LabelTo( from, "Ethereal Goods" );
				else if ( m_LabelNumber == 1016111 )
					LabelTo( from, "Farmer's Market" );
				else if ( m_LabelNumber == 1016112 )
					LabelTo( from, "Farmers' Market" );
				else if ( m_LabelNumber == 1016114 )
					LabelTo( from, "Finest Cuts" );
				else if ( m_LabelNumber == 1016115 )
					LabelTo( from, "First Academy of Music" );
				else if ( m_LabelNumber == 1016116 )
					LabelTo( from, "First Bank of Moonglow and Tinkers Guild" );
				else if ( m_LabelNumber == 1016117 )
					LabelTo( from, "First Defense" );
				else if ( m_LabelNumber == 1016118 )
					LabelTo( from, "First Trinsic Stablery" );
				else if ( m_LabelNumber == 1016119 )
					LabelTo( from, "Fisherman's Brew" );
				else if ( m_LabelNumber == 1016120 )
					LabelTo( from, "Fisherman's Wharf" );
				else if ( m_LabelNumber == 1016121 )
					LabelTo( from, "Fisherman's Guild and Supplies" );
				else if ( m_LabelNumber == 1016122 )
					LabelTo( from, "From Tree to Yew" );
				else if ( m_LabelNumber == 1016123 )
					LabelTo( from, "Gadgets and Things" );
				else if ( m_LabelNumber == 1016124 )
					LabelTo( from, "Gears and Gadgets" );
				else if ( m_LabelNumber == 1016126 )
					LabelTo( from, "Good Eats" );
				else if ( m_LabelNumber == 1016127 )
					LabelTo( from, "Gore Galore" );
				else if ( m_LabelNumber == 1016128 )
					LabelTo( from, "Great Horns Tavern" );
				else if ( m_LabelNumber == 1016129 )
					LabelTo( from, "Great Oak Bowyer" );
				else if ( m_LabelNumber == 1016130 )
					LabelTo( from, "Great Oak Vessels" );
				else if ( m_LabelNumber == 1016132 )
					LabelTo( from, "Hammer & Steel Smithy" );
				else if ( m_LabelNumber == 1016133 )
					LabelTo( from, "Hand of Death" );
				else if ( m_LabelNumber == 1016137 )
					LabelTo( from, "Healer of Britain" );
				else if ( m_LabelNumber == 1016138 )
					LabelTo( from, "Healer of Buccaneer's Den" );
				else if ( m_LabelNumber == 1016139 )
					LabelTo( from, "Healer of Magincia" );
				else if ( m_LabelNumber == 1016140 )
					LabelTo( from, "Healer of Occlo" );
				else if ( m_LabelNumber == 1016141 )
					LabelTo( from, "Healer of Skara Brae" );
				else if ( m_LabelNumber == 1016142 )
					LabelTo( from, "Healer of Vesper" );
				else if ( m_LabelNumber == 1016143 )
					LabelTo( from, "Healer of Yew" );
				else if ( m_LabelNumber == 1016145 )
					LabelTo( from, "Heavy Metal Armorer" );
				else if ( m_LabelNumber == 1016146 )
					LabelTo( from, "Herbal Splendor" );
				else if ( m_LabelNumber == 1016147 )
					LabelTo( from, "Honorable Arms" );
				else if ( m_LabelNumber == 1016148 )
					LabelTo( from, "Hut O' Magics" );
				else if ( m_LabelNumber == 1016149 )
					LabelTo( from, "Illusionist's Guild" );
				else if ( m_LabelNumber == 1016151 )
					LabelTo( from, "Incantations and Enchantments" );
				else if ( m_LabelNumber == 1016152 )
					LabelTo( from, "Jail" );
				else if ( m_LabelNumber == 1016153 )
					LabelTo( from, "Jewel of the Isle" );
				else if ( m_LabelNumber == 1016154 )
					LabelTo( from, "Jhelom Armory" );
				else if ( m_LabelNumber == 1016155 )
					LabelTo( from, "Jhelom Bank and Jeweler" );
				else if ( m_LabelNumber == 1016156 )
					LabelTo( from, "Jhemlom Dueling Pit" );
				else if ( m_LabelNumber == 1016157 )
					LabelTo( from, "Jhelom Healer" );
				else if ( m_LabelNumber == 1016158 )
					LabelTo( from, "Jhelom Library" );
				else if ( m_LabelNumber == 1016159 )
					LabelTo( from, "Jhelom Mage" );
				else if ( m_LabelNumber == 1016162 )
					LabelTo( from, "Last Chance Provisioners" );
				else if ( m_LabelNumber == 1016163 )
					LabelTo( from, "Lord British's Conservatory of Music" );
				else if ( m_LabelNumber == 1016164 )
					LabelTo( from, "Mage's Appetite" );
				else if ( m_LabelNumber == 1016165 )
					LabelTo( from, "Mages Bread" );
				else if ( m_LabelNumber == 1016168 )
					LabelTo( from, "Mages' Menagerie" );
				else if ( m_LabelNumber == 1016169 )
					LabelTo( from, "Mage's Things" );
				else if ( m_LabelNumber == 1016170 )
					LabelTo( from, "Magical Supplies" );
				else if ( m_LabelNumber == 1016171 )
					LabelTo( from, "Magincia Miner's Guild" );
				else if ( m_LabelNumber == 1016173 )
					LabelTo( from, "Magincia Magicka" );
				else if ( m_LabelNumber == 1016178 )
					LabelTo( from, "The Merchants' Guild" );
				else if ( m_LabelNumber == 1016179 )
					LabelTo( from, "Mess Hall" );
				else if ( m_LabelNumber == 1016181 )
					LabelTo( from, "Minoc Town Hall" );
				else if ( m_LabelNumber == 1016184 )
					LabelTo( from, "Moonglow Academy of Arts" );
				else if ( m_LabelNumber == 1016185 )
					LabelTo( from, "Moonglow Healer" );
				else if ( m_LabelNumber == 1016186 )
					LabelTo( from, "Moonglow Reagent Shop" );
				else if ( m_LabelNumber == 1016187 )
					LabelTo( from, "Moonglow Student Hostel" );
				else if ( m_LabelNumber == 1016188 )
					LabelTo( from, "Moonglow's Finest Alchemy" );
				else if ( m_LabelNumber == 1016189 )
					LabelTo( from, "More Than Just Mail" );
				else if ( m_LabelNumber == 1016191 )
					LabelTo( from, "Mystic Treasures" );
				else if ( m_LabelNumber == 1016192 )
					LabelTo( from, "Mystical Spirits" );
				else if ( m_LabelNumber == 1016193 )
					LabelTo( from, "Nature's Best Baked Goods" );
				else if ( m_LabelNumber == 1016194 )
					LabelTo( from, "Needful Things" );
				else if ( m_LabelNumber == 1016197 )
					LabelTo( from, "Now You're Cookin'" );
				else if ( m_LabelNumber == 1016198 )
					LabelTo( from, "Nujel'm Court" );
				else if ( m_LabelNumber == 1016199 )
					LabelTo( from, "Nujel'm Marketplace" );
				else if ( m_LabelNumber == 1016201 )
					LabelTo( from, "Nujel'm Theater" );
				else if ( m_LabelNumber == 1016202 )
					LabelTo( from, "Nujel'm Blacksmith" );
				else if ( m_LabelNumber == 1016203 )
					LabelTo( from, "Nujel'm Bowry" );
				else if ( m_LabelNumber == 1016204 )
					LabelTo( from, "Nujel'm Butcher" );
				else if ( m_LabelNumber == 1016205 )
					LabelTo( from, "Nujel'm Tannery" );
				else if ( m_LabelNumber == 1016206 )
					LabelTo( from, "Ocean's Treasure" );
				else if ( m_LabelNumber == 1016207 )
					LabelTo( from, "Occlo Public Library" );
				else if ( m_LabelNumber == 1016208 )
					LabelTo( from, "On Guard Armory" );
				else if ( m_LabelNumber == 1016211 )
					LabelTo( from, "Paint and More" );
				else if ( m_LabelNumber == 1016212 )
					LabelTo( from, "Paladin's Library" );
				else if ( m_LabelNumber == 1016214 )
					LabelTo( from, "Preforming Arts Centre" );
				else if ( m_LabelNumber == 1016215 )
					LabelTo( from, "Pier 39" );
				else if ( m_LabelNumber == 1016216 )
					LabelTo( from, "Pirate's Den" );
				else if ( m_LabelNumber == 1016217 )
					LabelTo( from, "Pirates Provisioner" );
				else if ( m_LabelNumber == 1016220 )
					LabelTo( from, "Plenty O' Dough" );
				else if ( m_LabelNumber == 1016221 )
					LabelTo( from, "Poor Gate" );
				else if ( m_LabelNumber == 1016222 )
					LabelTo( from, "Premier Gems" );
				else if ( m_LabelNumber == 1016223 )
					LabelTo( from, "Profuse Provisions" );
				else if ( m_LabelNumber == 1016224 )
					LabelTo( from, "Public Smithing" );
				else if ( m_LabelNumber == 1016225 )
					LabelTo( from, "Quality Fletching" );
				else if ( m_LabelNumber == 1016226 )
					LabelTo( from, "Quarantine Area: No pets permitted beyond this point!" );
				else if ( m_LabelNumber == 1016229 )
					LabelTo( from, "Ranger's Guild" );
				else if ( m_LabelNumber == 1016232 )
					LabelTo( from, "Restful Slumber" );
				else if ( m_LabelNumber == 1016233 )
					LabelTo( from, "River Road" );
				else if ( m_LabelNumber == 1016236 )
					LabelTo( from, "Sages Advice" );
				else if ( m_LabelNumber == 1016237 )
					LabelTo( from, "Sailor's Keeper" );
				else if ( m_LabelNumber == 1016238 )
					LabelTo( from, "Scholar's Cut" );
				else if ( m_LabelNumber == 1016239 )
					LabelTo( from, "Seaborne Ships" );
				else if ( m_LabelNumber == 1016240 )
					LabelTo( from, "Seat of Knowledge" );
				else if ( m_LabelNumber == 1016241 )
					LabelTo( from, "Second Defense Armory" );
				else if ( m_LabelNumber == 1016242 )
					LabelTo( from, "Second Skin" );
				else if ( m_LabelNumber == 1016243 )
					LabelTo( from, "Seeker's Inn" );
				else if ( m_LabelNumber == 1016244 )
					LabelTo( from, "Serpent Hold Meats" );
				else if ( m_LabelNumber == 1016245 )
					LabelTo( from, "Serpent Warrior's" );
				else if ( m_LabelNumber == 1016246 )
					LabelTo( from, "Serpent's Hold Healer" );
				else if ( m_LabelNumber == 1016247 )
					LabelTo( from, "Serpent's Spells" );
				else if ( m_LabelNumber == 1016248 )
					LabelTo( from, "Serpents Arms" );
				else if ( m_LabelNumber == 1016249 )
					LabelTo( from, "Serpents Hold Stablery" );
				else if ( m_LabelNumber == 1016250 )
					LabelTo( from, "Sewer Entrance: ENTER AT YOUR OWN PERIL!" );
				else if ( m_LabelNumber == 1016251 )
					LabelTo( from, "Shear Pleasure" );
				else if ( m_LabelNumber == 1016252 )
					LabelTo( from, "Shining Path Armory" );
				else if ( m_LabelNumber == 1016255 )
					LabelTo( from, "Silver Serpent Bows" );
				else if ( m_LabelNumber == 1016256 )
					LabelTo( from, "Silver Serpent Tailors" );
				else if ( m_LabelNumber == 1016258 )
					LabelTo( from, "Skara Brae Town Hall" );
				else if ( m_LabelNumber == 1016259 )
					LabelTo( from, "Sons of the Sea" );
				else if ( m_LabelNumber == 1016260 )
					LabelTo( from, "Sosarian Steeds" );
				else if ( m_LabelNumber == 1016261 )
					LabelTo( from, "Southside Stables" );
				else if ( m_LabelNumber == 1016262 )
					LabelTo( from, "Stables" );
				else if ( m_LabelNumber == 1016263 )
					LabelTo( from, "Stitchin' Time" );
				else if ( m_LabelNumber == 1016264 )
					LabelTo( from, "Strange Rocks" );
				else if ( m_LabelNumber == 1016265 )
					LabelTo( from, "Strength and Steel" );
				else if ( m_LabelNumber == 1016266 )
					LabelTo( from, "Sundry Supplies" );
				else if ( m_LabelNumber == 1016267 )
					LabelTo( from, "Superior Ships" );
				else if ( m_LabelNumber == 1016268 )
					LabelTo( from, "Supplies" );
				else if ( m_LabelNumber == 1016269 )
					LabelTo( from, "Sweet Dreams" );
				else if ( m_LabelNumber == 1016270 )
					LabelTo( from, "Sweet Meat" );
				else if ( m_LabelNumber == 1016271 )
					LabelTo( from, "Tailor of the Isle" );
				else if ( m_LabelNumber == 1016272 )
					LabelTo( from, "Tanner's Shop" );
				else if ( m_LabelNumber == 1016274 )
					LabelTo( from, "The Adventurer's Friend" );
				else if ( m_LabelNumber == 1016275 )
					LabelTo( from, "The Adventurers Supplies" );
				else if ( m_LabelNumber == 1016276 )
					LabelTo( from, "The Albatross" );
				else if ( m_LabelNumber == 1016277 )
					LabelTo( from, "The Alchemist of Wind" );
				else if ( m_LabelNumber == 1016278 )
					LabelTo( from, "The Barely Inn" );
				else if ( m_LabelNumber == 1016279 )
					LabelTo( from, " The Barnacle" );
				else if ( m_LabelNumber == 1016280 )
					LabelTo( from, "The Hides of Britain" );
				else if ( m_LabelNumber == 1016281 )
					LabelTo( from, "The Blue Boar" );
				else if ( m_LabelNumber == 1016283 )
					LabelTo( from, "The Bountiful Harvest" );
				else if ( m_LabelNumber == 1016284 )
					LabelTo( from, "The Bread Basket" );
				else if ( m_LabelNumber == 1016285 )
					LabelTo( from, "The Broken Arrow Inn" );
				else if ( m_LabelNumber == 1016286 )
					LabelTo( from, "The Bubbling Brew" );
				else if ( m_LabelNumber == 1016287 )
					LabelTo( from, "The Bucking Horse Stables" );
				else if ( m_LabelNumber == 1016288 )
					LabelTo( from, "The Busy Bees" );
				else if ( m_LabelNumber == 1016289 )
					LabelTo( from, "The Butcher's Knife" );
				else if ( m_LabelNumber == 1016290 )
					LabelTo( from, "The Carpentry House" );
				else if ( m_LabelNumber == 1016291 )
					LabelTo( from, "The Cat's Lair" );
				else if ( m_LabelNumber == 1016292 )
					LabelTo( from, "The Chamber of Virtue" );
				else if ( m_LabelNumber == 1016293 )
					LabelTo( from, "The Champions of Light" );
				else if ( m_LabelNumber == 1016294 )
					LabelTo( from, "The Circles of Magic" );
				else if ( m_LabelNumber == 1016295 )
					LabelTo( from, "The Cleaver" );
				else if ( m_LabelNumber == 1016296 )
					LabelTo( from, "The Colored Canvas" );
				else if ( m_LabelNumber == 1016298 )
					LabelTo( from, "The Dog and Lion Pub" );
				else if ( m_LabelNumber == 1016299 )
					LabelTo( from, "The Falconer's Inn" );
				else if ( m_LabelNumber == 1016300 )
					LabelTo( from, "The Family Jewels" );
				else if ( m_LabelNumber == 1016302 )
					LabelTo( from, "The First Bank of Britain" );
				else if ( m_LabelNumber == 1016304 )
					LabelTo( from, "The First Library of Britain" );
				else if ( m_LabelNumber == 1016305 )
					LabelTo( from, "The Fisherman's Guild" );
				else if ( m_LabelNumber == 1016306 )
					LabelTo( from, "The Forgery" );
				else if ( m_LabelNumber == 1016307 )
					LabelTo( from, "The Furled Sail" );
				else if ( m_LabelNumber == 1016308 )
					LabelTo( from, "The Gadget's Corner" );
				else if ( m_LabelNumber == 1016309 )
					LabelTo( from, "The Golden Pick Axe" );
				else if ( m_LabelNumber == 1016310 )
					LabelTo( from, "The Great Southern Way" );
				else if ( m_LabelNumber == 1016311 )
					LabelTo( from, "The Hammer and Anvil" );
				else if ( m_LabelNumber == 1016312 )
					LabelTo( from, "The Hammer and Nail" );
				else if ( m_LabelNumber == 1016313 )
					LabelTo( from, "The Healing Hand" );
				else if ( m_LabelNumber == 1016314 )
					LabelTo( from, "The Horse's Head" );
				else if ( m_LabelNumber == 1016315 )
					LabelTo( from, "The Ironwood Inn" );
				else if ( m_LabelNumber == 1016316 )
					LabelTo( from, "The Ironworks" );
				else if ( m_LabelNumber == 1016317 )
					LabelTo( from, "The Jolly Baker" );
				else if ( m_LabelNumber == 1016318 )
					LabelTo( from, "The Just Inn" );
				else if ( m_LabelNumber == 1016319 )
					LabelTo( from, "the Keg and Anchor" );
				else if ( m_LabelNumber == 1016320 )
					LabelTo( from, "The Kings Men Theater" );
				else if ( m_LabelNumber == 1016321 )
					LabelTo( from, "The Learned Mage" );
				else if ( m_LabelNumber == 1016322 )
					LabelTo( from, "The Lord's Arms" );
				else if ( m_LabelNumber == 1016323 )
					LabelTo( from, "The Lord's Clothiers" );
				else if ( m_LabelNumber == 1016324 )
					LabelTo( from, "The Lycaeum" );
				else if ( m_LabelNumber == 1016325 )
					LabelTo( from, "The Magical Light" );
				else if ( m_LabelNumber == 1016326 )
					LabelTo( from, "The Magician's Friend" );
				else if ( m_LabelNumber == 1016327 )
					LabelTo( from, "The Majestic Boat" );
				else if ( m_LabelNumber == 1016328 )
					LabelTo( from, "The Marsh Hall" );
				else if ( m_LabelNumber == 1016329 )
					LabelTo( from, "The Matewan" );
				else if ( m_LabelNumber == 1016331 )
					LabelTo( from, "The Mighty Axe" );
				else if ( m_LabelNumber == 1016332 )
					LabelTo( from, "The Miners' Guild" );
				else if ( m_LabelNumber == 1016333 )
					LabelTo( from, "The Mint of Vesper" );
				else if ( m_LabelNumber == 1016334 )
					LabelTo( from, "The Morning Star Inn" );
				else if ( m_LabelNumber == 1016335 )
					LabelTo( from, "The Musician's Hall" );
				else if ( m_LabelNumber == 1016336 )
					LabelTo( from, "The Mystical Lute" );
				else if ( m_LabelNumber == 1016337 )
					LabelTo( from, "The New World Order" );
				else if ( m_LabelNumber == 1016338 )
					LabelTo( from, "The North Side Inn" );
				else if ( m_LabelNumber == 1016339 )
					LabelTo( from, "The Oak Throne" );
				else if ( m_LabelNumber == 1016340 )
					LabelTo( from, "The Oaken Oar" );
				else if ( m_LabelNumber == 1016341 )
					LabelTo( from, "The Old Miners' Supplies" );
				else if ( m_LabelNumber == 1016342 )
					LabelTo( from, "The Ore of Vesper" );
				else if ( m_LabelNumber == 1016343 )
					LabelTo( from, "The Pearl of Jhelom" );
				else if ( m_LabelNumber == 1016344 )
					LabelTo( from, "The Pearl of Trinsic" );
				else if ( m_LabelNumber == 1016345 )
					LabelTo( from, "The Peg Leg Inn" );
				else if ( m_LabelNumber == 1016346 )
					LabelTo( from, "The Pirate's Plunder" );
				else if ( m_LabelNumber == 1016347 )
					LabelTo( from, "The Ranger's Tool" );
				else if ( m_LabelNumber == 1016348 )
					LabelTo( from, "The Reagent Shoppe" );
				else if ( m_LabelNumber == 1016349 )
					LabelTo( from, "The Revenge Shoppe" );
				else if ( m_LabelNumber == 1016350 )
					LabelTo( from, "The Right Fit" );
				else if ( m_LabelNumber == 1016351 )
					LabelTo( from, "The Rusty Anchor" );
				else if ( m_LabelNumber == 1016352 )
					LabelTo( from, "The Salty Dog" );
				else if ( m_LabelNumber == 1016353 )
					LabelTo( from, "The Saw Horse" );
				else if ( m_LabelNumber == 1016354 )
					LabelTo( from, "The Scholar's Inn" );
				else if ( m_LabelNumber == 1016355 )
					LabelTo( from, "The Shattered Skull" );
				else if ( m_LabelNumber == 1016356 )
					LabelTo( from, "The Shimmering Jewel" );
				else if ( m_LabelNumber == 1016357 )
					LabelTo( from, "The Silver Bow" );
				else if ( m_LabelNumber == 1016358 )
					LabelTo( from, "The Slaughtered Cow" );
				else if ( m_LabelNumber == 1016359 )
					LabelTo( from, "The Sorcerer's Delight:  Shop, Library, & Guild" );
				else if ( m_LabelNumber == 1016360 )
					LabelTo( from, "The Southside Butchery" );
				else if ( m_LabelNumber == 1016361 )
					LabelTo( from, "The Spinning Wheel" );
				else if ( m_LabelNumber == 1016362 )
					LabelTo( from, "The Stag and Lion" );
				else if ( m_LabelNumber == 1016363 )
					LabelTo( from, "The Stretched Hide" );
				else if ( m_LabelNumber == 1016364 )
					LabelTo( from, "The Sturdy Bow" );
				else if ( m_LabelNumber == 1016365 )
					LabelTo( from, "The Supply Depot" );
				else if ( m_LabelNumber == 1016366 )
					LabelTo( from, "Survival Shop" );
				else if ( m_LabelNumber == 1016367 )
					LabelTo( from, "The Tanned Hide" );
				else if ( m_LabelNumber == 1016368 )
					LabelTo( from, "The Tic - Toc Shop" );
				else if ( m_LabelNumber == 1016369 )
					LabelTo( from, "The Tinkers' Guild" );
				else if ( m_LabelNumber == 1016371 )
					LabelTo( from, "The Travellers Inn" );
				else if ( m_LabelNumber == 1016372 )
					LabelTo( from, "The Trinsic Cut" );
				else if ( m_LabelNumber == 1016373 )
					LabelTo( from, "The Twisted Oven" );
				else if ( m_LabelNumber == 1016374 )
					LabelTo( from, "The Unicorn's Horn" );
				else if ( m_LabelNumber == 1016375 )
					LabelTo( from, "The Warrior's Supplies" );
				else if ( m_LabelNumber == 1016376 )
					LabelTo( from, "The Watch Tower" );
				else if ( m_LabelNumber == 1016377 )
					LabelTo( from, "The Wayfarer's Inn" );
				else if ( m_LabelNumber == 1016390 )
					LabelTo( from, "Tinker of the Isle" );
				else if ( m_LabelNumber == 1016391 )
					LabelTo( from, "Tinker's Paradise" );
				else if ( m_LabelNumber == 1016392 )
					LabelTo( from, "Tinkers Guild" );
				else if ( m_LabelNumber == 1016393 )
					LabelTo( from, "To Britain" );
				else if ( m_LabelNumber == 1016394 )
					LabelTo( from, "To Minoc" );
				else if ( m_LabelNumber == 1016396 )
					LabelTo( from, "To Skara Brae" );
				else if ( m_LabelNumber == 1016398 )
					LabelTo( from, "To Trinsic" );
				else if ( m_LabelNumber == 1016401 )
					LabelTo( from, "To the Farms" );
				else if ( m_LabelNumber == 1016402 )
					LabelTo( from, "To the Coast" );
				else if ( m_LabelNumber == 1016403 )
					LabelTo( from, "Tricks of the Trade" );
				else if ( m_LabelNumber == 1016404 )
					LabelTo( from, "Trinsic Fine Skins" );
				else if ( m_LabelNumber == 1016405 )
					LabelTo( from, "Trinsic Healer" );
				else if ( m_LabelNumber == 1016406 )
					LabelTo( from, "Trinsic Meeting Hall" );
				else if ( m_LabelNumber == 1016407 )
					LabelTo( from, "Trinsic Royal Bank" );
				else if ( m_LabelNumber == 1016408 )
					LabelTo( from, "Trinsic Stablery" );
				else if ( m_LabelNumber == 1016409 )
					LabelTo( from, "Trinsic Training Hall" );
				else if ( m_LabelNumber == 1016414 )
					LabelTo( from, "Vesper Customs" );
				else if ( m_LabelNumber == 1016416 )
					LabelTo( from, "Vesper Youth Hostel" );
				else if ( m_LabelNumber == 1016417 )
					LabelTo( from, "Violente Woodworks" );
				else if ( m_LabelNumber == 1016419 )
					LabelTo( from, "Virtue's Path" );
				else if ( m_LabelNumber == 1016420 )
					LabelTo( from, "Warrior's Bounty" );
				else if ( m_LabelNumber == 1016421 )
					LabelTo( from, "Warrior's Companion" );
				else if ( m_LabelNumber == 1016422 )
					LabelTo( from, "Warriors' Battle Gear" );
				else if ( m_LabelNumber == 1016424 )
					LabelTo( from, "Wind Alchemy" );
				else if ( m_LabelNumber == 1016425 )
					LabelTo( from, "Wind Healer" );
				else if ( m_LabelNumber == 1016426 )
					LabelTo( from, "Wind Clothes" );
				else if ( m_LabelNumber == 1016427 )
					LabelTo( from, "Windy Inn" );
				else if ( m_LabelNumber == 1016430 )
					LabelTo( from, "Ye Olde Eleventh Bank" );
				else if ( m_LabelNumber == 1016431 )
					LabelTo( from, "Ye Olde Loan And Savings" );
				else if ( m_LabelNumber == 1016432 )
					LabelTo( from, "Ye Olde Winery" );
				else if ( m_LabelNumber == 1016434 )
					LabelTo( from, "Yew Mill" );
				else if ( m_LabelNumber == 1016436 )
					LabelTo( from, "Yew's Finest Cuts" );
				else if ( m_LabelNumber == 1016439 )
					LabelTo( from, "Zoot's Hammer" );
				else if ( m_LabelNumber == 1016497 )
					LabelTo( from, "The Fatted Calf" );
				else if ( m_LabelNumber == 1016499 )
					LabelTo( from, "The Great Northern Bridge" );
				else if ( m_LabelNumber == 1016500 )
					LabelTo( from, "The Great Northern Road" );
				else if ( m_LabelNumber == 1016502 )
					LabelTo( from, "The Mages Seat" );
				else if ( m_LabelNumber == 1016503 )
					LabelTo( from, "The Main Gate" );
				else if ( m_LabelNumber == 1016504 )
					LabelTo( from, "the Moat" );
				else if ( m_LabelNumber == 1016505 )
					LabelTo( from, "The Northern Bridge" );
				else if ( m_LabelNumber == 1016506 )
					LabelTo( from, "The Scholar's Goods" );
				else if ( m_LabelNumber == 1022993 )
					LabelTo( from, "Customs" );
				else if ( m_LabelNumber == 1023026 )
					LabelTo( from, "a brass sign" );
				else if ( m_LabelNumber == 1023049 )
					LabelTo( from, "Sorcerers' Guild" );
			}
			else
			{
				LabelTo( from, this.Name );
			}
		}

		public LocalizedSign( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			writer.Write( m_LabelNumber );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 0:
				{
					m_LabelNumber = reader.ReadInt();
					break;
				}
			}
		}
	}
}