using Godot;

public partial class IngredientNode : ItemNode {
    [Export] public IngredientData data;

    public IngredientNode() { }

    public IngredientNode(IngredientData data) {
        this.data = data;
        Name = data.Name;
    }

    public override void _Ready () {
        base._Ready ();
        Texture = data.Texture;
    }
}
