using Godot;
using System;

public partial class PreparationScene : Node2D {
    [Export] string cuttingScenePath = "Scenes/CuttingScene.tscn";
    [Export] string brewingScenePath = "Scenes/BrewingScene.tscn";

    PlayerInventory playerInventory;

    [Export] WorkshopBrewingInventory brewingInventory;

    public override void _Ready () {
        playerInventory = GetNode<PlayerInventory>("/root/PlayerInventory");

        playerInventory.CreateIngredientUI();
        playerInventory.invIsPotions = false;
    }

    public void CuttingClicked () {
        Node cuttingScene = ResourceLoader.Load<PackedScene>(cuttingScenePath).Instantiate();

        GetTree().Root.AddChild(cuttingScene);
        QueueFree();
    }

    public void BrewingClicked()
    {
        if( brewingInventory.ingredients.Count <= 0 )
            return;

        BrewingScene brewingScene = (BrewingScene)ResourceLoader.Load<PackedScene>( brewingScenePath ).Instantiate();

        brewingScene.ingredients = brewingInventory.ingredients;

        GetTree().Root.AddChild(brewingScene);
        QueueFree();
    }
}