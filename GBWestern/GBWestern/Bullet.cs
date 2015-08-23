using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GBWestern
{
    public class Bullet
    {
        private Vector2 pos;
        private Texture2D texture;

        public Bullet(Vector2 pos)
        {
            this.pos = pos;
        }

        public Texture2D Texture
        {
            set
            {
                texture = value;
            }
            get
            {
                return texture;
            }
        }

        public Vector2 Pos
        {
            get
            {
                return pos;
            }
        }
    }
}
