using Godot;
using System.Linq;

[GlobalClass]
public partial class IngredientNode : ItemNode {
    public IngredientData data;

    public IngredientNode () { }

    public IngredientNode (IngredientData data) {
        this.data = data;
        Name = data.Name;
    }

    public override void _Ready () {
        base._Ready();

        var qualityNodes = (from a in GetChildren() where a.Name.ToString().StartsWith("Quality") select a).Cast<Node2D>().ToArray();

        for (int i = 0; i < (int) data.ItemQuality; i++) {
            qualityNodes[i].Visible = true;
        }

        var priceTag = (RichTextLabel) GetNode("Price tag");

        priceTag.Text = $"[center]{data.GetRealPrice()}G";

        Texture = data.Texture;

        ItemDropped = () => {
            isHolding = false;

            var raycastPrefab = ResourceLoader.Load<PackedScene>("Prefabs/Casts/IngredientCast.tscn");
            var raycast = raycastPrefab.Instantiate<RayCast2D>();

            GetTree().Root.AddChild(raycast);

            raycast.GlobalPosition = GetViewport().GetMousePosition();

            raycast.ForceRaycastUpdate();

            if (raycast.IsColliding()) {
                var otherCollider = (CollisionObject2D) raycast.GetCollider();

                Inventory targetInventory = (Inventory) otherCollider.GetParent();
                Inventory currentInventory = (Inventory) GetParent().GetParent().GetParent();

                if (targetInventory == currentInventory ||
                    targetInventory.ingredients.Count == targetInventory.ingredients.Capacity)
                    goto setpos;

                int index = currentInventory.ingredients.IndexOf(data);

                if (targetInventory != null) {
                    Inventory.TransferIngredient(index, currentInventory, targetInventory);
                }

                return;
            }

            else if (false      /*dropped on minigame*/       ) {

            }

            setpos:
            GlobalPosition = ((Node2D) GetParent()).GlobalPosition;
        };
    }
}