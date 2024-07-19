using Godot;

public partial class IngredientNode : Node2D {
    [Export] public IngredientData data;
    public Sprite2D sprite;

    public override void _Ready () {
        sprite.Texture = data.Texture;
    }
}
