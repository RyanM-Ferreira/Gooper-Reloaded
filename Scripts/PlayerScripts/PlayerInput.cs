using Godot;
using System;

public partial class PlayerCore : CharacterBody2D
{
    private bool _isJumping = false;
    private bool _canJump = true;

    private bool _isDashing = false;

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionPressed("A") && Input.IsActionPressed("D"))
        {
            _direction = 0;
        }
        else if (Input.IsActionPressed("A"))
        {
            _direction = -1;
        }
        else if (Input.IsActionPressed("D"))
        {
            _direction = 1;
        }
        else
        {
            _direction = 0;
        }

        if (Input.IsActionJustPressed("SHIFT"))
        {
            if (dashTimer <= 0)
            {
                InitializeDash();
            }
        }

        if (Input.IsActionJustPressed("SPACE"))
        {
            if (_canJump)
            {
                _isJumping = true;
            }
        }

        if (Input.IsActionJustPressed("M1"))
        {
            if (_canAttack)
            {
                Attack();
            }
        }
    }
}