//Michael, Brandon
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gdaps2game_reborn
{
    class Platform : GameObject
    {
		private int metaData;
		private List<Rectangle> locations;

		public int Id
		{
			get { return metaData; }
			set { metaData = value; }
		}
		public List<Rectangle> Locations
		{
			get { return locations; }
			set { locations = value; }
		}
        public Platform(int x, int y, int width, int height, int itemID) : base(x, y, width, height, ObjectID.Platform)
        {
			metaData = itemID;
			locations = new List<Rectangle>();
			locations.Add(new Rectangle(500, 0, 50, 50));
			locations.Add(new Rectangle(550, 0, 50, 50));
			locations.Add(new Rectangle(600, 0, 50, 50));
			locations.Add(new Rectangle(650, 0, 50, 50));
			locations.Add(new Rectangle(700, 0, 50, 50));
			locations.Add(new Rectangle(750, 0, 50, 50));
			locations.Add(new Rectangle(500, 0, 50, 50));
			locations.Add(new Rectangle(550, 0, 50, 50));
			locations.Add(new Rectangle(650, 0, 50, 50));
			locations.Add(new Rectangle(700, 0, 50, 50));
			locations.Add(new Rectangle(750, 0, 50, 50));
			locations.Add(new Rectangle(500, 0, 50, 50));
			locations.Add(new Rectangle(550, 0, 50, 50));
			locations.Add(new Rectangle(650, 0, 50, 50));
			locations.Add(new Rectangle(700, 0, 50, 50));
			locations.Add(new Rectangle(750, 0, 50, 50));
			this.Texture = locations[itemID];
		}



        //Returns true if hitbox collides with this.Hitbox
        public bool CheckIntersect(Rectangle hitbox)
        {
            if (this.Hitbox.Intersects(hitbox))
            {
                return true;
            }
            return false;
        }

        public void Impact(Player player)
        {
            player.IsFalling = false;
        }

        public void Impact(SodaCan can)
        {
            can.IsFalling = false;
        }
    }
}
