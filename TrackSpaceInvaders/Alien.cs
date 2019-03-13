﻿using Microsoft.Xna.Framework;
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
        Texture2D _texture;
        Game _game = new Game();
        Point _position;
        //Point _size = new Point(70, 49);
        Point _size = new Point(70, 70);
        Vector2 _speed;


        public Point Position { get => _position; private set => _position = value; }
        public Point Size { get => _size; set => _size = value; }
        public Vector2 Speed { get => _speed; set => _speed = value; }

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
            _texture = content.Load<Texture2D>("Sprite/Aliens_Vessel");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle(Position.X, Position.Y, Size.X, Size.Y), Color.White);
        }

        public void Move()
        {
            Position = new Point(this.Position.X + Convert.ToInt32(Speed.X), this.Position.Y);
            if (this.Position.X+Size.X >= _game.Window.ClientBounds.Width)
            {
                this.Down();
                Speed = -Speed;
                this._position.X = _game.Window.ClientBounds.Width - Size.X;
            }
            if (this.Position.X <= 0)
            {
                this.Down();
                Speed = -Speed;
                this._position.X = 0;
            }

        }
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
        public void Down()
        {
            Position = new Point(this.Position.X, this.Position.Y + Size.Y);
        }
        
        public Laser Shoot(ContentManager content)
        {
            Laser alienLaz = new Laser(new Point(this.Position.X + (Size.X / 2), this.Position.Y), 4, false, Origin.Alien);
            alienLaz.Position = new Point(alienLaz.Position.X - alienLaz.Size.X / 2, alienLaz.Position.Y);
            alienLaz.LoadContent(content);
            return alienLaz;
        }
    }
}
