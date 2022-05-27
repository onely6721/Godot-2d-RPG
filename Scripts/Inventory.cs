using Godot;
using System;
using System.Collections.Generic;
public class Inventory : Node2D
{

    public GridContainer _items;
    private PackedScene _itemScene = (PackedScene)GD.Load("res://Scenes/Item.tscn");

    public List<string> inventory = new List<string>();

    public void AddItem(int id)
    {
        var itemsBase = GetNode<ItemsBase>("/root/ItemsBase");
        var item = _itemScene.Instance<Item>();
        var tempItem = itemsBase.GetItem(id);

        item.id = tempItem.id;
        item.dmg = tempItem.dmg;
        item.armor = tempItem.armor;
        item.price = tempItem.price;
        item.range = tempItem.range;
        item.name = tempItem.name;



        var size = new Vector2(64, 64);
        item.RectMinSize = size;

        inventory.Add(item.Name);
        _items.AddChild(item);
    
    }
    
    public void RemoveItem(string name)
    {
        _items.GetNode<Item>(name).QueueFree();
        inventory.Remove(name);

    }

    public override void _Ready()
    {
        _items = GetNode<GridContainer>("Scroll/ItemsContainer");
        AddItem(1);
        AddItem(0);
    }


}
