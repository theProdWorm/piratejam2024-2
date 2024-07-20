using Godot;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public abstract partial class Inventory : Node2D {
    public List<IngredientData> ingredients;

    protected bool active = true;

    public override void _Ready () {
    }

    public override void _Process (double delta) {
    }

    /// <summary>
    /// Create the ingredient UI grid.<br></br>
    /// Hierarchy has the grid as the first child of inventory,
    /// the children of which are empty nodes to occupy the space for each box in the grid.<br></br>
    /// Item nodes are then childed to the empty box nodes.
    /// </summary>
    public void CreateIngredientUI (Node2D[] gridNodes) {
        Node2D ui = new() {
            Name = "Ingredients"
        };

        // Create ingredient nodes
        List<IngredientNode> ingredientNodes = new();
        ingredients.ForEach(ing => ingredientNodes.Add(new(ing)));

        for (int i = 0; i < ingredientNodes.Count; i++) {
            var gridBox = ui.GetChild(i);
            var ingredientNode = ingredientNodes[i];

            gridBox.AddChild(ingredientNode);
        }

        AddChild(ui);
    }

    public virtual void DestroyUI() {
        foreach (var grid in GetChildren()) {
            foreach (var node in grid.GetChildren()) {
                node.QueueFree();
            }
        }
    }

    public virtual void ToggleDisplay () {
        var children = GetChildren().Cast<Node2D>();
        active = !active;

        foreach (Node2D child in children) {
            child.Visible = active;
        }
    }
}