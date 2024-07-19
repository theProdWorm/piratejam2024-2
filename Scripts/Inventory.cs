using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Inventory : Node2D {
    public Array<IngredientData> ingredients = new();
    public Array<PotionData> potions = new();

    public override void _Ready () {
        PackedScene packedScene = ResourceLoader.Load<PackedScene>("Prefabs/Ingredient.tscn");
        var node = packedScene.Instantiate<IngredientNode>();

        node.data = ResourceLoader.Load<IngredientData>("Items/Ingredients/Critters/Red.tres");

        CallDeferred("Spawn", node);
    }

    public override void _Process (double delta) {
        
    }

    private void Spawn(Node node) {
        GetTree().Root.AddChild(node);
    }

    public void ToggleDisplay() {
        var children = GetChildren();

        foreach (Node2D child in children) {
            child.Visible = !child.Visible;
        }
    }
}