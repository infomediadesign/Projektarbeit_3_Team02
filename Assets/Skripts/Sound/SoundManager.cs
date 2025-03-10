using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;    //access from anywhere
    
    [SerializeField] private SoundLibrary sfxLibrary;   //reference to sound library
    [SerializeField] private AudioSource sfx2DSource;
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private AudioSource enemyAudioSource;

    private void Awake()
    {
        if(Instance != null)        //checks for instance -> if it already exists -> destroy
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);      //otherwise makes sure it does not get destroyed
        }
    }

    private void Start()
    {
        //register for button click events
        EventManager.Instance.StartListening("ButtonClicked", PlayDefaultButtonSound);
        EventManager.Instance.StartListening("StartGameButtonClicked", PlayStartGameSound);

    }

    public void PlaySound3D(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos);     //will spawn audio source at that position and play it 
        }                                               //this method is helpful if we play a clip outside our library
    }

    public void PlaySound3D(string soundName, Vector3 pos)
    {
        PlaySound3D(sfxLibrary.GetClipFromName(soundName), pos);    //accesses Library and gets the sound
    }

    public void PlaySound2D(string soundName)
    {

        sfx2DSource.PlayOneShot(sfxLibrary.GetClipFromName(soundName));
    }
    public void PlayPlayerSound(string soundName)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        if (clip != null)
        {
            playerAudioSource.PlayOneShot(clip);
        }
    }
    public void PlayEnemySound(string soundName)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        if (clip != null)
        {
            enemyAudioSource.PlayOneShot(clip);
        }
    }
    public void StopSound2D()
    {
        sfx2DSource.Stop();
    }
    public void StopPlayerSound2D()
    {
        playerAudioSource.Stop();
    }
    public void StopEnemySound2D()
    {
        enemyAudioSource.Stop();
    }

    public void PlaySoundLoop2D(string soundName)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        if (clip != null)
        {
            Debug.Log("sound found");
            sfx2DSource.clip = clip;
            sfx2DSource.loop = true;
            sfx2DSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        sfx2DSource.volume = volume; //set the 2D sound effects volume
    }

    public float GetVolume()
    {
        return sfx2DSource.volume; //retrieve the current volume for the slider
    }
    public bool IsSoundPlaying()
    {
        return playerAudioSource.isPlaying;
    }
    public bool IsEnemySoundPlaying()
    {
        return enemyAudioSource.isPlaying;
    }
    private void PlayDefaultButtonSound()
    {
        Debug.Log("Playing default button click sound.");
        
        SoundManager.Instance.PlaySound2D("DefaultButton");
    }

    private void PlayStartGameSound()
    {
        Debug.Log("Playing start game button sound.");

        SoundManager.Instance.PlaySound2D("StartGameButton");
    }


    private void OnDestroy()
    {
        //unregister listeners 
        EventManager.Instance.StopListening("ButtonClicked", PlayDefaultButtonSound);
        EventManager.Instance.StopListening("StartGameButtonClicked", PlayStartGameSound);
    }

}
