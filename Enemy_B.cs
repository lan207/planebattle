using Godot;
using System;

public partial class Enemy_B : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public Vector2 speedDir = new Vector2(0,1);
	[Export]protected float speed=10;
	public override void _Ready()
	{
		var t = GetChild(1) as Area2D;
		t.AreaEntered += (o) => { if (o.Name == "PlayerArea") QueueFree(); };
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GlobalPosition += speed * speedDir;
		if (GlobalPosition.X < 0 || GlobalPosition.X > GetWindow().Size.X || GlobalPosition.Y < 0 ||
			GlobalPosition.Y > GetWindow().Size.Y) QueueFree();
		
	}
}
