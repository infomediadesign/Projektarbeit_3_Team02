using UnityEngine;

public class EnemyConnector : MonoBehaviour
{
    void Start()
    {
        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.AddEnemy(this); // enemy an sich selbst übergeben
        }
    }

    private void OnDestroy()
    {
        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.RemoveEnemy(this);
        }
    }
}