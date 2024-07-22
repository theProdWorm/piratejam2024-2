using Godot;
using System;

public partial class ShoppingBag : Inventory {
    PlayerInventory playerInventory;

    public override void _Ready () {
        base._Ready();

        playerInventory = (PlayerInventory) GetNode("/root/PlayerInventory");

        int openSlots = playerInventory.ingredients.Capacity - playerInventory.ingredients.Count;
        ingredients = new(openSlots > 9 ? 9 : openSlots);

        CreateIngredientUI();
    }

    public int GetTotalValue() {
        int totalValue = 0;

        foreach(var ing in ingredients) {
            totalValue += ing.GetRealPrice();
        }

        return totalValue;
    }

    public bool ConfirmPurchase() {
        int totalValue = GetTotalValue();
        bool canAfford = playerInventory.balance - totalValue >= 0;

        if (!canAfford){
            GD.Print($"Can't afford! Current balance: {playerInventory.balance}\nCost of items: {totalValue}");

            return false;
        }

        playerInventory.ingredients.AddRange(ingredients);
        playerInventory.balance -= totalValue;

        GD.Print($"Paid {totalValue} coins. New balance: {playerInventory.balance}");

        return true;
    }
}