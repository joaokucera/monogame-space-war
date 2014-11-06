using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Spacewar.Scripts
{
    public class EnemySpawner
    {
        #region Constants

        private const float TimeToSpawn = 2.5f;

        #endregion

        #region Fields

        private Random random;
        private Rectangle screenSize;
        private float spawnTimer;

        private Texture2D enemyTexture;
        private Texture2D shotTexture;

        private List<Enemy> enemies = new List<Enemy>();

        #endregion

        #region Properties

        public List<Enemy> Enemies
        {
            get { return this.enemies; }
        }

        #endregion

        #region Constructors

        public EnemySpawner(ContentManager Content)
        {
            random = new Random();

            enemyTexture = Content.Load<Texture2D>("EnemyShip");
            shotTexture = Content.Load<Texture2D>("EnemyShip_Shot");
        }

        #endregion

        #region Methods

        public void Initialize(Rectangle screenSize, int amountOfEnemies)
        {
            this.screenSize = screenSize;

            for (int i = 0; i < amountOfEnemies; i++)
            {
                int choice = random.Next(0, 2);

                if (choice == 0)
                {
                    enemies.Add(new Enemy(enemyTexture, screenSize));
                }
                else
                {
                    enemies.Add(new SpecialEnemy(enemyTexture, shotTexture, screenSize));
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (enemies.Count > 0)
            {
                float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

                spawnTimer += deltaTime;

                if (spawnTimer >= TimeToSpawn)
                {
                    spawnTimer = 0;

                    Enemy firstEnemyAvailabe = TakeFirstEnemyAvailable();

                    if (firstEnemyAvailabe != null)
                    {
                        float xPosition = random.Next(screenSize.X + enemyTexture.Width, screenSize.Width - enemyTexture.Width * 2);

                        firstEnemyAvailabe.Spawn(xPosition);
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.IsVisible)
                {
                    enemy.Draw(gameTime, spriteBatch);
                }
            }
        }

        private Enemy TakeFirstEnemyAvailable()
        {
            Enemy enemy = null;

            foreach (Enemy e in enemies)
            {
                if (!e.IsVisible)
                {
                    enemy = e;
                    break;
                }
            }

            return enemy;
        }

        #endregion
    }
}
