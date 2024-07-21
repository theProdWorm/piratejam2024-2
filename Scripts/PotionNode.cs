using Godot;

public partial class PotionNode : ItemNode {
    [Export] public PotionData data;

    public PotionNode() { }

    public PotionNode(PotionData data) {
        this.data = data;
        Name = data.Name;
    }

    public override void _Ready () {
        Texture = data.Texture;
    }
}
