using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdaps2game_reborn
{
    class Elevator : InteractiveObject
    {
        public Elevator(int x, int y, int width, int height) : base(x, y, width, height, ObjectID.Elevator)
        {
            
        }
    }
}
