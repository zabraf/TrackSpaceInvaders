/* 
 * Project : TrackSpaceInvaders
 * Authors : Fabian Troller / Guntram Juling / Raphaël Lopes
 * Description : Space invaders controlled with head tracking(TrackIR) technology
 * File : Game1.cs
 * Date : 13.03.19
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace TrackSpaceInvaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private const int PITCH_LIMIT = 300;
        private const int YAW_LIMIT = 200;
        private const int DEFAULT_POS_Y = 400;
        private const int DEFAULT_PLAYER_SPEED = 5;
        private const int COOLDOWN_SHOT_PLAYER = 1000;
        private const int COOLDOWN_SHOT_ALIEN = 3500;

        public bool SpawnDone { get; set; } = false;
        public int AlienPreWaveAmount { get; set; } = 30;
        public int PlayerShootCD { get; set; }
        public int AliensShootCD { get; set; }
        public int GameWidth { get; set; }
        public static Random Rand { get; set; } = new Random();
        internal List<Laser> AlienLasers { get; set; } = new List<Laser>();
        internal List<Laser> PlayerLasers { get; set; } = new List<Laser>();
        public TimeSpan TimeElapsed { get; set; } = new TimeSpan();
        internal List<Alien> Aliens { get; set; } = new List<Alien>();
        internal List<Player> Players { get; set; } = new List<Player>();
        public SpriteBatch SpriteBatch { get; set; }
        public GraphicsDeviceManager Graphics { get; }

        /// <summary>
        /// Default game constructor
        /// </summary>
        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GameWidth = this.Window.ClientBounds.Width;
            //graphics.ToggleFullScreen();
            TrackIR.Init();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (Player p in Players)
            {
                p.LoadContent(this.Content);
            }
            foreach (Alien a in Aliens)
            {
                a.LoadContent(this.Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Intentionally blank
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Aliens.Count == 0 && SpawnDone)// if no more aliens alive and the spawn is done
            {
                Win();
            }
            if (Players.Count == 0 && SpawnDone)// if no more players alive and the spawn is done
            {
                Lose();
            }
            // increment cooldown timers
            PlayerShootCD += gameTime.ElapsedGameTime.Milliseconds;
            AliensShootCD += gameTime.ElapsedGameTime.Milliseconds;
            // handle alien shoot cooldown
            if (AliensShootCD >= COOLDOWN_SHOT_ALIEN + Rand.Next((COOLDOWN_SHOT_ALIEN / 100) * 1, (COOLDOWN_SHOT_ALIEN / 100) * 40))
            {
                Console.WriteLine(AliensShootCD);
                AlienLasers.Add(Aliens[Rand.Next(0, Aliens.Count)].Shoot(this.Content));
                AliensShootCD = 0;
            }
            // if the user presses escape or back, close the game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // player controls with keyboard
            // move left
            if (Keyboard.GetState().IsKeyDown(Keys.Left) || TrackIR.Yaw > YAW_LIMIT)
            {
                foreach (Player p in Players)
                {
                    p.MoveLeft();
                }
            }
            // move right
            if (Keyboard.GetState().IsKeyDown(Keys.Right) || TrackIR.Yaw < -YAW_LIMIT)
            {
                foreach (Player p in Players)
                {
                    p.MoveRight();
                }
            }
            // shoot laser
            if (Keyboard.GetState().IsKeyDown(Keys.Space) || TrackIR.Pitch < -PITCH_LIMIT)
            {
                // player shoot cooldown
                if (PlayerShootCD >= COOLDOWN_SHOT_PLAYER)
                {
                    foreach (Player p in Players)
                    {
                        PlayerLasers.Add(p.Shoot(this.Content));
                    }
                    
                    PlayerShootCD = 0;
                }
            }
            // check collisions
            Laser.CheckLazPlayer(PlayerLasers, Aliens);
            Laser.CheckLazAliens(AlienLasers, Players);
            Alien.CheckPlayer(Players, Aliens);
            // handle alien movement
            foreach (Alien a in Aliens)
            {
                a.Move();
            }
            // makes the game spawn aliens and the player
            if (AlienPreWaveAmount != Aliens.Count && !SpawnDone)
            {
                Alien a = new Alien(new Point(0, 0));
                a.LoadContent(this.Content);
                Aliens.Add(a);
                // each alien moves aside to make space for the next alien
                for (int i = 0; i < Aliens.Count*70; i++)
                {
                    a.Move();
                }
                // once the aliens spawned, spawn the player
                if (AlienPreWaveAmount == Aliens.Count)
                {
                    Player p = new Player(this, new Point(GameWidth/2-new Player(this,0,0).Size.X/2, DEFAULT_POS_Y), DEFAULT_PLAYER_SPEED);
                    Players.Add(p);
                    p.LoadContent(this.Content);
                    SpawnDone = true;// tell the game the spawn is done
                }
            }
            TimeElapsed += gameTime.ElapsedGameTime;
            base.Update(gameTime);
        }
        /// <summary>
        /// If the player wins, asks if the game has to restart or close
        /// </summary>
        private void Win()
        {

            if (System.Windows.Forms.MessageBox.Show("Voulez-vous recommencez le jeu ?", "Gagné", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                Exit();
            }
            else
                Application.Restart();
        }
        /// <summary>
        /// If the player loses, asks if the game has to restart or close
        /// </summary>
        private void Lose()
        {

            if (System.Windows.Forms.MessageBox.Show("Voulez-vous recommencez le jeu ?", "Echec", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                Exit();
            }
            else
                Application.Restart();
        }
        /// <summary>
        /// Updates the view
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
         
            SpriteBatch.Begin();
            foreach (Player p in Players)
            {
                p.Draw(SpriteBatch);
            }
            foreach (Alien a in Aliens)
            {
                a.Draw(SpriteBatch);
            }
            foreach (Laser laz in PlayerLasers)
            {
                laz.Draw(SpriteBatch);
                laz.Move();
            }
            foreach (Laser laz in AlienLasers)
            {
                laz.Draw(SpriteBatch);
                laz.Move();
            }
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
