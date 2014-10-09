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
        #region Fields
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle screenSize;
        Player player;
        KeyboardState keyboard;
        List<Enemy> enemies = new List<Enemy>();

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

            foreach (Enemy enemy in enemies)
            {
                enemy.Initialize(screenSize);
            }

            player.Initialize(screenSize);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            enemies.Add(new Enemy(Content));

            player = new Player(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                Enemy enemy = enemies[i];

                enemy.Update(gameTime);

                for (int j = 0; j < player.ShotList.Count; j++)
                {
                    Shot shot = player.ShotList[j];

                    if (enemy.Bounds.Intersects(shot.Bounds))
                    {
                        enemies.RemoveAt(i);
                        enemy = null;
                        i--;

                        player.ShotList.RemoveAt(j);
                        shot = null;
                        j--;
                    }
                }
            }

            player.Update(gameTime, keyboard);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(gameTime, spriteBatch);
            }

            player.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion
    }
}