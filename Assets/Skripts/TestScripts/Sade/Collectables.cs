using UnityEngine;

public class Collectables : MonoBehaviour
{
    SoundManager soundManager;
    public string itemType;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
    }

    public void PlaySound()
    {
        if(CompareTag("Collectable")) //compares tag on object 
        {
            soundManager.PlaySFX(soundManager.collectableLives);
        }
        
    }
}
