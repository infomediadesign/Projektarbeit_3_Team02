using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;    //access from anywhere
    
    [SerializeField] private SoundLibrary sfxLibrary;   //reference to sound library
    [SerializeField] private AudioSource sfx2DSource;

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
  
}
