using Godot;
using System;
using System.Linq;

public partial class ItemNode : Sprite2D {
    public CollisionObject2D collider;

    protected bool isHovering = false;
    protected bool isHolding = false;

    protected Action ItemDropped;

    public override void _Ready () {
        collider = GetChild<CollisionObject2D>(0);
        collider.InputPickable = true;
        collider.CollisionLayer = 45;

        collider.MouseEntered += () => {
            isHovering = true;
        };
        collider.MouseExited += () => {
            isHovering = false;
        };

        ItemDropped = () => {
            GlobalPosition = ((Node2D) GetParent()).GlobalPosition;
        };
    }

    public override void _Process (double delta) {
        if (Input.IsActionJustPressed("click") && isHovering) {
            isHolding = true;
        }
        else if (Input.IsActionJustReleased("click") && isHolding) {
            ItemDropped();
        }

        if (isHolding) {
            Vector2 mousePos = GetViewport().GetMousePosition();

            GlobalPosition = mousePos;
        }
    }
}
