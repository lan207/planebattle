using Godot;
using System;

public partial class ButtonManager : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var b1 = GetChild(0) as Button;
		var b2 = GetChild(1) as Button;
		b1.Pressed +=()=> GetTree().ChangeSceneToFile("res://control_2.tscn");
		b2.Pressed +=()=> GetTree().Quit();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
