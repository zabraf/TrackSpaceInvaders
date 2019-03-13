/* 
 * Project : TrackSpaceInvaders
 * Authors : Fabian Troller / Guntram Juling / Raphaël Lopes
 * Description : Space invaders controlled with head tracking(TrackIR) technology
 * File : Laser.cs
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
    public enum Origin
    {
        Player,
        Alien
    }
    class Laser
    {
        Point _size;
        Origin _origin;
        Texture2D _texture;
        Point _position;
        public Vector2 Speed { get; private set; }


        public Point Size { get => _size; set => _size = value; }
        public Point Position { get => _position; set => _position = value; }
        public Origin Origin { get => _origin; set => _origin = value; }


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

        public static  void CheckLazPlayer(List<Laser> listLaz,List<Alien> aliens)
        {


            for (int i = 0; i < listLaz.Count; i++)
            {
                for (int j = 0; j < aliens.Count; j++)
                {
                    if (new Rectangle(listLaz[i].Position, listLaz[i].Size).Intersects(new Rectangle(aliens[j].Position, aliens[j].Size)))
                    {
                        listLaz.RemoveAt(i);
                        aliens.RemoveAt(j);
                        //aliens[i].Delete(aliens[i]);
                        i -= 1;
                        j -= 1;
                        break;
                    }
                }
            }
        }


        public static void CheckLazAliens(List<Laser> listLaz, List<Player> players)
        {


            for (int i = 0; i < listLaz.Count; i++)
            {
                for (int j = 0; j < players.Count; j++)
                {
                    if (new Rectangle(listLaz[i].Position, listLaz[i].Size).Intersects(new Rectangle(players[j].Position, players[j].Size)))
                    {
                        listLaz.RemoveAt(i);
                        players.RemoveAt(j);
                        i -= 1;
                        j -= 1;
                        break;
                    }
                }
            }
        }



    }
}
