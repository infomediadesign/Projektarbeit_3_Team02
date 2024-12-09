using UnityEngine;

[System.Serializable]       //view in inspect
public struct SoundEffect
{
    public string groupID;
    public AudioClip[] clips;
}
   
public class SoundLibrary : MonoBehaviour
{
   public SoundEffect[] soundEffects;

   public AudioClip GetClipFromName(string name)
   {
    foreach (var soundEffect in soundEffects)       //searches for all sound effects
    {
        if (soundEffect.groupID == name)
        {
            return soundEffect.clips[Random.Range(0, soundEffect.clips.Length)];
        }
    }
    return null;        //if we don't find anything return null
   }



}
