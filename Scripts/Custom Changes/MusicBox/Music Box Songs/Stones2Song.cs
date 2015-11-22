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
    public class Stones2Song : MusicBoxTrack
    {
        [Constructable]
        public Stones2Song()
            : base(1075135)
        {
            Song = MusicName.Stones2;
            //Name = "Humanoids Song";
        }

        public Stones2Song(Serial s)
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


