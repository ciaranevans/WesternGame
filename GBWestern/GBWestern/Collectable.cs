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
    public class Collectable : Object
    {
        private Animation button;

        private bool showBut, collected;

        public Collectable(string name, Vector2 pos)
            : base(name, pos)
        {
            this.button = new Animation(32, 32, 11);
            this.showBut = false;
            this.collected = false;
        }

        public override void LoadContent(ContentManager Content)
        {
            base.LoadContent(Content);
            button.SpriteSheet = Content.Load<Texture2D>("Textures\\buttonB");
        }

        public override void Update(GameTime gT, Player p, GamePadState gP, KeyboardState kB, Game1 game, CollectionQuest lvl)
        {
            if (!collected)
            {
                gP = GamePad.GetState(PlayerIndex.One);
                kB = Keyboard.GetState();
                base.Update(gT, p, gP, kB, game, lvl);
                #region ButtonPrompt
                Vector2 vect = base.ObPos - p.PlayerPos;
                float dist = vect.Length();
                if (dist <= 10.0f)
                {
                    showBut = true;
                    if (gP.Buttons.B == ButtonState.Pressed || kB.IsKeyDown(Keys.B))
                    {
                        collected = true;
                        lvl.ColCount++;
                        Debug.WriteLine("Collected Items: " + lvl.ColCount);
                    }
                }
                else
                {
                    showBut = false;
                }
                #endregion
            }
        }

        public override void Draw(SpriteBatch sB, GameTime gT)
        {
            if (!collected)
            {
                base.Draw(sB, gT);
                if (showBut)
                {
                    button.Draw(sB, gT, new Vector2(base.ObPos.X, base.ObPos.Y + 30.0f));
                }
            }
        }
    }
}
