//Brandon, Aaron
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace gdaps2game_reborn
{
    class SodaCan : SwappableObject
    {
        // vectors to hold points in coordinate system
        int mouseX;
        int mouseY;

        int moveX;
        int moveY;

        bool moveLeft;

        // angle between the mouse and the player will be the angle thrown
        Vector2 direction;

        Player user;
        bool isFalling; // Set to true while in air
        bool alive; // Can is in motion

        // float angle;

        double power;

        double gravity = 9.81 * (4/60); // "gravity" functions like Earth's gravity but increments at a rate of 45 pixels a second

        // now also takes a player object to reference the player's global coordinates
        public SodaCan(int x, int y, int width, int height, Player player, MouseState mState, Rectangle cameraView) : base(x, y, width, height, ObjectID.SodaCan)
        {
            this.texture = new Rectangle(598, 52, 9, 14); //Sets a temporary sprite
            user = player;
            x = user.X + user.Width / 2; // default width and height are half that of the players
            y = user.Y + user.Height / 2;
            this.mouseX = mState.X + cameraView.X;
            this.mouseY = mState.Y + cameraView.Y;
            alive = true; // default the soda can is alive
            isFalling = false; // defaults to not falling when first created

            moveX = cameraView.Width;
            moveY = cameraView.Height;


            if (mouseX > x)
                moveLeft = false;
            else
                moveLeft = true;
        }

        public bool IsFalling
        {
            get { return this.isFalling; }
            set { this.isFalling = value; }
        }

        public bool IsAlive
        {
            get { return alive; }
            set { this.alive = value; }
        }

        public bool MoveLeft // determins whether the mouse should always move to the left or not
        {
            get { return moveLeft; }
        }

        public int MouseX
        {
            get { return mouseX; }
        }

        //Throw() will take a nmumber power and a direction. To be implemented further with basic
        //projectile motion.
        public void Throw()
        {
            Y -= (Y - mouseY) / (moveY / 200) + 1;

            if (this.Y <= this.mouseY)
                this.isFalling = true;
        }

        public void Fall()
        {
            //Y += (Y - mouseY) / (moveY / 200) + 1;
            Y += 15;
        }

        
    }
}
