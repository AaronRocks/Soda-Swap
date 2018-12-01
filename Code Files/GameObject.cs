//Base class for all objects in the game world.
//Brandon
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gdaps2game_reborn
{
    class GameObject
    {
        protected Rectangle texture;
        protected Rectangle hitBox;
        protected ObjectID id;

        //Use the set here for loading texture in Game1.LoadContent()
        public Rectangle Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }
        public Rectangle Hitbox
        {
            get { return this.hitBox; }
            set { this.hitBox = value; }
        }
        public ObjectID ID
        {
            get { return this.id; }
        }
        public int X
        {
            get { return this.hitBox.X; }
            set { this.hitBox.X = value; }
        }
        public int Y
        {
            get { return this.hitBox.Y; }
            set { this.hitBox.Y = value; }
        }
        public int Width
        {
            get { return this.hitBox.Width; }
            set { this.hitBox.Width = value; }
        }
        public int Height
        {
            get { return this.hitBox.Height; }
            set { this.hitBox.Height = value; }
        }

        //Both constructors initialize hitBox and id, but textures must be loaded using the Texture property.
        //This is to prevent a null reference exception in Game1.Initialize().

        //Use this constructor for actual GameObjects
        public GameObject(int x, int y, int width, int height)
        {
            this.hitBox = new Rectangle(x, y, width, height);
            this.id = ObjectID.GameObject;
        }
        //Use this constructor for child classes. Call base() with ObjectID.[child class] at end.
        public GameObject(int x, int y, int width, int height, ObjectID id)
        {
            this.hitBox = new Rectangle(x, y, width, height);
            this.id = id;
        }
    }
}
