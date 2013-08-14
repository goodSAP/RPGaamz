using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tile_r;


namespace Shooter
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

        // Amount of hit points that player has
        public int Health;

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


        KeyboardState keyboard;
        KeyboardState prevKeyboard;


        public void handleSpriteMovement(GameTime gameTime)
        {
            prevKeyboard = keyboard;
            keyboard = Keyboard.GetState();


                if (keyboard.IsKeyDown(Keys.W) == true)
                {
                    Position.Y -= 2;
                }


                if (keyboard.IsKeyDown(Keys.A) == true)
                {
                    Position.X -= 2;

                }

                if (keyboard.IsKeyDown(Keys.S) == true)
                {
                    Position.Y += 2;
                }

                if (keyboard.IsKeyDown(Keys.D) == true)
                {
                    Position.X += 2;
                }

            }


        public string Facing;

        

        // Initialize the player
        public void Initialize(Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;


            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;


            // Set the player to be active
            Active = true;


            // Set the player health
            Health = 100;
        }


        // Update the player animation
        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);

            hitbox  = new Rectangle((int)Position.X, (int)Position.Y, PlayerAnimation.FrameWidth,PlayerAnimation.FrameHeight);
        }

        // Draw the player
        public void Draw(SpriteBatch spriteBatch, float rotation)
        {
            PlayerAnimation.Draw(spriteBatch, rotation, Facing);
        }

    }
}
