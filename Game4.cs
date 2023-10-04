using Godot;
using System;

public partial class Game4 : Node2D
{
	[Export]public Label text, textL,timeT,res;
	[Export]Button btn1,btn2;
	[Export] AnimationPlayer p;
	[Export] new_script player;
	int x;
	bool b1,HasClear;
    float timer;
    [Export] PackedScene Boss;
	public override void _Ready()
	{
        player.life = 20;
        btn1.Pressed += () => GetTree().ChangeSceneToFile("res://control.tscn");
        btn2.Disabled = true;
        btn2.Pressed += () =>
        {
            p.Play("new_animation_2");
        };
        p.AnimationFinished += (o) =>
        {
            if (x == 2 && !HasClear)
            {
                HasClear = true;
                player.ChangeShoot();
                btn2.Disabled = true;
                player.life = 20;
                var b = Boss.Instantiate<Boss>();
                b.GlobalPosition = Vector2.Zero;
                AddChild(b);

            }

        };
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        
        textL.Text = "ÉúÃü£º" + player.life;
        if (p.CurrentAnimation == "new_animation_2") x = 2;
        else if (p.CurrentAnimation == "new_animation") x = 1;
        if(timer<=300)
        {
            timer += (float)delta;
            text.Text = timer.ToString();
        }
        else
        {
            if(!b1)
            {
                Fail();
            }
        }
        if(player.life<=0)
        {
            Fail();
        }
    }
    private void Fail()
    {
        End();
        if (GetNode<Boss>("Boss") != null) GetNode<Boss>("Boss").QueueFree();
        res.Text = "Ê§°Ü";
    }
    public void End()
    {
        if(!b1)
        {
            timer = 0;
            b1 = true;
            timeT.Text = "ÄãµÄ¼ÍÂ¼£º" + timer;
            p.Play("new_animation");
            player.ChangeShoot();
        }
        

    }
}
