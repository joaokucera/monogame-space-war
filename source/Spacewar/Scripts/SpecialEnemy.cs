using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Spacewar.Scripts
{
    public class SpecialEnemy : Enemy
    {
        #region Constants

        private const float TimeToShot = 1f;

        #endregion

        #region Fields

        protected Texture2D shotTexture;

        private List<Shot> shotList = new List<Shot>();
        private float spawnShot;

        #endregion

        #region Constructors

        public SpecialEnemy(Texture2D enemyTexture, Texture2D shotTexture, Rectangle screenSize)
            : base(enemyTexture, screenSize)
        {
            this.shotTexture = shotTexture;
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            spawnShot += deltaTime;

            if (spawnShot >= TimeToShot)
            {
                spawnShot = 0;

                Vector2 shotPosition = new Vector2(enemyPosition.X + enemyTexture.Width / 2, enemyPosition.Y);

                Shot shot = new EnemyShot(shotPosition, shotTexture);
                shotList.Add(shot);
            }

            foreach (Shot shot in shotList)
            {
                shot.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            foreach (Shot shot in shotList)
            {
                shot.Draw(gameTime, spriteBatch, shotTexture);
            }
        }

        #endregion
    }
}
