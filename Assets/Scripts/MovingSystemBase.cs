using Unity.Entities;

public partial class MovingSystemBase : SystemBase
{
    // Must override this function in SystemBase
    protected override void OnUpdate()
    {
       // No!!! New Method! Entities.ForEach()
       
       // Gets all LocalTransform components from Entities as well as their Speed components 
       // Can have nested cycles
       // RefRW: Read/Write
       // RefRO: Read-only
       /*foreach ((var transform, var speed, var targetPosition) in
                SystemAPI.Query<RefRW<LocalTransform>, RefRO<Speed>, RefRW<TargetPosition>>())
       {
           // Calculates the direction
           float3 Direction = math.normalize(targetPosition.ValueRO.Value - transform.ValueRO.Position);
           // Move in the calculated direction
           transform.ValueRW.Position += Direction * SystemAPI.Time.DeltaTime * speed.ValueRO.Value;
       }*/
       // SystemAPI.Query<> can have multiple templates just seperated with a ,. But then also the foreach variables must
       // also have multiple, one for each query type
       // e.g. ((MovementAspect movementAspect, LocalTransform transform) in SystemAPI.Query<MovementAspect, LocalTransform>())
       // Simplified with an aspect
       foreach (MovementAspect movementAspect in SystemAPI.Query<MovementAspect>())
       {
           movementAspect.Move(SystemAPI.Time.DeltaTime);
           movementAspect.TestReachedTargetPosition(SystemAPI.GetSingletonRW<RandomComponent>());
           
           // Need RefRW on random component to get a direct reference rather than a copy, if we have a value that never changes we can just get
           // the type rather than RefRW
           // SystemAPI.GetSingleton<> just gets a copy of the singleton, so original value of a property will never change
           // SystemAPI. GetSingletonRW<> gets an actual reference
       }
       
       // Old way to run through all entities
       // Its a Lambda!!!!!
       /*Entities.ForEach((RefRW<LocalTransform> transform) =>
       {
           transform.ValueRW.Position += new float3(SystemAPI.Time.DeltaTime, 0, 0);
       }).Run();*/
       // Run: Runs the code on the main thread
       // Schedule: Runs the code on a single worker thread
       // ScheduleParrallel: Runs the code on multiple worker threads
    }
}
