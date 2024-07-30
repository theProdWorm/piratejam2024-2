using Godot;
using System;

[GlobalClass]
public partial class RythmPlayer : AudioStreamPlayer
{
    // Beats per minute
    [Export] double Bpm;
	// Seconds per beat
	double Spb;


    public override void _Ready()
	{
        Spb = 60d / Bpm;
    }

	public override void _Process( double delta )
	{

	}

	public double GetDistanceToClosestBeat()
	{
		double playbackTime = (double)GetPlaybackPosition() + AudioServer.GetTimeSinceLastMix() - AudioServer.GetOutputLatency();

		double currentBeat = playbackTime / Spb;

		double precision = currentBeat - Mathf.Round( currentBeat );

		return precision;
	}
}
