using Godot;
using System;

public class Spawner : Node2D
{
    [Export]  public string  enemyName = "Enemy";

    private PackedScene _enemyScene;


    [Export] public int max_enemy = 5;
    public int counter = 0;
    public override void _Ready()
    {
        _enemyScene =  (PackedScene)GD.Load("res://Scenes/" + enemyName + ".tscn");
     
        

    }

    public  void OnTimerTimeout()
    {
        if (counter < max_enemy)
        {
            var rndX = new Random();
            var rndY = new Random();

            float x = rndX.Next(0, 800);
            float y = rndY.Next(0, 800);


            var position = new Vector2(x, y);

            var enemy = _enemyScene.Instance<Enemy>();

            enemy.Position = Position + position;
            GetParent().AddChild(enemy);

            counter++;
        }

    }
}
