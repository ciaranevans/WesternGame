using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GBWestern
{
    public class Animation
    {
        private Texture2D spriteSheet;
        private Vector2 origin;
        private float time;
        private float frameTime = 0.1f;
        private int frameHeight;
        private int frameWidth;
        private int frameIndex;
        private int totalFrames;

        public Animation(int w, int h, int f)
        {
            this.frameWidth = w;
            this.frameHeight = h;
            this.totalFrames = f;
            this.origin = new Vector2(frameWidth / 2.0f, frameHeight);
        }

        public Animation(int w, int h, int f, float fT)
        {
            this.frameTime = fT;
            this.frameWidth = w;
            this.frameHeight = h;
            this.totalFrames = f;
            this.origin = new Vector2(frameWidth / 2.0f, frameHeight);
        }

        public Texture2D SpriteSheet
        {
            set
            {
                spriteSheet = value;
            }
            get
            {
                return spriteSheet;
            }
        }

        public void Draw(SpriteBatch sB, GameTime gT, Vector2 pos)
        {
            time += (float)gT.ElapsedGameTime.TotalSeconds;

            while (time > frameTime)
            {
                frameIndex++;
                time = 0f;
            }

            if (frameIndex >= totalFrames)
            {
                frameIndex = 0;
            }

            Rectangle source = new Rectangle(frameIndex * frameWidth, 0, frameWidth, frameHeight);

            sB.Draw(spriteSheet, pos, source, Color.White, 0.0f, origin, 1.0f, SpriteEffects.None, 0.0f);
        }

        public int FrameIndex
        {
            set
            {
                frameIndex = value;
            }
            get
            {
                return frameIndex;
            }
        }
    }
}
