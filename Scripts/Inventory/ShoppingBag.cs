using Godot;
using System;

public partial class ShoppingBag : Inventory {
    public override void _Ready () {
        base._Ready();

        ingredients = new(9);

        CreateIngredientUI();
    }

    public int GetTotalValue() {
        int totalValue = 0;

        foreach(var ing in ingredients) {
            totalValue += ing.GetRealPrice();
        }

        return totalValue;
    }
}