using Unity.Entities;
using UnityEngine;

public class PlayerSpawnerAuthoring : MonoBehaviour
{
    // We want to convert this GameObject into an Entity
    public GameObject playerPrefab;
}

public class PlayerSpawnerBaker : Baker<PlayerSpawnerAuthoring>
{
    public override void Bake(PlayerSpawnerAuthoring authoring)
    {
        AddComponent(new PlayerSpawnerComponent
        {
            // Converts our game object to an Entity.
            // Need to pass in a TransformUsageFlags because the old method is deprecated with just passing in the gameobject
            playerPrefab = GetEntity(authoring.playerPrefab, TransformUsageFlags.Dynamic)
        });
    }
}
