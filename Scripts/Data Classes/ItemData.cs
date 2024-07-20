using Godot;

[GlobalClass]
public partial class ItemData : Resource
{
    [Export] public string Name { get; set; }
    [Export] public int Price { get; set; }
    [Export] public Texture2D Texture { get; set; }
}