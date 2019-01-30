using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TrackSpaceInvaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        const int DEFAULT_POS_X = 0;
        const int DEFAULT_POS_Y = 350;
        const int DEFAULT_PLAYER_SPEED = 5;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont text;
        List<Player> players = new List<Player>();
        //Alien alien;
        List<Alien> aliens = new List<Alien>();
        TimeSpan timeElapsed = new TimeSpan();
        List<Laser> lazPlayer = new List<Laser>();
        //Point gameSize;

        bool spawnDone = false;
        int alienPreWaveAmount = 40;// amount of enemies that will pre-spawn
        int shootCD;
        int gameWidth = DEFAULT_POS_X;
        public Game1()
        {
            this.Window.AllowAltF4 = false;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameWidth = this.Window.ClientBounds.Width;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            text = Content.Load<SpriteFont>("Text/Text");
            foreach (Player p in players)
            {
                p.LoadContent(this.Content);
            }
            foreach (Alien a in aliens)
            {
                a.LoadContent(this.Content);
            }
            //Laser.LoadContent(this.Content);
            // TODO: use this.Content to load your game content here
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
            shootCD += gameTime.ElapsedGameTime.Milliseconds;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                foreach (Player p in players)
                {
                    p.MoveLeft();
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                foreach (Player p in players)
                {
                    p.MoveRight();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {

                if (shootCD>=1000)
                {
                    foreach (Player p in players)
                    {
                        lazPlayer.Add(p.Shoot(this.Content));
                    }
                    shootCD = 0;
                }
            }
            // TODO: Add your update logic here
            if (timeElapsed.Milliseconds >= 2)
            {
                timeElapsed -= new TimeSpan(0,0,0,0,2);
                Laser.CheckLaz(lazPlayer, aliens);
                Alien.CheckPlayer(players, aliens);
                foreach (Alien a in aliens)
                {
                    a.Move();
                }
                if (alienPreWaveAmount != aliens.Count && !spawnDone)
                {
                    Alien a = new Alien(new Point(0, 0));
                    a.LoadContent(this.Content);
                    aliens.Add(a);
                    for (int i = 0; i < aliens.Count*70; i++)
                    {
                        a.Move();
                    }
                    if (alienPreWaveAmount == aliens.Count)
                    {
                        Player p = new Player(this, new Point(gameWidth/2-new Player(this,0,0).Size.X/2, DEFAULT_POS_Y), DEFAULT_PLAYER_SPEED);
                        players.Add(p);
                        p.LoadContent(this.Content);
                        spawnDone = true;
                    }
                }
            }
            timeElapsed += gameTime.ElapsedGameTime;
            base.Update(gameTime);

            if (aliens.Count == 0 && spawnDone)// if no more aliens alive and the spawn is done
            {
                Win();
            }
            if (players.Count == 0 && spawnDone)// if no more players alive and the spawn is done
            {
                Lose();
            }
            //alien = new Alien(new Point(0,0));


        }
        private void Win()
        {
            Exit();
        }
        private void Lose()
        {
            Exit();
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
         
            spriteBatch.Begin();
            
            spriteBatch.DrawString(text, $"Valeur X : 0", new Vector2(DEFAULT_POS_X, DEFAULT_POS_X), Color.Black);
            spriteBatch.DrawString(text, $"Valeur Y : 0", new Vector2(DEFAULT_POS_X + 100, DEFAULT_POS_X), Color.Black);
            spriteBatch.DrawString(text, $"Valeur Z : 0", new Vector2(DEFAULT_POS_X + 200, DEFAULT_POS_X), Color.Black);
            foreach (Player p in players)
            {
                p.Draw(spriteBatch);
            }
            foreach (Alien a in aliens)
            {
                a.Draw(spriteBatch);
            }
            foreach (Laser laz in lazPlayer)
            {
                laz.Draw(spriteBatch);
                laz.Move();
            }
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
