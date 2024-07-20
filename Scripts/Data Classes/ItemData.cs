using Godot;

public enum Quality {
    Good,
    Superb,
    Perfect
}

[GlobalClass]
public abstract partial class ItemData : Resource
{
    [Export] public string Name { get; set; }
    [Export] public int Price { get; set; }
    [Export] public Texture2D Texture { get; set; }
}
