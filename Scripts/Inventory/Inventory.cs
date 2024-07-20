using Godot;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public abstract partial class Inventory : Node2D {
    protected enum InventoryType {
        ingredient,
        potion
    }

    // Variables for grid display
    [Export] public Vector2 StartPos { get; set; }
    [Export] public Vector2 Margin { get; set; }
    [Export] public Vector2 Offset { get; set; }

    public List<IngredientData> ingredients;

    protected bool active = true;

    protected Node2D ingredientGrid;

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
    protected virtual void CreateUI () {
        ingredientGrid = FillGrid(4, 8);

        ingredientGrid.Name = "Ingredients";
        ingredientGrid.SetMeta("type", (int) InventoryType.ingredient);

        // Create ingredient nodes
        List<IngredientNode> ingredientNodes = new();
        ingredients.ForEach(ing => ingredientNodes.Add(new(ing)));

        // Create potion nodes

        for (int i = 0; i < ingredientNodes.Count; i++) {
            var gridBox = ingredientGrid.GetChild(i);
            var ingredientNode = ingredientNodes[i];

            gridBox.AddChild(ingredientNode);
        }

        AddChild(ingredientGrid);
    }

    /// <summary>
    /// Create empty nodes in a grid pattern with specified amount of rows and columns,
    /// using 'spacing' to determine how far apart the items should be (specified in the engine)
    /// </summary>
    /// <returns></returns>
    protected Node2D FillGrid (int rows, int columns) {
        Node2D grid = new() {
            Transform = new(0, StartPos)
        };

        // Create objects in a grid, spaced by 'spacing'        
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < columns; x++) {
                Node2D box = new();

                float xPos = (x + 1) * Margin.X + x * Offset.X;
                float yPos = (y + 1) * Margin.Y + y * Offset.Y;

                Vector2 pos = new(xPos, yPos);
                box.Transform = new(0, pos);

                box.Name = $"{x}, {y}";

                grid.AddChild(box);
            }
        }

        return grid;
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