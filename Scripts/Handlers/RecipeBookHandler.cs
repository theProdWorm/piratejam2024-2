using Godot;

public partial class RecipeBookHandler : TextureButton {
    private bool isOpen;

    public override void _Ready () {
        Pressed += Toggle;
    }

    public void Toggle() {
        isOpen = !isOpen;

        if(isOpen) {
            Node newScene = ResourceLoader.Load<PackedScene>("Prefabs/RecipeBook.tscn").Instantiate();

            GetTree().Root.AddChild(newScene);
        }
        else {
            GetNode("/root/RecipeBook").QueueFree();
        }
    }
}