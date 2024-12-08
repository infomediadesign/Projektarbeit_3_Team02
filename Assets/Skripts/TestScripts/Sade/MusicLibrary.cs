using System;
using UnityEngine;

[System.Serializable]
public struct MusicTrack
{
    public string trackName;
    public AudioClip clip;
}

public class MusicLibrary : MonoBehaviour
{
    public MusicTrack[] tracks;

    public AudioClip GetClipFromName(string trackName)      //returns audio clip with name
    {
        foreach (var track in tracks)       //looks trough all tracks
        {
            if (track.trackName == trackName)
            {
                return track.clip;
            }
        }
        return null;
    }

  
}
