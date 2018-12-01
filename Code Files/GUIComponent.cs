using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;


// worked on by Aaron
namespace gdaps2game_reborn
{
    //Currently blank expecting implementation guidelines
    class GUIComponent : GameObject
    {
        int xPos; // x and y to hold mouse positions
        int yPos;
        bool clicked = false; // var to hold whether or not the mouse is clicked

        public GUIComponent(int x, int y, int width, int height, Rectangle texture) : base(x, y, width, height, ObjectID.GUIComponent)
        {
            this.texture = texture;
        }

        public bool IsClicked() // makes a gui that is the button is clicked, the state changes
        {
            var touchState = TouchPanel.GetState(); // enables detection 
            var mouseState = Mouse.GetState(); // detects if mouse is clicked or not
            xPos = mouseState.X; // x and y positions of the mouse
            yPos = mouseState.Y;
            clicked = (mouseState.LeftButton == ButtonState.Pressed); // if the left mouse button is pressed, will set the clicked to true
            
            if (clicked && hitBox.Contains(xPos, yPos)) // if the object is clicked and the mouse is in the objects rectangle
            {
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D spriteSheet, Color color)  // takes and draws image that will now be clickable
        {
            spriteBatch.Draw(spriteSheet, this.hitBox, this.texture, color);
        }
    }
}
