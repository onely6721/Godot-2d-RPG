using Godot;
using System;

public class Dragon : Enemy
{
 
   

    public override void AnimationManager()
    {
        _animatedSprite.Play("run");

    }


    public override void _Ready()
    {
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _currentHP = GetNode<TextureRect>("CurrentHP");
        currentHP = 300;
        AddToGroup("enemies");
        ParamController();
        _player = GetParent().GetNode<Player>("Player");

    }


 


    public override void _PhysicsProcess(float delta)
    {
        ZIndex = (int)Position.y;
        AnimationManager();

        LookAt(_player.Position);
        RotationDegrees += 90;

        if (state == State.attack)
        {
            positionPlayer = _player.Position;
            velocity = Position.DirectionTo(positionPlayer) * speed;

            if (Position.x > positionPlayer.x)
                dir = 1;
            else
                dir = 2;

            if (Position.DistanceTo(positionPlayer) > 50)
            {
                velocity = MoveAndSlide(velocity);
                range = false;
            }
            else
            {
                range = true;
            }
        }

    }

}


