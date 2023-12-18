using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class PlayerSpawnerSystemBase : SystemBase
{
    protected override void OnUpdate()
    {
        EntityQuery playerSpawnQuery = EntityManager.CreateEntityQuery(typeof(PlayerTag));

        PlayerSpawnerComponent playerSpawnerComponent = SystemAPI.GetSingleton<PlayerSpawnerComponent>();

        EntityCommandBuffer entityCommandBuffer =
            SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                .CreateCommandBuffer(World.Unmanaged);

        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        
        // CalculateEntityCount returns the number of entities found with our query
        if (playerSpawnQuery.CalculateEntityCount() < 1000)
        {
            Entity newEntity = entityCommandBuffer.Instantiate(playerSpawnerComponent.playerPrefab);
            entityCommandBuffer.SetComponent(newEntity ,new Speed
            {
                Value = randomComponent.ValueRW.random.NextFloat(2.0f, 5.0f)
            });
            //EntityManager.Instantiate(playerSpawnerComponent.playerPrefab);
        }
    }
}
