using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private SpawnerBomb _spawnerBomb;

    protected override void DisableObject(Cube obj)
    {
        base.DisableObject(obj);
        _spawnerBomb.AtSpawn(obj.transform.position);
    }
}
