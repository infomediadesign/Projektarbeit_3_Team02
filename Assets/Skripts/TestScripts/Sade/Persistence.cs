using UnityEngine;

public class Persistence : MonoBehaviour
{
    
    public static Persistence instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ResetInstance()
    {
        Debug.LogError("Persistence wird komplett neu geladen...");
        Initializer.DestroyInitializer();

        Initializer.Execute();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


}
