using Godot;
using System;

public class SkillTree : Node2D
{


   

    public Label _points;
    public Label _shotPoints;
    public Label _firePoints;
    public Label _buffPoints;
    public Label _healPoints;

    public SkillVariables skillVariables;
    public void UpdateText()
    {
        _points.Text = skillVariables.skillPoints.ToString();
        _shotPoints.Text = skillVariables.defaultSkillLvl.ToString() + "/5";
        _firePoints.Text = skillVariables.fireSkillLvl.ToString() + "/5";
        _healPoints.Text = skillVariables.healSkillLvl.ToString() + "/5";
        _buffPoints.Text = skillVariables.buffSkillLvl.ToString() + "/5";

    }
    public override void _Ready()
    {
        skillVariables = (SkillVariables)GetNode("/root/SkillVariables");
        _points = GetNode<Label>("Points");
        _shotPoints = GetNode<Label>("shotPoints");
        _firePoints = GetNode<Label>("firePoints");
        _healPoints = GetNode<Label>("healPoints");
        _buffPoints = GetNode<Label>("buffPoints");

       

        _points.Text = skillVariables.skillPoints.ToString();
        _shotPoints.Text = skillVariables.defaultSkillLvl.ToString() + "/5";
        _firePoints.Text = skillVariables.fireSkillLvl.ToString() + "/5";
        _healPoints.Text = skillVariables.healSkillLvl.ToString() + "/5";
        _buffPoints.Text = skillVariables.buffSkillLvl.ToString() + "/5";


    }


    public void OnShotPressed()
    {
        if (skillVariables.skillPoints > 0 && skillVariables.defaultSkillLvl < 5)
        {
            skillVariables.skillPoints -= 1;
            skillVariables.defaultSkillLvl += 1;
        }
    }
    public void OnFirePressed()
    {
        if (skillVariables.skillPoints > 0 && skillVariables.fireSkillLvl < 5)
        {
            skillVariables.skillPoints -= 1;
            skillVariables.fireSkillLvl += 1;
        }
    }
    public void OnBuffPressed()
    {
        if (skillVariables.skillPoints > 0 && skillVariables.buffSkillLvl < 5)
        {
            skillVariables.skillPoints -= 1;
            skillVariables.buffSkillLvl += 1;
        }
    }
    public void OnHealPressed()
    {
        if (skillVariables.skillPoints > 0 && skillVariables.healSkillLvl < 5)
        {
            skillVariables.skillPoints -= 1;
            skillVariables.healSkillLvl += 1;
        }
    }


    public override void _Process(float delta)
    {
        if (skillVariables.defaultSkillLvl == 5)
        {
            GetNode<Button>("shot").Hide();
        }

        if (skillVariables.fireSkillLvl == 5)
        {
            GetNode<Button>("fire").Hide();
        }

        if (skillVariables.buffSkillLvl == 5)
        {
            GetNode<Button>("buff").Hide();
        }

        if (skillVariables.healSkillLvl == 5)
        {
            GetNode<Button>("heal").Hide();
        }

        UpdateText();
    }
}
