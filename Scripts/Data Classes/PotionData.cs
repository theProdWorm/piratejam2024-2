using Godot;
using Godot.Collections;

public partial class PotionData : ItemData {
    [Export] public Array<IngredientData> Ingredients { get; set; }
}
