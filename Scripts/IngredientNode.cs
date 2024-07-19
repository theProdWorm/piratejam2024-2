using Godot;

public partial class IngredientNode : Sprite2D {
    [Export] public IngredientData data;

    public override void _Ready () {
        Texture = data.Texture;
    }
}
