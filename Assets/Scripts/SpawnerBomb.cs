using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    public void AtSpawn(Vector3 position)
    {
        Bomb bomb = Spawn();
        bomb.transform.position = position;
    }
}
