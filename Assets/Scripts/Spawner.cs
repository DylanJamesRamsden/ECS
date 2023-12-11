using Unity.Entities;
using Unity.Mathematics;

public struct Spawner : IComponentData
{
    // The prefab representing this Entity
    public Entity Prefab;
    
    // The position at which to spawn an Entity
    public float3 SpawnPosition;
    
    // The time in which the next Entity will be spawned
    public float NextSpawnTime;
    
    // The rate at which new Entity's will be spawned
    public float SpawnRate;
}

