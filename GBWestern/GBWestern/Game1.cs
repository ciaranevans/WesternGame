using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace GBWestern
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        #region Variables
        private GraphicsDeviceManager graphics;
        private GamePadState gP, prev;
        private KeyboardState kB, kBP;
        private SpriteBatch spriteBatch;
        private Texture2D title;
        private Player player;
        private Camera cam = new Camera();
        private NPCS npcs = new NPCS();
        private Objects objects = new Objects();
        private Dictionary<string, Dictionary<string, Object>> allObjects;
        private Dictionary<string, Dictionary<string, NPC>> allNPCS;
        private Dictionary<string, Level> Levels;
        private Level area1, area2, area5;
        private CollectionQuest area3;
        private ShootingQuest area4;
        private int index = 1;
        public Animation portrait;
        public bool nearNPC;
        private float scaleX = 320 / 160;
        private float scaleY = 288 / 144;
        private float scale;
        private Matrix view;

        #endregion 

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {            
            graphics.PreferredBackBufferHeight = 288;
            graphics.PreferredBackBufferWidth = 320;
            graphics.ApplyChanges();
            scale = MathHelper.Min(scaleX, scaleY);
            #region Levels
            allNPCS = npcs.AllNPCS;
            allObjects = objects.AllObjects;
            Levels = new Dictionary<string, Level>();
            area1 = new Level("area1", true, allNPCS["area1"]);
            area2 = new Level("area2", true, allNPCS["area2"]);
            area3 = new CollectionQuest("area3", false, allNPCS["area3"], allObjects["testQuestObjects"]);
            area4 = new ShootingQuest("area1", true, new Dictionary<string, NPC>(), allObjects["testShootingObjects"]);
            area5 = new Level("area4", true, allNPCS["area5"]);
            Levels.Add("area1", area1);
            Levels.Add("area2", area2);
            Levels.Add("area3", area3);
            Levels.Add("area4", area4);
            Levels.Add("area5", area5);
            #endregion
            player = new Player(new Vector2(41, 108));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (KeyValuePair<string, Level> level in Levels)
            {
                level.Value.LoadContent(Content);
            }
            player.LoadContent(Content);
            title = Content.Load<Texture2D>("Textures\\Title");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            gP = GamePad.GetState(PlayerIndex.One);
            #region SwitchLevel
            //if (gP.Buttons.LeftShoulder == ButtonState.Pressed && prev.Buttons.LeftShoulder != ButtonState.Pressed)
            //{
            //    index++;
            //    if (index > Levels.Count)
            //    {
            //        index = 1;
            //    }
            //}
            #endregion
            player.Update(gP, gameTime, kB);
            cam.Update(player.PlayerPos);
            view = Matrix.CreateScale(scale, scale, 1) * cam.ViewMatrix;
            Levels["area" + index].Update(gameTime, player, gP, kB, this, view);
            prev = gP;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, view);
            Levels["area" + index].Draw(gameTime, spriteBatch, view);
            //spriteBatch.Draw(title, new Vector2(9,120), Color.White);
            player.Draw(spriteBatch, gameTime);
            #region Portrait
            Vector2 pos = new Vector2(192, 288);
            pos = Vector2.Transform(pos, Matrix.Invert(view));
            if (nearNPC && portrait != null && portrait.SpriteSheet != null)
            {
                portrait.Draw(spriteBatch, gameTime, pos);
            }
            #endregion
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public int Index
        {
            set
            {
                index = value;
            }
        }
    }
}
