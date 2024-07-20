using Godot;
using System.Collections.Generic;
using System.Linq;

public partial class PlayerInventory : Inventory {
    public List<PotionData> potions;

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
    }

    protected void CreatePotionUI () {
        Node2D potionGrid = new();

        potionGrid.Name = "Potions";

        List<PotionNode> potionNodes = new();
        potions.ForEach(pot => potionNodes.Add(new(pot)));

        AddChild(potionGrid);
    }
}
