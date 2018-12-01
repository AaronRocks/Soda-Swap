//Brandon, Gav, Ankita, Michael
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace gdaps2game_reborn
{
    class Level
    {
        //Holds GameObject that represent non-interactible, non-collideable objects.
        private List<GameObject> decorations;
        //Holds Platform objects that Player will collide with in level.
        private List<Platform> platforms;
        // hold list of sec objects
        private List<SecCamera> security;
        //Should hold all InteractiveObjects but problems casting may force us to change over time.
        private List<InteractiveObject> interactives;

		//holds the starting position for the player
		private Rectangle playerPos;
        private int startingX;
        private int startingY;

        //Background for the level
        private Background background;

        public Background Background
        {
            get { return this.background; }
            set { this.background = value; }
        }

		public List<GameObject> Decorations
        {
            get { return this.decorations; }
            set { this.decorations = value; }
        }
        public List<Platform> Platforms
        {
            get { return this.platforms; }
            set { this.platforms = value; }
        }
        public List<InteractiveObject> Interactives
        {
            get { return this.interactives; }
            set { this.interactives = value; }
        }
        public List<SecCamera> Security
        {
            get { return this.security; }
            set { this.security = value; }
        }
		public Rectangle PlayerPos
		{
			get { return playerPos; }
		}

        //Level constructor
        public Level(string filepath)
        {
            decorations = new List<GameObject>();
            platforms = new List<Platform>();
            interactives = new List<InteractiveObject>();
            security = new List<SecCamera>();

            StreamReader sr = new StreamReader(filepath);
            int numLines = File.ReadLines(filepath).Count();
            string line;

            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            int itemCode = 0;
            int itemNumber = 0;


			//Loops through each line of source file
			for (int i = 0; i < numLines; i++)
			{
				//Takes current line and puts it in a string
				line = sr.ReadLine();

				//each attribute is concatenated into a temporary string, then parsed
				string tempString = "";
				for (int stringIndex = 0; stringIndex < line.Length; stringIndex++)
				{
					if (line[stringIndex] == 'a')
					{
						x = int.Parse(tempString);
						tempString = "";
					}
					else if (line[stringIndex] == 'b')
					{
						y = int.Parse(tempString);
						tempString = "";
					}
					else if (line[stringIndex] == 'c')
					{
						width = int.Parse(tempString);
						tempString = "";
					}
					else if (line[stringIndex] == 'd')
					{
						height = int.Parse(tempString);
						tempString = "";
					}
					else if (line[stringIndex] == 'e')
					{
						//To be changed to GameObect enumeration
						itemCode = int.Parse(tempString);
						tempString = "";
					}
					else if (stringIndex == line.Length - 1)
					{
						tempString += line[stringIndex].ToString();
						itemNumber = int.Parse(tempString);
						tempString = "";
					}
					else
					{
						tempString += line[stringIndex].ToString();
					}

				}
				if (itemCode == (int)ObjectID.Player)
				{
					this.playerPos = new Rectangle(x, y, 50, 150);
				}
				if (itemCode == (int)ObjectID.Elevator)
				{
					Elevator tempElevator = new Elevator(x, y, width, height);
					tempElevator.Texture = new Rectangle(400, 0, 100, 150);
					this.interactives.Add(tempElevator);
				}
				if (itemCode == (int)ObjectID.VendingMachine)
				{
					VendingMachine tempMachine = new VendingMachine(x, y, width, height);
					tempMachine.Texture = new Rectangle(100, 0, 100, 150);
					this.interactives.Add(tempMachine);
				}
				if (itemCode == (int)ObjectID.Platform)
				{
					Platform tempPlatform = new Platform(x, y, width, height, itemNumber);
					this.platforms.Add(tempPlatform);

				}
				if(itemCode == (int)ObjectID.Crate)
				{
					Crate crate = new Crate(x, y, width, height);
					crate.Texture = new Rectangle(250, 0, 50, 50);
					this.interactives.Add(crate);
				}
				if(itemCode == (int)ObjectID.SecCamera)
				{
					SecCamera camera = new SecCamera(x, y, width, height);
					this.security.Add(camera);
				}
			}
			sr.Close();

            startingX = playerPos.X;
            startingY = playerPos.Y;
        }

        //Drawing methods 

        //Draws each individual decoration
        public void DrawDecorations(SpriteBatch spriteBatch, Texture2D spriteSheet, Rectangle cameraView)
        {
            Rectangle adjustedPoint;
            foreach (GameObject d in this.decorations)
            {
                adjustedPoint = new Rectangle(d.X - cameraView.X, d.Y - cameraView.Y, d.Width, d.Height); //Object's relative position
                if (cameraView.Intersects(d.Hitbox)) //If it's in camera view
                {
                    spriteBatch.Draw(spriteSheet, adjustedPoint, d.Texture, Color.White);
                }
            }
        }

        //Draws each individual platform
        public void DrawPlatforms(SpriteBatch spriteBatch, Texture2D spriteSheet, Rectangle cameraView)
        {
            Rectangle adjustedPoint;
            foreach (Platform p in this.platforms)
            {
                adjustedPoint = new Rectangle(p.X - cameraView.X, p.Y - cameraView.Y, p.Width, p.Height); //Object's relative position
                if (cameraView.Intersects(p.Hitbox)) //If it's in camera view
                {
                    spriteBatch.Draw(spriteSheet, adjustedPoint, p.Texture, Color.White);
                }
            }
        }

        //Draws each individual InteractiveObject
        public void DrawInteractives(SpriteBatch spriteBatch, Texture2D spriteSheet, Rectangle cameraView)
        {
            Rectangle adjustedPoint;
            foreach (InteractiveObject i in this.interactives)
            {
                adjustedPoint = new Rectangle(i.X - cameraView.X, i.Y - cameraView.Y, i.Width, i.Height); //Object's relative position
                if (cameraView.Intersects(i.Hitbox)) //If it's in camera view
                {
                    spriteBatch.Draw(spriteSheet, adjustedPoint, i.Texture, Color.White);
                }
            }
        }

        //Draws each individual security Camera
        public void DrawSecurity(SpriteBatch spriteBatch, Texture2D spriteSheet, Rectangle cameraView)
        {
            Rectangle adjustedPoint1;
            Rectangle adjustedPoint2;
            foreach (SecCamera i in this.security)
            {
                adjustedPoint1 = new Rectangle(i.X - cameraView.X, i.Y - cameraView.Y, i.Width, i.Height); //Object's relative position
                adjustedPoint2 = new Rectangle(i.VisionBox.X - cameraView.X, i.VisionBox.Y - cameraView.Y, i.VisionBox.Width, i.VisionBox.Height);
                if (cameraView.Intersects(i.Hitbox)) //If it's in camera view
                {
                    spriteBatch.Draw(spriteSheet, adjustedPoint2, i.VisionTexture, Color.White, -0.5f, new Vector2(0, 0), SpriteEffects.None, 0.0f);
                    spriteBatch.Draw(spriteSheet, adjustedPoint1, i.Texture, Color.White);
                }
            }
        }

        //Loops through platforms, if player touches platform return false else return true
        public bool CheckPlayerIsFalling(Player player)
        {
            foreach (Platform p in this.platforms)
            {
                if (p.CheckIntersect(player.Hitbox) && player.Y <= p.Y && Math.Abs(p.X - player.X) < 45)
                    return false;
            }
            foreach (InteractiveObject i in this.interactives)
            {
                if (i.ID == ObjectID.Crate && Math.Abs(i.Y - player.Y) > 140 && i.CheckIntersect(player.Hitbox))
                {
                    return false;
                }
            }
            return true;
        }

        //Checks if player is attempting to walk left into a wall
        //Returns false if player is walking into wall by checking 
        public bool CheckPlayerWalkingLeftValid(Player player)
        {
            foreach (Platform p in this.platforms)
            {
                if (Math.Abs(player.Y - p.Y) < 145 && p.CheckIntersect(player.Hitbox) && p.X < player.X)
                {
                    return false;
                }
            }
            return true;
        }

        //Checks if player is attempting to walk right into a wall
        public bool CheckPlayerWalkingRightValid(Player player)
        {
            foreach (Platform p in this.platforms)
            {
                if (Math.Abs(player.Y - p.Y) < 145 && p.CheckIntersect(player.Hitbox) && player.X < p.X)
                {
                    return false;
                }
            }
            return true;
        }

        //Checks if a given can should should be falling
        public bool CheckCanFalling(SodaCan can)
        {
            foreach (Platform p in this.platforms)
            {
                if (can.X >= p.X && can.X <= p.X + p.Width && Math.Abs((can.Y + can.Height) - p.Y) < 5)
                {
                    can.IsAlive = false;
                    return false;
                }
            }
            return true;
        }

        //Checks if a given can hits a wall mid air, returns false if so
        public bool CheckCanAlive(SodaCan can)
        {
            foreach (Platform p in this.platforms)
            {
                if (can.Y >= p.Y && can.Y <= p.Y + p.Height && (Math.Abs(p.X - (can.X + can.Width)) < 5 || Math.Abs(can.X - (p.X + p.Width)) < 5))
                {
                    return false;
                }
            }
            return true;
        }

        //Checks if the player is pushing any crates
		public void CheckPush(Player player)
		{
			for(int loop = 0; loop < interactives.Count; loop++)
			{
				if(this.interactives[loop].ID == ObjectID.Crate)
				{
					Rectangle rect = Rectangle.Intersect(Interactives[loop].Hitbox, player.Hitbox);
					if (!rect.IsEmpty && Math.Abs(interactives[loop].Y - player.Y) < 140)
					{
						if (Interactives[loop].Hitbox.X < player.Hitbox.X)
						{
							Interactives[loop].X -= 3;
						}
						else
						{
							Interactives[loop].X += 3;
						}
					}
				}
			}
		}

        //Checks if player was caught by any security cameras
        public void CheckRaiseAlarm(Player player)
        {
            foreach (SecCamera camera in this.Security)
            {
                if (camera.RaiseAlarm(player))
                {
                    player.X = startingX;
                    player.Y = startingY;
                }
            }
        }
    }
}
