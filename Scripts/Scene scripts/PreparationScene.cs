using Godot;
using System;

public partial class PreparationScene : Node2D
{
    [Export] string CuttingScenePath = "res://Scenes/CuttingScene.tscn";

    Inventory Inventory;

	public override void _Ready()
	{
        Inventory = GetNode<Inventory>( "/root/GlobalInventory" );
		IngredientData ing = new() {
			Name = "Yes"
		};
		Inventory.ingredients.Add( ing );

		PrintOrphanNodes();
	}

	public override void _Process(double delta)
	{
        
    }

    public override void _UnhandledInput( InputEvent @event )
    {
    }

	public void CuttingClicked()
	{
        Node cuttingScene = ResourceLoader.Load<PackedScene>( CuttingScenePath ).Instantiate();

        GetTree().Root.AddChild( cuttingScene );
        QueueFree();
    }
}
