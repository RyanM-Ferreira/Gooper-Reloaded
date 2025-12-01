using Godot;
using System;

public partial class Hitbox : Area2D
{
    [Export] private int damage = 1;
    [Export] float Knockback = 100;
    [Export] double hitstopTimeScale;
    [Export] double hitstopDuration;
}