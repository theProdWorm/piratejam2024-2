using Godot;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class Inventory : Node2D {
    private enum InventoryType {
        ingredient,
        potion
    }

    [Export] public Vector2 margin;
    [Export] public Vector2 offset;

    public List<IngredientData> ingredients = new(32);
    public List<PotionData> potions = new(32);

    private bool active = true;
    private InventoryType currentInventory;

    Node2D ingredientGrid;
    Node2D potionGrid;

    public override void _Ready () {
        currentInventory = InventoryType.ingredient;

        ingredients.Add(new() {
            Name = "Spider",
            Price = 69,
            Quality = Quality.Superb,
            Texture = ResourceLoader.Load<Texture2D>("res://Textures/Red.png"),
            HaggleValue = 0.75f
        });

        CreateUI();


    }

    public override void _Process (double delta) {
    }

    /// <summary>
    /// Create the ingredient UI grid as well as the potion UI grid.<br></br>
    /// Hierarchy has the grid as the first child of inventory,
    /// the children of which are empty nodes to occupy the space for each box in the grid.<br></br>
    /// Item nodes are then childed to the empty box nodes.
    /// </summary>
    private void CreateUI () {
        ingredientGrid = FillGrid(4, 8);
        potionGrid = FillGrid(2, 8);

        ingredientGrid.Name = "Ingredients";
        potionGrid.Name = "Potions";

        ingredientGrid.SetMeta("type", (int) InventoryType.ingredient);
        potionGrid.SetMeta("type", (int) InventoryType.potion);

        // Create ingredient nodes
        List<IngredientNode> ingredientNodes = new();
        ingredients.ForEach(ing => ingredientNodes.Add(new(ing)));

        // Create potion nodes
        List<PotionNode> potionNodes = new();
        potions.ForEach(pot => potionNodes.Add(new(pot)));

        for (int i = 0; i < ingredientNodes.Count; i++) {
            var gridBox = ingredientGrid.GetChild(i);
            var ingredientNode = ingredientNodes[i];

            gridBox.AddChild(ingredientNode);
        }

        AddChild(ingredientGrid);
        AddChild(potionGrid);
    }

    /// <summary>
    /// Create empty nodes in a grid pattern with specified amount of rows and columns,
    /// using 'spacing' to determine how far apart the items should be (specified in the engine)
    /// </summary>
    /// <returns></returns>
    private Node2D FillGrid (int rows, int columns) {
        Node2D grid = new();

        // Create objects in a grid, spaced by 'spacing'        
        for (int y = 0; y < rows; y++) {
            for (int x = 0; x < columns; x++) {
                Node2D box = new();

                Vector2 pos = new(margin.X * (x + 1) + offset.X, margin.Y * (y + 1) + offset.Y);
                box.Transform = new(0, pos);

                box.Name = $"{x}, {y}";

                grid.AddChild(box);
            }
        }

        return grid;
    }

    public void ToggleDisplay () {
        var children = GetChildren().Cast<Node2D>();
        active = !active;

        foreach (Node2D child in children) {
            if ((int) child.GetMeta("type") == (int) currentInventory)
                child.Visible = active;
        }
    }

    public void SwitchInventory () {
        switch (currentInventory) {
        case InventoryType.ingredient:
            currentInventory = InventoryType.potion;

            ingredientGrid.Visible = true;
            potionGrid.Visible = false;

            break;
        case InventoryType.potion:
            currentInventory = InventoryType.ingredient;

            potionGrid.Visible = true;
            ingredientGrid.Visible = false;

            break;
        }
    }
}