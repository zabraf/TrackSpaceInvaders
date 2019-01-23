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
        Player player;
        Alien alien;
        List<Alien> aliens = new List<Alien>();
        TimeSpan timeElapsed = new TimeSpan();
        List<Laser> lazPlayer = new List<Laser>();
        Point gameSize;

        int alienWaveAmount = 30;
        int shootCD;
        public Game1()
        {
            this.Window.AllowAltF4 = false;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameSize = new Point(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            player = new Player(this,new Point(DEFAULT_POS_X,DEFAULT_POS_Y),DEFAULT_PLAYER_SPEED);
            alien = new Alien(new Point(0,0));
            int width = gameSize.X;//-alien.AlienSize.X;
            int positions = width / (10);
            int alienY = default(int);
            int alienDirection = 1;// true = right

            for (int i = 0, j = 0; i < alienWaveAmount; i++, j++)
            {
                if (j/10 == 1)
                {
                    alienY = i/10*alien.AlienSize.Y;
                    alienDirection *= -1;
                    j = 0;
                }
                Alien a = new Alien(new Point(positions * j, alienY), alienDirection);
                //a.Speed.X *= alienDirection;
                aliens.Add(a);
                

            }
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
            player.LoadContent(this.Content);
            alien.LoadContent(this.Content);
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
                player.MoveLeft();

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                player.MoveRight();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {

                if (shootCD>=1000)
                {
                    lazPlayer.Add(player.Shoot(this.Content));
                    shootCD = 0;
                }
            }
            // TODO: Add your update logic here
            if (timeElapsed.Milliseconds >= 2)
            {
                timeElapsed -= new TimeSpan(0,0,0,0,2);
                alien.Move();
                foreach (Alien a in aliens)
                {
                    a.Move();
                }
            }
            timeElapsed += gameTime.ElapsedGameTime;
            base.Update(gameTime);
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
            player.Draw(spriteBatch);
            alien.Draw(spriteBatch);
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
