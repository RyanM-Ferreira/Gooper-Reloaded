using System;
using Godot;

public partial class Enemy : CharacterBody2D
{
    public bool isAlive = true;
    public float maxHealth { get; set; }
    public float health;

    HitStop hitStop = new HitStop();

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
        QueueFree();
    }

    public void TakeDamage(int damage, double hitstopTimeScale, double hitstopDuration)
    {
        health -= damage;

        GD.Print($"Damage Takan: {damage}. Life remaining: {health}");

        if (health <= 0)
        {
            EnemyDied();
        }
        else
        {
            DamageFeedback();
            hitStop.Hitstop(hitstopTimeScale, hitstopDuration, this);
        }
    }

    public void DamageFeedback()
    {
        animatedSprite.Modulate = new Color(0.8f, 0, 0, 1);
        GetTree().CreateTimer(0.10).Timeout += () => animatedSprite.Modulate = new Color(1, 1, 1, 1);
    }
}