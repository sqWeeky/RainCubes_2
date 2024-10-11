using System;
using System.Collections;
using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolable<T>
{
    [SerializeField] private float _spawnDelay = 3f;
    [SerializeField] private T _prefab;

    private ObjectPool<T> _pool;
    private float _minPositionX = -5f;
    private float _maxPositionX = 5f;
    private float _positionY = 15;
    private float _minPositionZ = -10f;
    private float _maxPositionZ = 10f;
    private int _activeCount = 0;
    private int _spawnsCount = 0;

    public event Action<int, int, int> CounterChanged;

    private void Awake()
    {
        _pool = new ObjectPool<T>(_prefab, transform);
    }

    private void Start()
    {
        StartCoroutine(SpawningObject());
    }

    protected void SpawnAtRandom()
    {
        T obj = Spawn();
        obj.transform.position = GetRandomPosition();
    }

    protected T Spawn()
    {
        T spawnedObject = _pool.GetFreeObject();
        spawnedObject.gameObject.SetActive(true);
        spawnedObject.DisappearedObject += DisableObject;
        CounterChanged?.Invoke(_pool.CreateObjectsCount, ++_activeCount, ++_spawnsCount);
        return spawnedObject;
    }

    protected virtual void DisableObject(T obj)
    {
        obj.DisappearedObject -= DisableObject;
        obj.gameObject.SetActive(false);
        CounterChanged?.Invoke(_pool.CreateObjectsCount, _activeCount--, _spawnsCount);
    }

    private IEnumerator SpawningObject()
    {
        var waitCube = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            yield return waitCube;
            SpawnAtRandom();
        }
    }

    private Vector3 GetRandomPosition() => new(
        UnityEngine.Random.Range(_minPositionX, _maxPositionX),
        _positionY,
        UnityEngine.Random.Range(_minPositionZ, _maxPositionZ));
}
