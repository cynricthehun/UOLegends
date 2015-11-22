using System;
using Server;

namespace Server.Items
{
	public class VirtueBook : BrownBook
	{
		[Constructable]
		public VirtueBook() : base( "VirtueBook", "Lord British ", 19, false ) // true writable so players can make notes
		{
		// NOTE: There are 8 lines per page and 
		// approx 22 to 24 characters per line! 
		//		0----+----1----+----2----+ 
		int cnt = 0; 
			string[] lines; 
			lines = new string[] 
			{ 
				"Within this world live", 
				"people with many", 
				"different ideals,and this", 
				"is good.Yet what is it", 
				"within the people of our", 
				"land that sorts out the",
				"good from the evil, the",
				"cherished form the", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"disdained?", 
				"Virtue, I say it is, and", 
				"virtue is the logical", 
				"outcome of a people", 
				"who wish to live together", 
				"in a bonded society.,", 
				"For without Virtues as a", 
				"code of conduct which", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"people maintai(n) in their", 
				"relations with each other,", 
				"the fabric of that society", 
				"ill become weakene(d.) For", 
				"a society to grow and", 
				"prosper for all,each must", 
				"grant the others a common", 
				"base of consideration.", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"I call this base the", 
				"Virtues.", 
				"", 
				"For though one person", 
				"might gain personal", 
				"advantage by breaching", 
				"such a code, the society", 
				"as a whole would", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"suffer. ", 
				"", 
				"There are three Principle", 
				"Virtues tha(t) should",
				"guide people to",
				"enlightenment. These are:", 
				"Truth, Love, and Courage.", 
				"From all the infinite", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"reasons. one may have to", 
				"found an action, such as", 
				"greed or charity,envy or", 
				"pity, the three Principle",
				"Virtues stand out.",
				"", 
				"In fact all other virtues", 
				"and vices can be show(n)", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"to be built from these", 
				"principle(s) and. their", 
				"opposite corruption's of", 
				"Falsehood, Hatred an(d)",
				"Cowardice. These three",
				"Principles can be", 
				"combined in eight ways,", 
				"which I will call the", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"eight virtues. The eight", 
				"virtues which we should", 
				"build our society upon", 
				"follow.",
				"",
				"Truth alone becomes", 
				"Honesty, for without", 
				"honesty between our", 
			};			
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"people,how can we build", 
				"the trust which is", 
				"needed to maximize our", 
				"successes.",
				"", 
				"Love alone becomes", 
				"compassion,for at some", 
				"time or another all of us", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"will need the compassion", 
				"of others, and most", 
				"likely compassion will be", 
				"shown to those who have", 
				"shown it.", 
				"", 
				"Courage alone becomes", 
				"Valor, without valor our", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"people will never reach", 
				"into the unknown or to", 
				"the risky and will never", 
				"achieve.", 
				"", 
				"Truth tempered by Love", 
				"gives us Justice, for", 
				"only in loving search for", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"the truth can one", 
				"dispense fair Justice,", 
				"rather than create a cold", 
				"and callous people.", 
				"", 
				"Love and Courage gives us", 
				"Sacrifice, for a people", 
				"who love each other will", 
			}; 
			Pages[cnt++].Lines = lines; 
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"be willing to make", 
				"personal sacrifices to", 
				"help others in need,", 
				"which one day, may be", 
				"needed in return. Courage", 
				"and Truth give us Honor,", 
				"great knights know this", 
				"well,that chivalric honor", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"can be found by adhering", 
				"to this code of conduct.", 
				"", 
				"Combining Truth, Love and", 
				"Courage suggest the", 
				"virtue of Spirituality", 
				"the virtu(e) that causes", 
				"one to be introspective,", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"to wonder about ones", 
				"place in this world and", 
				"whether one's deeds will", 
				"be recorded as a gift to", 
				"the world or a plague.", 
				"", 
				"The final Virtue is more", 
				"complicated. For the", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"eighth combination is", 
				"that devoid of Truth,", 
				"Love or Courage which", 
				"can only exist in a state", 
				"of great Pride,whic(h) of", 
				"course is not a virtue at", 
				"all. Perhaps this trick of", 
				"fate is a test to see if", 
			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"one can realize that the", 
				"true virtue is that of", 
				"Humility. I feel that", 
				"the people of Magincia", 
				"fail to see this to such", 
				"a degree that I would not", 
				"be surprised if some ill", 
				"fate awaited their", 

			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"future.", 
				"", 
				"Thus from the infinite", 
				"possibilities which", 
				"spawned the Three", 
				"Principles of Truth,Love", 
				"and Courage, come the", 
				"Eight Virtues of", 

			}; 
			Pages[cnt++].Lines = lines;
		//		0----+----1----+----2----+
			lines = new string[] 
			{ 
				"Honesty, Compassion,", 
				"Valor, Justice,", 
				"Sacrifice, Honor,", 
				"Spirituality, and", 
				"Humility.", 
				"", 
				"", 
				"", 

			}; 
			Pages[cnt++].Lines = lines;
		}

		public VirtueBook( Serial serial ) : base( serial )
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}
	}
}