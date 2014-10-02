using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class FysiikkaPeli1 : PhysicsGame
{
    Image SteveKuva = LoadImage("Steve");
    PhysicsObject Steve;
    AssaultRifle pelaajanase;
    public override void Begin()
    {
        Steve = new PhysicsObject(20, 20); 
        Steve.Restitution = 1.0;
        pelaajanase = new AssaultRifle(0.10, 0.10);
        pelaajanase.Ammo.Value = 10;
        pelaajanase.ProjectileCollision = AmmusOsui;

        Steve.Add(pelaajanase);

        AddCollisionHandler(Steve, SteveTormaa);
        SmoothTextures = false;
        MediaPlayer.Play("Dark Music - The Sealed Kingdom");
        Steve.Image = SteveKuva;
        Add(Steve); 
        Camera.Follow(Steve);
        Camera.ZoomFactor = 10;
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
        Steve.Velocity = vector;

    }
    void Kentta()
    {
        ColorTileMap Ruudut = ColorTileMap.FromLevelAsset("Kentta");
        Ruudut.SetTileMethod(Color.FromHexCode("#000000"), LuoTaso); 
        Ruudut.Execute(20, 20);

    }
    void LuoTaso(Vector paikka, double leveys, double korkeus)
    {
        PhysicsObject Blokki = PhysicsObject.CreateStaticObject(leveys, korkeus);
        Blokki.Position = paikka;
        Add(Blokki); 
     

     


    
    
    
    }
    void SteveTormaa(PhysicsObject Tormaaja, PhysicsObject Kohde)
    {
        Angle kulma = new Angle();
        kulma.Degrees = 0;
        Tormaaja.Angle = kulma; 
        
    
    }
    void AmmusOsui(PhysicsObject ammus, PhysicsObject kohde)
    {
    ammus.Destroy();
    
    
    
    } 
    void AmmuAseella(AssaultRifle ase)
    {
    PhysicsObject ammus = ase.Shoot();

    if (ammus != null) ;
   
        ammus.Size *= 0.1;
        //ammus.Image = ...
        ammus.MaximumLifetime = TimeSpan.FromSeconds(2.0);
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
