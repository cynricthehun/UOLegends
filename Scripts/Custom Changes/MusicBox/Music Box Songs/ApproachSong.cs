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
    public class ApproachSong : MusicBoxTrack
    {
        [Constructable]
        public ApproachSong()
            : base(1075139)
        {
            Song = MusicName.Approach;
            //Name = @"Ultima VII / Serpent Isle Combat";
        }

        public ApproachSong(Serial s)
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


