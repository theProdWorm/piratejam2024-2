using Godot;
using System;

public partial class PreparationScene : Node2D {
    [Export] string cuttingScenePath = "Scenes/CuttingScene.tscn";

    PlayerInventory playerInventory;

    public override void _Ready () {
        playerInventory = GetNode<PlayerInventory>("/root/PlayerInventory");

        playerInventory.CreateIngredientUI();

        PrintOrphanNodes();
    }

    public void CuttingClicked () {
        Node cuttingScene = ResourceLoader.Load<PackedScene>(cuttingScenePath).Instantiate();

        GetTree().Root.AddChild(cuttingScene);
        QueueFree();
    }
}