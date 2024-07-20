using Godot;
using System;
using System.IO;

public partial class MerchantInventory : Inventory {
    private IngredientData[] ingredientCodex;

    double timer = 0;


    public override void _Ready () {

        string[] ingredientPaths = Directory.GetFiles("Items/Ingredients");

        ingredientCodex = new IngredientData[ingredientPaths.Length];

        for (int i = 0; i < ingredientPaths.Length; i++) {
            var ingredient = ResourceLoader.Load<IngredientData>(ingredientPaths[i]);
            ingredientCodex[i] = ingredient;
        }

        CreateStock();

        foreach(var ing in ingredients) {
            string format = $"Name: {ing.Name}, Quality: {ing.ItemQuality}";

            GD.Print(format);
        }
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

    private IngredientData GetRandomItem() {
        int index = GD.RandRange(0, ingredientCodex.Length - 1);

        Quality quality = GetRandomQuality();

        float haggleValue = 0; // TODO: change when HaggleValue is relevant

        IngredientData item = new(ingredientCodex[index]) {
            ItemQuality = quality,
            HaggleValue = haggleValue
        };

        return item;
    }

    private Quality GetRandomQuality() {
        Quality quality = Quality.Normal;

        if (GD.RandRange(0, 2) == 0) {
            quality = Quality.Good;
            if (GD.RandRange(0, 2) == 0) {
                quality = Quality.Great;
                if (GD.RandRange(0, 2) == 0) {
                    quality = Quality.Exquisite;
                }
            }
        }

        return quality;
    }
}
