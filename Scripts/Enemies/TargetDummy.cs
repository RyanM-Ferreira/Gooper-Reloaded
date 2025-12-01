using Godot;

public partial class TargetDummy : Enemy
{
    public override void _Ready()
    {
        maxHealth = 3;
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        Velocity = new Vector2(0, 100 * (float)delta);

        MoveAndSlide();
    }
}