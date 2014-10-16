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
        #region Fields

        private Texture2D playerTexture;
        private Vector2 playerPosition;
        private Vector2 playerSpeed;
        //private int health;
        private Rectangle screenSize;

        private List<Shot> shotList = new List<Shot>();
        private Texture2D shotTexture;

        private bool isShooted = false;

        #endregion

        #region Properties

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerTexture.Width, playerTexture.Height);
            }
        }

        public List<Shot> ShotList
        {
            get
            {
                return shotList;
            }
        }

        #endregion

        #region Constructors

        public Player(ContentManager Content)
        {
            this.playerTexture = Content.Load<Texture2D>("PlayerIcon");
            this.shotTexture = Content.Load<Texture2D>("Player_Shot");
        }

        #endregion

        #region Methods

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

            if (keyboard.IsKeyDown(Keys.Space) && !isShooted)
            {
                Vector2 shotPosition = new Vector2(playerPosition.X + playerTexture.Width / 2, playerPosition.Y);

                Shot shot = new Shot(shotPosition, shotTexture);
                shotList.Add(shot);

                isShooted = true;
            }
            if (keyboard.IsKeyUp(Keys.Space))
            {
                isShooted = false;
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

        #endregion
    }
}