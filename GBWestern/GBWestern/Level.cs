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
    public class Level
    {
        private string name;
        private bool cloudsDraw;
        private Dictionary<string, NPC> npcs;
        private Texture2D background;
        private Texture2D textBox;
        private Texture2D clouds;
        private SpriteFont font;
        private Vector2 cloudsPos = new Vector2(0, 0);
        private Vector2 clouds2Pos = new Vector2(640, 0);

        public Level(string name, bool clouds, Dictionary<string, NPC> npcs)
        {
            this.name = name;
            this.npcs = npcs;
            this.cloudsDraw = clouds;
        }

        public virtual void LoadContent(ContentManager Content)
        {
            background = Content.Load<Texture2D>("Textures\\" + name);
            clouds = Content.Load<Texture2D>("Textures\\clouds");
            textBox = Content.Load<Texture2D>("Textures\\dialogueBox");
            font = Content.Load<SpriteFont>("Fonts\\NPCFont");
            foreach (KeyValuePair<string, NPC> npc in npcs)
            {
                npc.Value.LoadContent(Content);
            }
        }

        public virtual void Update(GameTime gT, Player p, GamePadState gP, KeyboardState kB, Game1 game, Matrix cam)
        {
            #region Clouds
            if (cloudsPos.X < -640)
            {
                cloudsPos.X = 0;
            }
            if (clouds2Pos.X < 0)
            {
                clouds2Pos.X = 640;
            }

            cloudsPos.X = cloudsPos.X - 0.5f;
            clouds2Pos.X = clouds2Pos.X - 0.5f;
            #endregion
            foreach (KeyValuePair<string, NPC> npc in npcs)
            {
                npc.Value.Update(gT, p, gP, kB, game);
            }
            game.nearNPC = CharacterTalking(npcs);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sB, Matrix cam)
        {
            sB.Draw(background, new Rectangle(0, 0, 640, 144), Color.White);
            if (cloudsDraw == true)
            {
                sB.Draw(clouds, cloudsPos, Color.White);
                sB.Draw(clouds, clouds2Pos, Color.White);
            }
            foreach (KeyValuePair<string, NPC> npc in npcs)
            {
                npc.Value.Draw(sB, gameTime, cam);
            }
        }

        private bool CharacterTalking(Dictionary<string, NPC> npcs)
        {

            foreach (KeyValuePair<string, NPC> npc in npcs)
            {
                if (npc.Value.Talking == true)
                {
                    return true;
                }
            }

            return false;
        }

        public Texture2D TextBox
        {
            get
            {
                return textBox;
            }
        }

        public SpriteFont Font
        {
            get
            {
                return font;
            }
        }
    }
}
