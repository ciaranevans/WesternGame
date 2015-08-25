using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GBWestern
{
    public class Dialogue
    {
        private Dictionary<string, Dictionary<string, Message>> allDialogue;
        
        #region Message Dictionaries Declaration
        private Dictionary<string, Message> manWindmill, ladyChurch, manRock, cow, manRockingHouse, testQuestNPC, sheriffGarett,
            sheriffGarettQuestDone, manRockQuestDone, signToJoes, manCactus, manHouse;
        #endregion

        public Dialogue()
        {
            allDialogue = new Dictionary<string, Dictionary<string, Message>>();

            #region Message Dictionaries Initialising
            #region Humans
            #region ladyChurch
            ladyChurch = new Dictionary<string, Message>();
            ladyChurch.Add("ladyChurch_1", new Message("ladyChurch_1", "I just love \ngoin' to church!"));
            ladyChurch.Add("ladyChurch_2", new Message("ladyChurch_2", "Don't you just love\nadmiring..."));
            ladyChurch.Add("ladyChurch_3", new Message("ladyChurch_3", "...the Lords Work?"));
            allDialogue.Add("ladyChurch", ladyChurch);
            #endregion
            #region manRock
            manRock = new Dictionary<string, Message>();
            manRock.Add("manRock_1", new Message("manRock_1", "Fancy earning a \nfew bucks?"));
            manRock.Add("manRock_2", new Message("manRock_2", "Just don't let \nthe law know!"));
            manRock.Add("manRock_3", new Message("manRock_3", "I hear the miners\nleft us some gold"));
            manRock.Add("manRock_4", new Message("manRock_4", "Maybe you could...\nstumble upon it?"));
            manRock.Add("manRock_5", new Message("manRock_5", "B To accept\nWalk away to decline"));
            allDialogue.Add("manRock", manRock);
            #endregion
            #region manRockQuestDone
            manRockQuestDone = new Dictionary<string, Message>();
            manRockQuestDone.Add("manRockQuestDone_1", new Message("manRockQuestDone_1", "Well, got it all?"));
            manRockQuestDone.Add("manRockQuestDone_2", new Message("manRockQuestDone_2", "Good.. very good\nI'll be in contact"));
            manRockQuestDone.Add("manRockQuestDone_3", new Message("manRockQuestDone_3", "Remember though,\nKeep quiet. Or else."));
            allDialogue.Add("manRockQuestDone", manRockQuestDone);
            #endregion
            #region manWindmill
            manWindmill = new Dictionary<string,Message>();
            manWindmill.Add("manWindmill_1", new Message("manWindmill_1", "Howdy there \npartner!"));
            manWindmill.Add("manWindmill_2", new Message("manWindmill_2", "What's rootin' \ntootin'?"));
            manWindmill.Add("manWindmill_3", new Message("manWindmill_3", "Watch out for them \nCoyotes 'n such!"));
            manWindmill.Add("manWindmill_4", new Message("manWindmill_4", "Say, have you \nseen my wife?"));
            allDialogue.Add("manWindmill", manWindmill);
            #endregion
            #region manCactus
            manCactus = new Dictionary<string, Message>();
            manCactus.Add("manCactus_1", new Message("manCactus_1", "¡Hola amigo!"));
            manCactus.Add("manCactus_2", new Message("manCactus_2", "My Village is not\ntoo far from here!"));
            manCactus.Add("manCactus_3", new Message("manCactus_3", "¿Maybe you can come\nsee it sometime?"));
            manCactus.Add("manCactus_4", new Message("manCactus_4", "B To accept\nWalk away to decline"));
            allDialogue.Add("manCactus", manCactus);
            #endregion
            #region manHouse
            manHouse = new Dictionary<string, Message>();
            manHouse.Add("manHouse_1", new Message("manHouse_1", "¿Did Juan send you?"));
            manHouse.Add("manHouse_2", new Message("manHouse_2", "¡Bueno!\n¡Welcome gringo!"));
            manHouse.Add("manHouse_3", new Message("manHouse_3", "We have plenty to do,\nTake a look around"));
            allDialogue.Add("manHouse", manHouse);
            #endregion
            #region manRockingHouse
            manRockingHouse = new Dictionary<string, Message>();
            manRockingHouse.Add("manRockingHouse_1", new Message("manRockingHouse_1", "You look like \na young fello!"));
            manRockingHouse.Add("manRockingHouse_2", new Message("manRockingHouse_2", "Maybe you could \nhelp me..."));
            manRockingHouse.Add("manRockingHouse_3", new Message("manRockingHouse_3", "...I gots me some \nmissing cows!"));
            allDialogue.Add("manRockingHouse", manRockingHouse);
            #endregion
            #region Sheriff Garett
            sheriffGarett = new Dictionary<string, Message>();
            sheriffGarett.Add("sheriffGarett_1", new Message("sheriffGarett_1", "Greetings stranger..\nFancy a badge?"));
            sheriffGarett.Add("sheriffGarett_2", new Message("sheriffGarett_2", "Bandits are common\nin these parts"));
            sheriffGarett.Add("sheriffGarett_3", new Message("sheriffGarett_3", "Prove your worth\nshooting and I'll..."));
            sheriffGarett.Add("sheriffGarett_4", new Message("sheriffGarett_4", "...Make you my\n next deputy"));
            sheriffGarett.Add("sheriffGarett_5", new Message("sheriffGarett_5", "B To accept\nWalk away to decline"));
            allDialogue.Add("sheriffGarett", sheriffGarett);
            #endregion
            #region sheriffGarettQuestDone
            sheriffGarettQuestDone = new Dictionary<string, Message>();
            sheriffGarettQuestDone.Add("sheriffGarettQuestDone_1", new Message("sheriffGarettQuestDone_1", "Well I'll be darned!\nNice shooting sport!"));
            sheriffGarettQuestDone.Add("sheriffGarettQuestDone_2", new Message("sheriffGarettQuestDone_2", "*Hands you a badge*\nWelcome Deputy!"));
            sheriffGarettQuestDone.Add("sheriffGarettQuestDone_3", new Message("sheriffGarettQuestDone_3", "I'll contact you if\nI need your help!"));
            allDialogue.Add("sheriffGarettQuestDone", sheriffGarettQuestDone);
            #endregion
            #region testQuestNPC
            testQuestNPC = new Dictionary<string, Message>();
            testQuestNPC.Add("testQuestNPC_1", new Message("testQuestNPC_1", "Hey there! \nFancy a quest?!"));
            allDialogue.Add("testQuestNPC", testQuestNPC);
            #endregion
            #endregion
            #region Animals
            #region Cow
            cow = new Dictionary<string, Message>();
            cow.Add("cow_1", new Message("cow_1", "Mooooo!"));
            cow.Add("cow_2", new Message("cow_2", "Moo!"));
            cow.Add("cow_3", new Message("cow_3", "MooOOOooo!"));
            cow.Add("cow_4", new Message("cow_4", "*chews*"));
            allDialogue.Add("cow", cow);
            #endregion
            #endregion
            #region Signs lol
            signToJoes = new Dictionary<string, Message>();
            signToJoes.Add("signToJoes_1", new Message("signToJoes_1", "Have you done all\nThe quests here?"));
            signToJoes.Add("signToJoes_2", new Message("signToJoes_2", "If so, I will take\nYou to 'Ol Joes"));
            signToJoes.Add("signToJoes_3", new Message("signToJoes_3", "You can't come back!\nP.S He's crazy"));
            signToJoes.Add("signToJoes_4", new Message("signToJoes_4", "B To accept\nWalk away to decline"));
            allDialogue.Add("signToJoes", signToJoes);
            #endregion
            #endregion
        }

        public Dictionary<string, Dictionary<string, Message>> AllDialogue
        {
            get
            {
                return allDialogue;
            }
        }
    }
}
