#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Spacewar.Scripts;
#endregion

namespace Spacewar
{
    public class Spacewar : Game
    {
        #region Constants

        private const int AmountOfEnemies = 20;

        #endregion

        #region Fields

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Rectangle screenSize;
        private KeyboardState keyboard;

        private Texture2D background;
        private Player player;
        private EnemySpawner enemySpawner;

        #endregion

        #region Constructors

        public Spacewar()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            base.Initialize();

            Viewport viewport = graphics.GraphicsDevice.Viewport;
            screenSize = new Rectangle(0, 0, (int)viewport.Width, (int)viewport.Height);

            enemySpawner.Initialize(screenSize, AmountOfEnemies);

            player.Initialize(screenSize);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("B1_stars");

            enemySpawner = new EnemySpawner(Content);

            player = new Player(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            enemySpawner.Update(gameTime);

            player.Update(gameTime, keyboard);

            UpdatePhysics(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background, screenSize, Color.White);

            enemySpawner.Draw(gameTime, spriteBatch);

            player.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void UpdatePhysics(GameTime gameTime)
        {
            for (int i = 0; i < enemySpawner.Enemies.Count; i++)
            {
                Enemy enemy = enemySpawner.Enemies[i];

                if (enemy != null)
                {
                    enemy.Update(gameTime);

                    for (int j = 0; j < player.ShotList.Count; j++)
                    {
                        Shot shot = player.ShotList[j];

                        if (enemy.Bounds.Intersects(shot.Bounds))
                        {
                            enemySpawner.Enemies.RemoveAt(i);
                            enemy = null;
                            i--;

                            player.ShotList.RemoveAt(j);
                            shot = null;
                            j--;
                        }
                    }
                }
            }
        }

        #endregion
    }
}