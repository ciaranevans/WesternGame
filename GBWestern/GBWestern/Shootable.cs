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
    public class Shootable : Object
    {
        private string name;
        private Vector2 pos;
        private Texture2D notHit, hit;
        private bool isHit;
        
        public Shootable(string name, Vector2 pos)
            : base(name, pos)
        {
            this.name = name;
            this.pos = pos;
            this.isHit = false;
        }

        public override void LoadContent(ContentManager Content)
        {
            notHit = Content.Load<Texture2D>("Textures\\" + name);
            hit = Content.Load<Texture2D>("Textures\\" + name + "Hit");
        }

        public override void Update(GameTime gT, Player p, GamePadState gP, KeyboardState kB, Game1 game, ShootingQuest lvl, Vector2 mP)
        {
            Vector2 vect = pos - mP;
            float dist = vect.Length();
            if (dist <= 5.0f && !isHit)
            {
                isHit = true;
                lvl.HitCount++;
            }
        }

        public override void Draw(SpriteBatch sB, GameTime gT)
        {
            if (isHit)
            {
                sB.Draw(hit, pos, Color.White);
            }
            else
            {
                sB.Draw(notHit, pos, Color.White);
            }
        }
    }
}
