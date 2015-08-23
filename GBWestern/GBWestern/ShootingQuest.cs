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
    public class ShootingQuest : Level
    {
        private bool isComplete;
        private int hitReq, hitCount;
        private Dictionary<string, Object> targets;
        private ShootingManager sM;
        private Texture2D revolver;
        private GamePadState prev;
        private KeyboardState kBP;

        public ShootingQuest(string name, bool clouds, Dictionary<string, NPC> npcs, Dictionary<string, Object> objects)
            : base(name, clouds, npcs)
        {
            this.isComplete = false;
            this.hitReq = objects.Count();
            this.hitCount = 0;
            this.targets = objects;
            this.sM = new ShootingManager();
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            foreach (KeyValuePair<string, Object> tgt in targets)
            {
                tgt.Value.LoadContent(Content);
            }
            revolver = Content.Load<Texture2D>("Textures\\revolverFPS");
            sM.LoadContent(Content);
        }

        public override void Update(GameTime gT, Player p, GamePadState gP, KeyboardState kB, Game1 game, Matrix cam)
        {
            base.Update(gT, p, gP, kB, game, cam);
            gP = GamePad.GetState(PlayerIndex.One);
            kB = Keyboard.GetState();

            sM.Update(gT, p, gP, kB, this, game, targets, cam);
            if (hitCount == hitReq)
            {
                isComplete = true;
            }
            if (isComplete == true && ((gP.Buttons.B == ButtonState.Pressed && prev.Buttons.B == ButtonState.Released) || kB.IsKeyDown(Keys.B) &&
                kBP.IsKeyUp(Keys.B)))
            {
                p.PlayerPos = new Vector2(41, 108);
                game.Index = 1;
            }

            prev = gP;
            kBP = kB;
        }

        public override void Draw(GameTime gT, SpriteBatch sB, Matrix cam)
        {
            base.Draw(gT, sB, cam);
            foreach (KeyValuePair<string, Object> tgt in targets)
            {
                tgt.Value.Draw(sB, gT);
            }
            sM.Draw(sB, gT, cam);
            Vector2 pos = new Vector2(0, 0);
            pos = Vector2.Transform(pos, Matrix.Invert(cam));
            Vector2 pos2 = new Vector2(10, 4);
            pos2 = Vector2.Transform(pos2, Matrix.Invert(cam));
            if (isComplete)
            {
                sB.Draw(base.TextBox, pos, Color.White);
                sB.DrawString(Font, "All Targets Hit\nB To Leave", new Vector2(pos2.X,pos2.Y + 10), new Color(139, 172, 15, 0));
                sB.DrawString(Font, "QUEST COMPLETE:", pos2, new Color(48, 98, 48, 0));
            }
        }

        public int HitReq
        {
            get
            {
                return hitReq;
            }
        }

        public int HitCount
        {
            set
            {
                hitCount = value;
            }
            get
            {
                return hitCount;
            }
        }
    }
}
