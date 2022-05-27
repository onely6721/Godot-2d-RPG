using Godot;
using System;

public class Enemy : KinematicBody2D
{
    protected enum State : int
    {
        move = 0,
        stay = 1,
        attack = 2
    }

    [Export] protected float maxHP = 300;
    [Export] public int speed = 50;
    [Export] protected float dmg = 25;

    protected float currentHP;

    protected Vector2 velocity = new Vector2();

    protected AnimatedSprite _animatedSprite;
    protected TextureRect _currentHP;

    protected Vector2 positionPlayer = new Vector2();
    protected Player _player;
    protected Timer _timer;

    protected State state = State.stay;
    protected int dir = 1;
    protected bool range = false;

    protected Vector2 spawnPosition;
    protected Vector2 movePosition;

    virtual public void SetMovePoint()
    {

       


        if (state == State.stay && _timer.IsStopped())
        {
            state = State.move;
            movePosition.y = Position.y;

            GD.Print("vd");
            if (dir == 1)
            {
                dir = -dir;
                movePosition.x = Position.x + 200;
            }
            else
            {
                dir = -dir;
                movePosition.x = Position.x - 200;
            }

         
        }
    }

    virtual public void AnimationManager()
    {
        if (state ==  State.stay)
        {
            _animatedSprite.Frame = 0;

            if (dir == 1)
                _animatedSprite.Play("move_left");
            else
                _animatedSprite.Play("move_right");

            _animatedSprite.Stop();
        } else if (state == State.attack && range == true)
        {
            if (dir == 1)
                _animatedSprite.Play("attack_left");
            else
                _animatedSprite.Play("attack_right");
        }
        else 
        {
            if (dir == 1)
                _animatedSprite.Play("move_left");
            else
                _animatedSprite.Play("move_right");
        }

    }

    public void TakeDmg(float dmg)
    {
        currentHP -= dmg;
        ParamController();
        state = State.attack;
    }

    public void ParamController()
    {
        var scale = new Vector2(1, 1);
        if (maxHP != 0)
            scale.x = currentHP / maxHP;
        _currentHP.RectScale = scale;
        if (currentHP <= 0)
            QueueFree();
    }


    public override void _Ready()
    {
       
        _animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        _currentHP = GetNode<TextureRect>("CurrentHP");
        _player = GetParent().GetNode<Player>("Player");
        _timer = GetNode<Timer>("Timer");

        currentHP = maxHP;
        spawnPosition = Position;

        AddToGroup("enemies");
        ParamController();
        


    }

    private void OnAnimationFinished()
    { 
    
        if(_animatedSprite.Animation == "attack_right" || _animatedSprite.Animation == "attack_left")
        {
            _player.TakeDmg(dmg);
        }
    }

    public override void _Input(InputEvent @event)
    {
        
    }


    public override void _PhysicsProcess(float delta)
    {
        ZIndex = (int)Position.y;
        AnimationManager();
        SetMovePoint();

        GD.Print(Position);

        if (state == State.attack)
        {
            positionPlayer = _player.Position;
            velocity = Position.DirectionTo(positionPlayer) * speed;

            if (Position.x > positionPlayer.x)
                dir = 1;
            else
                dir = -1;

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

        if (state == State.move)
        {
           

            if (Position.DistanceTo(movePosition) > 10)
            {
                velocity = Position.DirectionTo(movePosition) * speed;
                velocity = MoveAndSlide(velocity);
            }
            else
            {
                state = State.stay;
                _timer.Start();
            }


        }




    }

}


