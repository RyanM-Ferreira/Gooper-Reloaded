using Godot;
using System;

public partial class PlayerCore : CharacterBody2D
{
    private void Animations()
    {
        _animatedSprite.FlipV = false;

        if (!_canAttack && !IsOnWall())
        {
            PlayAnim("Attack");

            return;
        }

        if (!IsOnFloor() && IsOnWall())
        {
            PlayAnim("WallSlide");

            _animatedSprite.FlipV = Velocity.Y > 0;

            return;
        }

        if (IsOnFloor())
        {
            if (Velocity.X != 0)
            {
                PlayAnim("Run");
            }
            else if (Velocity.X == 0)
            {
                PlayAnim("Idle");
            }

            return;
        }

        if (Velocity.Y < 0)
        {
            PlayAnim("Jump");
        }
        else
        {
            PlayAnim("Fall");
        }
    }

    private void PlayAnim(string anim)
    {
        if (_animatedSprite.Animation != anim)
        {
            _animatedSprite.Play(anim);
        }
    }
}