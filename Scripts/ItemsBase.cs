using Godot;
using System;

public class ItemsBase : Node2D
{

    public System.Collections.Generic.List<Item> items = new System.Collections.Generic.List<Item>(50);


    public Item GetItem(int id)
    {
        return items[id];
    }

    public override void _Ready()
    {
        items.Add(new Item() { id = 0, name = "soul_ring", price = 2000, dmg = 10, armor = 10, range = 0 });
        items.Add(new Item() { id = 1, name = "necklace", price = 2000, dmg = 30, armor = 10, range = 0 });
        
    }


}
