//Brandon, Aaron
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gdaps2game_reborn
{
    class SecCamera : GameObject
    {
        private Rectangle visionBox; // The vision cone's hitbox
        private Rectangle visionTexture; // The vision cone's texture

        public Rectangle VisionBox
        {
            get { return this.visionBox; }
        }
        public Rectangle VisionTexture
        {
            get { return this.visionTexture; }
        }

        public SecCamera(int x, int y, int width, int height) : base(x, y, width, height, ObjectID.SecCamera)
        {
            this.texture = new Rectangle(200 , 0, 18, 27); // texture from spritesheet
            this.visionTexture = new Rectangle(800, 0, 100, 150); // size of vision cone
            this.visionBox = new Rectangle(x, y + 70, 100, 150); //Vision cone set in global coordinates
        }

        //Returns if the player is colliding with the vision cone
        public bool RaiseAlarm(Player player)
        {
            return this.visionBox.Intersects(player.Hitbox);
        }

        //Returns if a given soda can is touching this security camera
        public bool Explode(SodaCan can)
        {
            return can.Hitbox.Intersects(this.hitBox);
        }
    }
}
