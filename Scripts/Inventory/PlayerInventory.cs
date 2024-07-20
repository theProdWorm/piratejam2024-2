using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerInventory : Inventory {

    public List<PotionData> potions;

    Node2D potionGrid;
    InventoryType currentInventory;

    public override void _Ready () {
        potions = new(16);
        ingredients = new(32);

        for (int i = 0; i < 10; i++) {
            ingredients.Add(new() {
                Name = "Spider",
                Price = 69,
                ItemQuality = Quality.Good,
                Texture = ResourceLoader.Load<Texture2D>("res://Textures/Trash/Red.png"),
                HaggleValue = 0
            });
        }

        StartPos = new(20, 500);
        Margin = new(20, 20);
        Offset = new(50, 50);

        CreateUI();
    }

    protected override void CreateUI () {
        // Creates ingredient grid
        base.CreateUI();

        // Creates potion grid
        potionGrid = FillGrid(2, 8);

        potionGrid.Name = "Potions";
        potionGrid.SetMeta("type", (int) InventoryType.potion);

        List<PotionNode> potionNodes = new();
        potions.ForEach(pot => potionNodes.Add(new(pot)));

        AddChild(potionGrid);
    }

    public override void ToggleDisplay () {
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

            ingredientGrid.Visible = active;
            potionGrid.Visible = false;

            break;
        case InventoryType.potion:
            currentInventory = InventoryType.ingredient;

            potionGrid.Visible = active;
            ingredientGrid.Visible = false;

            break;
        }
    }
}
