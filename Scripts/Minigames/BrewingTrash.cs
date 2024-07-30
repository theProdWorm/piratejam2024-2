using Godot;
using System;

public partial class BrewingTrash : Sprite2D
{
    public IngredientData data;

    public float timer;

    public BrewingTrash() { }

    public BrewingTrash( IngredientData data )
    {
        this.data = data;
        Name = data.Name;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Texture = data.Texture;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
