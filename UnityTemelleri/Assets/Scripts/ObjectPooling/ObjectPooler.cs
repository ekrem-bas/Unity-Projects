using UnityEngine.Pool;
using UnityEngine;

public class ObjectPooler<T> where T : MonoBehaviour
{
    private ObjectPool<T> pool;
    private T prefab;

    public ObjectPooler(T prefab, int defaultCapacity = 10)
    {
        this.prefab = prefab;
        pool = new ObjectPool<T>(
            () => Object.Instantiate(prefab),
            obj => obj.gameObject.SetActive(true),
            obj => obj.gameObject.SetActive(false),
            obj => Object.Destroy(obj.gameObject),
            false, defaultCapacity, 100
        );

        // Havuz size kadar objeyi instantiate et ve havuz'u doldur
        for (int i = 0; i < defaultCapacity; i++)
        {
            T obj = Object.Instantiate(prefab);
            obj.gameObject.SetActive(false);
            pool.Release(obj);
        }
    }

    public T Get()
    {
        return pool.Get();
    }

    public void Release(T obj)
    {
        pool.Release(obj);
    }
}