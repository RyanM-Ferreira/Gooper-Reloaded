using System;
using Godot;

public partial class Enemy : CharacterBody2D
{
    public bool isAlive = true;
    public float maxHealth = 50.0f;
    public float health;

    public float speed = 20.0f;
    public float gravity = 256.0f;
    public int direction = -1;

    protected AnimatedSprite2D animatedSprite;

    public override void _Ready()
    {
        health = maxHealth;
        animatedSprite = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public void EnemyDied()
    {
        if (!isAlive)
        {
            QueueFree();
        }
    }
}