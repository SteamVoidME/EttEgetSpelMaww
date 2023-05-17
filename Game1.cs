using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Security.Principal;

namespace EttEgetSpel
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        Texture2D myship;
        Vector2 myship_pos;
        Vector2 myship_speed;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            myship_pos.X = 100;
            myship_pos.Y = 100;
            myship_speed.X = 2.5f;
            myship_speed.Y = 2.5f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            myship = Content.Load<Texture2D>("Sprites/Ship");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.D))
                myship_pos.X = myship_pos.X + myship_speed.X;
            if (keyboardState.IsKeyDown(Keys.A))
                myship_pos.X = myship_pos.X - myship_speed.X;
            if (keyboardState.IsKeyDown(Keys.W))
                myship_pos.Y = myship_pos.Y - myship_speed.Y;
            if (keyboardState.IsKeyDown(Keys.S))
                myship_pos.Y = myship_pos.Y + myship_speed.Y;

            // TODO: Add your update logic here

            if (keyboardState.IsKeyDown(Keys.A) && (myship_pos.X >= Window.ClientBounds.Width - myship.Width))
            {
                myship_speed.X = 2.5f;
            }
            else if (myship_pos.X >= Window.ClientBounds.Width - myship.Width)
            {
                myship_speed.X = 0;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(myship, myship_pos, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}