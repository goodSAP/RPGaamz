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
    class Encounter
    {

        public int enemyNumber;
        public int enemySelected;
        public int enemyAttack;
        public int enemyDefense;
        public int enemyHitpoints;
        public int enemyLevel;
        public Random enemyRandom = new Random();
        private Enemies enemyClassVar;
        public int playerLevel;
        public Vector2 Position;
        public Texture2D texture;
        public Rectangle sourceRect;

        

        public void runEncounter(int playerLevel, Vector2 Position)
        {
            enemyNumber = enemyRandom.Next(0, 100);
            if (enemyNumber >= 50) { enemySelected = 1; }
            else { enemySelected = 0; }

            this.playerLevel = playerLevel;
            enemyLevel = playerLevel;
            this.Position = Position;

            enemyClassVar.createEnemy(enemySelected, texture, Position, sourceRect);
            
            
        }

    }
}
