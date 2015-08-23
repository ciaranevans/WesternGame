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
    public class ShootingManager
    {
        private Bullet b0, b1, b2, b3, b4, b5;
        private Vector2 revolverPos, fpsPos, bV0, bV1, bV2, bV3, bV4, bV5, mP, mPTarget;
        private Texture2D revolverTexture, bulletTexture;
        private MouseState mS, mSP;
        private Animation crosshair, revolverIdle, revolverShoot;
        private int bCount = 6;
        private bool shot;

        public ShootingManager()
        {
            this.shot = false;
            b0 = new Bullet(new Vector2(24, 6));
            b1 = new Bullet(new Vector2(42, 14));
            b2 = new Bullet(new Vector2(42, 34));
            b3 = new Bullet(new Vector2(24, 42));
            b4 = new Bullet(new Vector2(6, 34));
            b5 = new Bullet(new Vector2(6, 14));
            crosshair = new Animation(32, 32, 12);
            revolverIdle = new Animation(128, 128, 1);
            revolverShoot = new Animation(128, 128, 7, 0.025f);
        }

        public void LoadContent(ContentManager Content)
        {
            revolverTexture = Content.Load<Texture2D>("Textures\\revolver");
            crosshair.SpriteSheet = Content.Load<Texture2D>("Textures\\cursorTest2");
            bulletTexture = Content.Load<Texture2D>("Textures\\bullet");
            revolverIdle.SpriteSheet = Content.Load<Texture2D>("Textures\\revolverFPS");
            revolverShoot.SpriteSheet = Content.Load<Texture2D>("Textures\\revolverFire3");
            b0.Texture = bulletTexture;
            b1.Texture = bulletTexture;
            b2.Texture = bulletTexture;
            b3.Texture = bulletTexture;
            b4.Texture = bulletTexture;
            b5.Texture = bulletTexture;
        }

        public void Update(GameTime gT, Player p, GamePadState gP, KeyboardState kB, ShootingQuest lvl, Game1 game, Dictionary<string, Object> targets, Matrix cam)
        {
            #region Mouse
            mS = Mouse.GetState(game.Window);
            mP = new Vector2(mS.X, mS.Y);
            mPTarget = new Vector2(mS.X - 16, mS.Y - 32);
            mPTarget = Vector2.Transform(mPTarget, Matrix.Invert(cam));
            #endregion
            #region Shooting
            if (mS.LeftButton == ButtonState.Pressed && mSP.LeftButton == ButtonState.Released && !shot)
            {
                if (bCount > 0)
                {
                    foreach (KeyValuePair<string, Object> tgt in targets)
                    {
                        tgt.Value.Update(gT, p, gP, kB, game, lvl, mPTarget);
                    }
                    shot = true;
                    revolverShoot.FrameIndex = 0;
                    bCount--;
                    Debug.WriteLine("Bang!" + " Rounds Left: " + bCount);
                }
                else
                {
                    Debug.WriteLine("OUT OF AMMO");
                }
            }

            if (mS.RightButton == ButtonState.Pressed && bCount == 0)
            {
                bCount = 6;
                Debug.WriteLine("RELOAD");
            }
            #endregion
            mSP = mS;
        }

        public void Draw(SpriteBatch sB, GameTime gT, Matrix cam)
        {
            #region Draw Positions
            Vector2 crosshairP = mP;
            crosshairP = Vector2.Transform(new Vector2(crosshairP.X, crosshairP.Y + 16.0f), Matrix.Invert(cam));
            revolverPos = new Vector2(0, 0);
            revolverPos = Vector2.Transform(revolverPos, Matrix.Invert(cam));
            fpsPos = new Vector2(192, 288);
            fpsPos = Vector2.Transform(fpsPos, Matrix.Invert(cam));
            bV0 = b0.Pos;
            bV0 = Vector2.Transform(bV0, Matrix.Invert(cam));
            bV1 = b1.Pos;
            bV1 = Vector2.Transform(bV1, Matrix.Invert(cam));
            bV2 = b2.Pos;
            bV2 = Vector2.Transform(bV2, Matrix.Invert(cam));
            bV3 = b3.Pos;
            bV3 = Vector2.Transform(bV3, Matrix.Invert(cam));
            bV4 = b4.Pos;
            bV4 = Vector2.Transform(bV4, Matrix.Invert(cam));
            bV5 = b5.Pos;
            bV5 = Vector2.Transform(bV5, Matrix.Invert(cam));
            #endregion
            #region Draw Revolver
            sB.Draw(revolverTexture, revolverPos, Color.White);
            if (shot)
            {
                revolverShoot.Draw(sB, gT, fpsPos);
                if (revolverShoot.FrameIndex == 6)
                {
                    shot = false;
                }
            }
            else
            {
                revolverIdle.Draw(sB, gT, fpsPos);
            }
            #endregion
            #region Draw Bullets
            switch (bCount)
            {
                case 0:
                    break;
                case 1:
                    sB.Draw(b5.Texture, bV5, Color.White);
                    break;
                case 2:
                    sB.Draw(b5.Texture, bV5, Color.White);
                    sB.Draw(b4.Texture, bV4, Color.White);
                    break;
                case 3:
                    sB.Draw(b5.Texture, bV5, Color.White);
                    sB.Draw(b4.Texture, bV4, Color.White);
                    sB.Draw(b3.Texture, bV3, Color.White);
                    break;
                case 4:
                    sB.Draw(b5.Texture, bV5, Color.White);
                    sB.Draw(b4.Texture, bV4, Color.White);
                    sB.Draw(b3.Texture, bV3, Color.White);
                    sB.Draw(b2.Texture, bV2, Color.White);
                    break;
                case 5:
                    sB.Draw(b5.Texture, bV5, Color.White);
                    sB.Draw(b4.Texture, bV4, Color.White);
                    sB.Draw(b3.Texture, bV3, Color.White);
                    sB.Draw(b2.Texture, bV2, Color.White);
                    sB.Draw(b1.Texture, bV1, Color.White);
                    break;
                case 6:
                    sB.Draw(b5.Texture, bV5, Color.White);
                    sB.Draw(b4.Texture, bV4, Color.White);
                    sB.Draw(b3.Texture, bV3, Color.White);
                    sB.Draw(b2.Texture, bV2, Color.White);
                    sB.Draw(b1.Texture, bV1, Color.White);
                    sB.Draw(b0.Texture, bV0, Color.White);
                    break;
                default:
                    break;
            }
            #endregion
            #region Draw Crosshair
            crosshair.Draw(sB, gT, crosshairP);
            #endregion
        }
    }
}
