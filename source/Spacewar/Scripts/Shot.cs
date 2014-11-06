using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spacewar.Scripts
{
    public class Shot
    {
        #region Fields

        protected Vector2 shotPosition;
        protected Vector2 shotSpeed;

        private Texture2D shotTexture;

        #endregion

        #region Properties

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)shotPosition.X, (int)shotPosition.Y, shotTexture.Width, shotTexture.Height);
            }
        }

        #endregion

        #region Constructors

        public Shot(Vector2 shotPosition, Texture2D shotTexture)
        {
            this.shotPosition = shotPosition;
            this.shotTexture = shotTexture;

            this.shotSpeed = new Vector2(0, 500);
        }

        #endregion

        #region Methods

        public virtual void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            shotPosition.Y -= shotSpeed.Y * deltaTime;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D shotTexture)
        {
            spriteBatch.Draw(shotTexture, shotPosition, Color.White);
        }

        #endregion
    }
}
