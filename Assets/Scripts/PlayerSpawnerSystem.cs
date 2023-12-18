using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine.Rendering;

[BurstCompile]
public partial struct PlayerSpawnerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        /*EntityQuery playerSpawnQuery =
            new EntityQueryBuilder(Allocator.Temp).WithAll<PlayerTag>().Build(state.EntityManager);
        // More queries can be added, just add: .WithAll<Component>()
        // https://docs.unity3d.com/Packages/com.unity.entities@1.0/manual/systems-entityquery-create.html

        PlayerSpawnerComponent playerSpawnerComponent = SystemAPI.GetSingleton<PlayerSpawnerComponent>();
        
        EntityCommandBuffer commandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(WorldUnmanaged)

        // CalculateEntityCount returns the number of entities found with our query
        if (playerSpawnQuery.CalculateEntityCount() < 3)
        {
            EntityManager.Instantiate(playerSpawnerComponent.playerPrefab);
        }*/
    }
}
