using Godot;
using Godot.Collections;

public partial class PotionData : ItemData {
    [Export] public Array<IngredientData> Formula { get; set; }
    public Array<IngredientData> UsedIngredients { get; set; }

    public override int GetRealPrice () {
        int price = 0;

        foreach (var ingredient in UsedIngredients) {
            price += ingredient.GetRealPrice();
        }

        return price;
    }

    public static bool operator == (PotionData left, PotionData right) =>
        left.Name == right.Name;
    public static bool operator != (PotionData left, PotionData right) =>
        left.Name != right.Name;
}