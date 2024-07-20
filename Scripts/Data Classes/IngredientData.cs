using Godot;
using Godot.Collections;

public enum PrepMethod {
    Cutting,
    Crushing
}

[GlobalClass]
public partial class IngredientData : ItemData {
    [Export] public Array<PrepMethod> StepsToPrepare { get; set; }
    public float HaggleValue { get; set; }

    public IngredientData() { }

    public IngredientData(IngredientData other) {
        Name = other.Name;
        Price = other.Price;
        Texture = other.Texture;
        ItemQuality = other.ItemQuality;

        StepsToPrepare = other.StepsToPrepare;
        HaggleValue = other.HaggleValue;
    }
}
