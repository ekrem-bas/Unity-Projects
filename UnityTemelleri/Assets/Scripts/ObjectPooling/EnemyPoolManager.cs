using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enemy;
public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    public GameObject[] enemyPrefabs; // Düşman prefab'ları
    public ObjectPooler<Enemy>[] enemyPools; // Her prefab için havuzlar

    void Awake()
    {
        Instance = this;
        enemyPools = new ObjectPooler<Enemy>[enemyPrefabs.Length];
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            enemyPools[i] = new ObjectPooler<Enemy>(enemyPrefabs[i].GetComponent<Enemy>(), 10);
        }
    }
}
