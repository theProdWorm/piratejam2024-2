using Godot;
using System;

[GlobalClass]
public partial class Item : Resource
{
    [Export] public string Name { get; set; }
    [Export] public int Price { get; set; }
}
