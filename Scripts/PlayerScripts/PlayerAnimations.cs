using Godot;
using System;

public partial class PlayerCore : CharacterBody2D
{
    private bool _isPlayingLanding = false;

    public void OnAnimationFinished(StringName animationName)
    {
        switch (animationName)
        {
            case "WallSlideDownTransition":
                PlayAnimation("WallSlideDown");
                break;
            case "WallSlideUpTransition":
                PlayAnimation("WallSlideUp");
                break;
            case "Landing":
                _isPlayingLanding = false;
                break;
        }
    }

    private void Animations()
    {
        if (_isPlayingLanding)
        {
            return;
        }

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

        if (_justLanded)
        {
            _isPlayingLanding = true;
            PlayAnimation("Landing");
            return;
        }

        if (!IsOnFloor() && IsOnWall())
        {
            bool inTransition = _animationPlayer.CurrentAnimation == "WallSlideDownTransition" || _animationPlayer.CurrentAnimation == "WallSlideUpTransition";

            if (!inTransition)
            {
                if (Velocity.Y > 0 && _animationPlayer.CurrentAnimation != "WallSlideDown")
                {
                    PlayAnimation("WallSlideDownTransition");
                }

                else if (Velocity.Y < 0 && _animationPlayer.CurrentAnimation != "WallSlideUp")
                {
                    PlayAnimation("WallSlideUpTransition");
                }
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

    private void PlayAnimation(string animation)
    {
        if (_animationPlayer.CurrentAnimation != animation)
        {
            _animationPlayer.Play(animation);
        }

    }
}