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
    public enum Origin
    {
        Player,
        Alien
    }
    class Laser
    {
        Point _size;
        Origin _Origin;
        Texture2D _texture;
        Point _position;
        public Vector2 Speed { get; private set; }


        public Point Size { get => _size; set => _size = value; }
        public Point Position { get => _position; set => _position = value; }
        public Origin Origin { get => _Origin; set => _Origin = value; }

        public Laser(int x, int y, bool isUpDown, Origin Origin) : this(new Point(x, y), 1, isUpDown, Origin)
        {

        }
        public Laser(Point position, float speed, bool isUpDown, Origin Origin)
        {
            this.Origin = Origin;
            this.Position = position;
            if(isUpDown)
            {
                this.Speed = new Vector2(0, -speed);
            }
            else
            {
                this.Speed = new Vector2(0, speed);
            }
            Size = new Point(9, 36);
        }
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Sprite/Laser");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle(Position.X, Position.Y, Size.X, Size.Y), Color.White);
        }
        public void Move()
        {
                Position = new Point(Position.X, Position.Y + Convert.ToInt32(Speed.Y));

        }
    }
}
