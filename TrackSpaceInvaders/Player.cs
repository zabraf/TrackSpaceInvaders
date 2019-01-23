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
        Game Game;
        Texture2D texture;
        Point gameSize;
        Point playerSize = new Point(100, 115);
        Point _position;
        public Vector2 Speed { get; private set; }


        public Point Position { get => _position; private set => _position = value; }
        public Point PlayerSize { get => playerSize; set => playerSize = value; }

        public Player(Game1 game, int x,int y):this(game, new Point(x,y),1)
        {

        }
        public Player(Game1 game, Point position):this(game, position.X,position.Y)
        {

        }
        public Player(Game1 game, Point position, float speed)
        {
            this.Position = position;
            this.Speed = new Vector2(speed, 0);

            Game = game;
            gameSize = new Point(Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprite/Player_Vessel");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle(Position.X, Position.Y, PlayerSize.X, PlayerSize.Y), Color.White);
        }
        public void MoveRight()
        {   
            if ((Position.X+PlayerSize.X) < gameSize.X)
            {
                Position = new Point(Position.X + Convert.ToInt32(Speed.X),Position.Y);
            }
            else
            {
                Position = new Point(gameSize.X-PlayerSize.X, Position.Y);
            }
        }
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
        public Laser Shoot(ContentManager content)
        {
            Laser PewPewPew = new Laser(new Point(this.Position.X + (PlayerSize.X/2),this.Position.Y ),12,true,Origin.Player);
            PewPewPew.Position = new Point(PewPewPew.Position.X - PewPewPew.Size.X / 2, PewPewPew.Position.Y);
            PewPewPew.LoadContent(content);
            return PewPewPew;
        }
    }
}
