using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Aseprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EttEgetSpel
{
    public class Game1 : Game
    {
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        float scale = 2f;
        bool bossSpawn = false, spawnSlimes = false;
        int points = 0, slimeAmount = 8, level = 1;
        int kills = 0;
        int BossKills = 0;
        Texture2D healthBarTex, GameOver, HP;
        Vector2 coin_pos, slimePos, slimeSpeed, bossSlimePos, bossSlimeSpeed, tempSpeed, tempBossSpeed;
        double timeSinceLastDamage;


        List<Vector2> coinPosList = new List<Vector2>();                //                     LIST & VAriabler
        List<Vector2> slimePosList = new List<Vector2>();
        List<Vector2> hPPosList = new List<Vector2>();
        List<Vector2> bossSlimePosList = new List<Vector2>();
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


            
            Random slump = new Random();

            for (int i = 0; i < 10; i++)
            {

                coin_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                coin_pos.Y = slump.Next(0, Window.ClientBounds.Height - 50);
                coinPosList.Add(coin_pos);
                    
            }


           



            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            gameFont = Content.Load<SpriteFont>("Utskrift/GameFont3");
            Globals.Illuminati = Content.Load<Texture2D>("Sprites/Illuminati");
            Globals.Coin = Content.Load<Texture2D>("Sprites/Bit Coin sprite");
            Globals.Slime = Content.Load<Texture2D>("Sprites/Slime1");
            Globals.Bullet = Content.Load<Texture2D>("Sprites/BulletGreen");
            Globals.BulletRight = Content.Load<Texture2D>("Sprites/BulletGreenRight");
            Globals.StartSpace = Content.Load<Texture2D>("Sprites/StartSpace");
            Globals.BackgroundMap = Content.Load<Texture2D>("Sprites/BackgoundMap");
            GameOver = Content.Load<Texture2D>("Sprites/GameOver");
            Globals.BossSlime = Content.Load<Texture2D>("Sprites/BossSlime2");
            HP = Content.Load<Texture2D>("Sprites/HP");
            healthBarTex = new Texture2D(GraphicsDevice, 1, 1);
            healthBarTex.SetData(new Color[] { Color.Red });
            
        }
                   
        
        public void BossSpawn(int j)
        {
            Globals.BossHP = 180;
            for (int i = 0; i < j; i++)
            {

                Random rng = new Random();
                bossSlimePos.X = rng.Next(0, Window.ClientBounds.Width - 300);
                bossSlimePos.Y = rng.Next(0, Window.ClientBounds.Height - 350);
                bossSlimePosList.Add(bossSlimePos);

                bossSlimeSpeed.X =  2;
                bossSlimeSpeed.Y =  2;
                Globals.BossSlimeSpeedList.Add(bossSlimeSpeed);

            }
        }
        public void SlimeSpawner(int j)
        {
            for (int i = 0; i < j; i++)
            {

                Random rnd = new Random();
                slimePos.X = rnd.Next(0, Window.ClientBounds.Width - 50);
                slimePos.Y = rnd.Next(0, Window.ClientBounds.Height - 50);
                slimePosList.Add(slimePos);

                slimeSpeed.X = rnd.Next(-2, 2);
                slimeSpeed.Y = rnd.Next(-1, 1);
                Globals.SlimeSpeedList.Add(slimeSpeed);

            }
        }

        //Update <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        protected override void Update(GameTime gameTime)
        {
            Globals.RecIlluminati = new Rectangle((int)Globals.IlluminatiPos.X, (int)Globals.IlluminatiPos.Y, Globals.Illuminati.Width, Globals.Illuminati.Height);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            bullet.SpawnBullet(gameTime);
            move.Movement();

            //Hit
            

            if(spawnSlimes == true)
            {
                spawnSlimes = false;
                SlimeSpawner(slimeAmount++);
                
                

            }
            foreach (Vector2 coin in coinPosList.ToList())
            {
                

                Globals.RecCoin = new Rectangle((int)coin.X, (int)coin.Y, Globals.Coin.Width, Globals.Coin.Height);
                
                if (Globals.RecIlluminati.Intersects(Globals.RecCoin))
                {
                    coinPosList.Remove(coin);
                    points += 10;
                }

            }
            foreach (Vector2 hP in hPPosList.ToList())
            {

                Globals.RecHP = new Rectangle((int)hP.X, (int)hP.Y, HP.Width, HP.Height);

                if (Globals.RecIlluminati.Intersects(Globals.RecHP))
                {
                    hPPosList.Remove(hP);
                    Globals.Health += 10;
                }

            }
            if(Globals.Health >= 100)
            {
                Globals.Health = 100;
            }


            for (int i  = 0; i < slimePosList.Count; i++)
            {
                slimePosList[i] += Globals.SlimeSpeedList[i];
            }

            foreach (Vector2 slime in slimePosList.ToList())
            {
                Globals.RecSlime = new Rectangle((int)slime.X, (int)slime.Y, Globals.Slime.Width, Globals.Slime.Height);

                for (int i = 0; i < Globals.BulletPosList.Count; i++)
                {
                    Globals.RecBullet = new Rectangle((int)Globals.BulletPosList[i].X, (int)Globals.BulletPosList[i].Y, Globals.Bullet.Width * 2, Globals.Bullet.Height * 2);

                    if (Globals.RecSlime.Intersects(Globals.RecBullet))
                    {
                        slimePosList.Remove(slime);
                        hPPosList.Add(slime);
                        kills += 1;
                        
                    }
                }
                for (int i = 0; i < Globals.BulletPosRightList.Count; i++)
                {
                    Globals.RecBullet = new Rectangle((int)Globals.BulletPosRightList[i].X, (int)Globals.BulletPosRightList[i].Y, Globals.Bullet.Width * 2, Globals.Bullet.Height * 2);

                    if (Globals.RecSlime.Intersects(Globals.RecBullet))
                    {
                        slimePosList.Remove(slime);
                        hPPosList.Add(slime);
                        kills += 1;

                    }
                }

                if (Globals.RecIlluminati.Intersects(Globals.RecSlime) && (gameTime.TotalGameTime.TotalMilliseconds > (timeSinceLastDamage + 300)))
                {
                    Globals.Health -= 8;
                    timeSinceLastDamage = gameTime.TotalGameTime.TotalMilliseconds; 
                }
                
            }


            for (int i = 0; i < bossSlimePosList.Count; i++)
            {
                bossSlimePosList[i] += Globals.BossSlimeSpeedList[i];
            }


            

            if (slimePosList.Count == 0 && Globals.Health > 0)
            {
                if(bossSpawn == true)
                {
                    BossSpawn(1);
                    bossSpawn = false;
                }
                    
                

                foreach (Vector2 bossSlime in bossSlimePosList.ToList())
                {
                    Globals.RecBossSlime = new Rectangle((int)bossSlime.X, (int)bossSlime.Y, Globals.BossSlime.Width, Globals.BossSlime.Height);

                    for (int i = 0; i < Globals.BulletPosList.Count; i++)
                    {
                        Globals.RecBullet = new Rectangle((int)Globals.BulletPosList[i].X, (int)Globals.BulletPosList[i].Y, Globals.Bullet.Width * 2, Globals.Bullet.Height * 2);
                        if (Globals.RecBossSlime.Intersects(Globals.RecBullet) && (gameTime.TotalGameTime.TotalMilliseconds > (timeSinceLastDamage + 200)))
                        {
                            Globals.BossHP -= 3;
                            Globals.BossHits += 1;
                            timeSinceLastDamage = gameTime.TotalGameTime.TotalMilliseconds;
                            if (Globals.BossHP <= 0)
                            {
                                spawnSlimes = true;
                                bossSpawn = true;
                                level++; 
                                
                                bossSlimePosList.Remove(bossSlime);
                                hPPosList.Add(bossSlime);
                                BossKills += 1;
                            }
                        }
                    }
                    for (int i = 0; i < Globals.BulletPosRightList.Count; i++)
                    {
                        Globals.RecBullet = new Rectangle((int)Globals.BulletPosRightList[i].X, (int)Globals.BulletPosRightList[i].Y, Globals.Bullet.Width * 2, Globals.Bullet.Height * 2);
                        if (Globals.RecBossSlime.Intersects(Globals.RecBullet) && (gameTime.TotalGameTime.TotalMilliseconds > (timeSinceLastDamage + 200)))
                        {
                            Globals.BossHP -= 3;
                            Globals.BossHits += 1;
                            timeSinceLastDamage = gameTime.TotalGameTime.TotalMilliseconds;

                            if (Globals.BossHP <= 0)
                            {
                                spawnSlimes = true;
                                bossSpawn = true;
                                level++;

                                bossSlimePosList.Remove(bossSlime);
                                hPPosList.Add(bossSlime);
                                BossKills += 1;
                            }
                        }
                    }

                    if (Globals.RecIlluminati.Intersects(Globals.RecBossSlime) && (gameTime.TotalGameTime.TotalMilliseconds > (timeSinceLastDamage + 500)))
                    {
                        Globals.Health -= 5;
                        timeSinceLastDamage = gameTime.TotalGameTime.TotalMilliseconds;
                    }


                }
            }




            Random slump = new Random();
            for (int i = 0; i < slimePosList.Count; i++)
            {
                if ((slimePosList[i].Y >= (Globals.WindowHeight - Globals.Slime.Height * 2) || slimePosList[i].Y <= 0 ))
                {
                    tempSpeed.X = Globals.SlimeSpeedList[i].X;
                    tempSpeed.Y = -Globals.SlimeSpeedList[i].Y;
                    Globals.SlimeSpeedList[i] = tempSpeed;

                }
                if ((slimePosList[i].X >= (Globals.WindowWidth - Globals.Slime.Width * 2) || slimePosList[i].X <= 0 ))
                {
                    tempSpeed.X = -Globals.SlimeSpeedList[i].X;
                    tempSpeed.Y = Globals.SlimeSpeedList[i].Y;
                    Globals.SlimeSpeedList[i] = tempSpeed;
                }
            }
            
            for (int i = 0; i < bossSlimePosList.Count; i++)
            {
                if ((bossSlimePosList[i].Y >= (Globals.WindowHeight - Globals.BossSlime.Height) || bossSlimePosList[i].Y <= 0))
                {
                    tempBossSpeed.X = Globals.BossSlimeSpeedList[i].X;
                    tempBossSpeed.Y = -Globals.BossSlimeSpeedList[i].Y;
                    Globals.BossSlimeSpeedList[i] = tempBossSpeed;

                }
                if ((bossSlimePosList[i].X >= (Globals.WindowWidth - Globals.BossSlime.Width) || bossSlimePosList[i].X <= 0))
                {
                    tempBossSpeed.X = -Globals.BossSlimeSpeedList[i].X;
                    tempBossSpeed.Y = Globals.BossSlimeSpeedList[i].Y;
                    Globals.BossSlimeSpeedList[i] = tempBossSpeed;
                }
            }
            
            //bossSlimeSpeed.X = 2f;
            //bossSlimeSpeed.Y = 2f;


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
            for (int i = 0; i < Globals.BulletPosRightList.Count; i++)
            {
                Vector2 temp_bulletRight;
                temp_bulletRight.X = Globals.BulletPosRightList.ElementAt(i).X;
                temp_bulletRight.Y = Globals.BulletPosRightList.ElementAt(i).Y;
                temp_bulletRight.X = temp_bulletRight.X - Globals.BulletSpeedRight.X;
                if (temp_bulletRight.X < 0)
                {
                    Globals.BulletPosRightList.RemoveAt(i);
                }
                else
                {
                    Globals.BulletPosRightList.RemoveAt(i);
                    Globals.BulletPosRightList.Insert(i, temp_bulletRight);
                }
            }
            base.Update(gameTime);
        }

        

        protected override void Draw(GameTime gameTime)
        {

            KeyboardState keyboardState = Keyboard.GetState();
            // TODO: Add your drawing code here

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);    // Start Menu
            if (Globals.Start == false)
            {
                GraphicsDevice.Clear(Color.DarkGray); //                 Placement X                    Placement Y        Width   Height   

                spriteBatch.Draw(Globals.StartSpace, new Rectangle(Globals.WindowWidth / 2 - 280, Globals.WindowHeight - 340, 600, 200), Color.White);

                spriteBatch.DrawString(gameFont, "Use WASD for Movement", new Vector2(30, 35), Color.Cyan);

                spriteBatch.DrawString(gameFont, "Use UP and Right arrows to Shoot !!!", new Vector2(30, 60), Color.Cyan);
                spriteBatch.DrawString(gameFont, "Every Level = 1 extra Slime", new Vector2(30, 10), Color.Cyan);

            }
            
            if (keyboardState.IsKeyDown(Keys.Space) && Globals.Start == false)
            {
                Globals.Start = true;
                
                foreach(Vector2 slime in slimePosList.ToList()) 
                {
                    slimePosList.Remove(slime);
                    Globals.SlimeSpeedList.Remove(slime);
                }
                foreach(Vector2 boss in bossSlimePosList.ToList())
                {
                    bossSlimePosList.Remove(boss);
                    Globals.BossSlimeSpeedList.Remove(boss);
                }
                ;
                bossSpawn = true;
                spawnSlimes = true;
            }

            if (Globals.Health > 0 && Globals.Start == true)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Draw(Globals.BackgroundMap, new Rectangle(0, 0, Globals.WindowWidth, Globals.WindowHeight), Color.White);
                spriteBatch.Draw(Globals.Illuminati, Globals.IlluminatiPos, Color.White);
                foreach (Vector2 coinPos in coinPosList)
                {
                    spriteBatch.Draw(Globals.Coin, new Rectangle(coinPos.ToPoint(), new Point(30, 30)), Color.White);
                }
                foreach (Vector2 slimePos in slimePosList)
                {
                    spriteBatch.Draw(Globals.Slime, new Rectangle(slimePos.ToPoint(), new Point(50, 50)), Color.White);

                }
               
                foreach (Vector2 potion in hPPosList)
                {
                    spriteBatch.Draw(HP, potion , Color.White);
                }
                
                foreach (Vector2 bullets in Globals.BulletPosList)
                {
                    spriteBatch.Draw(Globals.Bullet, bullets, Color.White);
                }
                foreach (Vector2 bulletsRight in Globals.BulletPosRightList)
                {
                    spriteBatch.Draw(Globals.BulletRight, bulletsRight, Color.White);
                }
                if(slimePosList.Count == 0) 
                {
                    foreach (Vector2 bossSlimePos in bossSlimePosList)
                    {
                        spriteBatch.Draw(Globals.BossSlime, bossSlimePos, Color.White);
                    }

                }
                spriteBatch.DrawString(gameFont, "BitCoins: " + points, new Vector2(10, 10), Color.White);
                spriteBatch.DrawString(gameFont, "Bullets Up: " + Globals.BulletPosList.Count(), new Vector2(10, 35), Color.White);
                spriteBatch.DrawString(gameFont, "Bullets Right: " + Globals.BulletPosRightList.Count(), new Vector2(10, 110), Color.White);
                spriteBatch.Draw(healthBarTex, new Rectangle(10, Globals.WindowHeight - (Globals.Health * 2 + 10), 20, +Globals.Health * 2), Color.Red);
                if(slimePosList.Count == 0) {
                    spriteBatch.Draw(healthBarTex, new Rectangle(Globals.WindowWidth - 10, Globals.WindowHeight - (Globals.BossHP * 2 + 10), 20, + Globals.BossHP * 2), Color.DeepPink);
                }
                
                spriteBatch.DrawString(gameFont, "Kills: " + kills, new Vector2(10, 60), Color.DarkBlue);
                spriteBatch.DrawString(gameFont, "BossKills: " + BossKills, new Vector2(10, 85), Color.DarkBlue);
                spriteBatch.DrawString(gameFont, "BossHits: " + Globals.BossHits, new Vector2(10, 135), Color.DarkBlue);
                spriteBatch.DrawString(gameFont, "Health: " + Globals.Health, new Vector2(Globals.WindowWidth - 100, 35), Color.Red);
                spriteBatch.DrawString(gameFont, "Enemys left: " + slimePosList.Count, new Vector2(10, 160), Color.DarkBlue);
                spriteBatch.DrawString(gameFont, "SlimeSpawn: " + spawnSlimes, new Vector2(10, 185), Color.DarkBlue);
                spriteBatch.DrawString(gameFont, "BossSpawn: " + bossSpawn, new Vector2(10, 210), Color.DarkBlue);
                spriteBatch.DrawString(gameFont, "Boss Health: " + Globals.BossHP, new Vector2(Globals.WindowWidth - 100, 60), Color.DeepPink);
                spriteBatch.DrawString(gameFont, "Level: " + level, new Vector2( Globals.WindowWidth - 70, 10), Color.Black);



            }
            else if (Globals.Health <= 0 && Globals.Start == true) 
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    Globals.Start = false;
                    Globals.Health = 100;


                }
                GraphicsDevice.Clear(Color.DarkGray);
                Vector2 gameOverPos = new Vector2();
                spriteBatch.Draw(GameOver, new Rectangle(Globals.WindowWidth / 2 - 40, Globals.WindowHeight - 300, 100, 100), Color.Red);
                spriteBatch.DrawString(gameFont, "Press Enter to Play again", new Vector2(325, 60), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}