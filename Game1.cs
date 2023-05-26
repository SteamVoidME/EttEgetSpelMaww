using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Security.Principal;


namespace EttEgetSpel
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        Texture2D myship;
        Texture2D Coin;
        Texture2D Slime;
        Vector2  coin_pos, slimePos, slimeSpeed; 
       

        List<Vector2> coinPosList = new List<Vector2>();
        List<Vector2> slimePosList = new List<Vector2>();

        Move move = new Move();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Globals.WindowHeight = Window.ClientBounds.Height;
            Globals.WindowWidth = Window.ClientBounds.Width;

            slimeSpeed.X = 5;
            slimeSpeed.Y = 0;
            Random slump = new Random();
            for (int i = 0; i  < 5; i++) 
            {
                coin_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                coin_pos.Y = slump.Next(0, Window.ClientBounds.Height - 50);
                coinPosList.Add(coin_pos);
            }
            for (int i = 0; i < 5; i++)
            {
                slimePos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                slimePos.Y = slump.Next(0, Window.ClientBounds.Height - 50);
                slimePosList.Add(slimePos);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Globals.Myship = Content.Load<Texture2D>("Sprites/Ship");
            Coin = Content.Load<Texture2D>("Sprites/Bit Coin sprite");
            Slime = Content.Load<Texture2D>("Sprites/Slime1");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            move.Movement();
            /*
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
            
            if (keyboardState.IsKeyDown(Keys.A) && (myship_pos.X >= Window.ClientBounds.Width - myship.Width) || keyboardState.IsKeyDown(Keys.W) && (myship_pos.Y >= Window.ClientBounds.Height - myship.Height) || keyboardState.IsKeyDown(Keys.D) && (myship_pos.X <= 0) || keyboardState.IsKeyDown(Keys.S) && (myship_pos.Y <= 0))
            {
                myship_speed.X = 3.5f;
                myship_speed.Y = 3.5f;
            }
            else if ((myship_pos.X >= Window.ClientBounds.Width - myship.Width) || myship_pos.Y >= Window.ClientBounds.Height - myship.Height || myship_pos.X <= 0 || myship_pos.Y <= 0)
            {
                myship_speed.X = 0;
                myship_speed.Y = 0; 
            }
            */
            Random slump = new Random();
            for (int i = 0; i < slimePosList.Count; i++)
            {
                if (slimePosList[i].X >= Window.ClientBounds.Width)
                {
                    
                    slimePos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                    slimePos.Y = slump.Next(0, Window.ClientBounds.Height - 50);
                    slimePosList[i] = slimePos;
                }
            }
            




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(Globals.Myship, Globals.Myship_pos, Color.BlueViolet);
            foreach (Vector2 cn in coinPosList) 
            { 
                spriteBatch.Draw(Coin, cn, Color.White);
            }
            foreach (Vector2 cn in slimePosList)
            {
                spriteBatch.Draw (Slime, cn, Color.White);
               

            }
            for(int i = 0; i < slimePosList.Count; i++)
            {
                slimePosList[i] = slimePosList[i] + slimeSpeed;
            }
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}