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
    AssaultRifle pelaajanase;
    public override void Begin()
    {
       
        
        SmoothTextures = false;
        MediaPlayer.Play("Dark Music - The Sealed Kingdom");
        Kentta();

        //CreateMonster();


        Keyboard.Listen(Key.Space, ButtonState.Down, AmmuAseella, "Ammu", pelaajanase);
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.A, ButtonState.Down, LiikutaPelaajaa, null, new Vector(-200, 0));
        Keyboard.Listen(Key.D, ButtonState.Down,
        LiikutaPelaajaa, null, new Vector(200, 0));
        Keyboard.Listen(Key.W, ButtonState.Down,
        LiikutaPelaajaa, null, new Vector(0, 200));
        Keyboard.Listen(Key.S, ButtonState.Down,
        LiikutaPelaajaa, null, new Vector(0, -200)); 
        Keyboard.Listen(Key.D, ButtonState.Released,
        LiikutaPelaajaa, null, new Vector(0, 0));
        Keyboard.Listen(Key.W, ButtonState.Released,
        LiikutaPelaajaa, null, new Vector(0, 0));
        Keyboard.Listen(Key.S, ButtonState.Released,
        LiikutaPelaajaa, null, new Vector(-0, 0));
        Keyboard.Listen(Key.A, ButtonState.Released,
        LiikutaPelaajaa, null, new Vector(-0, 0));





    }
    void LiikutaPelaajaa(Vector vector)
    {
        Steve.Walk(-100); 

    }
    void LiikutaPelaajaaOikea(Vector vector)
    {
        Steve.Walk(100);
    
    
    
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
    void AmmuAseella(AssaultRifle ase)
    {
        PhysicsObject ammus = ase.Shoot();

        if (ammus != null)
        {
            ammus.Size *= 0.5;
            //ammus.Image = ...
            ammus.MaximumLifetime = TimeSpan.FromSeconds(5.0);
        }

       
    } 
    void LuoPelaaja(Vector paikka, double korkeus, double leveys) 
    {
        Steve = new PlatformCharacter(15, 15);
        Steve.Position = paikka;
        pelaajanase = new AssaultRifle(15.0, 10.0);
        pelaajanase.Ammo.Value = 10; 
        Steve.Image = SteveKuva;
        pelaajanase.ProjectileCollision = AmmusOsui;
        Steve.Add(pelaajanase);
        Add(Steve);
        Camera.Follow(Steve);
        Camera.ZoomFactor = 10; 

    
    
    }
    //void CreateMonster()

    //PhysicsObject Monster = new PhysicsObject(60, 60);
    //Monster.Shape = Shape.Circle; 


    //RandomMoverBrain MonsterBarain = new RandomMoverBrain(220);
    //MonsterBarain.ChangeMovementSeconds = 2;
    //Monster.Brain = MonsterBarain;

    //Add(Monster); 




    //AddCollisionHandler<Surface, PhysicsObject>(VasenReuna, Steve);


    //void Tormaus(PhysicsObject reuna, PhysicsObject kohde)
    //{ 
}
