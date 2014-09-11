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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle screenSize;
        Player player;
        Player player2;
        KeyboardState keyboard;

        public Spacewar()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            Viewport viewport = graphics.GraphicsDevice.Viewport;
            screenSize = new Rectangle(0, 0, (int)viewport.Width, (int)viewport.Height);

            player.Initialize(screenSize);
            player2.Initialize(screenSize);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = new Player(Content);
            player2 = new Player(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            player.Update(gameTime, keyboard);

            Rectangle playerRectangle = new Rectangle((int)player.playerPosition.X, (int)player.playerPosition.Y, player.playerTexture.Width, player.playerTexture.Height);
            Rectangle playerRectangle2 = new Rectangle((int)player2.playerPosition.X, (int)player2.playerPosition.Y, player2.playerTexture.Width, player2.playerTexture.Height);

            if (playerRectangle.Intersects(playerRectangle2))
            {

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            player.Draw(gameTime, spriteBatch);
            player2.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
