using Godot;
using Godot.Collections;

public enum PrepMethod {
    Cutting,
    Crushing
}

public enum Quality {
    Bad,
    Good,
    Great
}

[GlobalClass]
public partial class Ingredient : Item
{
    [Export] public Array<PrepMethod> StepsToPrepare { get; set; }
    public Quality Quality { get; set; }
    public float HaggleValue { get; set; }
}
