using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;

public class Bullet
{
	Vector2 i, bulletPos, bulletSpeed;
	KeyboardState keyboardState = Keyboard.GetState();
	double timeSinceLastBullet;


	public Bullet()
	{
		bulletSpeed.Y = 2f;
		bulletSpeed.X = 0;
		Globals.BulletSpeed = bulletSpeed;
	}
	public void SpawnBullet(GameTime gameTime)
	{
        MouseState mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed && gameTime.TotalGameTime.Milliseconds > timeSinceLastBullet + 300)
		{
			bulletPos.X = Globals.Myship_pos.X + Globals.Myship.Width / 2;
            bulletPos.Y = Globals.Myship_pos.Y;
			Globals.BulletPosList.Add(bulletPos);

			timeSinceLastBullet = gameTime.TotalGameTime.Milliseconds;
        }




	}
	
}
