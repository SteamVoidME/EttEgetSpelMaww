using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;


public class Move
{
    Vector2 myship_pos, myship_speed;

    public Move()
    {
        myship_pos.X = 365;
        myship_pos.Y = 210;
        myship_speed.X = 3.5f;
        myship_speed.Y = 3.5f;
    }
    
	public void Movement()
	{
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
        Random slump = new Random();
        if (keyboardState.IsKeyDown(Keys.A) && (myship_pos.X >= Globals.WindowWidth - Globals.Illuminati.Width) || keyboardState.IsKeyDown(Keys.W) && (myship_pos.Y >= Globals.WindowHeight - Globals.Illuminati.Height) || keyboardState.IsKeyDown(Keys.D) && (myship_pos.X <= 0) || keyboardState.IsKeyDown(Keys.S) && (myship_pos.Y <= 0))
        {
            myship_speed.X = 3.5f;
            myship_speed.Y = 3.5f;
        }
        else if ((myship_pos.X >= Globals.WindowWidth - Globals.Illuminati.Width) || myship_pos.Y >= Globals.WindowHeight - Globals.Illuminati.Height || myship_pos.X <= 0 || myship_pos.Y <= 0)
        {
            myship_speed.X = 0;
            myship_speed.Y = 0;
        }
        Globals.IlluminatiPos = myship_pos;
    }
}
