using Godot;
using System;

public class CanvasLayer : Godot.CanvasLayer
{

    public Player player;


    public Label hp_label;
    public string hp;

    public Label mp_label;
    public string mp;


    public TextureRect hpIndicator;
    public TextureRect mpIndicator;

    public void updateIndicators()
    {
        hp = Convert.ToString(player.currentHP) + "/" + Convert.ToString(player.maxHP);
        hp_label.Text = hp;

        mp = Convert.ToString(player.currentMP) + "/" + Convert.ToString(player.maxMP);
        mp_label.Text = mp;

       
        var hpVec = new Vector2(0, 15);
        hpVec.x = player.currentHP / player.maxHP * 100 * (float)1.92;
        hpIndicator.RectSize = hpVec;

        var mpVec = new Vector2(0, 15);
        mpVec.x = player.currentMP / player.maxMP * 100 * (float)1.92;
        mpIndicator.RectSize = mpVec;
    }
 
    public override void _Ready()
    {
        hp_label = GetNode<Label>("Hud/Indicators/HpBar/HpIndicator/Hp");
        mp_label = GetNode<Label>("Hud/Indicators/MpBar/MpIndicator/Mp");
        hpIndicator = GetNode<TextureRect>("Hud/Indicators/HpBar/HpIndicator");
        mpIndicator = GetNode<TextureRect>("Hud/Indicators/MpBar/MpIndicator");
        player = GetParent().GetNode<Player>("Player");
    }



    public override void _Process(float delta)
    {
        updateIndicators();
    }


}
