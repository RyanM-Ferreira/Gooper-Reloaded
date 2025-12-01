using Godot;
using System;

public partial class PlayerCore : CharacterBody2D
{
    HitStop hitStop = new HitStop();
    public float health;
    [Export] float maxHealth = 5;

    public void PlayerHurt(int damage, double hitstopTimeScale, double hitstopDuration)
    {
        health -= damage;

        GD.Print($"Damage Taken: {damage}. Life remaining: {health}");

        if (health <= 0)
        {
            GetTree().ReloadCurrentScene();
            return;
        }
        else
        {
            DamageFeedback();
            hitStop.Hitstop(hitstopTimeScale, hitstopDuration, this);
        }
    }
}
