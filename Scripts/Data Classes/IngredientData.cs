using Godot;
using Godot.Collections;

public enum PrepMethod {
    Cutting,
    Crushing
}

[GlobalClass]
public partial class IngredientData : ItemData {
    [Export] public Array<PrepMethod> StepsToPrepare { get; set; }
    public Quality Quality { get; set; }
    public float HaggleValue { get; set; }
}
