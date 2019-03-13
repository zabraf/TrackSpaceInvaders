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
        public Vector2 Speed { get; private set; }
        public Point Size { get; set; } = new Point(9, 36);
        public Point Position { get; set; }
        public Origin Origin { get; set; }
        public Texture2D Texture { get; set; }
        
        /// <summary>
        /// Constructor with x and y as a Point
        /// </summary>
        /// <param name="position"></param>
        /// <param name="speed"></param>
        /// <param name="isUpDown"></param>
        /// <param name="Origin"></param>
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
        }
        /// <summary>
        /// Loads the texture
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("Sprite/Laser");
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
        /// Moves the laser
        /// </summary>
        public void Move()
        {
            Position = new Point(Position.X, Position.Y + Convert.ToInt32(Speed.Y));
        }
        /// <summary>
        /// checks collision with the player
        /// </summary>
        /// <param name="listLaz"></param>
        /// <param name="aliens"></param>
        public static void CheckLazPlayer(List<Laser> listLaz,List<Alien> aliens)
        {
            for (int i = 0; i < listLaz.Count; i++)
            {
                for (int j = 0; j < aliens.Count; j++)
                {
                    if (new Rectangle(listLaz[i].Position, listLaz[i].Size).Intersects(new Rectangle(aliens[j].Position, aliens[j].Size)))
                    {
                        listLaz.RemoveAt(i);
                        aliens.RemoveAt(j);

                        i -= 1;
                        j -= 1;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// checks collision with the aliens
        /// </summary>
        /// <param name="listLaz"></param>
        /// <param name="players"></param>
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
