using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.IO;

public partial class BrewingScene : Node2D
{
    float brewingTime = 30f;

	float cauldronTemp = 50f;
	[Export] float cauldronMaxTemp = 100f;

	float fireTemp = 50f;
	[Export] float fireMaxTemp = 100f;

	[Export] ProgressBar cauldronTempProgressBar;
    [Export] VSlider fireTempSlider;

    [ExportGroup("Debug")] [Export] RichTextLabel heatEventText;
    [ExportGroup("Debug")] [Export] RichTextLabel trashEventText;
    [ExportGroup("Debug")] [Export] RichTextLabel ingredientsText;

    public List<IngredientData> ingredients = new();

	bool heatEvent = false;

	// Time between events
    float heatEventTimer = 0f;
    [ExportGroup("Heat Event")] [Export] float heatEventTimerMax = 10f;
    [ExportGroup("Heat Event")] [Export] float heatEventTimerMin = 5f;

	// How long an event lasts
	float heatEventDuration	= 0f;
    [ExportGroup("Heat Event")] [Export] float heatEventDurationMax = 5f;
	[ExportGroup("Heat Event")] [Export] float heatEventDurationMin = 2f;

	// The heat of the event
	float heatEventHeat	= 0f;
	[ExportGroup("Heat Event")] [Export] float heatEventHeatMax	= 30f;
	[ExportGroup("Heat Event")] [Export] float heatEventHeatMin	= 15f;


   
    List<BrewingTrash> trashInPot = new();
    List<IngredientData> trashNotRemoved = new();

    // Time between events
    float trashEventTimer = 0f;
    [ExportGroup("Trash Event")] [Export] float trashEventTimerMax = 5f;
    [ExportGroup("Trash Event")] [Export] float trashEventTimerMin = 5f;

    // How long trash lives
    float trashTimer = 5f;

    // How long an event lasts
    float trashEventDuration = 0f;
    [ExportGroup("Trash Event")] [Export] float trashEventDurationMax = 5f;
    [ExportGroup("Trash Event")] [Export] float trashEventDurationMin = 2f;

    [ExportGroup("Trash Event")] [Export] CollisionShape2D trashSpawnArea;



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        cauldronTempProgressBar.Value = cauldronTemp;
		cauldronTempProgressBar.MaxValue = cauldronMaxTemp;

		fireTempSlider.Value = fireTemp;
		fireTempSlider.MaxValue = fireMaxTemp;

        heatEventTimer = (float)GD.RandRange( heatEventTimerMin, heatEventTimerMax );
        trashEventTimer = (float)GD.RandRange( trashEventTimerMin, trashEventTimerMax );
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        brewingTime -= (float)delta;
        if( brewingTime <= 0f )
        {
            EndBrewing();
        }

        ProcessHeatEvent( delta );
        ProcessTrashEvent( delta );
        DebugPrint();
    }

    private void EndBrewing()
    {
        if( BrewHandler.TryBrew( ingredients, out PotionData potion ) )
        {
            GetNode<PlayerInventory>( "/root/PlayerInventory" ).potions.Add( potion );
        }

        Node scene = ResourceLoader.Load<PackedScene>( "res://Scenes/WorkshopScene.tscn" ).Instantiate();

        GetTree().Root.AddChild( scene );
        QueueFree();
    }

    private void ProcessHeatEvent( double delta )
    {
        double HeatAdd = (fireTempSlider.Value - fireMaxTemp / 2);

        if( heatEvent )
        {
            HeatAdd += heatEventHeat;

            heatEventDuration -= (float)delta;

            if( heatEventDuration <= 0f )
                EndHeatEvent();
        }
        else
        {
            heatEventTimer -= (float)delta;

            if( heatEventTimer <= 0f )
                StartHeatEvent();
        }

        cauldronTempProgressBar.Value += HeatAdd * 0.004d;
    }

	private void StartHeatEvent()
	{
		heatEvent = true;
        heatEventDuration = (float)GD.RandRange( heatEventDurationMin, heatEventDurationMax );

		float heatEventPositive = GD.RandRange( 0, 1 );
		heatEventPositive = heatEventPositive * 2 - 1;

        heatEventHeat = (float)GD.RandRange( heatEventHeatMin, heatEventHeatMax ) * heatEventPositive;

        GD.Print( "Started heat event" );
    }

    private void EndHeatEvent()
    {
		heatEvent = false;
        heatEventTimer = (float)GD.RandRange( heatEventTimerMin, heatEventTimerMax );

        GD.Print( "Ended heat event" );
    }

    private void ProcessTrashEvent( double delta )
    {
        trashEventTimer -= (float)delta;

        if( trashEventTimer <= 0f )
        {
            trashEventTimer = (float)GD.RandRange( trashEventTimerMin, trashEventTimerMax );

            StartTrashEvent();
        }

        for (int i = 0; i < trashInPot.Count; i++)
        {
            BrewingTrash trash = trashInPot[i];

            trash.timer -= (float)delta;
            if( trash.timer <= 0f && !trash.IsQueuedForDeletion() )
            {
                trashNotRemoved.Add( trash.data );
                trashInPot.Remove( trash );
                GetNode( "Pot holder" ).RemoveChild( trash );
                trash.QueueFree();
                continue;
            }

            trash.Rotate( 0.01f );
        }
    }

    private void StartTrashEvent()
    {
        GD.Print( "Started trash event" );

        CircleShape2D spawnArea = (CircleShape2D)trashSpawnArea.Shape;

        // The length from the middle of the circle
        float length = spawnArea.Radius * Mathf.Sqrt( GD.Randf() );
        float angle = GD.Randf() * 2f * Mathf.Pi;

        float posX = trashSpawnArea.GlobalPosition.X + length * Mathf.Cos( angle ) * trashSpawnArea.GlobalScale.X;
        float posY = trashSpawnArea.GlobalPosition.Y + length * Mathf.Sin( angle ) * trashSpawnArea.GlobalScale.Y;

        PackedScene prefab = ResourceLoader.Load<PackedScene>( "Prefabs/BrewingTrash.tscn" );
        BrewingTrash ingredient = prefab.Instantiate<BrewingTrash>();

        ingredient.data = GetNode<PlayerInventory>( "/root/PlayerInventory" ).GetRandomItem();
        ingredient.timer = trashTimer;

        ingredient.GetNode<Button>( "Button" ).Pressed += () =>
        {
            if( !ingredient.IsQueuedForDeletion() )
            {
                trashInPot.Remove( ingredient );
                ingredient.QueueFree();
            }
        };

        trashInPot.Add( ingredient );

        GetNode("Pot holder").AddChild( ingredient );

        ingredient.GlobalPosition = new Vector2( posX, posY );
    }

    private void DebugPrint()
    {
        heatEventText.Text = "Heat: " + heatEvent + "\nTimer: " + heatEventDuration + "\nEvent Heat: " + heatEventHeat;
        trashEventText.Text = "Trash in pot: " + trashInPot.Count + "\nTrash not removed: " + trashNotRemoved.Count;
        ingredientsText.Text = "";
        foreach( var ingredient in ingredients )
        {
            ingredientsText.Text += ingredient.Name + "\n";
        }
    }
}
