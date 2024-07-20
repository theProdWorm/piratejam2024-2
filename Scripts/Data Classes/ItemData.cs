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
}
