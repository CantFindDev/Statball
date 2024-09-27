using Server.Gumps;
using Server.Mobiles;
using Server.Network;
using System;

//Statball system by CantFind
namespace Server.Items
{
    public class StatBall : Item
    {
        public int MaxUses = 1; //Maximum uses before being deleted

        [Constructable]
        public StatBall() : base(0x186E) //Setting the Ball
        {
            Movable = true;
            Weight = 1.0;
            Name = "225 Statball";
            LootType = LootType.Blessed;
            Hue = 0;
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack)) { from.SendLocalizedMessage(1042001); }// That must be in your pack for you to use it.
            else if (from is PlayerMobile) { _ = from.SendGump(new StatBallGump((PlayerMobile)from, this)); }
        }

        public override bool DisplayLootType => false;

        public StatBall(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0);// version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            _ = reader.ReadInt();
        }
    }
}

namespace Server.Items
{
    public class StatBallGump : Gump
    {
        private readonly PlayerMobile PlayerFormMobile;
        private readonly StatBall Ball;

        private int Strenght;
        private int Dexterity;
        private int Intelligence;

        private readonly int StatLimit = 225; //Maximum total limit

        public StatBallGump(PlayerMobile player, StatBall ball) : base(150, 250)
        {
            PlayerFormMobile = player; //Player Mobile
            Ball = ball; //The Stat Ball
            int LimitPerStat = StatLimit / 3; //Limit per stat

            //General Gump Settings
            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;
            AddPage(0);

            //Background
            AddBackground(50, 50, 450, 230, 9250); //Stone Background
            AddBackground(57, 59, 437, 215, 9300); //Yellow Paper Background

            //Exit Button
            AddButton(60, 61, 1227, 1227, 0, GumpButtonType.Reply, 0);

            //Title
            AddLabel(220, 70, 137, "Stat Ball Selection");

            //Description
            AddLabel(133, 92, 0, "Choose your Strength, Dexterity, and Intelligence");

            //Strenght
            AddBackground(90, 137, 81, 74, 9350);
            AddLabel(101, 150, 32, "Strenght");
            AddTextEntry(119, 179, 20, 20, 1152, 0, LimitPerStat.ToString());

            //Dexterity
            AddBackground(234, 137, 81, 74, 9350);
            AddLabel(242, 150, 53, "Dexterity");
            AddTextEntry(265, 179, 20, 20, 1152, 1, LimitPerStat.ToString());

            //Intelligence
            AddBackground(372, 137, 81, 74, 9350);
            AddLabel(377, 149, 88, "Intelligence");
            AddTextEntry(405, 179, 20, 20, 1152, 2, LimitPerStat.ToString());

            //Footer
            AddLabel(67, 248, 0, "Stat totals should equal " + StatLimit);

            //Accept Gump
            AddBackground(322, 284, 179, 88, 9250); //Stone Background
            AddBackground(329, 292, 167, 75, 9300); //Yellow Paper Background
            AddLabel(345, 319, 0, "Do you accept?");
            AddButton(429, 306, 92, 92, 1, GumpButtonType.Reply, 0); //Accept button
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (Ball.Deleted || !IsValuesValid(info))
            {
                return;
            }

            int TotalStat = Strenght + Dexterity + Intelligence;
            if (TotalStat < StatLimit)
            {
                PlayerFormMobile.SendMessage($"The total of Strenght: {Strenght}, Dexterity: {Dexterity}, Intelligence: {Intelligence} makes {TotalStat} you need to add {StatLimit - TotalStat} more to reach {StatLimit} ");
                _ = PlayerFormMobile.SendGump(new StatBallGump(PlayerFormMobile, Ball));
                return;
            }

            if (TotalStat > StatLimit)
            {
                PlayerFormMobile.SendMessage($"The total of Strenght: {Strenght}, Dexterity: {Dexterity}, Intelligence: {Intelligence} makes {TotalStat} you need to subtract {TotalStat - StatLimit} from the total to be {StatLimit} ");
                _ = PlayerFormMobile.SendGump(new StatBallGump(PlayerFormMobile, Ball));
                return;
            }

            if (info.ButtonID == 1)
            { //If accepted set values
                PlayerFormMobile.RawStr = Strenght;
                PlayerFormMobile.RawDex = Dexterity;
                PlayerFormMobile.RawInt = Intelligence;
                Ball.MaxUses--;
                if (Ball.MaxUses == 0)
                {
                    Ball.Delete();
                }
            }
        }

        private bool IsValuesValid(RelayInfo info)
        {
            TextRelay strenght = info.GetTextEntry(0);
            try { Strenght = Convert.ToInt32(strenght.Text); }
            catch { PlayerFormMobile.SendMessage("You were too strong on the keyboard that you pressed invalid keys. Try again"); return false; }

            TextRelay dexterity = info.GetTextEntry(1);
            try { Dexterity = Convert.ToInt32(dexterity.Text); }
            catch { PlayerFormMobile.SendMessage("That value of dexterity does not really look like a number. Try again"); return false; }

            TextRelay intelligence = info.GetTextEntry(2);
            try { Intelligence = Convert.ToInt32(intelligence.Text); }
            catch { PlayerFormMobile.SendMessage("Intelligence? uhh... That does not look like a valid number. Try again"); return false; }

            return Strenght >= 10 && Dexterity >= 10 && Intelligence >= 10;
        }
    }
}
