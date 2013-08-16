using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tile_r;
using Shooter;

namespace GoodRPG
{
    class Player
    {
        // Animation representing the player
        public Animation PlayerAnimation;

        // Position of the Player relative to the upper left side of the screen
        public Vector2 Position;

        //Hitbox
        public Rectangle hitbox;

        // State of the player
        public bool Active;
        
        // Get the width of the player ship
        public int Width
        {
            get { return PlayerAnimation.FrameWidth; }
        }

        // Get the height of the player ship
        public int Height
        {
            get { return PlayerAnimation.FrameHeight; }
    
        }

        Rectangle sourceRect;
        int currentFrame = 0;

        int spriteWidth = 32;
        int spriteHeight = 32;
        int spriteSpeed = 2;

        float timer = 0f;
        float interval = 200f;
        Texture2D texture;

        public int playerDirection;

        public int playerAttack;
        public int playerDefense;
        public int playerHitpoints;
        public int playerLevel;
        public int playerWeapon;
        public int playerArmour;
        public bool moving = false;

        KeyboardState keyboard;
        KeyboardState prevKeyboard;


        public Player(Texture2D texture, int currentFrame, int spriteWidth, int spriteHeight, Rectangle screenBounds)
        {
            this.texture = texture;
            this.currentFrame = currentFrame;
            this.spriteWidth = spriteWidth;
            this.spriteHeight = spriteHeight;

        }

        public void handleSpriteMovement(GameTime gameTime)
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();
            
            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight);
            if (keyboard.GetPressedKeys().Length == 0)
            {
                moving = false;

                if (currentFrame > 0 && currentFrame < 4)
                {
                    currentFrame = 0;
                }
                if (currentFrame > 4 && currentFrame < 8)
                {
                    currentFrame = 4;
                }
                if (currentFrame > 8 && currentFrame < 12)
                {
                    currentFrame = 8;
                }
                if (currentFrame > 12 && currentFrame < 16)
                {
                    currentFrame = 12;
                }
            }

            if (keyboard.IsKeyDown(Keys.W))
            {
                AnimateUp(gameTime);
                playerDirection = 1;
                moving = true;
                if (Position.Y > 0)
                    Position.Y -= spriteSpeed;
            }

            else if (keyboard.IsKeyDown(Keys.A))
            {
                playerDirection = 2;
                AnimateLeft(gameTime);
                moving = true;
                if (Position.X > 0)
                    Position.X -= spriteSpeed;
            }


            else if (keyboard.IsKeyDown(Keys.S))
            {
                playerDirection = 3;
                AnimateDown(gameTime);
                moving = true;
                if (Position.Y < 672)
                    Position.Y += spriteSpeed;
            }


            else if (keyboard.IsKeyDown(Keys.D))
            {
                playerDirection = 4;
                AnimateRight(gameTime);
                moving = true;
                if (Position.X < 1248)
                    Position.X += spriteSpeed;
            }

            


            

          


       //     velocity = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
        }


        public void AnimateRight(GameTime gametime)
        {

            if (keyboard != prevKeyboard)
                currentFrame = 9;

            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 11)
                    currentFrame = 8;

                timer = 0f;
            }
        }





        public void AnimateLeft(GameTime gametime)
        {

            if (keyboard != prevKeyboard)
                currentFrame = 5;

            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 7)
                    currentFrame = 4;

                timer = 0f;
            }
        }


        public void AnimateUp(GameTime gametime)
        {

            if (keyboard != prevKeyboard)
                currentFrame = 13;

            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 15)
                    currentFrame = 12;

                timer = 0f;
            }
        }


        public void AnimateDown(GameTime gametime)
        {

            if (keyboard != prevKeyboard)
                currentFrame = 1;

            timer += (float)gametime.ElapsedGameTime.TotalMilliseconds;

            if (timer > interval)
            {
                currentFrame++;

                if (currentFrame > 3)
                    currentFrame = 0;

                timer = 0f;
            }
        }


        public string Facing;

        

        // Initialize the player
      


        // Update the player animation
        public void Update(GameTime gameTime)
        {
            
            

            hitbox  = new Rectangle((int)Position.X, (int)Position.Y, spriteWidth,spriteHeight);
        }

        // Draw the player
        public void Draw(SpriteBatch spriteBatch, float rotation)
        {
       //     PlayerAnimation.Draw(spriteBatch, rotation, Facing);
            spriteBatch.Draw(texture, Position, sourceRect, Color.White);
            
        }

    }
}
