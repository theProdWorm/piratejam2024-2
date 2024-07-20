using Godot;
using Godot.Collections;

public partial class PotionData : ItemData {
    [Export] public Array<IngredientData> Formula { get; set; }
    public Array<Quality> IngredientQualities { get; set; }
}
