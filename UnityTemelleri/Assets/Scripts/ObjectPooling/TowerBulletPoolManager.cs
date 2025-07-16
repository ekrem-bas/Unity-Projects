using UnityEngine;

public class TowerBulletPoolManager : MonoBehaviour
{
    public static TowerBulletPoolManager Instance;
    public Projectile bulletPrefab;
    public ObjectPooler<Projectile> towerBulletPool;

    void Awake()
    {
        Instance = this;
        towerBulletPool = new ObjectPooler<Projectile>(bulletPrefab, 10);
    }
}