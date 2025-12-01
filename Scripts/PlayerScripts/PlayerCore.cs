using Godot;
using System;

public partial class PlayerCore : CharacterBody2D
{
	private AnimatedSprite2D _animatedSprite;
	private AnimationPlayer _animationPlayer;

	private RayCast2D _leftWall;
	private RayCast2D _rightWall;

	public override void _Ready()
	{
		health = maxHealth;

		_animatedSprite = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
		_animationPlayer = GetNodeOrNull<AnimationPlayer>("AnimationPlayer");

		_leftWall = GetNodeOrNull<RayCast2D>("LeftColliding");
		_rightWall = GetNodeOrNull<RayCast2D>("RightColliding");
	}

	public override void _Process(double delta)
	{
		Animations();
	}

	public override void _PhysicsProcess(double delta)
	{
		ProcessMovement((float)delta);
		ProcessAttackCooldown((float)delta);
	}
}
