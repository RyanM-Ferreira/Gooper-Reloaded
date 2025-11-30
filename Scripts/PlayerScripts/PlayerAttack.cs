using Godot;
using System;

public partial class PlayerCore : CharacterBody2D
{
    private bool _canAttack = false;
    private float _attackCooldown;
    [Export] private float _defaultAttackCooldown = 0.5f;

    private PackedScene projectileScene = (PackedScene)ResourceLoader.Load("res://Scenes/Player/Projectile.tscn");

    private void Attack()
    {
        if (_canAttack)
        {
            Node projectile = projectileScene.Instantiate();
            GetParent().AddChild(projectile);

            if (projectile is Node2D obj2D)
            {
                obj2D.Position = Position;
            }

            int _projectileDirecion = _direction == 0 ? _lastDirection : _direction;
            int _finalDirection = IsOnWall() && !IsOnFloor() ? (_projectileDirecion * -1) : _projectileDirecion;

            projectile.Call("SetDirection", _finalDirection);

            _attackCooldown = _defaultAttackCooldown;
        }
    }

    private void ProcessAttackCooldown(float delta)
    {
        if (_attackCooldown > 0)
        {
            _canAttack = false;
            _attackCooldown -= delta;
        }
        else
        {
            _canAttack = true;
            return;
        }
    }
}