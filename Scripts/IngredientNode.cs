using Godot;

[GlobalClass]
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

        ItemDropped = () => {
            isHolding = false;

            //TODO: check where the item is dropped
            if(false /*dropped on minigame*/) {

            }
            else {
                GlobalPosition = ((Node2D) GetParent()).GlobalPosition;
            }
        };
    }
}
