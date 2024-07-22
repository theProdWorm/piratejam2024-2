using Godot;
using Godot.Collections;

public enum PrepMethod {
    Cutting,
    Crushing,
    None
}

[GlobalClass]
public partial class IngredientData : ItemData {
    [Export] public PrepMethod PrepMethod { get; set; }
    public float HaggleValue { get; set; }

    public IngredientData() { }

    public IngredientData(IngredientData other) {
        Name = other.Name;
        Price = other.Price;
        Texture = other.Texture;
        ItemQuality = other.ItemQuality;

        PrepMethod = other.PrepMethod;
        HaggleValue = other.HaggleValue;
    }
}
