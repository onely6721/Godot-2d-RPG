using Godot;
using System;

public class Save : Node2D
{

   

	public void SaveGame()
	{
		var saveGame = new File();
		saveGame.Open("res://Saves/save.dat", File.ModeFlags.Write);

		var saveNodes = GetTree().GetNodesInGroup("Persist");
		
		foreach (Node saveNode in saveNodes)
		{

			if(!saveNode.HasMethod("Save"))
			{
				GD.Print("no save");
			}

			var nodeData = saveNode.Call("Save");
			saveGame.StoreLine(JSON.Print(nodeData));
			GD.Print(JSON.Print(nodeData));
		}

		
		saveGame.Close();
	}

	public void LoadGame()
	{
	 
	}


	public override void _Ready()
	{
		LoadGame();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_save"))
		{
			SaveGame();
		}
	}


   

}
