using System;
using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;        //access from anywhere in the game

    [SerializeField] private MusicLibrary musicLibrary;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource backgroundSource;

    private string lastTrackPlayed;
    private string lastBackgroundPlayed;
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
    public void PlayMusic(string trackName, string backgroundName, float fadeDuration = 0.5f)
    {
        lastTrackPlayed = trackName;
        lastBackgroundPlayed = backgroundName;
        StartCoroutine(AnimateMusicCrossfade(musicLibrary.GetClipFromName(trackName), musicLibrary.GetClipFromName(backgroundName), fadeDuration));
    }
    public string GetLastTrackPlayed()
    {
        return lastTrackPlayed; 
    }
    public string GetLastBackgroundPlayed()
    {
        return lastBackgroundPlayed;
    }
    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, AudioClip nextBackground, float fadeDuration = 0.5f)  //coroutine
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(1f, 0, percent);        //fades out music (1 to 0)
            backgroundSource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;  //needed because inside of a coroutine
        }

        musicSource.clip = nextTrack;       //replace with new track
        musicSource.loop = true;    //loops track 
        musicSource.Play();

        backgroundSource.clip = nextBackground;       //replace with new track
        backgroundSource.loop = true;    //loops track 
        backgroundSource.Play();

        percent = 0;    //turn up volume of new track
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeDuration;
            musicSource.volume = Mathf.Lerp(0, 1f, percent); //from 0 to 1
            backgroundSource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;
        }

    }

    //set volume
    public void SetVolume(float volume)
    {
        musicSource.volume = volume;
        backgroundSource.volume = volume;
    }
    //get current volume
    public float GetMusicVolume()
    {
        return musicSource.volume;
    }
    public float GetBackgroundVolume()
    {
        return backgroundSource.volume;
    }

}
