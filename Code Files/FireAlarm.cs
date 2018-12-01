using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gdaps2game_reborn
{
    class FireAlarm : InteractiveObject
    {
        public FireAlarm(int x, int y, int width, int height, ObjectID id) : base(x, y, width, height, id)
        {
            //width = something
            //height = something
        }

        public override void OnClick()
        {
            //screen fade to black + maybe cue a distant fire alarm noise or something
        }
    }
}
