using UnityEngine;

public class Initializer : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    public static void Execute()
    {
        Debug.Log("Loaded by the Persitent Objects from the Intitializer script");
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PersistentData")));
    }
}