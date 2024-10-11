using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    public void AtSpawn(Vector3 position) => Spawn().transform.position = position;    
}
