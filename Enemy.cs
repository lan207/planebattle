using Godot;
using System;
using System.Net.NetworkInformation;

public partial class Enemy : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] protected Area2D HitArea;
	protected Game game;
	protected Game4 g2;
	[Export]protected int Health = 1,speed=2;
	protected int hp;
	
    public override void _Ready()
	{
		hp = Health;
		HitArea.AreaEntered += (otherArea) =>
		{
			if(otherArea.Name == "BulletArea")
			{
				hp--;
			}
			else if(otherArea.Name == "BombArea")
			{
				hp -= 3;
			}
			
        };
		if(GetParent()as Game!=null) game = GetParent() as Game;
		else g2 = GetParent() as Game4;	
    }
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Remove_Rule(delta);
		
    }
	protected virtual void Remove_Rule(double delta)
	{
        var p = GlobalPosition;
        p.Y += speed;
        GlobalPosition = p;
        if (GlobalPosition.Y >= GetWindow().Size.Y)
        {
            game.Combo = 0;
            game.Num -= 10;
            Safe_Remove();
        }
        if (hp <= 0)
        {
            Safe_Remove();
            game.Goal();
        }
    }
	protected  void Safe_Remove()
	{
        if(game!=null)game.Enemies.Remove(this);
        QueueFree();
    }
}
