//Brandon
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdaps2game_reborn
{
    class SwappableObject : InteractiveObject
    {
        private bool toggled;
        public bool Toggled
        {
            get { return toggled; }
            set { toggled = value; }
        }

        //Use for SwappableObjects
        public SwappableObject(int x, int y, int width, int height) : base(x, y, width, height, ObjectID.SwappableObject)
        {

        }

        //Use for child classes crate + SodaCan
        public SwappableObject(int x, int y, int width, int height, ObjectID id) : base(x, y, width, height, id)
        {

        }

        public void Swap(Player player)
        {
            int tempX = this.X;
            int tempY = this.Y;

            this.X = player.X + (player.Width / 2) - (this.X / 2); //Adjust object's X
            this.Y = player.Y + player.Height - this.Y; //Adjust object's Y

            player.Swap(tempX, this.Width, tempY, this.Height);
        }
    }
}
