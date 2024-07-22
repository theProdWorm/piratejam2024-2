using Godot;
using System;

public partial class ShoppingBag : Inventory {
    public int GetTotalValue() {
        int totalValue = 0;

        foreach(var ing in ingredients) {
            totalValue += ing.GetRealPrice();
        }

        return totalValue;
    }
}