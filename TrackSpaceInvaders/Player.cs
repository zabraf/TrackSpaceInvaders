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
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Speed { get; private set; }
        public Point Position{ get
            {
                return new Point(this.X, this.Y);
            }
            private set
            {
                Position = value;
            }
        }
        public Player(Point position):this(position.X,position.Y)
        {

        }
        public Player(int x,int y):this(new Point(x,y),1)
        {

        }
        public Player(Point position, int speed):this(position.X,position.Y,speed)
        {
            
        }
        public Player(int x, int y, int speed)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprite/Player_Vessel");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(0, 0, 100, 100), Color.White);
        }
        public void MoveRight()
        {
            X += Speed;
        }
        public void MoveLeft()
        {
            X -= Speed;
        }
        public void Shoot()
        {

        }
    }
}
