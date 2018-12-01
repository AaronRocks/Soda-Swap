//Brandon
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdaps2game_reborn
{
    class Crate : SwappableObject
    {
        public Crate(int x, int y, int width, int height) : base(x, y, width, height, ObjectID.Crate)
        {

        }



        //Unsure if create will be animated or how we'll move the crate itself.
        public void Push()
        { }
    }
}
