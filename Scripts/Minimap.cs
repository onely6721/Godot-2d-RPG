using Godot;
using System;

public class Minimap : Node2D
{

    public Player player;
    public Camera2D camera;
    public Sprite head;

    public override void _Ready()
    {
        player = GetParent().GetParent().GetNode<Player>("Player");
        camera = GetNode<Camera2D>("ViewportContainer/Viewport/Camera2D");
        head = GetNode<Sprite>("ViewportContainer/Viewport/Sprite");
    }

    public override void _Process(float delta)
    {
        camera.Position = player.Position;
        head.Position = player.Position;
    }


}
