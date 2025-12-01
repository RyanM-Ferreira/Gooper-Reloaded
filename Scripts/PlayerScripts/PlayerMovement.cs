using Godot;
using System;

public partial class PlayerCore : CharacterBody2D
{
    private float _gravity;
    private float _defaultGravity = (float)ProjectSettings.GetSetting("physics/2d/default_gravity");

    public float verticalStickyModifier = 0.05f;

    public float speed { get; set; } = 250.0f;
    public float jumpForce { get; set; } = -400.0f;

    private bool _wasOnFloor = false;
    private bool _justLanded = false;

    private int _direction = 0;
    private int _lastDirection = 1;

    [Export] private Curve _dashCurve;
    public float dashSpeed = 128.0f;
    private float _dashMaxDistance = 256.0f;
    private float _dashStartPosition = 0;
    public float dashCooldown = 1.0f;
    public float dashTimer = 0;

    // ! Mó Preguiçakkkkkkkkk
    // private bool _extendJump = false;
    // private float _decelerateOnJumpRelease = 0.5f;  // * Up to 1.0f

    private float _acceleration = 0.1f; // * Up to 1.0f
    private float _deceleration = 0.075f; // * Up to 1.0f

    private void ProcessMovement(float delta)
    {
        _gravity = IsOnWall() ? verticalStickyModifier : _defaultGravity;
        _canJump = IsOnFloor() || IsOnWall();
        _justLanded = !_wasOnFloor && IsOnFloor();
        _wasOnFloor = IsOnFloor();

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

        if (_isDashing)
        {
            ApplyDash();
        }

        CorrectTranlation();
        DashTimer(delta);

        MoveAndSlide();
    }

    private void CorrectTranlation()
    {
        if (_direction != 0 && _direction != Scale.Y)
        {
            Scale = new Vector2(Scale.X * -1, Scale.Y);
        }
    }

    private void InitializeDash()
    {
        _isDashing = true;
        _dashStartPosition = Position.X;
        dashTimer = dashCooldown;
    }

    private void ApplyDash()
    {
        float currentDistance = Math.Abs(Position.X - _dashStartPosition);

        int dashDirection = _direction != 0 ? _direction : _lastDirection;

        if (currentDistance >= _dashMaxDistance || IsOnWall())
        {
            _isDashing = false;
        }
        else
        {
            float curveFactor = _dashCurve.Sample(Math.Abs(currentDistance / _dashMaxDistance));
            Velocity = new Vector2(Velocity.X + dashDirection * dashSpeed * curveFactor, Velocity.Y);
        }
    }

    private void DashTimer(float delta)
    {
        if (dashTimer > 0)
        {
            dashTimer -= delta;
        }
    }
}