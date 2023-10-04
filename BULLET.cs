using Godot;
using System;

public partial class BULLET : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] public Timer DelayTimer;
	protected float speed=12;
	public override void _Ready()
	{
		DelayTimer.Timeout += () => QueueFree();
		var a = GetChild(1) as Area2D;
		ready(a);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var p = GlobalPosition;
		p.Y -= speed;
		GlobalPosition = p;
	}
	protected virtual void ready(Area2D a)
	{
		a.AreaEntered += (otherA) => { if (otherA.Name == "EnemyArea") QueueFree(); };
    }
}
