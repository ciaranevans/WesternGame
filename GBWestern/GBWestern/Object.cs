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
    public class Object
    {
        private Vector2 obPos;
        private Animation idle;
        private string name;

        public Object(string name, Vector2 pos)
        {
            this.name = name;
            this.obPos = pos;
            idle = new Animation(32, 32, 10);
        }

        public virtual void LoadContent(ContentManager Content)
        {
            idle.SpriteSheet = Content.Load<Texture2D>("Textures\\" + name + "idle");
        }

        public virtual void Update(GameTime gT, Player p, GamePadState gP, KeyboardState kB, Game1 game, CollectionQuest lvl)
        {
        }

        public virtual void Update(GameTime gT, Player p, GamePadState gP, KeyboardState kB, Game1 game, ShootingQuest lvl, Vector2 mP)
        {
        }

        public virtual void Draw(SpriteBatch sB, GameTime gT)
        {
            idle.Draw(sB, gT, obPos);
        }

        public Vector2 ObPos
        {
            get
            {return obPos;}
        }
    }
}
