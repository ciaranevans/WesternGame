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
    public class Camera
    {
        private Vector2 pos;
        private Matrix viewMatrix;

        public Matrix ViewMatrix
        {
            get
            {
                return viewMatrix;
            }
        }

        public Vector2 Pos
        {
            get
            {
                return pos;
            }
        }

        public void Update(Vector2 playerPos)
        {
            pos.X = playerPos.X - (float)(320 / 3);

            if (pos.X < 0)
            {
                pos.X = 0;
            }

            if (playerPos.X < 587)
            {
                viewMatrix = Matrix.CreateTranslation(new Vector3(2 * -pos, 0));
            }
        }
    }
}
