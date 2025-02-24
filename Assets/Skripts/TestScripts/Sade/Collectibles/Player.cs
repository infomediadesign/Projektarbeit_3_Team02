using UnityEngine;

public class Player : MonoBehaviour
{
    public int memoriesCollected;
    public int livesCollected;
    public void CollectMemories()
    {
        memoriesCollected++;
        Debug.Log("Collected memories: " + memoriesCollected);
        //update memory ui
    }

    public void CollectLives()
    {
        livesCollected++;
        Debug.Log("Collected lives: " + livesCollected);
        //update live ui
    }

 
}
