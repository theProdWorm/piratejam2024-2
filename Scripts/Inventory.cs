using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Inventory : Node2D {
    public Array<IngredientData> ingredients = new();
    public Array<PotionData> potions = new();

    public override void _Ready () {
    }

    public override void _Process (double delta) {
        IngredientNode node = null;

        if(Input.IsActionJustPressed("enter")) {
            PackedScene packedScene = GD.Load<PackedScene>("res://Prefabs/Ingredient.tscn");
            node = packedScene.Instantiate<IngredientNode>();

        }

        node?.sprite.Rotate(0.05f);
    }

    public void ToggleDisplay() {
        var children = GetChildren();

        foreach (Node2D child in children) {
            child.Visible = !child.Visible;
        }
    }
}