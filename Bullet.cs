using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;

public class Bullet
{
	Vector2 i, bulletPos, bulletPosRight, bulletSpeed, bulletSpeedRight;
	KeyboardState keyboardState = Keyboard.GetState();
	double timeSinceLastBullet;
    MouseState mouseState = Mouse.GetState();

    public Bullet()
	{

		bulletSpeed.Y = 6f;
		bulletSpeed.X = 0;
        Globals.BulletSpeed = bulletSpeed; 

        bulletSpeedRight.Y = 0;
        bulletSpeedRight.X = -6;
        Globals.BulletSpeedRight= bulletSpeedRight;
	}
	public void SpawnBullet(GameTime gameTime)
	{
        MouseState mouseState = Mouse.GetState();
        KeyboardState keyboardState = Keyboard.GetState();
        
        
        if (Keyboard.GetState().IsKeyDown(Keys.Up) && (gameTime.TotalGameTime.TotalMilliseconds > (timeSinceLastBullet + 170)))
		{
			bulletPos.X = Globals.IlluminatiPos.X + Globals.Illuminati.Width / 2;
            bulletPos.Y = Globals.IlluminatiPos.Y;
			Globals.BulletPosList.Add(bulletPos);

			timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
        }
        if (Keyboard.GetState().IsKeyDown(Keys.Right) && (gameTime.TotalGameTime.TotalMilliseconds > (timeSinceLastBullet + 170)))
        {
            bulletPosRight.X = Globals.IlluminatiPos.X ;
            bulletPosRight.Y = Globals.IlluminatiPos.Y + Globals.Illuminati.Height / 2;
            Globals.BulletPosRightList.Add(bulletPosRight);
            
            timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
        }




    }
	
}
