using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spacewar.Scripts
{
    class Shot
    {
        private Vector2 shotPosition;
        private Vector2 shotSpeed;

        public Shot(Vector2 startPosition)
        {
            shotPosition = startPosition;
            shotSpeed = new Vector2(0, 1000);
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            shotPosition.Y -= shotSpeed.Y * deltaTime;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Texture2D shotTexture)
        {
            spriteBatch.Draw(shotTexture, shotPosition, Color.White);
        }
    }
}
