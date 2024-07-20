using Godot;

public partial class IngredientNode : Sprite2D {
    public IngredientData data;

    public IngredientNode() { }

    public IngredientNode(IngredientData data) {
        this.data = data;
        Name = data.Name;
    }

    public override void _Ready () {
        Texture = data.Texture;
    }
}
