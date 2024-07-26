using Godot;
using System;

public partial class VolumeSlider : VSlider {
    [Export] public string busName;
    private int busIndex;

    public override void _Ready () {
        busIndex = AudioServer.GetBusIndex(busName);
    }

    public void OnValueChanged(float value) {
        value *= 0.01f;

        AudioServer.SetBusVolumeDb(busIndex, Mathf.LinearToDb(value));
    }
}
