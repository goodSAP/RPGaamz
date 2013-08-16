using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TestGame.TileEngine;
using Shooter;
using tile_r;

namespace GoodRPG
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Tile> tileList = new List<Tile>();
        List<Tile> encGroundPatches = new List<Tile>();
        List<Tile> doorList = new List<Tile>();
        
        Texture2D sprite;
        //     Texture2D speederSprite;
        Vector2 playerVelocity = new Vector2(0, 0);
        float rotation;
        KeyboardState prevKey;
        GamePadState prevGamePad;
        public Camera2D cam = new Camera2D();
        Animation animation = new Animation();
        bool running;

        //Tiles
        Texture2D wood;
        Texture2D crate;
        Texture2D grass1;

        GamePadState gamepad;


        //Player Vars
        Player player;
        int playerAttack;
        int playerDefense;
        int playerLevel;
        int playerArmour;
        int playerWeapon;
        int playerHitpoints;


        //Encounter Vars
        Enemies enemyStuff;
        Encounter encounters;
        int encounterInt;
        Random encounterSuccessRoll = new Random();
        bool encounterSuccess = false;

        int partOfWorld;
        //      float tempPos;



        int[,] map = new int[,]
            {                                                                                                                                                                                                /*introduce enemies here*/                                   
                 {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
                 {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,3,3,2,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,3,2,2,2,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,3,3,2,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,3,3,3,3,3,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,},
                 {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,},
            };



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
            this.IsMouseVisible = true;

            

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

            crate = Content.Load<Texture2D>("Art/Tiles/Crate");
            
            wood = Content.Load<Texture2D>("Art/Tiles/floor board");

            grass1 = Content.Load<Texture2D>("Art/Tiles/grassTile1");

            

            

            gamepad = GamePad.GetState(PlayerIndex.One);

            Createflor();

            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();


            double dt = (double)1000 / (double)120;
            graphics.SynchronizeWithVerticalRetrace = false;

            this.TargetElapsedTime = TimeSpan.FromMilliseconds(dt);
            graphics.ApplyChanges();


            sprite = Content.Load<Texture2D>("Art/jasperrun");
            animation.Initialize(sprite, new Vector2(0, 0), 32, 48, 4, 150, Color.White, 1f, true);


            
            player = new Player(sprite,1,32,48,new Rectangle(0,0,1280,720));
            player.Position.X = 500;
            player.Position.Y = 500;
            
            KeyboardState prevKey = Keyboard.GetState();
            prevGamePad = GamePad.GetState(PlayerIndex.One);

            // TODO: use this.Content to load your game content here
        }


        private void Createflor()
        {

            int tileMapWidth = map.GetLength(1);
            int tileMapHeight = map.GetLength(0);

            for (int y = 0; y < tileMapHeight; y++)
            {

                for (int x = 0; x < tileMapWidth; x++)
                {
                    int textureIndex = map[y, x];
                    

                    switch (textureIndex)
                    {
                        case 1:
                            Tile tile;
                            Vector2 pos = new Vector2(48 * x, 48 * y);

                            tile = new Tile(wood, 48, 48, pos, true);

                            tileList.Add(tile);

                            Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);

                            

                            break;

                        case 2:


                            Tile crateTile;

                            crateTile = new Tile(crate, 48, 48, new Vector2(48 * x, 48 * y), false);

                            tileList.Add(crateTile);

                            Console.WriteLine("x=" + x + "y=" + y + " is " + textureIndex);

                            break;

                        case 3:


                            Tile grass1Tile;

                            grass1Tile = new Tile(grass1, 48, 48, new Vector2(48 * x, 48 * y), true);

                            encGroundPatches.Add(grass1Tile);

                            Console.WriteLine("x=" + x + "y=" + y + " is " + textureIndex);

                            break;




                        default:
                            Console.WriteLine("x=" + x + " y=" + y + " is " + textureIndex);
                            break;
                    }
                }
            }

        }



        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);

            KeyboardState keyBoard = Keyboard.GetState();

            if (keyBoard.IsKeyDown(Keys.F))
            {
                graphics.IsFullScreen = !graphics.IsFullScreen;
                graphics.ApplyChanges();
            }

            if (keyBoard.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            player.handleSpriteMovement(gameTime);
            player.Update(gameTime);
            //Giving the stats to the stats
            playerLevel = 1;
            playerArmour = 0;
            playerAttack = playerLevel * 5;
            playerDefense = playerLevel * 3;
            playerWeapon = 0;
            playerHitpoints = playerLevel * 8;

            player.playerAttack = playerAttack;
            player.playerDefense = playerDefense;
            player.playerLevel = playerLevel;
            player.playerArmour = playerArmour;
            player.playerWeapon = playerWeapon;
            player.playerHitpoints = playerHitpoints;






            
            foreach (Tile tile in tileList)
            {
                if (tile.BoundingBox.Intersects(player.hitbox))
                {
                    if (tile.Landable == false)
                    {
                        if (player.playerDirection == 1)
                        {
                            player.Position.Y += 2;
                        }
                        if (player.playerDirection == 2)
                        {
                            player.Position.X += 2;
                        }
                        if (player.playerDirection == 3)
                        {
                            player.Position.Y -= 2;
                        }
                        if (player.playerDirection == 4)
                        {
                            player.Position.X -= 2;
                        }
                    }
                }
            }


            //Encounter stuff

            foreach (Tile tile in encGroundPatches)
            {
                if (tile.BoundingBox.Intersects(player.hitbox))
                {
                    encounterInt = encounterSuccessRoll.Next(0, 1000);
                    if (encounterInt == 666)
                    {
                        encounterSuccess = true;
                    }
                    else
                    {
                        encounterSuccess = false;
                    }
                    
                }
            }

            if (encounterSuccess == true)
            {
                encounters.runEncounter(playerLevel, player.Position);
                
            }






            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                        BlendState.AlphaBlend,
                        null,
                        null,
                        null,
                        null,
                        cam.get_transformation(GraphicsDevice /*Send the variable that has your graphic device here*/));
            
            foreach (Tile tile in tileList)
            {
                tile.Draw(spriteBatch);

            }
            foreach (Tile tile in doorList)
            {
                tile.Draw(spriteBatch);
            }
            foreach (Tile tile in encGroundPatches)
            {
                tile.Draw(spriteBatch);
            }
            if (encounterSuccess == true)
            {
                enemyStuff.Draw(spriteBatch);
            }
            player.Draw(spriteBatch,0f);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
