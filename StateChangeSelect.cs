using Godot;
using System;

public partial class StateChangeSelect : Control
{
    // Called when the node enters the scene tree for the first time.\
    [Export] PackedScene game;
	public override void _Ready()
	{
		var b1 = GetChild(0) as Button;
		var b2 = GetChild(1)as Button;
		var b3 = GetChild(2)as Button;
        var g = GetTree().Root.GetNode<Game>("node_2d") ;
        if (g == null) GD.Print("null");
        b1.Pressed += () => {
            GetTree().ChangeSceneToFile("res://world_3.tscn");
            
        };
        b2.Pressed += () => {
            GetTree().ChangeSceneToFile("res://world_2.tscn");
            
        };
        b3.Pressed += () => {
            GetTree().ChangeSceneToFile("res://node_2d.scn");
            
        };
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
