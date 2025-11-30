using Godot;
using System;

public partial class PlayerCore : CharacterBody2D
{
    private float _gravity;
    private float _defaultGravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");

    public float verticalStickyModifier = 15.0f;

    public float speed { get; set; } = 250.0f;
    public float jumpForce { get; set; } = -400.0f;

    private bool _wasOnFloor = false;
    private bool _justLanded = false;

    private int _direction = 0;
    private int _lastDirection = 1;

    // ! Mó Preguiçakkkkkkkkk
    // private bool _extendJump = false;
    // private float _decelerateOnJumpRelease = 0.5f;  // * Up to 1.0f

    private float _acceleration = 0.1f; // * Up to 1.0f
    private float _deceleration = 0.075f; // * Up to 1.0f

    private void ProcessMovement(float delta)
    {
        _canJump = IsOnFloor() || IsOnWall();

        _gravity = IsOnWall() ? _defaultGravity / verticalStickyModifier : _defaultGravity;

        if (!IsOnFloor())
        {
            Velocity = new Vector2(Velocity.X, Velocity.Y + _gravity * delta);
        }

        if (_direction != 0)
        {
            Velocity = new Vector2(Mathf.MoveToward(Velocity.X, _direction * speed, speed * _acceleration), Velocity.Y);
            _lastDirection = _direction;
        }
        else
        {
            Velocity = new Vector2(Mathf.MoveToward(Velocity.X, 0, speed * _deceleration), Velocity.Y);
        }

        if (_isJumping)
        {
            Velocity = new Vector2(Velocity.X, jumpForce);
            _isJumping = false;
        }

        CorrectTranlation();

        MoveAndSlide();
    }

    private void CorrectTranlation()
    {
        if (_direction != 0 && _direction != Scale.Y)
        {
            Scale = new Vector2(Scale.X * -1, Scale.Y);
        }
    }
}