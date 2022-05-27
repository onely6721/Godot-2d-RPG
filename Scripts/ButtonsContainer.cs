using Godot;
using System;

public class ButtonsContainer : VBoxContainer
{
  
    public override void _Ready()
    {
        
    }

    
    public void _OnStartPressed()
    {
        GetTree().ChangeScene("res://Scenes/Game.tscn");
    }

}
