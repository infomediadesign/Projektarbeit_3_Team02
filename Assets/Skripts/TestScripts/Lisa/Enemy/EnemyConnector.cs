using UnityEngine;

public class EnemyConnector : MonoBehaviour
{
    void Start()
    {
        EnemyManager.instance.AddEnemy(this); //enemy an sich selbst übergeben
    }

    private void OnDestroy()
    {
        EnemyManager.instance.RemoveEnemy(this);
    }
}
