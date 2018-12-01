// Aaron, Brandon, Gav, Michael, and Ankita
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace gdaps2game_reborn
{
    //Describes current game state
    public enum GameState { Title, Play, Pause, Options }
    //Each value descibes a class in the GameObject tree
    public enum ObjectID { Background, Crate, GameObject, GUIComponent, Platform, Player, SecCamera, SecLaser, SodaCan, SwappableObject, VendingMachine, Vent, Elevator }

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameState gameState;
        GameState prevGameState;

        Player player;
        Level currentLevel;
        List<SodaCan> cans = new List<SodaCan>();

        Rectangle cameraView; //Rectangle representing the screen
        Texture2D spriteSheet;
        SpriteFont font;
        Texture2D walkCycle;
        Texture2D jumpCycle;
        Texture2D throwCycle;

        Texture2D startScreen;
        Texture2D pauseScreen;

        KeyboardState kbState;
        KeyboardState prevState;
        MouseState mouseState;
        MouseState prevMouseState;

        GUIComponent guiTitlePlay; // play gooey on title screen (initilized in load as parem takes image to draw)
        GUIComponent guiExitButton; // exits the game
        GUIComponent guiPlayToPause; // pause button on play screen
        GUIComponent guiPauseToPlay; // pplay button on Pause screen
        GUIComponent guiPauseToTitle; // button from pause screen to title screen

        int screenWidth; // var to max width and height to enable manipulation of object sizes and positions regardless of screen size
        int screenHeight;
        int levelCounter; //Keeps track of what level player is on
        int maxHeight; //Describes how high the player will reach on a given jump

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //automatically fullscreens the game. Can change width and height with ApplyChanges()
            //graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 5 * 4;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 5 * 4;
            graphics.ApplyChanges();
            
            screenWidth = GraphicsDevice.Viewport.Width; // absolute value of the screen width for placement purposes
            screenHeight = GraphicsDevice.Viewport.Height;

            gameState = GameState.Title; //Initially starts game at title
            player = new Player(0, 0, 0, 0); //Player hitbox set in NewLevel
            player.Texture = new Rectangle(900, 0, 50, 150);
            player.NumCans = 5;
            levelCounter = 0;
            NewLevel(".\\Content\\Level1.txt"); //Creates first level
            cameraView = new Rectangle(0, 0, screenWidth, screenHeight);    //Camera coordinates, size of screen

            IsMouseVisible = true; // makes the mouse visible to enable clicking buttons


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spriteSheet = Content.Load<Texture2D>("Sprites5");
            font = Content.Load<SpriteFont>("mainFont");
            walkCycle = Content.Load<Texture2D>("walkCycle");
            jumpCycle = Content.Load<Texture2D>("jumpCycle");
            throwCycle = Content.Load<Texture2D>("throwCycle");
            startScreen = Content.Load<Texture2D>("StartScreen");
            pauseScreen = Content.Load<Texture2D>("PauseScreen");

            // initilization and placement of the gooey boxes
            // title screen
            guiTitlePlay = new GUIComponent(screenWidth/ 2 - 400, 3 * screenHeight / 5, 200, 100, new Rectangle(500, 0, 50, 50));
            guiExitButton = new GUIComponent(2 * screenWidth / 3 + 200, 3 * screenHeight / 5, 200, 100, new Rectangle(500, 0, 50, 50));
                // play screen
            guiPlayToPause = new GUIComponent(screenWidth - 400, screenHeight / 8, 100, 50, new Rectangle(500, 0, 50, 50));
                // pause screen
            guiPauseToPlay = new GUIComponent(screenWidth / 2 + 200, 3 * screenHeight / 5, 200, 100, new Rectangle(500, 0, 50, 50));
            guiPauseToTitle = new GUIComponent(screenWidth / 2 - 100, 2 * screenHeight / 5, 200, 100, new Rectangle(500, 0, 50, 50));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            /*if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit(); NOT REQUIRED */


            // TODO: Add your update logic here
            kbState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            //Changes player to current frame
            player.Texture = player.GetCurrentFrame(gameTime);

            //Switch statement for Gamestate
            switch (gameState)
            {
                case GameState.Title:
                    if (prevGameState == GameState.Pause)
                    {
                        for(int i = 0;  i < cans.Count; i++) // resets the can count
                        {
                            cans.Remove(cans[i]);
                            i--;
                        }
                        Initialize(); // re-initilizes all the elements of the game to default setting which will reset the game
                    }

                    if (guiTitlePlay.IsClicked()) // if the clickable box is clicked
                    {
                        gameState = GameState.Play;
                    }

                    if (guiExitButton.IsClicked())
                    {
                        Exit();
                    }
                    break;
                case GameState.Play:
                    //Sets cameraView.X & .Y based on player global coordinates
                    cameraView.X = player.X - (screenWidth / 2) + (player.Width / 2);
                    cameraView.Y = player.Y - (screenHeight / 2) + (player.Height / 2);

                    SodaMove(); // calls fall method for all cans

                    //On right click, throw a soda can
                    if (mouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released)
                    {
                        if (player.NumCans > 0)
                        {
                            if ((mouseState.Y + cameraView.Y) <= player.Y)
                            {
                                SodaCan thrownCan = new SodaCan(player.X, player.Y, 9, 14, player, mouseState, cameraView);
                                cans.Add(thrownCan);
                                player.NumCans--;
                            }
                            
                            
                        }
                    }

                    //SWAPPING / VENDING CODE - On left click, swap, give player more cans, or change level
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
						Rectangle mRect = new Rectangle(mouseState.Position.X + cameraView.X, mouseState.Position.Y + cameraView.Y, 2, 2);
						
						foreach (InteractiveObject i in currentLevel.Interactives) // loop through interactables
                        {
							Rectangle sect = Rectangle.Intersect(mRect, i.Hitbox);
							if (i.ID == ObjectID.Crate && !sect.IsEmpty)
							{
								int px = player.X;
								int py = player.Y;
								player.Swap(i.Hitbox.X, i.Hitbox.Width, i.Hitbox.Y, i.Hitbox.Height);
								i.X = px;
								i.Y = py;
							}
							if (i.ID == ObjectID.VendingMachine && !sect.IsEmpty)
							{
								player.NumCans = 5;
							}
                            if (i.ID == ObjectID.Elevator && !sect.IsEmpty)
                            {
                                switch (levelCounter)
                                {
                                    case 1:
                                        NewLevel(".\\Content\\Level2.txt");
                                        break;
                                    case 2:
                                        NewLevel(".\\Content\\Level3.txt");
                                        break;
                                    case 3:
                                        gameState = GameState.Title;
                                        break;
                                }
                            }
                        }
                        foreach (SodaCan can in cans)
                        {
                            Rectangle sect = Rectangle.Intersect(mRect, can.Hitbox);
                            if (!sect.IsEmpty)
                            {
                                int px = player.X;
                                int py = player.Y;
                                player.Swap(can.Hitbox.X, can.Hitbox.Width, can.Hitbox.Y, can.Hitbox.Height);
                                can.X = px;
                                can.Y = py;
                            }
                        }
                    }

                    // FSM CODE
                    if (guiPlayToPause.IsClicked() || (kbState.IsKeyDown(Keys.Escape) && prevState.IsKeyUp(Keys.Escape)))
                    {
                        prevGameState = gameState;
                        gameState = GameState.Pause;
                    }

                    //Security code
                    currentLevel.CheckRaiseAlarm(player);

                    //sets playerstate based on keyboard input
                    player.IsFalling = (!player.IsJumping && currentLevel.CheckPlayerIsFalling(player));

                    Move(kbState);
                    if ((player.PrevPlayerState == PlayerState.StandRight || player.PrevPlayerState == PlayerState.WalkRight) && (player.IsJumping || player.IsFalling))
                    {
                        if (player.PlayerState != PlayerState.JumpRight)
                        {
                            player.PrevPlayerState = player.PlayerState;
                        }
                        player.PlayerState = PlayerState.JumpRight;
                    }
                    else if ((player.PrevPlayerState == PlayerState.StandLeft || player.PrevPlayerState == PlayerState.WalkLeft) && (player.IsJumping || player.IsFalling))
                    {
                        if (player.PlayerState != PlayerState.JumpLeft)
                        {
                            player.PrevPlayerState = player.PlayerState;
                        }
                        player.PlayerState = PlayerState.JumpLeft;
                    }
                    else if (kbState.IsKeyDown(Keys.Right) || kbState.IsKeyDown(Keys.D))
                    {
                        if (player.PlayerState != PlayerState.WalkRight)
                        {
                            player.PrevPlayerState = player.PlayerState;
                        }
                        player.PlayerState = PlayerState.WalkRight;
                    }
                    else if (kbState.IsKeyDown(Keys.Left) || kbState.IsKeyDown(Keys.A))
                    {
                        if (player.PlayerState != PlayerState.WalkLeft)
                        {
                            player.PrevPlayerState = player.PlayerState;
                        }
                        player.PlayerState = PlayerState.WalkLeft;
                    }
                    else if (player.PrevPlayerState == PlayerState.JumpLeft || player.PrevPlayerState == PlayerState.WalkLeft)
                    {
                        if (player.PlayerState != PlayerState.StandLeft)
                        {
                            player.PrevPlayerState = player.PlayerState;
                        }
                        player.PlayerState = PlayerState.StandLeft;
                    }
                    else
                    {
                        if (player.PlayerState != PlayerState.StandRight)
                        {
                            player.PrevPlayerState = player.PlayerState;
                        }
                        player.PlayerState = PlayerState.StandRight;
                    }

                    //Crate pushing and falling code

                    currentLevel.CheckPush(player);

                    foreach (InteractiveObject i in currentLevel.Interactives)
					{
						if(i.ID == ObjectID.Crate)
						{
							bool colliding = false;
							foreach(Platform p in currentLevel.Platforms)
							{
								Rectangle rect = Rectangle.Intersect(i.Hitbox, p.Hitbox);
								if(!rect.IsEmpty)
								{
									colliding = true;
								}
							}
							if (!colliding)
							{
								i.Y = i.Y + 4;
							}
						}
					}

                    prevMouseState = mouseState;
                    prevState = kbState; // assigns the current state back to the prev state so will update properly
                    break;
                case GameState.Pause:

                    if (guiPauseToPlay.IsClicked() || (kbState.IsKeyDown(Keys.Escape) && prevState.IsKeyUp(Keys.Escape))) // go from pause back to play
                    {
                        prevGameState = gameState;
                        gameState = GameState.Play;
                    }

                    if (guiPauseToTitle.IsClicked())
                    {
                        prevGameState = gameState;
                        gameState = GameState.Title;
                    }
                    break;
            }

            prevState = kbState;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            // TODO: Add your drawing code here
            //Switch statement for Gamestate
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.Title:
                    // draws the clickable boxes
                    spriteBatch.Draw(startScreen, new Rectangle(0, 0, cameraView.Width, cameraView.Height), Color.White);
                    guiTitlePlay.Draw(spriteBatch, spriteSheet, Color.Black);
                    guiExitButton.Draw(spriteBatch, spriteSheet, Color.Black);
                    
                    // draws the strings over the clickable boxes to identify them
                    spriteBatch.DrawString(font, "Start", new Vector2(guiTitlePlay.Hitbox.X + 75, guiTitlePlay.Hitbox.Y + 40), Color.White);
                    spriteBatch.DrawString(font, "Exit", new Vector2(guiExitButton.Hitbox.X + 55, guiExitButton.Hitbox.Y + 40), Color.White);

                    break;
                case GameState.Play:
                    Rectangle adjustedPoint;
                    
                    //Various draw methods within Level.cs draw the level
                    currentLevel.DrawDecorations(spriteBatch, spriteSheet, cameraView);
                    currentLevel.DrawPlatforms(spriteBatch, spriteSheet, cameraView);
                    currentLevel.DrawInteractives(spriteBatch, spriteSheet, cameraView);
                    currentLevel.DrawSecurity(spriteBatch, spriteSheet, cameraView);

                    //Draw any cans on screen
                    foreach (SodaCan s in cans)
                    {
                        adjustedPoint = new Rectangle(s.X - cameraView.X, s.Y - cameraView.Y, s.Width, s.Height);
                        spriteBatch.Draw(spriteSheet, adjustedPoint, s.Texture, Color.White); // *not clickable texture*
                    }

                    //Tells player how many soda cans are available in inventory
                    spriteBatch.DrawString(font, $"Soda cans in inventory :: {player.NumCans}", new Vector2(50, 50), Color.Blue);

                    // draw the pause button on the play screen
                    guiPlayToPause.Draw(spriteBatch, spriteSheet, Color.Black);
                    spriteBatch.DrawString(font, "Pause", new Vector2(guiPlayToPause.X + 25, guiPlayToPause.Y + 20), Color.White);
                    adjustedPoint = new Rectangle(cameraView.Width / 2 - player.Width / 2, cameraView.Height / 2 - player.Height / 2, player.Width, player.Height);

                    switch (player.PlayerState)
                    {
                        case PlayerState.StandRight:
                            spriteBatch.Draw(spriteSheet, adjustedPoint, player.Texture, Color.White);
                            break;
                        case PlayerState.StandLeft:
                            spriteBatch.Draw(spriteSheet, adjustedPoint, player.Texture, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                            break;
                        case PlayerState.WalkRight:
                            spriteBatch.Draw(walkCycle, adjustedPoint, player.Texture, Color.White);
                            break;
                        case PlayerState.WalkLeft:
                            spriteBatch.Draw(walkCycle, adjustedPoint, player.Texture, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                            break;
                        case PlayerState.JumpRight:
                            spriteBatch.Draw(jumpCycle, adjustedPoint, player.Texture, Color.White);
                            break;
                        case PlayerState.JumpLeft:
                            spriteBatch.Draw(jumpCycle, adjustedPoint, player.Texture, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                            break;
                        case PlayerState.ThrowRight:
                            spriteBatch.Draw(throwCycle, adjustedPoint, player.Texture, Color.White);
                            break;
                        case PlayerState.ThrowLeft:
                            spriteBatch.Draw(throwCycle, adjustedPoint, player.Texture, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
                            break;
                        default:
                            break;
                    }
                    break;
                case GameState.Pause:
                    // draw the play and options button on the pause screen
                    spriteBatch.Draw(pauseScreen, new Rectangle(0, 0, cameraView.Width, cameraView.Height), Color.White);
                    guiPauseToPlay.Draw(spriteBatch, spriteSheet, Color.Black);
                    guiPauseToTitle.Draw(spriteBatch, spriteSheet, Color.Black);

                    spriteBatch.DrawString(font, "PAUSE", new Vector2(100, 200), Color.White);

                    spriteBatch.DrawString(font, "Play", new Vector2(guiPauseToPlay.X + 55, guiPauseToPlay.Y+ 40), Color.White);
                    spriteBatch.DrawString(font, "Title", new Vector2(guiPauseToTitle.X + 55, guiPauseToTitle.Y + 40), Color.White);
                    break;

            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        // take in level file, pass it to level constructor, and store as currentLevel
        public void NewLevel(string levelFile)
        {
            levelCounter++;
            currentLevel = new Level(levelFile); //Get txt file, to be based on levelCounter
            player.Hitbox = currentLevel.PlayerPos;
            cans.Clear();
        }

        //Takes input from keyboard and changes player position accordingly
        public void Move(KeyboardState kstate)
        {
            // Right movement
            if ((kstate.IsKeyDown(Keys.Right) || kstate.IsKeyDown(Keys.D)) && currentLevel.CheckPlayerWalkingRightValid(player))
            {
                player.X += 3;
                // player state changes to forward
            }

            // Left movement
            if ((kstate.IsKeyDown(Keys.Left) || kstate.IsKeyDown(Keys.A)) && currentLevel.CheckPlayerWalkingLeftValid(player))
            {
                player.X -= 3; // move back by three pixels
                // player state changes to backward
            }

            // if the player crouches
            if (kstate.IsKeyDown(Keys.Down) || kstate.IsKeyDown(Keys.S))
            {
                // player state changes to crouch - different rectancle will be drawn
            }

            // if the player jumps
            if ((kstate.IsKeyDown(Keys.Up) || kstate.IsKeyDown(Keys.W)) && (player.IsJumping == false && player.IsFalling == false)) // checks if either key is down and it was pressed previously
            {
                // player jump state jumping = max height
                // jump until at max height then change state to falling
                maxHeight = player.Y - 65;
                //Set player state to Jumping
                player.IsJumping = true;
            }

            //Makes player jump at reasonable pace
            if (player.IsJumping)
                player.Y -= 4;

            //Detects when player reaches top of jump
            if (player.IsJumping && player.Y <= maxHeight)
            {
                player.IsJumping = false;
            }

            //player falls to a platform
            if (player.IsFalling)
            {
                player.Y += 4;
            }
        }

        //For each soda can in cans, sets their position according to stored variables
        public void SodaMove()
        {
            foreach(SodaCan can in cans) // call fall for all the cans in play every time sodaMove is called
            {
                if (can.IsAlive)
                {
                    if (can.MoveLeft)
                    {
                        can.X -= 10;
                    }
                    else
                    {
                        can.X += 10;
                    }

                    can.IsAlive = currentLevel.CheckCanAlive(can);
                }

                if (!can.IsFalling)
                {
                    can.Throw();
                }
                else if (currentLevel.CheckCanFalling(can))
                {
                    can.Fall();
                }
            }
        }
    }
}