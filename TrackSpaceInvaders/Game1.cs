﻿/* 
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
        const int PITCH_LIMIT = 300;
        const int YAW_LIMIT = 200;
        const int DEFAULT_POS_X = 0;
        const int DEFAULT_POS_Y = 450;
        const int DEFAULT_PLAYER_SPEED = 5;
        private const int COOLDOWN_SHOT = 1000;
        private const int COOLDOWN_SHOT_ALIEN = 3500;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Player> players = new List<Player>();
        List<Alien> aliens = new List<Alien>();
        TimeSpan timeElapsed = new TimeSpan();
        List<Laser> lazPlayer = new List<Laser>();
        List<Laser> lazAliens = new List<Laser>();
        static Random rnd = new Random(); 

        bool spawnDone = false;
        int alienPreWaveAmount = 30;// amount of enemies that will pre-spawn 40
        int shootCD;
        int AliensShootCD;
        int gameWidth = DEFAULT_POS_X;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameWidth = this.Window.ClientBounds.Width;
           graphics.ToggleFullScreen();
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (Player p in players)
            {
                p.LoadContent(this.Content);
            }
            foreach (Alien a in aliens)
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
            //Intentionally blank
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (aliens.Count == 0 && spawnDone)// if no more aliens alive and the spawn is done
            {
                Win();
            }
            if (players.Count == 0 && spawnDone)// if no more players alive and the spawn is done
            {
                Lose();
            }
            shootCD += gameTime.ElapsedGameTime.Milliseconds;
            AliensShootCD += gameTime.ElapsedGameTime.Milliseconds;
            

            if (AliensShootCD >= COOLDOWN_SHOT_ALIEN + rnd.Next((COOLDOWN_SHOT_ALIEN / 100) * 1, (COOLDOWN_SHOT_ALIEN / 100) * 40))
            {
                Console.WriteLine(AliensShootCD);
                lazAliens.Add(aliens[rnd.Next(0, aliens.Count)].Shoot(this.Content));
                AliensShootCD = 0;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.Left) || TrackIR.X > YAW_LIMIT)
            {
                foreach (Player p in players)
                {
                    p.MoveLeft();
                }
            }


            if (Keyboard.GetState().IsKeyDown(Keys.Right) || TrackIR.X < -YAW_LIMIT)
            {
                foreach (Player p in players)
                {
                    p.MoveRight();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space) || TrackIR.Y < -PITCH_LIMIT)
            {
                if (shootCD>= COOLDOWN_SHOT)
                {
                    foreach (Player p in players)
                    {
                        lazPlayer.Add(p.Shoot(this.Content));
                    }
                    
                    shootCD = 0;
                }
            }
            if (timeElapsed.Milliseconds >= 2)
            {
                timeElapsed -= new TimeSpan(0,0,0,0,2);
                Laser.CheckLazPlayer(lazPlayer, aliens);
                Laser.CheckLazAliens(lazAliens, players);
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


        }
        private void Win()
        {

            if (System.Windows.Forms.MessageBox.Show("Voulez-vous recommencez le jeu ?", "Gagné", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                Exit();
            }
            else
                Application.Restart();
        }
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
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
         
            spriteBatch.Begin();
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
            foreach (Laser laz in lazAliens)
            {
                laz.Draw(spriteBatch);
                laz.Move();
            }
            spriteBatch.End();

            base.Draw(gameTime);
            

        }
    }
}
