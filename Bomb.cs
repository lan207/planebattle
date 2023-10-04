using Godot;
using System;

public partial class Bomb : Node2D
{
    private float speed=5;
    [Export] PackedScene Boom;
    private int r = GD.RandRange(0, 1)==0?-1:1;
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        var p = GlobalPosition;
        if (speed >0)
        {
            speed -= (float)delta * 2.5f;
        }
        else
        {
            speed = 0;
            var b = Boom.Instantiate() as Node2D;
            GetParent().AddChild(b);
            b.GlobalPosition = GlobalPosition;
            QueueFree();
        }
        Rotation +=r*speed* .008f;
        p.Y -= speed;
        GlobalPosition = p;
    }
}
