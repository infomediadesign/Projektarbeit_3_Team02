using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    private List<EnemyConnector> enemies = new List<EnemyConnector>(); //initialisierung mit leerer Liste
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("EnemyManager already exists", gameObject);
        }

    }
    public void AddEnemy(EnemyConnector enemy)
    {

        enemies.Add(enemy);
        //Debug.Log(enemies.Count, gameObject);
    }
    public void RemoveEnemy(EnemyConnector enemy)
    {
        enemies.Remove(enemy);
        //Debug.Log(enemies.Count, gameObject);
    }

    public int GetCount()
    {
        return enemies.Count;
    }
    public List<EnemyConnector> GetEnemies()
    {
        return enemies;
    }
}
