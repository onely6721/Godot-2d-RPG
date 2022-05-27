using Godot;
using System;

public class Item : TextureRect
{
    public int id;
    public float price;
    public string name;
    public float dmg;
    public float armor;
    public float range;

    public Panel _panel;
    public override void _Ready()
    {
        var path = "res://Resources/Items/" + name + ".png";
        var texture = (Texture)GD.Load(path);
        
        Texture = texture;
    }

    public override void _PhysicsProcess(float delta)
    {
       
        if(!GetParent().GetParent().GetParent<Inventory>().Visible)
        {
            GetNode<Panel>("Panel").Hide();
        }

    }

    public void OnMouseEntered()
    {
        GetNode<Panel>("Panel").Show();
    }

    public void OnMouseExited()
    {
       GetNode<Panel>("Panel").Hide();
    }
}
    