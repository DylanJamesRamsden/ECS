using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// An Aspect must be read-only
public readonly partial struct MovementAspect : IAspect
{
    // Will be automatically filled with the entity using this aspect
    // Can only have one field of type entity
    private readonly Entity entity;
    
    // The properties in an aspect must also be read-only
    private readonly RefRW<LocalTransform> transform;
    private readonly RefRO<Speed> speed;
    private readonly RefRW<TargetPosition> targetPosition;

    public void Move(float DeltaTime)
    {
        float3 Direction = math.normalize(targetPosition.ValueRO.Value - transform.ValueRO.Position);
        // Move in the calculated direction
        // NB! Cannot use SystemAPI inside an aspect
        transform.ValueRW.Position += Direction * DeltaTime * speed.ValueRO.Value;
    }
}
