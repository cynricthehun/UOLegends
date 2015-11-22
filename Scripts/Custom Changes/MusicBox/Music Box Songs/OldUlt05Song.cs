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
    public class OldUlt05Song : MusicBoxTrack
    {
        [Constructable]
        public OldUlt05Song()
            : base(1075134)
        {
            Song = MusicName.OldUlt05;
            //Name = "Great Earth Serpent's Theme";
        }

        public OldUlt05Song(Serial s)
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


