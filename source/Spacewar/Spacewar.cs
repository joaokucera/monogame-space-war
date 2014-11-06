#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Spacewar.Scripts;
#endregion

namespace Spacewar
{
    public enum GameScreen
    {
        Menu,
        Level,
        Victory,
        Defeat
    }

    public class Spacewar : Game
    {
        #region Constants

        private const int AmountOfEnemies = 5;

        #endregion

        #region Fields

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Rectangle screenSize;
        private Vector2 pressButtonPosition;
        private string pressButtonText = "<PRESS ENTER TO START>";
        private bool isButtonPressed = false;

        private KeyboardState keyboardState;
        private SpriteFont gameSpriteFont;
        private GameScreen gameScreen = GameScreen.Menu;

        private Texture2D backgroundLevel, backgroundMenu, backgroundVictory, backgroundDefeat;
        private Player player;
        private EnemySpawner enemySpawner;

        #endregion

        #region Constructors

        public Spacewar()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            base.Initialize();

            Viewport viewport = graphics.GraphicsDevice.Viewport;
            screenSize = new Rectangle(0, 0, (int)viewport.Width, (int)viewport.Height);

            Vector2 pressButtonSize = gameSpriteFont.MeasureString(pressButtonText);
            pressButtonPosition = new Vector2(screenSize.Width / 2 - pressButtonSize.Length() / 2, screenSize.Height / 1.15f);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundLevel = Content.Load<Texture2D>("B1_stars");
            backgroundMenu = Content.Load<Texture2D>("Spacewar_Title_FINAL");
            backgroundVictory = Content.Load<Texture2D>("Earth");
            backgroundDefeat = Content.Load<Texture2D>("Enemy_Planet");

            gameSpriteFont = Content.Load<SpriteFont>("GameFont");
        }

        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (gameScreen == GameScreen.Level)
            {
                enemySpawner.Update(gameTime);

                player.Update(gameTime, keyboardState);

                UpdatePhysics(gameTime);

                if (enemySpawner.Enemies.Count <= 0)
                {
                    gameScreen = GameScreen.Victory;
                }
            }
            else
            {
                if (keyboardState.IsKeyDown(Keys.Enter) && !isButtonPressed)
                {
                    isButtonPressed = true;

                    if (gameScreen == GameScreen.Menu)
                    {
                        enemySpawner = new EnemySpawner(Content);
                        enemySpawner.Initialize(screenSize, AmountOfEnemies);

                        player = new Player(Content);
                        player.Initialize(screenSize);

                        gameScreen = GameScreen.Level;
                    }
                    else
                    {
                        gameScreen = GameScreen.Menu;
                    }
                }
                else if (keyboardState.IsKeyUp(Keys.Enter))
                {
                    isButtonPressed = false;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (gameScreen == GameScreen.Level)
            {
                spriteBatch.Draw(backgroundLevel, screenSize, Color.White);

                enemySpawner.Draw(gameTime, spriteBatch);

                player.Draw(gameTime, spriteBatch);
            }
            else if (gameScreen == GameScreen.Menu)
            {
                spriteBatch.Draw(backgroundMenu, screenSize, Color.White);
            }
            else if (gameScreen == GameScreen.Victory)
            {
                spriteBatch.Draw(backgroundVictory, screenSize, Color.White);
            }
            else if (gameScreen == GameScreen.Defeat)
            {
                spriteBatch.Draw(backgroundDefeat, screenSize, Color.White);
            }

            DrawHUD();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawHUD()
        {
            if (gameScreen == GameScreen.Level)
            {
                spriteBatch.DrawString(gameSpriteFont, "ENEMIES: " + enemySpawner.Enemies.Count, new Vector2(5, 5), Color.Yellow);
            }
            else
            {
                spriteBatch.DrawString(gameSpriteFont, pressButtonText, pressButtonPosition, Color.Yellow);
            }
        }

        private void UpdatePhysics(GameTime gameTime)
        {
            for (int i = 0; i < enemySpawner.Enemies.Count; i++)
            {
                Enemy enemy = enemySpawner.Enemies[i];

                if (enemy != null)
                {
                    enemy.Update(gameTime);

                    if (enemy.Bounds.Intersects(player.Bounds))
                    {
                        gameScreen = GameScreen.Defeat;
                    }

                    for (int j = 0; j < player.ShotList.Count; j++)
                    {
                        Shot shot = player.ShotList[j];

                        if (enemy.Bounds.Intersects(shot.Bounds))
                        {
                            enemySpawner.Enemies.RemoveAt(i);
                            enemy = null;
                            i--;

                            player.ShotList.RemoveAt(j);
                            shot = null;
                            j--;

                            break;
                        }
                    }
                }
            }
        }

        #endregion
    }
}