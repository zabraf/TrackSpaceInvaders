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
        Point _position;
        public Vector2 Speed { get; private set; }


        public Point Position { get => _position; private set => _position = value; }

        public Alien(Point position) : this(position,1)
        {

        }
        public Alien(Point position,float speed)
        {
            this.Position = position;
            this.Speed = new Vector2(speed,0);
        }
        
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprite/Aliens_Vessel");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(Position.X, Position.Y, 100, 57), Color.White);
        }
        public void MoveRight()
        {
            Position = new Point(this.Position.X + Convert.ToInt32(Speed.X), this.Position.Y);
        }

        public void MoveLeft()
        {
            Position = new Point(this.Position.X - Convert.ToInt32(Speed.X), this.Position.Y);
        }

        public void Shoot()
        {

        }
    }
}
