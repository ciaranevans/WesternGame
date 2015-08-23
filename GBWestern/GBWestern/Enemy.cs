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
    public class Enemy
    {
        private Vector2 enemyPos;
        private float attackDist;
        private string direction, type;
        private Animation idleL, idleR, runL, runR, current;

        public Enemy(Vector2 pos, string type)
        {
            this.enemyPos = pos;
            this.direction = "RIGHT";
            this.type = type;
            this.idleL = new Animation(32, 32, 22);
            this.idleR = new Animation(32, 32, 22);
            this.runL = new Animation(32, 32, 6);
            this.runR = new Animation(32, 32, 6);
            this.current = idleR;
        }

        public void LoadContent(ContentManager Content)
        {
            switch (type)
            {
                case "BISON":
                idleL.SpriteSheet = Content.Load<Texture2D>("Textures\\bisonIdleL");
                idleR.SpriteSheet = Content.Load<Texture2D>("Textures\\bisonIdleR");
                runL.SpriteSheet = Content.Load<Texture2D>("Textures\\bisonRunL");
                runR.SpriteSheet = Content.Load<Texture2D>("Textures\\bisonRunR");
                    break;
                case "COYOTE":
                idleL.SpriteSheet = Content.Load<Texture2D>("Textures\\coyoteIdleL");
                idleR.SpriteSheet = Content.Load<Texture2D>("Textures\\coyoteIdleR");
                runL.SpriteSheet = Content.Load<Texture2D>("Textures\\coyoteRunL");
                runR.SpriteSheet = Content.Load<Texture2D>("Textures\\coyoteRunR");
                break;
                default:
                    break;
            }
        }

        public void Update(GameTime gT, Player p)
        {
            Vector2 vect = enemyPos - p.PlayerPos;
            float dist = vect.Length();
            vect.Normalize();

            if (dist < 50.0f  && dist > 25.0f)
            {
                if (vect.X < 0)
                {
                    current = runR;
                    direction = "RIGHT";
                    enemyPos.X++;
                }
                else
                {
                    current = runL;
                    direction = "LEFT";
                    enemyPos.X--;
                }
            }
            else
            {
                if (direction == "LEFT")
                {
                    current = idleL;
                }
                else if (direction == "RIGHT")
                {
                    current = idleR;
                }
            }

        }

        public void Draw(SpriteBatch sB, GameTime gT)
        {
            current.Draw(sB, gT, enemyPos);
        }
    }
}
