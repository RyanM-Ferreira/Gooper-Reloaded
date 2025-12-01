using Godot;
using System;

public partial class PlayerCore : CharacterBody2D
{
    private void Animations()
    {
        if (!_canAttack && !IsOnWall())
        {
            PlayAnimation("Attack");
            return;
        }

        if (_isDashing)
        {
            PlayAnimation("Dash");

            return;
        }

        if (!IsOnFloor() && IsOnWall())
        {
            if (Velocity.Y > 0)
            {
                PlayAnimation("WallSlideDown");
            }
            else if (Velocity.Y < 0)
            {
                PlayAnimation("WallSlideUp");
            }

            return;
        }

        if (IsOnFloor())
        {
            if (Velocity.X != 0 && !_isDashing)
            {
                PlayAnimation("Run");
            }
            else if (Velocity.X == 0)
            {
                PlayAnimation("Idle");
            }

            return;
        }

        if (Velocity.Y < 0)
        {
            PlayAnimation("Jump");
        }
        else
        {
            PlayAnimation("Fall");
        }
    }

    private void PlayAnimation(string anim)
    {
        if (_animatedSprite.Animation != anim)
        {
            _animatedSprite.Play(anim);
        }
    }
}