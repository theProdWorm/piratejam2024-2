using Godot;
using System;
using System.IO;

public partial class MerchantInventory : Inventory {

    public override void _Ready () {
        // Load ingredients dynamically. This way, ingredients can be added
        // to the project without changing the code.
        
        CreateStock();

        CreateIngredientUI();
    }

    public override void _Process (double delta) {
    }

    private void CreateStock() {
        int stockCount = GD.RandRange(10, 16);

        ingredients = new(stockCount);

        for (int i = 0; i < stockCount; i++) {
            ingredients.Add(GetRandomItem());
        }
    }
}
