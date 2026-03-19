using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic object pool for GameObjects. Reuses instances instead of Instantiate/Destroy.
/// </summary>
public class ObjectPool<T> where T : Component
{
    private readonly Queue<T> pool;
    private readonly T prefab;
    private readonly Transform parent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        pool = new Queue<T>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            var instance = Object.Instantiate(prefab, parent);
            instance.gameObject.SetActive(false);
            pool.Enqueue(instance);
        }
    }

    public T Get(Vector3 position, Quaternion rotation)
    {
        T instance;
        if (pool.Count > 0)
        {
            instance = pool.Dequeue();
        }
        else
        {
            instance = Object.Instantiate(prefab, parent);
        }

        instance.transform.SetPositionAndRotation(position, rotation);
        instance.gameObject.SetActive(true);
        return instance;
    }

    public void Release(T instance)
    {
        if (instance == null) return;
        instance.gameObject.SetActive(false);
        pool.Enqueue(instance);
    }
}