using Godot;
using System.Collections.Generic;

public partial class PlayerInventory : Inventory {
    public List<PotionData> potions;

    public int money;
    public IngredientData shopRequest;



    public override void _Ready () {
        potions = new(16);
        ingredients = new(24);

        for (int i = 0; i < 10; i++) {
            var ingredient = ResourceLoader.Load<IngredientData>("Items/Ingredients/Mermaid Tears.tres");

            ingredients.Add(ingredient);
        }

        CreateIngredientUI();
    }

    protected void CreatePotionUI () {
        Node2D ui = (Node2D) GetNode("Potions");

        List<PotionNode> potionNodes = new();
        foreach(var pot in potions) {
            var prefab = ResourceLoader.Load<PackedScene>("Prefabs/Potion.tscn");
            var potion = prefab.Instantiate<PotionNode>();

            potion.data = pot;

            potionNodes.Add(potion);
        };

        for (int i = 0; i < potionNodes.Count; i++) {
            ui.GetChild(i).AddChild(potionNodes[i]);
        }

        AddChild(ui);
    }

    public override void UpdateUI () {
        DestroyUI();

    }
}
