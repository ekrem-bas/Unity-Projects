using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPoolManager : MonoBehaviour
{
    public static MagicPoolManager Instance;
    public Projectile magicPrefab;
    public ObjectPooler<Projectile> magicPool;

    void Awake()
    {
        Instance = this;
        magicPool = new ObjectPooler<Projectile>(magicPrefab, 10);
    }
}
