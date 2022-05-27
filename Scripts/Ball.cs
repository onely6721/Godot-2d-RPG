using Godot;
using System;

public class Ball : KinematicBody2D
{
    public Vector2 target = new Vector2();
    public Vector2 velocity = new Vector2();
    public Vector2 spawnPosition = new Vector2();
    private Sprite _sprite;

    public float dmg = 50;

    public int speed = 400;

    public Player.Skill skill = Player.Skill.defaultAttack;
   

    public override void _Ready()
    {
        spawnPosition = GetParent().GetNode<Player>("Player").Position;
        _sprite = GetNode<Sprite>("Sprite");
        Position = spawnPosition;

        LookAt(target);
        

    }

    public void _OnBallBodyEntered(Node2D body)
    {
        if(body.IsInGroup("enemies"))
        {
           
            if (skill == Player.Skill.defaultAttack)
            {
                body.Call("TakeDmg", dmg);
            }
            else if (skill == Player.Skill.fireAttack)
            {
                body.Call("TakeDmg", dmg);
                var vertex = GetNode<Particles2D>("Vertex");
                vertex.Show();
      
            }

            QueueFree();
            

        }
        
    }

    public void OnTimerTimeout()
    {
        QueueFree();
    }

  public override void _Process(float delta)
  {
        velocity = spawnPosition.DirectionTo(target) * speed;
        velocity = MoveAndSlide(velocity);
        GD.Print(GetNode<Timer>("Timer").TimeLeft);

  }
}
