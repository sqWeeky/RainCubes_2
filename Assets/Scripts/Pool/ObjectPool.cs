using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public Transform Countainer { get; }
    public int NumberOfActiveObjects { get; private set; }
    public int NumberOfCreatObjects { get; private set; }

    private List<T> _pool;    

    public ObjectPool(T prefab, Transform countainer)
    {
        this.Prefab = prefab;
        this.Countainer = countainer;

        this.CreatePool();
    }

    private void CreatePool()
    {
        this._pool = new List<T>();
    }

    private T CreatObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(this.Prefab, this.Countainer);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this._pool.Add(createdObject);
        NumberOfCreatObjects++;
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var obj in _pool)
        {
            if (!obj.gameObject.activeInHierarchy)
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
        if (this.HasFreeElement(out var obj))        
            return obj;
        
        return this.CreatObject(true);
    }

    public void CheckNumberOfActiveObjects()
    {
        NumberOfActiveObjects = 0;

        foreach (var obj in _pool)
        {
            if (obj.gameObject.activeInHierarchy)
            {
                NumberOfActiveObjects++;
            }
        }
    }
}
