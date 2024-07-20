using Godot;
using System;

public partial class CuttingScene : Node2D
{
    [Export] string PreparationScenePath = "res://Scenes/PreparationScene.tscn";


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{

        Inventory inv = GetNode<Inventory>( "/root/GlobalInventory" );
        GD.Print( inv.ingredients[0].Name );
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void BackButtonClicked()
	{
        Node preparationScene = ResourceLoader.Load<PackedScene>( PreparationScenePath ).Instantiate();

        GetTree().Root.AddChild( preparationScene );
        QueueFree();
    }
}
