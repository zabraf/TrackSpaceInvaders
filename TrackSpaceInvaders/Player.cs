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
        Texture2D texture;
        Point _position;
        public Vector2 Speed { get; private set; }


        public Point Position { get => _position;private set => _position = value; }

        public Player(Point position):this(position.X,position.Y)
        {

        }
        public Player(int x,int y):this(new Point(x,y),1)
        {

        }
        public Player(Point position, float speed)
        {
            this.Position = position;
            this.Speed = new Vector2(speed, 0);
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprite/Player_Vessel");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(Position.X, Position.Y, 100, 100), Color.White);
        }
        public void MoveRight()
        {
            Position = new Point(Position.X + Convert.ToInt32(Speed.X),0);
        }
        public void MoveLeft()
        {
            Position = new Point(Position.X - Convert.ToInt32(Speed.X),0);
        }
        public void Shoot()
        {

        }
    }
}
