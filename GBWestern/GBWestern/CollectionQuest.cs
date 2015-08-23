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
    public class CollectionQuest : Level
    {
        private bool isComplete;
        private int colReq;
        private int colCount;
        private Dictionary<string, Object> objects;
        private GamePadState prev;
        private KeyboardState kBP;

        public CollectionQuest(string name, bool clouds, Dictionary<string, NPC> npcs, Dictionary<string, Object> objects)
            : base(name, clouds, npcs)
        {
            this.isComplete = false;
            this.colReq = objects.Count();
            this.colCount = 0;
            this.objects = objects;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            foreach (KeyValuePair<string, Object> obj in objects)
            {
                obj.Value.LoadContent(Content);
            }
        }

        public override void Update(GameTime gT, Player p, GamePadState gP, KeyboardState kB, Game1 game, Matrix cam)
        {
            base.Update(gT, p, gP, kB, game, cam);
            gP = GamePad.GetState(PlayerIndex.One);
            kB = Keyboard.GetState();

            if (isComplete && ((gP.Buttons.B == ButtonState.Pressed && prev.Buttons.B == ButtonState.Released)||
                kB.IsKeyDown(Keys.B) && kBP.IsKeyUp(Keys.B)))
            {
                p.PlayerPos = new Vector2(41, 108);
                game.Index = 1;
            }
            foreach (KeyValuePair<string, Object> obj in objects)
            {
                obj.Value.Update(gT, p, gP, kB, game, this);
            }
            if (colCount == colReq)
            {
                isComplete = true;
            }
            prev = gP;
            kBP = kB;
        }

        public override void Draw(GameTime gT, SpriteBatch sB, Matrix cam)
        {
            base.Draw(gT, sB, cam);
            foreach (KeyValuePair<string, Object> obj in objects)
            {
                obj.Value.Draw(sB, gT);
            }
            Vector2 pos = new Vector2(0,0);
            pos = Vector2.Transform(pos, Matrix.Invert(cam));
            Vector2 pos2 = new Vector2(10, 4);
            pos2 = Vector2.Transform(pos2, Matrix.Invert(cam));
            if (isComplete)
            {
                sB.Draw(base.TextBox, pos, Color.White);
                sB.DrawString(Font, "Gold Stolen\nB To Leave", new Vector2(pos2.X, pos2.Y + 10), new Color(139, 172, 15, 0));
                sB.DrawString(Font, "QUEST COMPLETE:", pos2, new Color(48, 98, 48, 0));
            }
        }

        public int ColReq
        {
            get
            {
                return colReq;
            }
        }

        public int ColCount
        {
            set
            {
                colCount = value;
            }
            get
            {
                return colCount;
            }
        }
    }
}
