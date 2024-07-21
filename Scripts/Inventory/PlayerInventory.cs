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
        Node2D ui = (Node2D) GetNode("Potions");

        List<PotionNode> potionNodes = new();
        potions.ForEach(pot => {
            var prefab = ResourceLoader.Load<PackedScene>("Prefabs/Potion.tscn");
            var potion = prefab.Instantiate<PotionNode>();

            potion.data = pot;

            potionNodes.Add(potion);
        });

        AddChild(ui);
    }
}
