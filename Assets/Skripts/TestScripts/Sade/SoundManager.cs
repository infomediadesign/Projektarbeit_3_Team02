using UnityEngine;

public enum SoundType
{
    COLLECTABLELIVES,
    COLLECTABLEMEMORIES,
    BACKGROUND,

}

[RequireComponent(typeof(AudioSource))]     //always requires a sound
public class SoundManager : MonoBehaviour
{
   [SerializeField] private AudioClip[] soundList;
   private static SoundManager instance;
   private AudioSource audioSource;

   private void Awake()
   {
        instance = this;
   }

   private void Start()
   {
        audioSource = GetComponent<AudioSource>();
   }

   public static void PlaySound(SoundType sound, float volume = 1)
   {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);    //OneShot only plays the audio once
   }





}
