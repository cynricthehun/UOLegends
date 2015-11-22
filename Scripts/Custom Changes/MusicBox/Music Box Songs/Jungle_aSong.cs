using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.ContextMenus;
using Server.Network;
using Server.Mobiles;
using Server.Items;

namespace Server.Items.MusicBox
{
    public class Jungle_aSong : MusicBoxTrack
    {
        [Constructable]
        public Jungle_aSong()
            : base(1075137)
        {
            Song = MusicName.Jungle_a;
            //Name = "PawsSong (U9)";
        }

        public Jungle_aSong(Serial s)
            : base(s)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}


