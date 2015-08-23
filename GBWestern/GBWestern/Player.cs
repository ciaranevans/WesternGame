using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GBWestern
{
    public class Player
    {
        private Vector2 playerPos;
        private GamePadState prev;
        private KeyboardState kBP;
        private string direction;
        private bool isJumping, isFalling;
        private Animation idleL, idleR, runL, runR, current;
        private SoundEffect sndRun;
        private SoundEffectInstance sndEfRun;

        public Player(Vector2 pos)
        {
            this.playerPos = pos;
            direction = "RIGHT";
            isJumping = false;
            isFalling = false;
            this.idleL = new Animation(32, 32, 22);
            this.idleR = new Animation(32, 32, 22);
            this.runL = new Animation(32, 32, 6);
            this.runR = new Animation(32, 32, 6);
        }

        public void LoadContent(ContentManager Content)
        {
            idleL.SpriteSheet = Content.Load<Texture2D>("Textures\\horseIdleL");
            idleR.SpriteSheet = Content.Load<Texture2D>("Textures\\horseIdleR");
            runL.SpriteSheet = Content.Load<Texture2D>("Textures\\horseRunL");
            runR.SpriteSheet = Content.Load<Texture2D>("Textures\\horseRunR");
            #region Sounds
            sndRun = Content.Load<SoundEffect>("Sounds\\horseRun2");
            sndEfRun = sndRun.CreateInstance();
            sndEfRun.Volume = 0.5f;
            sndEfRun.Pitch = 0.1f;
            #endregion
            //idleL.SpriteSheet = Content.Load<Texture2D>("Textures\\cartIdleL");
            //idleR.SpriteSheet = Content.Load<Texture2D>("Textures\\cartIdleR");
            //runL.SpriteSheet = Content.Load<Texture2D>("Textures\\cartRunL");
            //runR.SpriteSheet = Content.Load<Texture2D>("Textures\\cartRunR");
        }

        public void Update(GamePadState gP, GameTime gT, KeyboardState kB)
        {
            Controls(gP, kB);
        }

        public void Draw(SpriteBatch sB, GameTime gT)
        {
            current.Draw(sB, gT, playerPos);
        }

        public Vector2 PlayerPos
        {
            set
            {
                playerPos = value;
            }
            get
            {
                return playerPos;
            }
        }

        private void Controls(GamePadState gP, KeyboardState kB)
        {
            gP = GamePad.GetState(PlayerIndex.One);
            kB = Keyboard.GetState();

            if (direction == "LEFT")
            {
                current = idleL;
            }
            else if (direction == "RIGHT")
            {
                current = idleR;
            }

            //if (gP.Buttons.B == ButtonState.Pressed)
            //{
            //    Debug.WriteLine(playerPos.X);
            //}

            if (gP.DPad.Left == ButtonState.Pressed || kB.IsKeyDown(Keys.A))
            {
                direction = "LEFT";
                current = runL;
                playerPos.X--;
                sndEfRun.Play();
            }
            if (gP.DPad.Right == ButtonState.Pressed || kB.IsKeyDown(Keys.D))
            {
                direction = "RIGHT";
                current = runR;
                playerPos.X++;
                sndEfRun.Play();
            }
            if (((gP.Buttons.A == ButtonState.Pressed) && prev.Buttons.A == ButtonState.Released && !isJumping && !isFalling) ||
                ((kB.IsKeyDown(Keys.Space))) && kBP.IsKeyUp(Keys.Space) && !isJumping && !isFalling) 
            {
                isJumping = true;
            }
            if (isJumping)
            {
                if (playerPos.Y > 102)
                {
                    playerPos.Y--;
                }
                else if (playerPos.Y > 100)
                {
                    playerPos.Y = playerPos.Y - 0.5f;
                }
                else
                {
                    isJumping = false;
                    isFalling = true;
                }
            }
            if (!isJumping)
            {
                if (playerPos.Y < 108 && isFalling == true)
                {
                    playerPos.Y++;
                }
                else
                {
                    isFalling = false;
                }
            }

            prev = gP;
            kBP = kB;
        }
    }
}
