using Godot;
using System;

public class Skill : TextureRect
{


    public Label _cooldown;
    public Label _key;

    public Timer _timer;

    public override void _Ready()
    {
        _cooldown = GetNode<Label>("Cooldown");
        _key = GetNode<Label>("Key");
        _timer = GetNode<Timer>("Timer");
    }

    public override void _Process(float delta)
    {
        if (_timer.IsStopped())
        {
            _cooldown.Hide();
            _key.Show();
        }
        else
        {
            _cooldown.Show();
            _key.Hide();
        }

        _cooldown.Text = _timer.TimeLeft.ToString();
    }


}
