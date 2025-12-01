using Godot;
using System;

public partial class PlayerCore : CharacterBody2D
{
    public void DamageFeedback()
    {
        _animatedSprite.Modulate = new Color(0.8f, 0, 0, 1);
        GetTree().CreateTimer(0.10).Timeout += () => _animatedSprite.Modulate = new Color(1, 1, 1, 1);
    }
}