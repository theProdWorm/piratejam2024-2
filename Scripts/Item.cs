using Godot;
using System;

[GlobalClass]
public partial class Item : Resource
{
    public string Name { get; set; }
    public int Price { get; set; }
}
