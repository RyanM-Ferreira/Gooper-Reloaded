using Godot;
using System;

public partial class PlayerUi : CanvasLayer
{
    PlayerCore player;

    private AnimatedSprite2D _animatedSprite;

    private Node2D _heartContainer;

    private int _lastHealth = -1;

    public override void _Ready()
    {
        player = GetParent<PlayerCore>();
        _animatedSprite = GetNode<AnimatedSprite2D>("PlayerHealth");

        _heartContainer = new Node2D();
        AddChild(_heartContainer);

        _animatedSprite.Visible = false;
    }

    public override void _Process(double delta)
    {
        if (_lastHealth != player.health)
        {
            UpdateLife();
        }
    }


    public void UpdateLife()
    {
        _lastHealth = (int)player.health;

        foreach (Node child in _heartContainer.GetChildren())
        {
            child.QueueFree();
        }

        int heartPosition = 24;
        for (int i = 0; i < player.health; i++)
        {
            var heart = (AnimatedSprite2D)_animatedSprite.Duplicate();
            heart.Visible = true;
            heart.Position = new Vector2((i + 1) * heartPosition, 16);
            _heartContainer.AddChild(heart);
        }
    }
}