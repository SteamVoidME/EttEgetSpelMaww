using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Principal;

public static class Globals
{
    static Vector2 illuminatiPos = new Vector2(0, 0), gameOverPos = new Vector2(0, 0), hPPos = new Vector2(0, 0), bossSlimePos = new Vector2(0, 0), illuminatiSpeed, coin_pos = new Vector2(0, 0), bulletPos = new Vector2(0, 0), bulletSpeed, bulletSpeedRight;
    static int windowWidth, windowHeight, health = 100, stHP, bossHP = 200, bossHits = 0;
    static Texture2D illuminati, coin, slime, bullet, bulletRight, startSpace, backgroundMap, bossSlime;
    static Rectangle recIlluminati, recCoin, recSlime, recBullet, recBossSlime, recHP;
    static List<Vector2> bulletPosList = new List<Vector2>();
    static List<Vector2> bulletPosRightList = new List<Vector2>();
    static bool start = false;
    static List<Vector2> slimeSpeedList = new List<Vector2>();
    static List<Vector2> bossSlimeSpeedList = new List<Vector2>();


    public static Texture2D Illuminati
    {
        get { return illuminati; }
        set { illuminati = value; }
    }
    
    public static Texture2D BossSlime
    {
        get { return bossSlime; }
        set { bossSlime = value; }
    }
    public static int StHP
    {
        get { return stHP; }
        set { stHP = value; }
    }
    public static int BossHits
    {
        get { return bossHits; }
        set { bossHits = value; }
    }
    public static int BossHP
    {
        get { return bossHP; }
        set { bossHP = value; }
    }
    public static Rectangle RecBullet
    {
        get { return recBullet; }
        set { recBullet = value; }
    }
    public static Rectangle RecHP
    {
        get { return recHP; }
        set { recHP = value; }
    }
    public static Rectangle RecBossSlime
    {
        get { return recBossSlime; }
        set { recBossSlime = value; }
    }
    public static Texture2D BackgroundMap
    {
        get { return backgroundMap; }
        set { backgroundMap = value; }
    }
    public static Texture2D StartSpace
    {
        get { return startSpace; }
        set { startSpace = value; }
    }
    public static bool Start
    {
        get { return start; }
        set { start = value; }
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
    public static Texture2D BulletRight
    {
        get { return bulletRight; }
        set { bulletRight = value; }
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
    public static Vector2 IlluminatiPos
    {
        get { return illuminatiPos; }
        set { illuminatiPos = value; }
    }
    public static Vector2 GameOverPos
    {
        get { return gameOverPos; }
        set { gameOverPos = value; }
    }
    public static Vector2 HPPos
    {
        get { return hPPos; }
        set { hPPos = value; }
    }
    public static Vector2 IlluminatiSpeed
    {
        get { return illuminatiSpeed; }
        set { illuminatiSpeed = value; }
    }
    public static Rectangle RecIlluminati
    {
        get { return recIlluminati; }
        set { recIlluminati = value; }
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
    public static List<Vector2> BulletPosRightList
    {
        get { return bulletPosRightList; }
        set { bulletPosRightList = value; }
    }
    public static List<Vector2> SlimeSpeedList
    {
        get { return slimeSpeedList; }
        set { slimeSpeedList = value; }
    }
    public static List<Vector2> BossSlimeSpeedList
    {
        get { return bossSlimeSpeedList; }
        set { bossSlimeSpeedList = value; }
    }
    public static Vector2 BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }
    public static Vector2 BulletSpeedRight
    {
        get { return bulletSpeedRight; }
        set { bulletSpeedRight = value; }
    }
}
