using Godot;
using System;

public partial class CuttingScene : Node2D
{
    [Export] string PreparationScenePath = "res://Scenes/PreparationScene.tscn";
    [Export] RythmPlayer RythmPlayer;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void _Input( InputEvent @event )
    {
        if( @event is InputEventMouseButton mouseButton )
        {
            if( mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed )
            {
                RythmPlayer.GetDistanceToClosestBeat();
            }
        }
    }

    public void BackButtonClicked()
	{
        Node preparationScene = ResourceLoader.Load<PackedScene>( PreparationScenePath ).Instantiate();

        GetTree().Root.AddChild( preparationScene );
        QueueFree();
    }
}
