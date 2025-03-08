using System;
using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;        //access from anywhere in the game

    [SerializeField] private MusicLibrary musicLibrary;
    [SerializeField] private AudioSource musicSource;

    private string lastTrackPlayed;
    private void Awake()
    {
        if (Instance != null)       //checks if instance is already set
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
            //musicSource.loop = true; //tracks automatically loop
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ResetInstance()
    {
        Instance = null;
    }
    public void PlayMusic(string trackName, float fadeDuration = 0.5f)
    {
        lastTrackPlayed = trackName;
        StartCoroutine(AnimateMusicCrossfade(musicLibrary.GetClipFromName(trackName), fadeDuration));
    }
    public string GetLastTrackPlayed()
    {
        return lastTrackPlayed; 
    }
    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeDuration = 0.5f)  //coroutine
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(1f, 0, percent);        //fades out music (1 to 0)
            yield return null;  //needed because inside of a coroutine
        }

        musicSource.clip = nextTrack;       //replace with new track
        musicSource.loop = true;    //loops track 
        musicSource.Play();

        percent = 0;    //turn up volume of new track
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, 1f, percent); //from 0 to 1
            yield return null;
        }

    }

    //set volume
    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
    }
    //get current volume
    public float GetVolume()
    {
        return musicSource.volume;
    }

}
