using Godot;
using Godot.Collections;

public partial class Player : Node {
    public Array<Ingredient> ingredients = new();
    public Array<Potion> potions = new();

    public override void _Ready () {
        
    }

    public override void _Process (double delta) {
    }
}