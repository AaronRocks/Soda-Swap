//Brandon
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdaps2game_reborn
{
    class Vent : InteractiveObject
    {
        public Vent(int x, int y, int width, int height) : base(x, y, width, height, ObjectID.Vent)
        {
            width = 100;
            height = 50;
        }
    }
}
