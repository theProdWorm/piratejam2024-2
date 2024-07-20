using Godot;
using System;
using System.IO;

public partial class MerchantInventory : Inventory
{
    private readonly Random random = new();

    private IngredientData[] ingredientCodex;

    double timer = 0;


    public override void _Ready () {

        string[] ingredientPaths = Directory.GetFiles("Items/Ingredients");

        ingredientCodex = new IngredientData[ingredientPaths.Length];

        for (int i = 0; i < ingredientPaths.Length; i++) {
            var ingredient = ResourceLoader.Load<IngredientData>(ingredientPaths[i]);
            ingredientCodex[i] = ingredient;
        }

        StartPos = new(20, 20);
        Margin = new(20, 20);
        Offset = new(50, 50);

        CreateStock();

        foreach(var ing in ingredients) {
            string format = $"Name: {ing.Name}, Quality: {ing.ItemQuality}";

            GD.Print(format);
        }

        CreateUI();
    }

    public override void _Process (double delta) {
    }

    private void CreateStock() {
        int stockCount = random.Next(10, 17);

        ingredients = new(stockCount);

        for (int i = 0; i < stockCount; i++) {
            ingredients.Add(GetRandomItem());
        }
    }

    private IngredientData GetRandomItem() {
        int index = random.Next(ingredientCodex.Length);

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

        if (random.Next(3) == 0) {
            quality = Quality.Good;
            if (random.Next(3) == 0) {
                quality = Quality.Great;
                if (random.Next(3) == 0) {
                    quality = Quality.Exquisite;
                }
            }
        }

        return quality;
    }
}
