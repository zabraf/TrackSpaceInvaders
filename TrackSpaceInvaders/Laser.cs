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
    public enum Origin
    {
        Player,
        Alien
    }
    class Laser
    {
        Origin _Origin;
        Texture2D _texture;
        Point _position;
        public Vector2 Speed { get; private set; }


        public Point Position { get => _position; private set => _position = value; }

        public Laser(int x, int y, bool isUpDown, Origin Origin) : this(new Point(x, y), 1, isUpDown, Origin)
        {

        }
        public Laser(Point position, float speed, bool isUpDown, Origin Origin)
        {
            _Origin = Origin;
            this.Position = position;
            if(isUpDown)
            {
                this.Speed = new Vector2(0, -speed);
            }
            else
            {
                this.Speed = new Vector2(0, speed);
            }
            
        }
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Sprite/Laser");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle(Position.X, Position.Y, 9,36), Color.White);
        }
        public void Move()
        {
                Position = new Point(Position.X, Position.Y + Convert.ToInt32(Speed.Y));

        }
    }
}
