using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TargetPositionAuthoring : MonoBehaviour
{
    public float3 Value;
}

public class TargetPositionBaker : Baker<TargetPositionAuthoring>
{
    public override void Bake(TargetPositionAuthoring authoring)
    {
        // Adds a new component
        // Grants the value from the authoring class and assigns it to the component
        AddComponent(new TargetPosition()
        {
            Value = authoring.Value
        });
    }
}
