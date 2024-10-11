using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private List<T> _pool;
    private int _createObjectsCount = 0;

    public ObjectPool(T prefab, Transform countainer)
    {
        Prefab = prefab;
        Countainer = countainer;
        _pool = new List<T>();
    }

    public T Prefab { get; }
    public Transform Countainer { get; }
    public int CreateObjectsCount => _createObjectsCount;

    public bool TryGetElement(out T element)
    {
        foreach (var obj in _pool)
        {
            if (obj.gameObject.activeInHierarchy == false)
            {
                element = obj;
                obj.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeObject()
    {
        if (TryGetElement(out var obj))
            return obj;

        return CreatObject(true);
    }

    private T CreatObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(Prefab, Countainer);
        _createObjectsCount++;
        createdObject.gameObject.SetActive(isActiveByDefault);
        _pool.Add(createdObject);
        return createdObject;
    }
}
