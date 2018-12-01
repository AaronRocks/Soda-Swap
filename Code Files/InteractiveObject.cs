//Brandon
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gdaps2game_reborn
{
    class InteractiveObject : GameObject
    {
        public InteractiveObject(int x, int y, int width, int height, ObjectID id) : base(x, y, width, height, id)
        {

        }

        //Glow if mouse coordinates fall in hitbox and player is close enough.
        //This is to indicate to the player when the object is usable.
        public void Glow()
        {

        }

        //Should check if player is close enough as well
        public virtual void OnClick() //Carry out code when clicked. Can be overidden.
        {

        }

        public bool CheckIntersect(Rectangle playerHitbox)
        {
            return false;
        }
    }
}
