using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;

public static class Globals
{
    static Vector2 myship_pos = new Vector2(0, 0), myshipSpeed, coin_pos = new Vector2(0, 0), bulletPos = new Vector2(0, 0), bulletSpeed;
    static int windowWidth, windowHeight, health = 100;
    static Texture2D myship, coin, slime, bullet, gameOver;
    static Rectangle recMyShip, recCoin, recSlime;
    static List<Vector2> bulletPosList = new List<Vector2>();


    public static Texture2D Myship
    {
        get { return myship; }
        set { myship = value; }
    }
    public static Texture2D GameOver
    {
        get { return gameOver; }
        set { gameOver = value; }
    }
    public static Rectangle RecSlime
    {
        get { return recSlime; }
        set { recSlime = value; }
    }
    public static Texture2D Bullet
    {
        get { return bullet; }
        set { bullet = value; }
    }
    public static int WindowWidth
    {
        get { return windowWidth; }
        set { windowWidth = value; }
    }

    public static int WindowHeight
    {
        get { return windowHeight; }
        set { windowHeight = value; }
    }
    public static Vector2 Myship_pos
    {
        get { return myship_pos; }
        set { myship_pos = value; }
    }
    public static Vector2 MyshipSpeed
    {
        get { return myshipSpeed; }
        set { myshipSpeed = value; }
    }
    public static Rectangle RecMyShip
    {
        get { return recMyShip; }
        set { recMyShip = value; }
    }
    public static Rectangle RecCoin
    {
        get { return recCoin; }
        set { recCoin = value; }
    }
    public static Texture2D Coin
    {
        get { return coin; }
        set { coin = value; }
    }
    public static Vector2 Coin_pos
    {
        get { return coin_pos; }
        set { coin_pos = value; }
    }
    public static Texture2D Slime
    {
        get { return slime; }
        set { slime = value; }
    }
    public static int Health
    {
        get { return health; }
        set { health = value; }
    }
    public static Vector2 BulletPos
    {
        get { return bulletPos; }
        set { bulletPos = value; }
    }
    public static List<Vector2> BulletPosList
    {
        get { return bulletPosList; }
        set { bulletPosList = value; }
    }
    public static Vector2 BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }
}
