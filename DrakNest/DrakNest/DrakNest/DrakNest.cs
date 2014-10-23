using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class DrakNest : PhysicsGame
{
    Image SteveKuva = LoadImage("Steve");
    Image BlokinKuva = LoadImage("Kentanaita");
    PlatformCharacter Steve;
    LaserGun pelaajanase;
    public override void Begin()
    {
       
        
        SmoothTextures = false;
        MediaPlayer.Play("Dark Music - The Sealed Kingdom");
        Kentta();

        //CreateMonster();


        Keyboard.Listen(Key.Space, ButtonState.Down, AmmuAseella, "Ammu", pelaajanase);
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.A, ButtonState.Down, LiikutaPelaajaa, null, -200);
        Keyboard.Listen(Key.D, ButtonState.Down,
        LiikutaPelaajaa, null, 200);
        Keyboard.Listen(Key.W, ButtonState.Down,
        Ylos, null);
        Keyboard.Listen(Key.S, ButtonState.Released,
        Alas, null);
        





    }
    void LiikutaPelaajaa(int suunta)
    {
        Steve.Walk(suunta);

    }
    void Ylos()
    {
        Steve.Hit(new Vector(0, 50));
    }
     void Alas() 
     {
     
     Steve.Hit(new Vector(0, -100));
     
     }         
    void Kentta()
    {
        ColorTileMap Ruudut = ColorTileMap.FromLevelAsset("Kentta");
        Ruudut.SetTileMethod(Color.FromHexCode("#000000"), LuoTaso);
        Ruudut.SetTileMethod(Color.FromHexCode("#FF6A00"), LuoPelaaja);
        Ruudut.Execute(20, 20);
        Level.Background.Color = Color.Gray;


    }
    void LuoTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject Blokki = PhysicsObject.CreateStaticObject(leveys, korkeus);
        Blokki.Position = paikka;
        Blokki.Image = BlokinKuva;
        Add(Blokki);

    }
   
   // void SteveTormaa(PhysicsObject Tormaaja, PhysicsObject Kohde)
    //{
        //Angle kulma = new Angle();
        //kulma.Degrees = 0;
        //Tormaaja.Angle = kulma;


    //}
    void AmmusOsui(PhysicsObject ammus, PhysicsObject kohde)
    {
        ammus.Destroy();



    }
    void AmmuAseella(LaserGun ase)
    {
        PhysicsObject ammus = ase.Shoot();

        if (ammus != null)
        {
            ammus.Size *= 0.3;
            //ammus.Image = ...
            ammus.MaximumLifetime = TimeSpan.FromSeconds(5.0);
        }

       
    } 
    void LuoPelaaja(Vector paikka, double korkeus, double leveys) 
    {
        Steve = new PlatformCharacter(15, 15);
        Steve.Position = paikka;
        pelaajanase = new LaserGun(10, 5); 
        pelaajanase.Ammo.Value = 10; 
        Steve.Image = SteveKuva;
        pelaajanase.ProjectileCollision = AmmusOsui;
        //Steve.Add(pelaajanase) 
        Steve.Weapon= pelaajanase;  
        Add(Steve);
        Camera.Follow(Steve);
        Camera.ZoomFactor = 10; 

    
    
    }
    void CreateMonster()
    {
        Add(Monster);
        PhysicsObject Monster = new PhysicsObject(60, 60);
        Monster.Shape = Shape.Circle;


        RandomMoverBrain MonsterBarain = new RandomMoverBrain(220);
        MonsterBarain.ChangeMovementSeconds = 2;
        Monster.Brain = MonsterBarain;
       
    }

}
