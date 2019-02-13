using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackSpaceInvaders
{
    class Player
    {
        Game _game;
        Texture2D _texture;
        Point _gameSize;
        Point _size = new Point(75, 75);
        Point _position;
        private int testtxt = 0;

        public Vector2 Speed { get; private set; }


        public Point Position { get => _position; private set => _position = value; }
        public Point Size { get => _size; set => _size = value; }

        /// <summary>
        /// Player constructor with the game, x and y starting position as parameters
        /// </summary>
        /// <param name="game"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Player(Game1 game, int x,int y):this(game, new Point(x,y),1)
        {

        }
        /// <summary>
        /// Player constructor with the game and starting position as parameters
        /// </summary>
        /// <param name="game"></param>
        /// <param name="position"></param>
        public Player(Game1 game, Point position):this(game, position.X,position.Y)
        {

        }
        /// <summary>
        /// Player constructor with the game, starting position and the player speed as parameters
        /// </summary>
        /// <param name="game"></param>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public Player(Game1 game, Point position, float speed)
        {
            this.Position = position;
            this.Speed = new Vector2(speed, 0);

            _game = game;
            _gameSize = new Point(_game.Window.ClientBounds.Width, _game.Window.ClientBounds.Height);
        }
        /// <summary>
        /// Loads the player sprite
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Sprite/Player_Vessel");
        }
        /// <summary>
        /// Draws the player on the game on his x and y position
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle(Position.X, Position.Y, Size.X, Size.Y), Color.White);
        }
        /// <summary>
        /// Moves the player to the right until he reaches the max position
        /// </summary>
        public void MoveRight()
        {   
            if ((Position.X+Size.X) < _gameSize.X)
            {
                Position = new Point(Position.X + Convert.ToInt32(Speed.X),Position.Y);
            }
            else
            {
                Position = new Point(_gameSize.X-Size.X, Position.Y);
            }
        }
        /// <summary>
        /// Moves the player to the right until he reaches the max position
        /// </summary>
        public void MoveLeft()
        {
            if (Position.X > 0)
            {
                Position = new Point(Position.X - Convert.ToInt32(Speed.X), Position.Y);
            }
            else
            {
                Position = new Point(0, Position.Y);
            }
        }
        // TrackIR
        public void Move()
        {
            Position = new Point(Position.X + testtxt, Position.Y);
            if (Position.X < 0)
            {
                Position = new Point(0, Position.Y);
            }
            else if((Position.X + Size.X) > _gameSize.X)
            {
                Position = new Point((Position.X + Size.X), Position.Y);
            }
            else
            {
                // ?
            }
        }


        /// <summary>
        /// shoots a laser at the center of the player
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Laser Shoot(ContentManager content)
        {
            Laser laser = new Laser(new Point(this.Position.X + (Size.X/2),this.Position.Y ),12,true,Origin.Player);
            laser.Position = new Point(laser.Position.X - laser.Size.X / 2, laser.Position.Y);
            laser.LoadContent(content);
            return laser;
        }
    }
}
