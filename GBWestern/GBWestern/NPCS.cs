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
    public class NPCS
    {
        private Dictionary<string, Dictionary<string, NPC>> allNPCS;
        private Dictionary<string, NPC> area1, area2, area3, area5;
        private Dialogue dialogue = new Dialogue();
        private Dictionary<string, Dictionary<string, Message>> allDialogue;

        public NPCS()
        {
            allNPCS = new Dictionary<string, Dictionary<string, NPC>>();
            allDialogue = dialogue.AllDialogue;

            #region NPC Dictionaries Initialising
            #region area1
            area1 = new Dictionary<string, NPC>();
            area1.Add("manWindmill", new NPC("manWindmill", "Fred McGraw", "MAN", false, 0, new Vector2(280, 108), allDialogue["manWindmill"], null));
            area1.Add("manRock", new NPC("manRock", "Shady Fellow", "MAN", true, 3, new Vector2(193, 108),
                allDialogue["manRock"], allDialogue["manRockQuestDone"]));
            area1.Add("manCactus", new NPC("manCactus", "Juan Gonzalez", "MANMEXICAN", true, 5, new Vector2(471, 108), allDialogue["manCactus"], null));
            area1.Add("sheriffGarett", new NPC("sheriffGarett", "Sheriff Garett", "MAN", true, 4, new Vector2(570, 108),
                allDialogue["sheriffGarett"], allDialogue["sheriffGarettQuestDone"]));
            area1.Add("ladyChurch", new NPC("ladyChurch", "Mary McGraw", "LADY", false, 0, new Vector2(387, 108), allDialogue["ladyChurch"], null));
            area1.Add("cow", new NPC("cow", "Daisy", "COW", false, 0, new Vector2(87, 108), allDialogue["cow"], null));
            area1.Add("signToJoes", new NPC("signToJoes", "Sign", "SIGN", true, 2, new Vector2(630, 108), allDialogue["signToJoes"], null));
            allNPCS.Add("area1", area1);
            #endregion
            #region area2
            area2 = new Dictionary<string, NPC>();
            area2.Add("manRockingHouse", new NPC("manRockingHouse", "'Ol Joe", "MANROCKING", false, 0, new Vector2(87, 108), allDialogue["manRockingHouse"], null));
            area2.Add("cow1", new NPC("cow1", "COW", new Vector2(269, 108)));
            area2.Add("cow2", new NPC("cow2", "COW", new Vector2(300, 108)));
            area2.Add("cow3", new NPC("cow3", "COW", new Vector2(407, 108)));
            area2.Add("cow4", new NPC("cow4", "COW", new Vector2(457, 108)));
            area2.Add("cow5", new NPC("cow", "Buttercup", "COW", false, 0, new Vector2(519, 108), allDialogue["cow"], null));
            allNPCS.Add("area2", area2);
            #endregion
            #region area3
            area3 = new Dictionary<string, NPC>();
            //area3.Add("testQuestNPC", new NPC("testQuestNPC", "testQuestNPC", "MAN", true, 0, new Vector2(200, 108), allDialogue["testQuestNPC"]));
            allNPCS.Add("area3", area3);
            #endregion
            #region area5
            area5 = new Dictionary<string, NPC>();
            area5.Add("manHouse", new NPC("manHouse", "Carlos Morales", "MANMEXICAN", false, 0, new Vector2(60, 105), allDialogue["manHouse"], null));
            allNPCS.Add("area5", area5);
            #endregion
            #endregion
        }

        public Dictionary<string, Dictionary<string, NPC>> AllNPCS
        {
            get
            {
                return allNPCS;
            }
        }
    }
}
