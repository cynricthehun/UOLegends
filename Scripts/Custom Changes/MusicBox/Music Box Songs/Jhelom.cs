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
    public class JhelomSong : MusicBoxTrack
    {
        [Constructable]
        public JhelomSong()
            : base(1075147)
        {
            Song = MusicName.Jhelom;
            //Name = "JhelomSong";
        }

        public JhelomSong(Serial s)
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


