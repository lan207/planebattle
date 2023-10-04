using Godot;
using System;

public partial class Game3 : Game
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Intitialize();
	}
    protected override void Intitialize()
    {
        p = GetNode<PathFollow2D>("Path2D/PathFollow2D");
        timer_true = TimerMax;
        P.life = 3;
        SpawnTimer.Timeout += SpawnEnemy;
        button.Pressed += () => GetTree().ChangeSceneToFile("res://control.tscn");
        btn2.Disabled = true;
        btn2.Pressed += () =>
        {
            player.Play("new_animation_2");
        };
        player.AnimationFinished += (o) =>
        {
            if (x == 2 && !HasClear)
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Enemies[i].QueueFree();
                }
                Enemies.Clear();
                timer_true = TimerMax;
                MaxCombo = Combo = Num = 0;
                SpawnTimer.Timeout += SpawnEnemy;
                HasStop = false;
                HasClear = true;
                P.ChangeShoot();
                btn2.Disabled = true;
                P.life = 3;
            }
        };
    }
}
