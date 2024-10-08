using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] private Cube _prefabCube;
    [SerializeField] private Bomb _prefabBomb;

    private ObjectPool<Cube> _poolCube;
    private ObjectPool<Bomb> _poolBomb;

    private void Start()
    {
        this._poolCube = new ObjectPool<Cube>(_prefabCube, transform);
        this._poolBomb = new ObjectPool<Bomb>(_prefabBomb, transform);
    }

    public Cube GetCube()
    {
        var cube = this._poolCube.GetFreeObject();
        return cube;
    }

    public Bomb GetBomb()
    {
        var bomb = _poolBomb.GetFreeObject();
        return bomb;
    }
}
