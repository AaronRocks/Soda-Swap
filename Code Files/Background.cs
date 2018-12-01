//Brandon
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace gdaps2game_reborn
{
    class Background : GameObject
    {
        //Determines what portion of the larger background image will be drawn
        Rectangle sourceRectangle;

        //Set method for updating
        Rectangle SourceRectangle
        {
            set { this.sourceRectangle = value; }
        }



        //Use GraphicsDevice.Viewport.Width and .Height
        public Background(int width, int height) : base(0, 0, width, height, ObjectID.Background)
        {
        }
    }
}
