using Godot;
using System.Collections.Generic;

public partial class PlayerInventory : Inventory {
    [Export] public int startBalance;

    public List<PotionData> potions;

    public int balance;
    public IngredientData shopRequest;

    public RichTextLabel balanceLabel;

    public bool invIsPotions = false;

    public override void _Ready () {
        base._Ready();
        balance = startBalance;

        potions = new(16);
        ingredients = new(21);

        //balanceLabel = (RichTextLabel) GetNode("/root/Overlay/Balance background/Balance");
        //
        //UpdateBalanceLabel();

        for (int i = 0; i < 21; i++)
            ingredients.Add( GetRandomItem() );
    }

    public void CreatePotionOfVirility() {
        List<IngredientData> ings = new() {
            new(ingredientCodex["Mermaid Tears"]),
            new(ingredientCodex["Mermaid Tears"]),
            new(ingredientCodex["Octopus Ink"]),
            new(ingredientCodex["Witch Finger"])
        };

        foreach(var ing in ings) {
            ing.RandomizeQuality();
        }

        bool success = BrewHandler.TryBrew(ings, out PotionData resultPotion);

        if (success) {
            GD.Print("Brew success!");
            resultPotion.Print();
        }
    }

    public void CreatePotionUI () {
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

        if(invIsPotions)
            CreatePotionUI();
        else
            CreateIngredientUI();
    }

    public void UpdateBalanceLabel() {
        balanceLabel.Text = $"[right]{balance}G";
    }
}
