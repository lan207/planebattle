using Godot;
using System;

public partial class Boss : Enemy
{
    public float halfX, halfY;
    private enum State
    {
        Shoot,
        Dash,
        Spawn,
        Shoot2,
        None
    }
    private float V22Rot(Vector2 vector2)
    {
        return (float)(Mathf.Atan2(vector2.Y, vector2.X));
    }
    [Export] PackedScene e1, eb, eb2;
    new_script player;
    State state = State.Shoot;
    [Export] const float bulletWait = .2f, stateWait = 10, movespeed = .25f;
    int ShootIndex, I2playerdirx = 1;
    bool shootB, b, c, d;
    float timer1, t2, t3, t = 1, ShootTimer = 2.5f, DashTimer = 10;
    Vector2 des;
    PathFollow2D p1, p2;
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Ready()
    {
        base._Ready();
        player = GetParent().GetNode<new_script>("P");
        halfX = 64 * GetTransform().Scale.X;
        halfY = 64 * GetTransform().Scale.Y;
        p1 = GetParent().GetNode("Path2D2").GetChild(0) as PathFollow2D;
        p2 = GetParent().GetNode("Path2D3").GetChild(0) as PathFollow2D;
    }
    public override void _Process(double delta)
    {
        if (player.GlobalPosition.X - GlobalPosition.X > 0) I2playerdirx = 1;
        else if (player.GlobalPosition.X == GlobalPosition.X) I2playerdirx = 0;
        else I2playerdirx = -1;
        GlobalPosition = new Vector2(Mathf.Clamp(GlobalPosition.X, 0, GetWindow().Size.X)
            , Mathf.Clamp(GlobalPosition.Y, 0, GetWindow().Size.Y));
        base._Process(delta);
        switch (state)
        {
            case State.Shoot:
                GlobalPosition = new Vector2(GlobalPosition.X + I2playerdirx * speed, halfY);
                Rotation = 0;
                if (ShootIndex == 0)
                {
                    Shoot1(delta);
                    SetBullet(delta);
                }
                else if (ShootIndex == 1)
                {
                    Shoot1_2(delta);
                    SetBullet(delta);
                }
                else if (ShootIndex == 2)
                {
                    Shoot1(delta);
                    SetBullet(delta);
                }
                else
                {
                    Shoot1_2(delta);
                    SetBullet(delta, true);
                }
                break;

            case State.Dash:
                Dash(delta);
                break;
            case State.Spawn:
                SpawnMob(delta);
                break;
            case State.Shoot2:
                Rotation = 0;
                Shoot2(delta);
                break;
            case State.None:
                break;
        }
    }
    private void Shoot2(double delta)
    {
        if (p2.ProgressRatio < 1) p2.ProgressRatio += (float)delta * movespeed;
        else p2.ProgressRatio = 0;
        GlobalPosition = p2.GlobalPosition;
        if (DashTimer > 0) DashTimer -= (float)delta;
        else
        {
            state = 0;
            DashTimer = 10;
            b = c = d = false;
        }
        if (!b)
        {
            ShootBullet(true).GlobalPosition = GlobalPosition + new Vector2(0, halfY);
            b = true;
        }
        if (!c)
        {
            if (t > 0) t -= (float)delta;
            else
            {
                ShootBullet(true).GlobalPosition = GlobalPosition + new Vector2(0, halfY);
                if (!d)
                {
                    d = true;
                }
                else c = true;
                t = 1f;
            }

        }



    }
    private void SpawnMob(double delta)
    {
        if (p1.ProgressRatio < 1) p1.ProgressRatio += (float)delta * movespeed;
        else p1.ProgressRatio = 0;
        GlobalPosition = new Vector2(p1.GlobalPosition.X, halfY);
        if (DashTimer > 0) DashTimer -= (float)delta;
        else
        {
            state++;
            DashTimer = stateWait;
        }
        Rotation += .08f;
        if (t2 <= 0)
        {
            var e = e1.Instantiate<Enemy>();
            e.GlobalPosition = GlobalPosition;
            GetParent().AddChild(e);
            game.Enemies.Add(e);
        }



    }
    private void Dash(double delta)
    {
        if (DashTimer > 0) DashTimer -= (float)delta;
        else
        {
            state++;
            DashTimer = stateWait;
        }
        if (t2 >= 1)
        {
            des = player.GlobalPosition;
            t2 = 0;
            Rotation = V22Rot((des - GlobalPosition).Normalized());
        }
        else
        {
            t2 += (float)delta;
            GlobalPosition += t2 * (des - GlobalPosition);

        }
    }
    private void Shoot1(double delta)
    {
        if (timer1 > 0) timer1 -= (float)delta;
        else
        {
            timer1 = bulletWait;
            var b = ShootBullet();
            b.GlobalPosition = GlobalPosition + new Vector2(0, halfY);
        }

    }
    private void SetBullet(double delta, bool a = false)
    {
        if (ShootTimer > 0) ShootTimer -= (float)delta;
        else
        {
            if (a)
            {
                ShootIndex = 0;
                state++;
            }
            else ShootIndex++;
            ShootTimer = stateWait / 4;
        }

    }
    private void Shoot1_2(double delta)
    {
        if (timer1 > 0) timer1 -= (float)delta;
        else
        {
            timer1 = bulletWait;
            var b = ShootBullet();
            b.GlobalPosition = GlobalPosition + new Vector2(halfX, halfY);
            b.speedDir = new Vector2(-1, 1).Normalized();
            var b2 = ShootBullet();
            b2.GlobalPosition = GlobalPosition + new Vector2(-halfX, halfY);
            b2.speedDir = new Vector2(1, 1).Normalized();
        }
    }
    private Enemy_B ShootBullet(bool a = false)
    {
        Enemy_B b = null;
        if (a) b = eb2.Instantiate<Enemy_B>();
        else b = eb.Instantiate() as Enemy_B;
        GetParent().AddChild(b);
        return b;
    }
    protected override void Remove_Rule(double delta)
    {
        if (hp <= 0)
        {
            Modulate = new Color(Modulate.R, Modulate.G, Modulate.B, Modulate.A - (float)delta);
            if (Modulate.A <= 0)
            {
                Safe_Remove();
                state = State.None;
                if (game != null)
                {
                    for (int i = 0; i < 20; i++)
                        game.Goal();
                }
                else
                {
                    if (g2.res.Text == string.Empty)
                    {
                        g2.res.Text = "³É¹¦";
                        g2.End();
                    }
                }


            }
        }
    }
}