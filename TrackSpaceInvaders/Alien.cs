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
        Texture2D texture;
        Game game = new Game();
        Point _position;
        Point _alienSize = new Point(100, 57);
        public Vector2 Speed { get; private set; }


        public Point Position { get => _position; private set => _position = value; }
        public Point AlienSize { get => _alienSize; set => _alienSize = value; }

        public Alien(Point position) : this(position, 1)
        {

        }
        public Alien(Point position, float speed)
        {
            this.Position = position;
            this.Speed = new Vector2(speed, 0);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprite/Aliens_Vessel");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(Position.X, Position.Y, AlienSize.X, AlienSize.Y), Color.White);
        }

        public void Move()
        {
            Position = new Point(this.Position.X + Convert.ToInt32(Speed.X), this.Position.Y);
            if (this.Position.X+AlienSize.X >= game.Window.ClientBounds.Width)
            {
                this.Down();
                Speed = -Speed;
            }
            if (this.Position.X <= 0)
            {
                this.Down();
                Speed = -Speed;
            }

        }
        public void Down()
        {
            Position = new Point(this.Position.X, this.Position.Y + AlienSize.Y);
        }
        
        public void Shoot()
        {

        }
    }
}
