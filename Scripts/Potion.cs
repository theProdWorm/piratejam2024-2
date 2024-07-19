using Godot;
using Godot.Collections;

public partial class Potion : Item {
    [Export] public Array<Ingredient> Ingredients { get; set; }
}
