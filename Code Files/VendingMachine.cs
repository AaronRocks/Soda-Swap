//Brandon
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdaps2game_reborn
{
    class VendingMachine : InteractiveObject
    {
        public VendingMachine(int x, int y, int width, int height) : base(x, y, width, height, ObjectID.VendingMachine)
        {
            width = 100;
            height = 150;
        }

        public void OnClick(Player p)
        {
            Dispense(p);
        }

        public void Dispense(Player p)
        {
            p.NumCans = 5;    //Raise player's numCans field to max.
        }
    }
}
