﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;


namespace EttEgetSpel
{
    public class Game1 : Game
    {
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        int points = 0;
        Texture2D healthBarTex;
        Vector2 coin_pos, slimePos, slimeSpeed;
        double timeSinceLastDamage;


        List<Vector2> coinPosList = new List<Vector2>();
        List<Vector2> slimePosList = new List<Vector2>();
        SpriteFont gameFont;
        Move move = new Move();
        Bullet bullet = new Bullet();

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
            for (int i = 0; i < 10; i++)
            {
                coin_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                coin_pos.Y = slump.Next(0, Window.ClientBounds.Height - 50);
                coinPosList.Add(coin_pos);
            }
            for (int i = 0; i < 8; i++)
            {
                slimePos.X = slump.Next(0, Window.ClientBounds.Width - 20);
                slimePos.Y = slump.Next(0, Window.ClientBounds.Height - 20);
                slimePosList.Add(slimePos);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            gameFont = Content.Load<SpriteFont>("Utskrift/GameFont3");
            Globals.Myship = Content.Load<Texture2D>("Sprites/Illuminati");
            Globals.Coin = Content.Load<Texture2D>("Sprites/Bit Coin sprite");
            Globals.Slime = Content.Load<Texture2D>("Sprites/Slime1");
            Globals.Bullet = Content.Load<Texture2D>("Sprites/BulletGreen");
            Globals.GameOver = Content.Load<Texture2D>("Sprites/GameOver");
            healthBarTex = new Texture2D(GraphicsDevice, 1, 1);
            healthBarTex.SetData(new Color[] { Color.Red });
        }
                                                                                                         //Update <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        protected override void Update(GameTime gameTime)
        {
            Globals.RecMyShip = new Rectangle((int)Globals.Myship_pos.X, (int)Globals.Myship_pos.Y, Globals.Myship.Width, Globals.Myship.Height);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            bullet.SpawnBullet(gameTime);
            move.Movement();

            //Hit
            Globals.RecMyShip = new Rectangle((int)Globals.Myship_pos.X, (int)Globals.Myship_pos.Y, Globals.Myship.Width, Globals.Myship.Height);
            Globals.RecCoin = new Rectangle((int)Globals.Coin_pos.X, (int)Globals.Coin_pos.Y, Globals.Coin.Width, Globals.Coin.Height);
            

            
            foreach (Vector2 coin in coinPosList.ToList())
            {
                

                Globals.RecCoin = new Rectangle((int)coin.X, (int)coin.Y, Globals.Coin.Width, Globals.Coin.Height);
                
                if (Globals.RecMyShip.Intersects(Globals.RecCoin))
                {
                    coinPosList.Remove(coin);
                    points += 10;
                }

               

            }
            foreach (Vector2 slime in slimePosList.ToList())
            {
                Globals.RecSlime = new Rectangle((int)slime.X, (int)slime.Y, Globals.Slime.Width, Globals.Slime.Height);

                if (Globals.RecMyShip.Intersects(Globals.RecSlime) && (gameTime.TotalGameTime.TotalMilliseconds > (timeSinceLastDamage + 180)))
                {
                    Globals.Health -= 10;
                    timeSinceLastDamage = gameTime.TotalGameTime.TotalMilliseconds; 
                }
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
            if ((slimePos.X >= Globals.WindowWidth - Globals.Slime.Width) || (slimePos.Y >= Globals.WindowHeight - Globals.Myship.Height) || (slimePos.X <= 0) ||  (slimePos.Y <= 0))
            {
                slimeSpeed.X = 2f;
                slimeSpeed.Y = 2f;
            }
            else if ((slimePos.X >= Globals.WindowWidth - Globals.Slime.Width) || slimePos.Y >= Globals.WindowHeight - Globals.Slime.Height || slimePos.X <= 0 || slimePos.Y <= 0)
            {
                slimeSpeed.X = 0f;
                slimeSpeed.Y = 0f;
            }

            for (int i = 0; i < Globals.BulletPosList.Count; i++)
{
                Vector2 temp_bullet;
                temp_bullet.X = Globals.BulletPosList.ElementAt(i).X;
                temp_bullet.Y = Globals.BulletPosList.ElementAt(i).Y;
                temp_bullet.Y = temp_bullet.Y - Globals.BulletSpeed.Y;
                if (temp_bullet.Y < 0)
                {
                    Globals.BulletPosList.RemoveAt(i);
                }
                else
                {
                    Globals.BulletPosList.RemoveAt(i);
                    Globals.BulletPosList.Insert(i, temp_bullet);
                }
            }
            base.Update(gameTime);
        }

        

        protected override void Draw(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();
            // TODO: Add your drawing code here

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (Globals.Health > 0)
                {
                    GraphicsDevice.Clear(Color.CornflowerBlue);
                    spriteBatch.Draw(Globals.Myship, Globals.Myship_pos, Color.White);
                    foreach (Vector2 coinPos in coinPosList)
                    {
                        spriteBatch.Draw(Globals.Coin, new Rectangle(coinPos.ToPoint(), new Point(30, 30)), Color.White);
                    }
                    foreach (Vector2 slimePos in slimePosList)
                    {
                        spriteBatch.Draw(Globals.Slime, new Rectangle(slimePos.ToPoint(), new Point(50, 50)), Color.White);


                    }
                    for (int i = 0; i < slimePosList.Count; i++)
                    {
                        slimePosList[i] = slimePosList[i] + slimeSpeed;
                    }
                    foreach (Vector2 bullets in Globals.BulletPosList)
                    {
                        spriteBatch.Draw(Globals.Bullet, bullets, Color.White);
                    }
                    spriteBatch.DrawString(gameFont, "Points: " + points, new Vector2(10, 10), Color.White);
                    spriteBatch.DrawString(gameFont, "Bullets: " + Globals.BulletPosList.Count(), new Vector2(10, 30), Color.White);
                    spriteBatch.Draw(healthBarTex, new Rectangle(10, Globals.WindowHeight - (Globals.Health * 2 + 10), 20, +Globals.Health * 2), Color.Red);


                }
                else
                {
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.DrawString(gameFont, "Game Over ", new Vector2(350, 245), Color.White);
                    spriteBatch.Draw(Globals.GameOver, new Rectangle(slimePos.ToPoint(), new Point(50, 50)), Color.White);
                }
                
            }
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}