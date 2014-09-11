using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class DarkWeed : PhysicsGame
{
    Image TaustaKuva = LoadImage("Kentanaita"); 
    PhysicsObject YlaReuna;
    PhysicsObject AlaReuna;
    PhysicsObject VasenReuna;
    PhysicsObject OikeaReuna;
    PhysicsObject Steve;
    public override void Begin()
    {
        Steve = new PhysicsObject(10, 10);
        Steve.Shape = Shape.Circle;    
        Steve.Restitution = 2.0;
        Add(Steve);      
        Camera.Follow(Steve);
        NormKenttä();
        Camera.ZoomFactor = 1;
        //CreateMonster();
        
        
      
        PhoneBackButton.Listen(ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.Escape, ButtonState.Pressed, ConfirmExit, "Lopeta peli");
        Keyboard.Listen(Key.A, ButtonState.Down, LiikutaPelaajaa, null, new Vector(-200, 0));
        Keyboard.Listen(Key.D, ButtonState.Down,
        LiikutaPelaajaa, null, new Vector(200, 0));
        Keyboard.Listen(Key.W, ButtonState.Down,
        LiikutaPelaajaa, null, new Vector(0, 200));
        Keyboard.Listen(Key.S, ButtonState.Down,
        LiikutaPelaajaa, null, new Vector(00, -200));
        Keyboard.Listen(Key.D, ButtonState.Released,
        LiikutaPelaajaa, null, new Vector(0, 0));
        Keyboard.Listen(Key.W, ButtonState.Released,
        LiikutaPelaajaa, null, new Vector(0, 0));
        Keyboard.Listen(Key.S, ButtonState.Released,
        LiikutaPelaajaa, null, new Vector(0, -0)); 
        Keyboard.Listen(Key.A, ButtonState.Released, 
        LiikutaPelaajaa, null, new Vector(-0, 0));
        

         

    }
    void LiikutaPelaajaa(Vector vector)
    {
        Steve.Velocity = vector;
    
    }
    //void CreateMonster()
  
     //PhysicsObject Monster = new PhysicsObject(60, 60);
     //Monster.Shape = Shape.Circle; 
     
    
     //RandomMoverBrain MonsterBarain = new RandomMoverBrain(220);
     //MonsterBarain.ChangeMovementSeconds = 2;
     //Monster.Brain = MonsterBarain;
     
     //Add(Monster); 
   
    void NormKenttä()
    {
     VasenReuna = new PhysicsObject(10, 100);
     AlaReuna = new PhysicsObject(100, 10);
     OikeaReuna = new PhysicsObject(10, 100);
     YlaReuna = new PhysicsObject(100, 10);
     Add(VasenReuna);
     Add(OikeaReuna);
     Add(YlaReuna);
     Add(AlaReuna);
     Level.Background.Image = TaustaKuva;
     VasenReuna.X = 200;
     VasenReuna.Y = 200;
     OikeaReuna.X = 200;
     OikeaReuna.Y = 200;
     YlaReuna.X = 200;
     YlaReuna.Y = 200;
     AlaReuna.X = 200;
     AlaReuna.Y = 200;
    }
     
     //AddCollisionHandler<Surface, PhysicsObject>(VasenReuna, Steve);
    
     
    //void Tormaus(PhysicsObject reuna, PhysicsObject kohde)
    //{ 
    
     
 }

      
    


