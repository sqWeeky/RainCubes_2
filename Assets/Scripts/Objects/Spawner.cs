using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Pool _pool;
    [SerializeField] private List<Vector3> _positions;
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private Displayer _displayer;

    private float _minPositionX = -5f;
    private float _maxPositionX = 5f;
    private float _positionY = 15;
    private float _minPositionZ = -10f;
    private float _maxPositionZ = 10f;

    private Cube _cube;

    private void Start()
    {
        StartCoroutine(SpawningObject());
    }

    private IEnumerator SpawningObject()
    {
        var waitCube = new WaitForSeconds(_spawnDelay);

        while (true)
        {
            yield return waitCube;

            _cube = _pool.GetCube();
            _displayer.AllObjects++;
            _cube.gameObject.SetActive(true);
            _cube.transform.position = GetRandomPosition();
            _cube.CubeDisappeared += SpawnBomb;
        }
    }

    private void SpawnBomb(Cube cube)
    {
        Bomb bomb = _pool.GetBomb();
        _displayer.AllObjects++;
        bomb.gameObject.SetActive(true);
        bomb.transform.position = cube.transform.position;
        cube.CubeDisappeared -= SpawnBomb;
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = new(Random.Range(_minPositionX, _maxPositionX),
            _positionY,
            Random.Range(_minPositionZ, _maxPositionZ));

        return randomPosition;
    }
}
