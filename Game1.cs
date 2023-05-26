﻿using Microsoft.Xna.Framework;
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
        Vector2 myship_pos;
        Vector2 myship_speed;
        Texture2D Coin;
        Vector2 coin_pos;
        List<Vector2> coin_pos_list = new List<Vector2>();


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            myship_pos.X = 365;
            myship_pos.Y = 210;
            myship_speed.X = 3.5f;
            myship_speed.Y = 3.5f;
            Random slump = new Random();
            for (int i = 0; i  < 5; i++) 
            {
                coin_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                coin_pos.Y = slump.Next(0, Window.ClientBounds.Height - 50);
                coin_pos_list.Add(coin_pos);
            }
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            myship = Content.Load<Texture2D>("Sprites/Ship");
            Coin = Content.Load<Texture2D>("Sprites/Bit Coin sprite");
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
            




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(myship, myship_pos, Color.BlueViolet);
            foreach (Vector2 cn in coin_pos_list) 
            { 
                spriteBatch.Draw(Coin, cn, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}