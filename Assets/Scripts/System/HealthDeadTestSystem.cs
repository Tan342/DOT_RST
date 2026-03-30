using Unity.Burst;
using Unity.Entities;
using Unity.Collections;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
partial struct HealthDeadTestSystem : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        
        EntityCommandBuffer entityCommandBuffer  = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
        foreach (var (health, entity) in SystemAPI.Query<RefRO<Health>>().WithEntityAccess())
        {
            if (health.ValueRO.healthAmount <= 0) 
            {
                entityCommandBuffer.DestroyEntity(entity);
            }
        }


    }

}
