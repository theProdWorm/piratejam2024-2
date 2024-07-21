using Godot;
using System;

public partial class ItemNode : Sprite2D {
    public AnimatableBody2D collider;
    
    protected bool isHovering = false;
    protected bool isHolding = false;

    public override void _Ready () {
        collider = GetChild<AnimatableBody2D>(0);
        collider.InputPickable = true;
        collider.CollisionLayer = 45;

        collider.MouseEntered += () => {
            GD.Print("Hovering over: " + Name);
            isHovering = true;
        };
        collider.MouseExited += () => {
            GD.Print("No longer hovering over: " + Name);
            isHovering = false;
        };
    }

    public override void _Process (double delta) {
        if (Input.IsActionJustPressed("click") && isHovering) {
            isHolding = true;
        }
        else if (Input.IsActionJustReleased("click") && isHolding) {
            isHolding = false;

            //TODO: check if the item is released on a minigame

            GlobalPosition = ((Node2D) GetParent()).GlobalPosition;
        }

        if (isHolding) {
            Vector2 mousePos = GetViewport().GetMousePosition();

            GlobalPosition = mousePos;
        }
    }
}
