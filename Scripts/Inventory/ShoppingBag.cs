using Godot;
using System;

public partial class ShoppingBag : Inventory {
    private PlayerInventory playerInventory;

    private RichTextLabel purchaseLabel;
    private RichTextLabel totalValueLabel;

    private int totalValue;
    private bool canAfford;

    public override void _Ready () {
        base._Ready();

        playerInventory = (PlayerInventory) GetNode("/root/PlayerInventory");

        purchaseLabel = (RichTextLabel) GetNode("/root/MerchantScene/Purchase button/Purchase label");
        totalValueLabel = (RichTextLabel) GetNode("Total value");

        int openSlots = playerInventory.ingredients.Capacity - playerInventory.ingredients.Count;
        ingredients = new(openSlots > 9 ? 9 : openSlots);

        CreateIngredientUI();

        totalValue = GetTotalValue();
        canAfford = playerInventory.balance - totalValue >= 0;

        UpdateLabels();
    }

    public override void _Process (double delta) {
        DisplayPrices();
    }

    public override void CreateIngredientUI() {
        base.CreateIngredientUI();

        UpdateLabels();
    }

    public int GetTotalValue() {
        int totalValue = 0;

        foreach(var ing in ingredients) {
            totalValue += ing.GetRealPrice();
        }

        return totalValue;
    }

    public void ConfirmPurchase() {
        if (!canAfford)
            return;

        // Add ingredients to player inventory and remove money accordingly
        playerInventory.ingredients.AddRange(ingredients);
        playerInventory.balance -= totalValue;

        playerInventory.UpdateBalanceLabel();

        // Load new scene
        Node workshopScene = ResourceLoader.Load<PackedScene>("Scenes/WorkshopScene.tscn").Instantiate();

        var root = GetTree().Root;
        root.AddChild(workshopScene);

        root.GetNode("MerchantScene").QueueFree();
    }

    public void UpdateLabels() {
        totalValue = GetTotalValue();
        canAfford = playerInventory.balance - totalValue >= 0;

        totalValueLabel.Text = $"[right]{totalValue}G";
        purchaseLabel.Text = "[center]" + (canAfford ? (totalValue == 0 ? "Buy nothing. I have all I need!" : "Pay and start the day!") : "Can't afford!");
    }
}