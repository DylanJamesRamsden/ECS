using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Random = UnityEngine.Random;

// Because ISystem is an unmanaged system it needs to be a struct and not a class
public partial struct MovingISystem : ISystem
{
    
    // Would be nice if ISystem had empty implementations of OnCreate and OnDestroy so we don't need to implement them ourselves
    // if we want something simple
    
    public void OnCreate(ref SystemState state)
    {
        
    }
    
    public void OnDestroy(ref SystemState state)
    {
        
    }
    
    public void OnUpdate(ref SystemState state)
    {
        // Refer to MovingSystemBase for more info/comments
        // If we have both the MovingSystemBase and MovingISystem running, our capsule will move at double to speed

        // Starts our job
        // Cannot access SystemAPI in Job initializer, need to do it before
        float newDeltaTime = SystemAPI.Time.DeltaTime;
        JobHandle jobHandle = new MoveJob { deltaTime = newDeltaTime }.ScheduleParallel(state.Dependency);
        // Run: Runs the code on the main thread
        // Schedule: Runs the code on a single worker thread
        // ScheduleParrallel: Runs the code on multiple worker threads
        
        jobHandle.Complete();
        
        RefRW<RandomComponent> newRandomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        new TestReachedTargetPositionJob { randomComponent = newRandomComponent }.Run();
    }
}
// IJobChunk : If we want to iterate over multiple chunks
// IJobEntity : If we want to iterate over multiple entities
public partial struct MoveJob : IJobEntity
{
    public float deltaTime;
    public void Execute(MovementAspect movementAspect)
    {
        // Dont need a Foreach as IJobEntity is going to iterate over all of our entities anyways
        movementAspect.Move(deltaTime);
    }
}

public partial struct TestReachedTargetPositionJob : IJobEntity
{
    // [NativeDisableUnsafePtrRestriction] Only use if you know that the Ptr you are using is safe!!!
    [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> randomComponent;
    public void Execute(MovementAspect movementAspect)
    {
        // Dont need a Foreach as IJobEntity is going to iterate over all of our entities anyways
        movementAspect.TestReachedTargetPosition(randomComponent);
    }
}
