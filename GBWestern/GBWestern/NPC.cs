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
    public class NPC
    {
        private Vector2 npcPos;
        private string id, type, name;
        private int msgCount, questAreaIndex;
        private Animation idleL, idleR, current, qCurrent, questL,
            questR, button, portraitL;
        private Texture2D dialogueBox;
        private bool showPrompt, showQuest, talking;
        private GamePadState prev;
        private KeyboardState kBP;
        private SpriteFont font;
        private Dictionary<string, Message> dialogue, questFinished;

        public NPC(string id, string name, string type, bool quest, int questIndex, Vector2 pos,
            Dictionary<string, Message> dialogue, Dictionary<string, Message> questFinished)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.npcPos = pos;
            this.dialogue = dialogue;
            this.questFinished = questFinished;
            this.idleL = new Animation(32, 32, 22);
            this.idleR = new Animation(32, 32, 22);
            this.questL = new Animation(32, 32, 18);
            this.questR = new Animation(32, 32, 18);
            this.button = new Animation(32, 32, 11);
            this.portraitL = new Animation(128, 128, 11);
            this.current = idleL;
            this.qCurrent = questL;
            this.msgCount = 1;
            this.showPrompt = false;
            this.talking = false;
            this.showQuest = quest;
            this.questAreaIndex = questIndex;
        }

        public NPC(string id, string type, Vector2 pos)
        {
            this.id = id;
            this.type = type;
            this.npcPos = pos;
            this.idleL = new Animation(32, 32, 22);
            this.idleR = new Animation(32, 32, 22);
            this.questL = new Animation(32, 32, 18);
            this.questR = new Animation(32, 32, 18);
            this.button = new Animation(32, 32, 11);
            this.portraitL = new Animation(128, 128, 11);
            this.msgCount = 1;
            this.current = idleL;
            this.qCurrent = questL;
        }

        public void LoadContent(ContentManager Content)
        {
            #region Misc
            questL.SpriteSheet = Content.Load<Texture2D>("Textures\\questL");
            questR.SpriteSheet = Content.Load<Texture2D>("Textures\\questR");
            button.SpriteSheet = Content.Load<Texture2D>("Textures\\buttonB");
            dialogueBox = Content.Load<Texture2D>("Textures\\dialogueBox");
            font = Content.Load<SpriteFont>("Fonts\\DialogueFont");
            #endregion
            #region NPC
            switch (type)
            {
                case "MAN":
                    idleL.SpriteSheet = Content.Load<Texture2D>("Textures\\manIdleL");
                    idleR.SpriteSheet = Content.Load<Texture2D>("Textures\\manIdleR");
                    #region MAN Character Portraits
                    switch (name)
                    {
                        case "Shady Fellow":
                            portraitL.SpriteSheet = Content.Load<Texture2D>("Textures\\shadyFellowPortraitL");
                            break;
                        default:
                            break;
                    }
                    #endregion
                    break;
                case "MANROCKING":
                    idleL.SpriteSheet = Content.Load<Texture2D>("Textures\\manRockingL");
                    idleR.SpriteSheet = Content.Load<Texture2D>("Textures\\manRockingR");
                    #region MANROCKING Character Portraits
                    switch (name)
                    {
                        case "'Ol Joe":
                            portraitL.SpriteSheet = Content.Load<Texture2D>("Textures\\olJoePortraitL");
                            break;
                        default:
                            break;
                    }
                    #endregion
                    break;
                case "MANMEXICAN":
                    idleL.SpriteSheet = Content.Load<Texture2D>("Textures\\manMexicanIdleL");
                    idleR.SpriteSheet = Content.Load<Texture2D>("Textures\\manMexicanIdleR");
                    break;
                case "LADY":
                    idleL.SpriteSheet = Content.Load<Texture2D>("Textures\\ladyIdleL");
                    idleR.SpriteSheet = Content.Load<Texture2D>("Textures\\ladyIdleR");
                    break;
                case "COW":
                    idleL.SpriteSheet = Content.Load<Texture2D>("Textures\\cowIdleL");
                    idleR.SpriteSheet = Content.Load<Texture2D>("Textures\\cowIdleR");
                    portraitL.SpriteSheet = Content.Load<Texture2D>("Textures\\cowPortraitL");
                    break;
                case "COYOTE":
                    idleL.SpriteSheet = Content.Load<Texture2D>("Textures\\coyoteIdleL");
                    idleR.SpriteSheet = Content.Load<Texture2D>("Textures\\coyoteIdleR");
                    break;
                case "SIGN":
                    idleL.SpriteSheet = Content.Load<Texture2D>("Textures\\signIdle");
                    idleR.SpriteSheet = Content.Load<Texture2D>("Textures\\signIdle");
                    break;
                default:
                    break;
            }
            #endregion
        }

        public void Update(GameTime gT, Player p, GamePadState gP, KeyboardState kB, Game1 game)
        {
            gP = GamePad.GetState(PlayerIndex.One);
            kB = Keyboard.GetState();
            Vector2 vect = npcPos - p.PlayerPos;
            float dist = vect.Length();
            vect.Normalize();

            #region FacingPlayer && Dialogue
            if (dist < 20.0f)
            {
                if (dialogue != null)
                {
                    showPrompt = true;
                    if ((gP.Buttons.B == ButtonState.Pressed && prev != gP) || (kB.IsKeyDown(Keys.B) && kBP.IsKeyUp(Keys.B)))
                    {
                        talking = true;
                        game.portrait = portraitL;
                        msgCount++;
                        if (!showQuest && msgCount > dialogue.Count() + 1)
                        {
                            //msgCount = 1;
                            talking = false;
                            showQuest = false;
                            showPrompt = false;
                            dialogue = null;
                        }
                        else if (showQuest && msgCount - 2 == dialogue.Count())
                        {
                            talking = false;
                            showQuest = false;
                            showPrompt = false;
                            dialogue = questFinished;
                            if (questAreaIndex != 0)
                            {
                                p.PlayerPos = new Vector2(10, 108);
                                game.Index = questAreaIndex;
                            }
                        }
                    }
                }

                if (vect.X < 0)
                {
                    current = idleR;
                    qCurrent = questR;
                }
                else
                {
                    current = idleL;
                    qCurrent = questL;
                }
            }
            else
            {
                showPrompt = false;
                talking = false;
                msgCount = 1;
            }

            prev = gP;
            kBP = kB;
            #endregion
        }

        public void Draw(SpriteBatch sB, GameTime gT, Matrix cam)
        {
            current.Draw(sB, gT, npcPos);
            if (showQuest)
            {
                qCurrent.Draw(sB, gT, new Vector2(npcPos.X, npcPos.Y - 4.0f));
            }
            if (showPrompt)
            {
                button.Draw(sB, gT, new Vector2(npcPos.X, 138.0f));
            }
            if (talking)
            {
                int count = msgCount - 1;
                if (count < 1)
                {
                    count = dialogue.Count;
                }
                Vector2 pos = new Vector2(10, 24);
                pos = Vector2.Transform(pos, Matrix.Invert(cam));
                Vector2 pos2 = new Vector2(0, 0);
                pos2 = Vector2.Transform(pos2, Matrix.Invert(cam));
                sB.Draw(dialogueBox, pos2, Color.White);
                sB.DrawString(font, name + ":", new Vector2(pos.X, pos.Y - 10), new Color(48, 98, 48, 0));
                if (dialogue != questFinished)
                {
                    sB.DrawString(font, dialogue[id + "_" + count].Text, pos, new Color(139, 172, 15, 0));
                }
                else
                {
                    sB.DrawString(font, dialogue[id + "QuestDone_" + count].Text, pos, new Color(139, 172, 15, 0));
                }
            }
        }

        public bool Talking
        {
            get
            {
                return talking;
            }
        }

        public Animation Portrait
        {
            get
            {
                return portraitL;
            }
        }
    }
}
