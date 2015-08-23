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
    public class Objects
    {
        private Dictionary<string, Dictionary<string, Object>> allObjects;

        private Dictionary<string, Object> testQuestObjects, testShootingObjects;

        public Objects()
        {
            allObjects = new Dictionary<string, Dictionary<string, Object>>();

            #region TestQuestObjects
            testQuestObjects = new Dictionary<string, Object>();
            testQuestObjects.Add("nuggets1", new Collectable("nuggets", new Vector2(50, 108)));
            testQuestObjects.Add("nuggets2", new Collectable("nuggets", new Vector2(156, 108)));
            testQuestObjects.Add("nuggets3", new Collectable("nuggets", new Vector2(310, 108)));
            testQuestObjects.Add("nuggets4", new Collectable("nuggets", new Vector2(450, 108)));
            testQuestObjects.Add("nuggets5", new Collectable("nuggets", new Vector2(620, 108)));
            allObjects.Add("testQuestObjects", testQuestObjects);
            #endregion

            #region TestShootingObjects
            testShootingObjects = new Dictionary<string, Object>();
            testShootingObjects.Add("target1", new Shootable("target", new Vector2(34, 92)));
            testShootingObjects.Add("target2", new Shootable("target", new Vector2(129, 44)));
            testShootingObjects.Add("target3", new Shootable("target", new Vector2(307, 92)));
            testShootingObjects.Add("target4", new Shootable("target", new Vector2(421, 92)));
            testShootingObjects.Add("target5", new Shootable("target", new Vector2(519, 92)));
            allObjects.Add("testShootingObjects", testShootingObjects);
            #endregion
        }

        public Dictionary<string, Dictionary<string, Object>> AllObjects
        {
            get
            {
                return allObjects;
            }
        }
    }
}
