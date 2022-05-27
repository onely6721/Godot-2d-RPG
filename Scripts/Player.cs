using Godot;
using System;

public class Player : KinematicBody2D
{
	private PackedScene _arrowScene = (PackedScene)GD.Load("res://Scenes/Ball.tscn");
	public enum State : int
	{
		move = 0,
		stay = 1,
		attack = 2,
		casting = 3
	}

    public enum Skill : int
    {
		defaultAttack = 0,
		fireAttack = 1,
		buff = 2,
		heal = 3
	}


	[Export] public int speed = 50;
	[Export] public int lvl;
	[Export] public float maxHP = 200;
	[Export] public float maxMP = 200;
	[Export] public int dmg = 50;


	private Resource defaultCursor;
	private Resource attackCursor;

	public float currentHP = 50;
	public float currentMP = 100;

	private bool attacking = false;

	private AnimatedSprite _animatedSprite;
	private Camera2D _camera2D;
	private Inventory _inventory;
	private Skillbar _skillbar;
	private SkillVariables _skillVariables;



	public Vector2 mousePos = new Vector2(0, 0);
	public Vector2 target = new Vector2(0,0);
	public Vector2 velocity = new Vector2();
	public Vector2 cameraScale = new Vector2(1,1);

	public int dir = 1;
	public State state = State.stay;
	public Skill skill = Skill.defaultAttack;
	
	public override void _Ready()
	{
		defaultCursor = ResourceLoader.Load("res://Resources/Sprites/Cursors/cursor.png");
		attackCursor = ResourceLoader.Load("res://Resources/Sprites/Cursors/attack.png");

		_animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		_camera2D = GetNode<Camera2D>("Camera2D");
		_inventory = GetParent().GetNode<Inventory>("GUI/Inventory");
		_skillbar = GetParent().GetNode<Skillbar>("GUI/Skillbar");
		_skillVariables = (SkillVariables)GetNode("/root/SkillVariables");

		Input.SetCustomMouseCursor(defaultCursor);
		Load();
	}


	public void Dead()
	{
		return;
	}

	public float GetMaxHp()
	{
		return 1;
	}

	public float GetMaxMp()
    {
		return 1;
    }

	public float GetDmg()
    {
		float resultDmg = dmg + lvl * 5;

		return resultDmg;
    }



    public void TakeDmg(float dmg)
    {
		currentHP -= dmg;
        if (currentHP < 0)
        {
			Dead();
		}


	}


	public void Attack()
    {
		
		var mousePosition = GetGlobalMousePosition();
		SetDirection(mousePosition);
		attacking = true;
		mousePos = mousePosition;

		if(skill == Skill.defaultAttack)
			_skillbar.GetNode<Timer>("DefaultAttack/Timer").Start();
		else if (skill == Skill.fireAttack)
			_skillbar.GetNode<Timer>("FireAttack/Timer").Start();

	}


	
	public void Heal()
	{
		currentHP += _skillVariables.healHp + _skillVariables.healHp * _skillVariables.healSkillLvl/5;
        if (currentHP > maxHP)
        {
			currentHP = maxHP;
		}

	}

	public void Buff()
    {
		dmg += _skillVariables.buffDmg + _skillVariables.buffDmg * _skillVariables.buffSkillLvl / 5;
		GetNode<Particles2D>("vertex").Hide();
		GetNode<Particles2D>("slave").Show();
	}

	public void _OnAnimatedSpriteAnimationFinished()
	{
		if (_animatedSprite.Animation == "attack_right" || _animatedSprite.Animation == "attack_left")
		{
			var arrow = _arrowScene.Instance<Ball>();
			var mousePosition = GetGlobalMousePosition();
			arrow.target = mousePos;
			arrow.dmg = dmg;
			arrow.skill = skill;
			if (skill == Skill.defaultAttack)
			{
				arrow.dmg += _skillVariables.defaultDmg + _skillVariables.defaultDmg * _skillVariables.defaultSkillLvl / 5;
			}
			else if (skill == Skill.fireAttack)
            {
				arrow.dmg += _skillVariables.fireDmg + _skillVariables.fireDmg * _skillVariables.fireSkillLvl / 5;
            }

                   


			GetParent().AddChild(arrow);

			state = State.stay;
			attacking = false;
		}
		else if (_animatedSprite.Animation == "cast_right" || _animatedSprite.Animation == "cast_left")
		{
			if (skill == Skill.heal)
				Heal();
			else if (skill == Skill.buff)
				Buff();

			state = State.stay;

		}
	}

	public Godot.Collections.Dictionary<string, object> Save()
	{
		return new Godot.Collections.Dictionary<string, object>()
		{

			{ "PosX", Position.x }, 
			{ "PosY", Position.y },
			{ "CurrentHP", currentHP },
			{ "CurrentMP", currentMP },
			{ "Lvl", lvl },
			{ "maxHP", maxHP },
			{ "maxMP", maxMP },
			{ "Dir", dir }
   
		};
	}

  
	public void Load()
	{
		var saveGame = new File();
		if (!saveGame.FileExists("res://Saves/save.dat"))
			return;

		saveGame.Open("res://Saves/save.dat", File.ModeFlags.Read);

		while (saveGame.GetPosition() < saveGame.GetLen())
		{
		   
			var nodeData = new Godot.Collections.Dictionary<string, object>((Godot.Collections.Dictionary)JSON.Parse(saveGame.GetLine()).Result);
			var pos = new Vector2((float)nodeData["PosX"], (float)nodeData["PosY"]);

			Position = pos;
		  
			float v = (float)nodeData["Dir"];
			dir = (int)v;
			   
			

		}

		saveGame.Close();
	}


