using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;
    public Projectile bulletPrefab;
    public ObjectPooler<Projectile> bulletPool;

    void Awake()
    {
        Instance = this;
        bulletPool = new ObjectPooler<Projectile>(bulletPrefab, 10);
    }
}