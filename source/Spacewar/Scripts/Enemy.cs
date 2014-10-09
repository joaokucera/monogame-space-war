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
        private Vector2 enemyPosition;
        private Vector2 enemySpeed;
        //private int health;
        private Rectangle screenSize;

        private List<Shot> shotList = new List<Shot>();
        private Texture2D shotTexture;

        #endregion

        #region Properties

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, enemyTexture.Width, enemyTexture.Height);
            }
        }

        #endregion

        #region Constructors

        public Enemy(ContentManager Content)
        {
            enemyTexture = Content.Load<Texture2D>("EnemyShip");
            shotTexture = Content.Load<Texture2D>("EnemyShip_Shot");
        }

        #endregion

        #region Methods

        public void Initialize(Rectangle screenSize)
        {
            this.screenSize = screenSize;

            enemyPosition.X = this.screenSize.Width / 2 - enemyTexture.Width / 2;
            enemyPosition.Y = enemyTexture.Height;

            enemySpeed = new Vector2(50f, 50f);
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
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