	public void SetDirection(Vector2 target)
	{

		Vector2 pos_dir = Position.DirectionTo(target);

		if (pos_dir.x < 0)
		{
			dir = 1;
			return;
		   
		}
		else 
		{
			dir = 2;
			return;
		}

	}

	public void AnimationManager()
	{
		if (state == State.move)
		{
			switch (dir)
			{
				case 1:
					_animatedSprite.Play("move_left");
					break;
				case 2:
					_animatedSprite.Play("move_right");
					break;

			}
		}
		else if (state == State.stay)
		{
			_animatedSprite.Frame = 0;
			switch (dir)
			{
				case 1:
					_animatedSprite.Play("attack_left");
					_animatedSprite.Stop();

					break;
				case 2:
					_animatedSprite.Play("attack_right");
					_animatedSprite.Stop();
					break;

			}

		}
		else if (state == State.attack)
		{
			if (attacking)
			{
				switch (dir)
				{
					case 1:
						_animatedSprite.Play("attack_left");
						break;
					case 2:
						_animatedSprite.Play("attack_right");
						break;

				}
			}
			else

			{
				switch (dir)
				{
					case 1:
						_animatedSprite.Play("attack_left");
						break;
					case 2:
						_animatedSprite.Play("attack_right");
						break;

				}

				if (_animatedSprite.Frame == 5)
					_animatedSprite.Stop();
			}

		}
		else if (state == State.casting)
		{
			switch (dir)
			{
				case 1:
					_animatedSprite.Play("cast_left");
					break;
				case 2:
					_animatedSprite.Play("cast_right");
					break;
			}
            if (skill == Skill.buff)
            {
				GetNode<Particles2D>("vertex").Show();
				GetNode<Particles2D>("slave").Hide();
			}

		}
	   
	}



	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_click") && state != State.casting)
		{
			if (state == State.attack)
			{
				Attack();

			}
			else
			{
				if (!_inventory.Visible)
				{
					target = GetGlobalMousePosition();
					SetDirection(target);
					state = State.move;
				}
			}
		}
		else if (@event.IsActionPressed("ui_cancel"))
		{
			var save = GetParent().GetNode<Save>("Save");
			save.SaveGame();
			GetTree().ChangeScene("res://Scenes/MainMenu.tscn");
		}
		else if (@event.IsActionPressed("scroll_down") && cameraScale.x < 1.2 && cameraScale.y < 1.2)
		{
			cameraScale.x += 0.01f;
			cameraScale.y += 0.01f;
		}
		else if (@event.IsActionPressed("scroll_up") && cameraScale.x > 1 && cameraScale.y > 1)
		{
			cameraScale.x -= 0.01f;
			cameraScale.y -= 0.01f;
		}
		else if (@event.IsActionPressed("defaultAttack") && state != State.casting)
		{
			if (_skillbar.GetNode<Timer>("DefaultAttack/Timer").IsStopped())
			{ 
				state = State.attack;
				skill = Skill.defaultAttack;
				
			}

		}
		else if (@event.IsActionPressed("fireAttack") && state != State.casting)
		{
			if (_skillbar.GetNode<Timer>("FireAttack/Timer").IsStopped())
			{
				state = State.attack;
				skill = Skill.fireAttack;
	
			}

		}
		else if (@event.IsActionPressed("buff") && state != State.casting)
		{
			if (_skillbar.GetNode<Timer>("Buff/Timer").IsStopped())
			{
				state = State.casting;
				skill = Skill.buff;
				_skillbar.GetNode<Timer>("Buff/Timer").Start();
			}
		
		}
		else if (@event.IsActionPressed("heal") && state != State.casting)
		{
			if (_skillbar.GetNode<Timer>("Heal/Timer").IsStopped())
			{
				state = State.casting;
				skill = Skill.heal;
				_skillbar.GetNode<Timer>("Heal/Timer").Start();
			}

		}
		else if(@event.IsActionPressed("inventory"))
		{

			_inventory.Visible = !_inventory.Visible;
			
        }



	}


  public override void _PhysicsProcess(float delta)
  {
		ZIndex = (int)Position.y;
		_camera2D.Zoom = cameraScale;

		velocity = Position.DirectionTo(target) * speed;
		
		AnimationManager();


		if (state == State.move)
		{
			if (Position.DistanceTo(target) > 5)
			{
				velocity = MoveAndSlide(velocity);
			}
			else
			{
				state = State.stay;
			}
		}


		if (state == State.attack)
			Input.SetCustomMouseCursor(attackCursor);
		else
			Input.SetCustomMouseCursor(defaultCursor);
		
	   
	}
}
