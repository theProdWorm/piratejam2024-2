using Godot;
using System;

public partial class WorkshopBrewingInventory : Inventory
{

	public override void _Ready()
	{
		base._Ready();

		ingredients = new( 6 );
	}

	public override void _Process( double delta )
	{
	
	}
}
