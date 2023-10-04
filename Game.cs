using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public partial class Game : Node
{
	[Export] public PackedScene EnemyPrefab,E2,Boss;
	[Export] public Timer SpawnTimer;
	[Export] public TextEdit text;
	[Export] public Label text2,text3,t4,t5,t6,t7;
	[Export] public Button button,btn2 ;
	public int Num,Combo,MaxCombo, TimerMax = 180;
	public Game instance;
	[Export]public float moveSpeed=.25f;
	protected float timer_true=180;
	protected bool HasStop,HasClear,HasBossSpawn;
	[Export]protected AnimationPlayer player;
	public List<Node2D> Enemies = new();
	[Export]protected new_script P;
	protected int x,y;
    protected PathFollow2D p;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        Intitialize();
        var b = Boss.Instantiate<Boss>();
        AddChild(b);
        b.GlobalPosition = Vector2.Zero;
    }
    protected virtual void Intitialize()
    {
        p = GetNode<PathFollow2D>("Path2D/PathFollow2D");
        timer_true = TimerMax;
        P.life = 100;
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
                P.life = 100;
                var b =Boss.Instantiate<Boss>();
                b.GlobalPosition = Vector2.Zero;
                AddChild(b);
                Enemies.Add(b);
            }

        };
    }
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        //GD.Print(delta);
		if (player.CurrentAnimation == "new_animation_2") x = 2;
		else if (player.CurrentAnimation == "new_animation") x = 1;
        text.Text = "分数：" + Num;
		text2.Text = "连击:" + Combo;
		if(timer_true>0)timer_true -= (float)delta;
		else
		{
            Prepare_End();
        }
		text3.Text = TimeSpan.FromSeconds(timer_true).ToString(format:@"mm\:ss\:ff");
        t7.Text = "生命值：" + P.life;
        if (P.life <= 0) Prepare_End();
	}
	public virtual void Prepare_End()
	{
        timer_true = 0;
        if (!HasStop)
        {

            SpawnTimer.Timeout -= SpawnEnemy;
            HasStop = true;
            player.Play("new_animation");
            t4.Text = "最终分数:" + Num;
            t5.Text = "最大连击数:" + MaxCombo;
            if (Num <= 0) t6.Text = "???";
            else if (Num > 0 && Num <= 2000) t6.Text = "菜";
            else if (Num > 2000 && Num <= 5000) t6.Text = "行";
            else if (Num > 5000 && Num <= 10000) t6.Text = "彳亍";
            else t6.Text = "牛";
            HasClear = false;
            P.ChangeShoot();
            btn2.Disabled = false;
        }
    }
	protected void SpawnEnemy()
	{
		y++;
		y %= 3;
        if (p.ProgressRatio < 1) p.ProgressRatio += (float).016667 * moveSpeed;
        else p.ProgressRatio = 0;
        var pos = new Vector2(p.GlobalPosition.X, -50);
        var e = EnemyPrefab.Instantiate() as Node2D;
        AddChild(e);
        e.GlobalPosition = pos;
		Enemies.Add(e);
		if(y==0)
		{
            p.ProgressRatio = GD.Randf();
            var pos2 = new Vector2(GD.RandRange(0, GetWindow().Size.X), -50);
            var e2 = E2.Instantiate<Node2D>();
            AddChild(e2);
            e2.GlobalPosition = pos2;
            Enemies.Add(e2);
        }
        
    }
	public void Goal()
	{
        Combo++;
		if (MaxCombo < Combo) MaxCombo = Combo;
		if(Combo>=3)
		{
            Num += 5 + Combo;
        }
        else
		{
			Num += 5;
		}
		
	}
}
