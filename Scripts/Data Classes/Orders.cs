using Godot;
using System;
using System.Collections.Generic;

public partial class Orders : Node2D
{
	public List<Order> orders = new(3);

	public int minOrders = 1; 
	public int maxOrders = 3;

    public int minPotions = 1;
    public int maxPotions = 3;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	
	public void GenerateNewOrders()
	{
        foreach (var order in orders)
        {
			orders.Remove(order);
			order.Free();
        }

		int numOrders = GD.RandRange( minOrders, maxOrders );
		for (int i = 0; i < numOrders; i++)
		{
            int numPotions = GD.RandRange( minPotions, maxPotions );
            Order order = new Order();
			orders.Add( order );
            order.requestedPotions = new( numPotions );

            for (int j = 0; j < numPotions; j++)
                order.requestedPotions.Add( BrewHandler.GetRandomPotion() );
		}
		GD.Print( "Orders: " + numOrders );
    }
}
