/* 
 * Project : TrackSpaceInvaders
 * Authors : Fabian Troller / Guntram Juling / Raphaël Lopes
 * Description : Space invaders controlled with head tracking(TrackIR) technology
 * File : Alien.cs
 * Date : 13.03.19
 */
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
    class Alien
    {
        private const int DEFAULT_SPEED = 1;
        private const int MIN_X = 0;

        Point _position;

        public Point Position { get => _position; private set => _position = value; }
        public Point Size { get; set; } = new Point(70, 70);
        public Vector2 Speed { get; set; }
        public Texture2D Texture { get; set; }
        public Game Game { get; set; } = new Game();

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="position"></param>
        public Alien(Point position) : this(position, DEFAULT_SPEED)
        {
            // Intentionally blank
        }
        /// <summary>
        /// Constructor with position and speed as parameters
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        public Alien(Point position, float speed)
        {
            this.Position = position;
            this.Speed = new Vector2(speed, 0);// 0 = don't move the Y
        }
        /// <summary>
        /// Loads the texture
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Sprite/Aliens_Vessel");
        }
        /// <summary>
        /// Updates the view
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle(Position.X, Position.Y, Size.X, Size.Y), Color.White);
        }
        /// <summary>
        /// Moves the alien
        /// </summary>
        public void Move()
        {
            Position = new Point(this.Position.X + Convert.ToInt32(Speed.X), this.Position.Y);
            if (this.Position.X+Size.X >= Game.Window.ClientBounds.Width)// cannot set a const with the game screen width
            {
                this.Down();
                Speed = -Speed;
                this._position.X = Game.Window.ClientBounds.Width - Size.X;
            }
            if (this.Position.X <= MIN_X)
            {
                this.Down();
                Speed = -Speed;
                this._position.X = MIN_X;
            }

        }
        /// <summary>
        /// Checks the collision with the player
        /// </summary>
        /// <param name="players"></param>
        /// <param name="aliens"></param>
        public static void CheckPlayer(List<Player> players, List<Alien> aliens)
        {


            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < aliens.Count; j++)
                {
                    if (new Rectangle(players[i].Position, players[i].Size).Intersects(new Rectangle(aliens[j].Position, aliens[j].Size)))
                    {
                        players.RemoveAt(i);
                        aliens.RemoveAt(j);

                        i -= 1;
                        j -= 1;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Moves downwards
        /// </summary>
        public void Down()
        {
            Position = new Point(this.Position.X, this.Position.Y + Size.Y);
        }
        /// <summary>
        /// Shoots a laser
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Laser Shoot(ContentManager content)
        {
            Laser alienLaz = new Laser(new Point(this.Position.X + (Size.X / 2), this.Position.Y), 4, false, Origin.Alien);
            alienLaz.Position = new Point(alienLaz.Position.X - alienLaz.Size.X / 2, alienLaz.Position.Y);
            alienLaz.LoadContent(content);
            return alienLaz;
        }
    }
}
