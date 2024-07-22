using Godot;

public enum Quality {
    Normal,
    Good,
    Great,
    Exquisite
}

[GlobalClass]
public abstract partial class ItemData : Resource
{
    [Export] public string Name { get; set; }
    [Export] public int Price { get; set; }
    [Export] public Texture2D Texture { get; set; }
    public Quality ItemQuality { get; set; }

    public int GetRealPrice() {
        if(ItemQuality == Quality.Normal)
            return Price;

        float qualityMod = 1 + (float) ItemQuality / 5f;

        int realPrice = (int) Mathf.Round(Price * qualityMod);

        return realPrice;
    }
}