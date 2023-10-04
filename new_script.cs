using Godot;
using System;

public partial class new_script : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] public PackedScene BulletPrefab,BombPrefab;
	private Timer t;
	[Export]public int has = 1,life = 3;
	private float timer2,speed=8;
	public override void _Ready()
	{
		t = GetChild(0) as Timer;
		t.Timeout += Shoot;
        var a = GetChild(1) as Area2D;
        a.AreaEntered += (otherA) => { if (otherA.Name == "EnemyArea" || otherA.Name == "EB") life--; };
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var inputAxis =Input.GetVector("my_left", "my_right",
			"my_up", "my_down").Normalized();
		GlobalPosition += inputAxis * speed;
		GlobalPosition = new Vector2(Mathf.Clamp(GlobalPosition.X, 0, GetWindow().Size.X)
			, Mathf.Clamp(GlobalPosition.Y, 0, GetWindow().Size.Y));
		if (timer2 > 0) timer2 -= (float)delta;
		if(Input.IsKeyPressed(Key.Enter))
		{
			ChangeShoot();
		}
		if(Input.IsKeyPressed(Key.Space))
		{
			if(timer2<=0)
			{
                ShootB();
				timer2 = 2;
            }
			
		}
	}
	public void ChangeShoot()
	{
        if (has > 0)
        {
            t.Timeout -= Shoot;
        }
        else
        {
            t.Timeout += Shoot;
        }
        has *= -1;
    }
	private void Shoot()
	{
		var b = BulletPrefab.Instantiate () as Node2D;
		Set(b);
	}
    private void ShootB()
    {
        var b = BombPrefab.Instantiate() as Node2D;
		Set(b);
    }
	private void Set(Node2D node)
	{
        GetParent().AddChild(node);
        node.GlobalPosition = GlobalPosition;
    }
}
