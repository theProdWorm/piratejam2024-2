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

    public virtual int GetRealPrice() {
        if(ItemQuality == Quality.Normal)
            return Price;

        double qualityMod = 1 + (double) ItemQuality / 5f;

        int realPrice = Mathf.RoundToInt(Price * qualityMod);
        return realPrice;
    }

    public void RandomizeQuality () {
        if (GD.RandRange(0, 2) == 0) {
            ItemQuality = Quality.Good;
            if (GD.RandRange(0, 2) == 0) {
                ItemQuality = Quality.Great;
                if (GD.RandRange(0, 2) == 0) {
                    ItemQuality = Quality.Exquisite;
                }
            }
        }
    }
}