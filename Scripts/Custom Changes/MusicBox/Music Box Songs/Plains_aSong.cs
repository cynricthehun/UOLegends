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
    public class Plains_aSong : MusicBoxTrack
    {
        [Constructable]
        public Plains_aSong()
            : base(1075138)
        {
            Song = MusicName.Plains_a;
            //Name = "Selim's Bar (Strike Commander)";
        }

        public Plains_aSong(Serial s)
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


