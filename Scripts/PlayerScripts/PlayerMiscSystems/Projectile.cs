using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class Projectile : CharacterBody2D
{
    private float _speed = 20000f;
    private float _lifetime = 5.0f;
    private float _timeAlive = 0.0f;
    
    KinematicCollision2D _collisionObject;

    AnimatedSprite2D _animatedSprite2D;

    public override void _Ready()
    {
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = new Vector2(_speed * (float)delta, 0);
        _timeAlive += (float)delta;

        if (_timeAlive >= _lifetime)
        {
            QueueFree();
        }

        MoveAndSlide();

        _collisionObject = GetLastSlideCollision();
        
        if ( _collisionObject!= null)
        {
            QueueFree();
        }
    }
      public void HitboxEntered(Area2D area)
    {
        if (area.Owner.IsInGroup("Player")) return;
        QueueFree();
    }

    public void SetDirection(int direction)
    {
        _speed = Math.Abs(_speed) * direction;
        _animatedSprite2D.FlipH = direction > 0;
    }
}