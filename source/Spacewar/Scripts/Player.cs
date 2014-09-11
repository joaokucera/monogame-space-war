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
    public class Player
    {
        public Texture2D playerTexture;
        public Vector2 playerPosition;
        public Vector2 playerSpeed;
        public int health;
        public Rectangle screenSize;

        private List<Shot> shotList = new List<Shot>();
        private Texture2D shotTexture;

        public Player(ContentManager Content)
        {
            playerTexture = Content.Load<Texture2D>("PlayerIcon");
            shotTexture = Content.Load<Texture2D>("Player_Shot");
        }

        public void Initialize(Rectangle screenSize)
        {
            this.screenSize = screenSize;

            playerPosition.X = this.screenSize.Width / 2 - playerTexture.Width / 2;
            playerPosition.Y = this.screenSize.Height - playerTexture.Height;

            playerSpeed = new Vector2(150f, 150f);
        }

        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboard.IsKeyDown(Keys.Left))
            {
                playerPosition.X -= playerSpeed.X * deltaTime;
            }
            else if (keyboard.IsKeyDown(Keys.Right))
            {
                playerPosition.X += playerSpeed.X * deltaTime;
            }

            if (keyboard.IsKeyDown(Keys.Up))
            {
                playerPosition.Y -= playerSpeed.Y * deltaTime;
            }
            else if (keyboard.IsKeyDown(Keys.Down))
            {
                playerPosition.Y += playerSpeed.Y * deltaTime;
            }

            playerPosition.X = MathHelper.Clamp(playerPosition.X, 0, this.screenSize.Width - playerTexture.Width);
            playerPosition.Y = MathHelper.Clamp(playerPosition.Y, 0, this.screenSize.Height - playerTexture.Height);

            if (keyboard.IsKeyDown(Keys.Space))
            {
                Shot shot = new Shot(playerPosition);
                shotList.Add(shot);
            }

            foreach (Shot shot in shotList)
            {
                shot.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, playerPosition, Color.White);

            foreach (Shot shot in shotList)
            {
                shot.Draw(gameTime, spriteBatch, shotTexture);
            }
        }
    }
}
