using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

[GlobalClass]
public partial class PotionData : ItemData {
    [Export] public Array<IngredientData> Formula { get; set; }
    public List<IngredientData> IngredientsUsed { get; set; }

    public new Quality ItemQuality {
        get {
            int ingredientAmount = IngredientsUsed.Count;
            float qualityPoints = 0;

            foreach (var ing in IngredientsUsed) {
                qualityPoints += (float) ing.ItemQuality;
            }

            Quality resultQuality = (Quality) Mathf.Round(qualityPoints / ingredientAmount + 0.1);
            return resultQuality;
        }
    }

    public override int GetRealPrice () {
        if (ItemQuality == Quality.Normal)
            return Price;

        double qualityMod = 1 + (double) ItemQuality / (6 - (int) ItemQuality);

        int realPrice = Mathf.RoundToInt(Price * qualityMod);
        return realPrice;
    }

    public void Print() {
        string usedIngStr = IngredientsUsed.Aggregate("", (result, ing) => {
            string ingString = $@"
        {ing.Name}:
            Quality: {ing.ItemQuality}
            Base price: {ing.Price}
            Real price: {ing.GetRealPrice()}";

            return result + ingString;
        });

        string prStr = $@"{Name}:
    Quality: {ItemQuality}
    Ingredients used: {usedIngStr}
    Base price: {Price}
    Real price: {GetRealPrice()}
    ";

        GD.Print(prStr);
    }

    public static bool operator == (PotionData left, PotionData right) =>
        left.Name == right.Name;
    public static bool operator != (PotionData left, PotionData right) =>
        left.Name != right.Name;
}