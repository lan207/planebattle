using Godot;
using System;

public partial class Boom : Node2D
{
    // Called when the node enters the scene tree for the first time.
    private Timer DelayTimer;
    public override void _Ready()
	{
        DelayTimer = GetChild(0) as Timer;
        DelayTimer.Timeout += () => QueueFree();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
