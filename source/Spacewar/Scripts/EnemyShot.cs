using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spacewar.Scripts
{
    public class EnemyShot : Shot
    {
        #region Constructors

        public EnemyShot(Vector2 shotPosition, Texture2D shotTexture)
            : base(shotPosition, shotTexture)
        {
        }

        #endregion

        #region Methods

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            shotPosition.Y += shotSpeed.Y * deltaTime;
        }

        #endregion
    }
}
