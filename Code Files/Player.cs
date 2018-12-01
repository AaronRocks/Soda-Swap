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
    //Describes the current state of player for the purpose of animation.
    public enum PlayerState { StandRight, StandLeft, WalkRight, WalkLeft, JumpRight, JumpLeft, ThrowRight, ThrowLeft }

    class Player : GameObject
    {
        //constants
        const int walkTimePerFrame = 100;
        const int numWalkFrames = 12;
        const int jumpTimePerFrame = 200;
        const int numJumpFrames = 6;
        const int throwTimePerFrame = 100;
        const int numThrowFrames = 3;

        //Used to track time passed and current frame
        private int framesElapsed;
        private Rectangle standingFrame;
        private Rectangle currentFrame;
        private int currentFrameNumber;
        private Texture2D spritesheet;

        //Tracks number of cans the player has
        private int numCans;

        //Tracks if player is jumping or falling
        private bool isJumping;
        private bool isFalling;

        //Tracks 

        //Holds walkcycle frames
        private Rectangle[] walkCycleFrames = {
            new Rectangle(7, 0, 50, 150),
            new Rectangle(70, 0, 63, 150),
            new Rectangle(130, 0, 88, 150),
            new Rectangle(210, 0, 100, 150),
            new Rectangle(290, 0, 91, 150),
            new Rectangle(370, 0, 91, 150),
            new Rectangle(455, 0, 63, 150),
            new Rectangle(520, 0, 57, 150),
            new Rectangle(577, 0, 82, 150),
            new Rectangle(652, 0, 94, 150),
            new Rectangle(730, 0, 88, 150),
            new Rectangle(803, 0, 88, 150)
        };

        //Holds jump cycle frames
        private Rectangle[] jumpCycleFrames =
        {
            new Rectangle(0, 0, 50, 150),
            new Rectangle(50, 0, 51, 150),
            new Rectangle(100, 0, 100, 150),
            new Rectangle(200, 0, 100, 150),
            new Rectangle(300, 0, 100, 150),
            new Rectangle(400, 0, 100, 150)
        };

        //Holds throw cycle frames
        private Rectangle[] throwCycleFrames =
        {
            new Rectangle(0, 0, 90, 150),
            new Rectangle(155, 0, 105, 150),
            new Rectangle(260, 0, 140, 150)
        };

        //property for current spritesheet
        public Texture2D currentSpritesheet
        {
            get { return spritesheet; }
            set { spritesheet = value; }
        }

        public int NumCans
        {
            get { return numCans; }
            set { numCans = value; }
        }

        private PlayerState playerState;
        private PlayerState prevPlayerState;

        public PlayerState PlayerState
        {
            get {return playerState; }
            set { playerState = value; }
        }

        public PlayerState PrevPlayerState
        {
            get { return prevPlayerState; }
            set { prevPlayerState = value; }
        }

        public bool IsJumping
        {
            get { return this.isJumping; }
            set { this.isJumping = value; }
        }
        public bool IsFalling
        {
            get { return this.isFalling; }
            set { this.isFalling = value; }
        }

        public Player(int x, int y, int width, int height) : base(x, y, width, height, ObjectID.Player)
        {
            this.numCans = 0;
            this.isJumping = false;
            this.isFalling = false;

            this.standingFrame = new Rectangle(900, 0, 50, 150);
        }


        //Sets the playerstate to the Stand, Walk, Jump, and Throw enums
        public void StandLeft()
        {
            playerState = PlayerState.StandLeft;
        }

        public void StandRight()
        {
            playerState = PlayerState.StandRight;
        }

        public void WalkLeft()
        {
            playerState = PlayerState.WalkLeft;
        }

        public void WalkRight()
        {
            playerState = PlayerState.WalkRight;
        }

        public void JumpLeft()
        {
            playerState = PlayerState.JumpLeft;
        }

        public void JumpRight()
        {
            playerState = PlayerState.JumpRight;
        }

        public void ThrowLeft()
        {
            playerState = PlayerState.ThrowLeft;
        }

        public void ThrowRight()
        {
            playerState = PlayerState.ThrowRight;
        }

        //Returns the proper frame & source spritesheet for the player
        public Rectangle GetCurrentFrame(GameTime gameTime)
        {
            switch (playerState)
            {
                case PlayerState.StandLeft:
                case PlayerState.StandRight:
                    return standingFrame;
                case PlayerState.WalkLeft:
                case PlayerState.WalkRight:
                    framesElapsed = (int)(gameTime.TotalGameTime.TotalMilliseconds/walkTimePerFrame);
                    return walkCycleFrames[framesElapsed % numWalkFrames];
                case PlayerState.JumpLeft:
                case PlayerState.JumpRight:
                    framesElapsed = (int)(gameTime.TotalGameTime.TotalMilliseconds / jumpTimePerFrame);
                    return jumpCycleFrames[framesElapsed % numJumpFrames];
                case PlayerState.ThrowLeft:
                case PlayerState.ThrowRight:
                    framesElapsed = (int)(gameTime.TotalGameTime.TotalMilliseconds / throwTimePerFrame);
                    return throwCycleFrames[framesElapsed % numThrowFrames];
            }
            return new Rectangle(0, 0, 0, 0);
        }


        //Swapping code for player
        public void Swap(int objectX, int objectWidth, int objectY, int objectHeight)
        {
            this.X = objectX; //Adjust player's X
            this.Y = objectY - (this.Height - objectHeight) - 10; //Adjust player's Y
        }
    }
}
