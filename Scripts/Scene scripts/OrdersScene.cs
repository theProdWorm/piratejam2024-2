using Godot;
using System;
using System.Collections.Generic;

public partial class OrdersScene : Node2D
{
	Orders orders;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        PlayerInventory playerInventory = GetNode<PlayerInventory>( "/root/PlayerInventory" );

		playerInventory.invIsPotions = true;

		orders = GetNode<Orders>( "/root/Orders" );
		orders.GenerateNewOrders();

		CreateOrdersUI();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void UpdateOrderUI()
	{
		DestroyOrdersUI();
		CreateOrdersUI();
	}

	public void CreateOrdersUI()
	{
		Node2D orderPositions = GetNode<Node2D>( "Order Positions" );
        GD.Print( "Generating Orders UI" );
        for (int i = 0; i < orders.orders.Count; i++)
        {
			Node orderBox = ResourceLoader.Load<PackedScene>( "Prefabs/OrderBox.tscn" ).Instantiate();
			RichTextLabel orderText = orderBox.GetChild<RichTextLabel>( 0 );
			orderText.Text = "";
			orderPositions.GetChild( i ).AddChild( orderBox );
			GD.Print( "Added order to screen" );
			foreach( PotionData potion in orders.orders[i].requestedPotions )
			{
				orderText.Text += potion.Name + "\n";
            }
		}
	}

    public void DestroyOrdersUI()
	{
        foreach ( var orderPositions in GetNode("/root/Order Positions").GetChildren() )
        {
			orderPositions.GetChild( 0 ).QueueFree();
        }
    }
}
