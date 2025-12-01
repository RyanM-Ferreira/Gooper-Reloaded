using Godot;

public partial class TargetDummy : CharacterBody2D
{
    [Export] int _health = 5;
    HitStop hitStop = new HitStop();

    private AnimatedSprite2D sprite;

    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
    public override void _PhysicsProcess(double delta)
    {
        Velocity = new Vector2(0, 100 * (float)delta);
        MoveAndSlide();
    }
    public void TakeDamage(int damage, double hitstopTimeScale, double hitstopDuration)
    {
        _health -= damage;
        GD.Print("Target Dummy Health: " + _health);

        if (_health <= 0)
        {
            QueueFree();
        }
        else
        {
            sprite.Modulate = new Color(0.85f, 0, 0, 1);
            GetTree().CreateTimer(0.10).Timeout += () => sprite.Modulate = new Color(1, 1, 1, 1);
            hitStop.Hitstop(hitstopTimeScale, hitstopDuration, this);
        }
    }
}
