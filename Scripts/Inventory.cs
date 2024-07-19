using Godot;
using Godot.Collections;

[GlobalClass]
public partial class Inventory : Node {
    public Array<Ingredient> ingredients = new();
    public Array<Potion> potions = new();

    public override void _Ready () {
        
    }

    public override void _Process (double delta) {
    }
}