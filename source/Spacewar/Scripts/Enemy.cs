using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spacewar.Scripts
{
    public class Enemy
    {
        #region Fields

        private Texture2D enemyTexture;
        private Texture2D shotTexture;

        private Vector2 enemyPosition;
        private Vector2 enemySpeed;

        private List<Shot> shotList = new List<Shot>();
        private Rectangle screenSize;

        private bool isVisible;

        #endregion

        #region Properties

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, enemyTexture.Width, enemyTexture.Height);
            }
        }

        public bool IsVisible { get { return this.isVisible; } }

        #endregion

        #region Constructors

        public Enemy(Texture2D enemyTexture, Texture2D shotTexture, Rectangle screenSize)
        {
            this.enemyTexture = enemyTexture;
            this.shotTexture = shotTexture;
            this.screenSize = screenSize;

            enemyPosition = new Vector2(0, -enemyTexture.Height);
        }

        #endregion

        #region Methods

        public void Spawn(float xPosition)
        {
            enemyPosition.X = xPosition;
            enemyPosition.Y = -enemyTexture.Height;

            enemySpeed = new Vector2(0, 50f);
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            enemyPosition.Y += enemySpeed.Y * deltaTime;

            if (enemyPosition.Y + enemyTexture.Height > screenSize.Y)
            {
                isVisible = true;
            }
            else if (enemyPosition.Y > screenSize.Height)
            {
                isVisible = false;

                enemySpeed = Vector2.Zero;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTexture, enemyPosition, null, null, null, 0, null, Color.White, SpriteEffects.FlipVertically, 0);

            foreach (Shot shot in shotList)
            {
                shot.Draw(gameTime, spriteBatch, shotTexture);
            }
        }

        #endregion
    }
}