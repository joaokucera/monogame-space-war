﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spacewar.Scripts
{
    public class Shot
    {
        #region Fields
        
        private Vector2 shotPosition;
        private Vector2 shotSpeed;
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

            shotSpeed = new Vector2(0, 1000);
        }

        #endregion

        #region Methods
        
        public void Update(GameTime gameTime)
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
