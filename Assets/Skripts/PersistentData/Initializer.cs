using UnityEngine;

public class Initializer : MonoBehaviour
{
    private static GameObject instance;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        /* Debug.Log("Loaded Persistent Objects from the Initializer script");
         //load the prefab from the Resources folder and instantiate it
         if (!GameOverUI.gameOver)
         {
             Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PersistentDataPlayCam")));
         }*/



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

        /* Debug.Log("Loaded Persistent Objects from the Initializer script");
         GameObject persistentData = Object.Instantiate(Resources.Load("PersistentDataPlayCam")) as GameObject;
         Object.DontDestroyOnLoad(persistentData);*/
        if (instance == null)
        {
            GameObject persistentData = Object.Instantiate(Resources.Load("PersistentDataPlayCam")) as GameObject;
            instance = persistentData;
            DontDestroyOnLoad(persistentData);
        }
    }
    public static void DestroyInitializer()
    {
        if (instance != null)
        {
            Debug.Log("Initializer wird gelöscht!");
            Destroy(instance.gameObject);
            instance = null;
        }
    }

 }
      
 





