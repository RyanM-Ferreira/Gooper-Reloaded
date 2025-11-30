using Godot;
using System;

public partial class Projectile : CharacterBody2D
{
    private float _speed = 400f;
    private float _lifetime = 5.0f;
    private float _timeAlive = 0.0f;

    AnimatedSprite2D _animatedSprite2D;

    public override void _Ready()
    {
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += new Vector2(_speed * (float)delta, 0);
        _timeAlive += (float)delta;

        if (_timeAlive >= _lifetime)
        {
            QueueFree();
        }
    }

    public void SetDirection(int direction)
    {
        _speed = Math.Abs(_speed) * direction;
        _animatedSprite2D.FlipH = direction > 0;
    }
}