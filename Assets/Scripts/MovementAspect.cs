using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

// An Aspect must be read-only
namespace Unity.Entities
{
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

        public void TestReachedTargetPosition(RefRW<RandomComponent> randomComponent)
        {
            if (math.distance(transform.ValueRO.Position, targetPosition.ValueRO.Value) < 2.0f)
            {
                targetPosition.ValueRW.Value = GetRandomPosition(randomComponent);
            }
        }

        private float3 GetRandomPosition(RefRW<RandomComponent> randomComponent)
        {
            // Cannot use Random.Range (will break when we use Burst)
        
            return new float3(randomComponent.ValueRW.random.NextFloat(-20.0f, 20.0f), 0.0f, randomComponent.ValueRW.random.NextFloat(-20.0f, 20.0f));
        }
    }
}
