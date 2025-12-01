using Godot;
using System;

public partial class Brushte : Enemy
{
    public new float speed = 50.0f;
    public new float gravity = 256.0f;
    public new int direction = -1;

    private RayCast2D right;
    private RayCast2D left;
    private RayCast2D rightBottom;
    private RayCast2D leftBottom;

    public override void _Ready()
    {
        maxHealth = 5;

        right = GetNodeOrNull<RayCast2D>("Right");
        left = GetNodeOrNull<RayCast2D>("Left");
        rightBottom = GetNodeOrNull<RayCast2D>("RightBottom");
        leftBottom = GetNodeOrNull<RayCast2D>("LeftBottom");

        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveBrushte((float)delta);
        base._PhysicsProcess(delta);
    }

    public void MoveBrushte(float delta)
    {
        if (!IsOnFloor())
        {
            Velocity = new Vector2(Velocity.X, Velocity.Y + gravity * delta);
        }

        if (right.IsColliding() || !rightBottom.IsColliding())
        {
            direction = -1;
        }

        if (left.IsColliding() || !leftBottom.IsColliding())
        {
            direction = 1;
        }

        if (direction != 0)
        {
            Velocity = new Vector2(speed * direction, Velocity.Y);
        }

        animatedSprite.FlipH = direction < 0 ? true : false;

        MoveAndSlide();
    }
}