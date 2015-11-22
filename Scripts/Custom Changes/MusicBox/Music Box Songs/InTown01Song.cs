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
    public class InTown01Song : MusicBoxTrack
    {
        [Constructable]
        public InTown01Song()
            : base(1075136)
        {
            Song = MusicName.InTown01;
            //Name = "Minoc (Negative)";
        }

        public InTown01Song(Serial s)
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


