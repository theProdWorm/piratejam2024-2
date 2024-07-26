using Godot;

public partial class StartGameButton : TextureButton {
    public void ChangeScene() {
        var overlay = ResourceLoader.Load<PackedScene>("Scenes/Overlay.tscn").Instantiate();
        var playerInventory = ResourceLoader.Load<PackedScene>("Prefabs/Inventory/PlayerInventory.tscn").Instantiate();
        var merchantScene = ResourceLoader.Load<PackedScene>("Scenes/MerchantScene.tscn").Instantiate();

        var root = GetTree().Root;

        root.AddChild(overlay);
        root.AddChild(playerInventory);
        root.AddChild(merchantScene);

        GetNode("/root/MainMenu").QueueFree();
    }
}