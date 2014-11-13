using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Effects;
using Jypeli.Widgets;

public class DrakNest : PhysicsGame 
{    
    Image Monsteri = LoadImage("Monsteri");
    Image SteveKuva = LoadImage("Steve");
    Image BlokinKuva = LoadImage("Kentanaita");
    PlatformCharacter Steve;
    Image TaustaKuva = LoadImage("BR");
    AssaultRifle pelaajanase;  
    private Image[] ukkelinKavely = LoadImages("Steve1", "Steve2", "Steve3");
    List<Vector> Spawnipaikat = new List<Vector>();
    IntMeter IntMeter1;
    PhysicsObject Blokki;
    public override void Begin()  
    {
        //ukkelinkavely = LoadAnimation("SteveAnim");
        Gravity = new Vector(0, -1000);
        SmoothTextures = false; 
        Kentta();
        LuoPistelaskuri();
        IsPaused = true;
        Pause();
        MediaPlayer.Play("TristTram");
        MediaPlayer.IsRepeating = true;



        MultiSelectWindow alkuValikko = new MultiSelectWindow("Pelin alkuvalikko",
        "Aloita peli", "Parhaat pisteet", "Lopeta");
        Add(alkuValikko);

       


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
        Keyboard.Listen(Key.A, ButtonState.Released, LopetaAnimointi, null);
        Keyboard.Listen(Key.D, ButtonState.Released, LopetaAnimointi, null);
        Keyboard.Listen(Key.E, ButtonState.Pressed, Pause, "Pysäyttää pelin");




    }
    void LopetaAnimointi()
    {
        Steve.Animation.Stop();
    }
    void LiikutaPelaajaa(int suunta)
    {
        Steve.Walk(suunta);
        if (!Steve.Animation.IsPlaying) Steve.Animation.Start();

    }
    void Ylos()
    {
        Steve.Hit(new Vector(0, 50));
    }
     void Alas() 
     {
     
     //Steve.Hit(new Vector(0, -1000));
     
     }         
    void Kentta()
    {
        ColorTileMap Ruudut = ColorTileMap.FromLevelAsset("Kentta");
        Ruudut.SetTileMethod(Color.FromHexCode("#000000"), LuoTaso);
        Ruudut.SetTileMethod(Color.FromHexCode("#FF6A00"), LuoPelaaja);
        Ruudut.SetTileMethod(Color.FromHexCode("#FF0000"), LisaaVihollinen);
        Ruudut.Execute(20, 20);
        LuoAikaLaskuri(20.0);
        Level.Background.Image = TaustaKuva;
        Level.Background.FitToLevel();

       
        
        


    }
    void LuoTaso(Vector paikka, double leveys, double korkeus)
    {
        Blokki = PhysicsObject.CreateStaticObject(leveys, korkeus);
        Blokki.Position = paikka;
        Blokki.Image = BlokinKuva;
        Blokki.Tag = "Seina";
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
        
        if (kohde.Tag == "Seina")
        {
            return;
        }
        IntMeter1.Value += 1;
        
        if (kohde.Tag == "Monsteri")
        {

            kohde.Destroy();
        }

    }
    void AmmuAseella(AssaultRifle ase)
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
        Steve = new PlatformCharacter(15, 30);
        Steve.Position = paikka;
        pelaajanase = new AssaultRifle(15, 5); 
         
        Steve.Image = SteveKuva; 
        pelaajanase.ProjectileCollision = AmmusOsui;
        //Steve.Add(pelaajanase) 
        Steve.Weapon = pelaajanase;
        AddCollisionHandler(Steve, "Monsteri", TormaaMonsteriin);
        Steve.Animation = new Animation(ukkelinKavely);
        Add(Steve);
        Camera.Follow(Steve);
        Camera.ZoomFactor = 2; 

    
    
  
  
       
    }
    void TormaaMonsteriin(PhysicsObject Tormaaja, PhysicsObject Kohde)
    {
     Tormaaja.Destroy();
     IntMeter1.Value+= 1;
     
    
    
    
    }
    void LuoAikaLaskuri(double aika)
    {
        Timer ajastin = new Timer();
        ajastin.Interval = aika;
        ajastin.Timeout += LisaaVihollisia;
        ajastin.Start();
    
    
    
    }
    void LisaaVihollinen(Vector paikka, double leveys, double korkeus)
    { 
        /*PhysicsObject Monster = new PhysicsObject(leveys, korkeus);
        Add(Monster);
        Monster.Position = paikka;
        Monster.Shape = Shape.Circle;
        Monster.Image = Monsteri;
        Monster.Tag = "Monsteri";

        RandomMoverBrain MonsterBarain = new RandomMoverBrain(220);
        MonsterBarain.ChangeMovementSeconds = 2;
        Monster.Brain = MonsterBarain;
    */
        Spawnipaikat.Add(paikka);
    }
    void LisaaVihollisia()
    {

        foreach (Vector paikka in Spawnipaikat)
        {
            PhysicsObject Monster = new PhysicsObject(40, 40);
            Add(Monster);
            Monster.Position = paikka;
            Monster.Shape = Shape.Circle;
            Monster.Image = Monsteri;
            Monster.Tag = "Monsteri";

            RandomMoverBrain MonsterBarain = new RandomMoverBrain(220);
            MonsterBarain.ChangeMovementSeconds = 2;
            Monster.Brain = MonsterBarain;
        }
    } 
    void LuoPistelaskuri() 
    {
       IntMeter1 = new IntMeter(0);
            Label pisteNaytto = new Label();
            pisteNaytto.X = Screen.Left + 100;
            pisteNaytto.Y = Screen.Top - 100;
            pisteNaytto.TextColor = Color.Red;
            pisteNaytto.Color = Color.White;

            pisteNaytto.BindTo(IntMeter1);
            Add(pisteNaytto);      
    } 





}
     
     



     




