using Godot;

public partial class PotionNode : Sprite2D {
    public PotionData data;

    public PotionNode() { }

    public PotionNode(PotionData data) {
        this.data = data;
        Name = data.Name;
    }

    public override void _Ready () {
        Texture = data.Texture;
    }
}
