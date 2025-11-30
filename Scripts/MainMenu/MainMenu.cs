using Godot;
using System;

public partial class MainMenu : Node2D
{
    private Button _startButton;
    private Button _quitButton;

    public override void _Ready()
    {
        _startButton = GetNodeOrNull<Button>("CanvasLayer/StartButton");
        _quitButton = GetNodeOrNull<Button>("CanvasLayer/QuitButton");
    }
    
    private void _on_StartButton_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Levels/DebugLevel.tscn");
    }

    private void _on_QuitButton_pressed()
    {
        GetTree().Quit();
    }
}