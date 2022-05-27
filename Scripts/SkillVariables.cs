using Godot;
using System;

public class SkillVariables : Node2D
{
    public int skillPoints;

    public int defaultSkillLvl;
    public int fireSkillLvl;
    public int buffSkillLvl;
    public int healSkillLvl;


    public int defaultDmg;
    public int fireDmg;
    public int healHp;
    public int buffDmg;

    public override void _Ready()
    {
        skillPoints = 30;

        defaultSkillLvl = 1;
        fireSkillLvl = 1;
        healSkillLvl = 1;
        buffSkillLvl = 1;

        defaultDmg = 30;
        fireDmg = 60;
        healHp = 50;
        buffDmg = 20;
    }


}
