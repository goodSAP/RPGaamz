using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using tile_r;

//TEMPORARILY FUCK THIS SHIT

namespace GoodRPG
{
    class Enemies
    {


        public int enemyNumber;
        public int enemySelected;
        public int enemyAttack;
        public int enemyDefense;
        public int enemyHitpoints;
        public int enemyLevel;
        private Texture2D texture;
        private Vector2 Position;
        private Rectangle sourceRect;


        public Player playerForEnemies;
        public Encounter encounterForEnemies;



        


      
        public void createEnemy(int enemySelected, Texture2D texture, Vector2 Position, Rectangle sourceRect)
        {
            enemyLevel = encounterForEnemies.enemyLevel;
            enemyAttack = encounterForEnemies.enemyAttack;
            enemyDefense = encounterForEnemies.enemyDefense;
            enemyHitpoints = encounterForEnemies.enemyHitpoints;

            this.sourceRect = sourceRect;
            this.texture = texture;
            this.Position = Position;


            
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, Position, sourceRect, Color.White);

        }




    }
}
