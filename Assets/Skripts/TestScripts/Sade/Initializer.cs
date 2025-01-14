using UnityEngine;

public class Initializer : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        Debug.Log("Loaded Persistent Objects from the Initializer script");
        //load the prefab from the Resources folder and instantiate it
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PersistentDataPlayCam")));

        
        //GameObject persistentData = Object.Instantiate(Resources.Load("PersistentDataPlayCam")) as GameObject;

        //checks if the object was successfully loaded and instantiated
        /*if (persistentData != null)
        {
            //mark the root GameObject as persistent across scenes
            Object.DontDestroyOnLoad(persistentData);
        }
        else
        {
            Debug.LogError("Failed to load PersistentDataPlayCam prefab from Resources.");
        }*/
    }
    }
