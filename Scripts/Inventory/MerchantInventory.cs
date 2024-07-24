using Godot;
using System.Linq;

public partial class MerchantInventory : Inventory {
    public override void _Ready () {
        base._Ready();

        CreateStock();

        CreateIngredientUI();
        DisplayPrices();
    }

    public override void _Process (double delta) {
        DisplayPrices();
    }

    private void CreateStock() {
        int stockCount = GD.RandRange(10, 16);

        ingredients = new(16);

        for (int i = 0; i < stockCount; i++) {
            ingredients.Add(GetRandomItem());
        }
    }
}
