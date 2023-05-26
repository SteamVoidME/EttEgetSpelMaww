﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;


namespace EttEgetSpel
{
    public class Game1 : Game
    {   
        bool hit;
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

            slimeSpeed.X = 2;
            slimeSpeed.Y = 2;
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
                                      //Update <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        protected override void Update(GameTime gameTime) 
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            move.Movement();




            Globals.RecMyShip = new Rectangle((int) Globals.Myship_pos.X, (int) Globals.Myship_pos.Y, Globals.Myship.Width, Globals.Myship.Height);
            Globals.RecCoin = new Rectangle((int) Globals.Coin_pos.X, (int)Globals.Coin_pos.Y, Globals.Coin.Width, Globals.Coin.Height);
            hit = Globals.RecMyShip.Intersects(Globals.RecCoin);
            bool CheckCollision(Rectangle player, Rectangle mynt)
            {
                return player.Intersects(mynt);
            }
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
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            spriteBatch.Draw(Globals.Myship, Globals.Myship_pos, Color.BlueViolet);
            foreach (Vector2 coinPos in coinPosList) 
            { 
                spriteBatch.Draw(Coin, new Rectangle(coinPos.ToPoint(), new Point(30, 30)), Color.White);
            }
            foreach (Vector2 slimePos in slimePosList)
            {
                spriteBatch.Draw (Slime, new Rectangle(slimePos.ToPoint(), new Point(50, 50)), Color.White);
               

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