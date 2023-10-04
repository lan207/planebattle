using Godot;
using System;

public partial class Zuizong_bullet :BULLET
{
	[Export] int N = 20;
    new_script p;
    Vector2 velocity = Vector2.Down;
    Node2D node;
    protected override void ready(Area2D a)
    {
        a.AreaEntered += (otherA) => { if (otherA.Name == "PlayerArea") QueueFree(); };
        p = GetParent().GetNode<new_script>("P");
        node = GetChild(0) as Node2D;
    }
    public override void _Process(double delta)
    { 
        velocity = speed*(((velocity * N) + (p.GlobalPosition - GlobalPosition).Normalized()*.75f*N)/(N+1));
        GlobalPosition += velocity;
        node.Rotation += .05f;
    }
}
