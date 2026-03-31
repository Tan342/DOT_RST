using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(LateSimulationSystemGroup))]
partial struct HealthBarSystem : ISystem
{
    //[BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        Vector3 cameraForward = Vector3.zero;
        if (Camera.main != null) 
        {
            cameraForward = Camera.main.transform.forward;
        }
        foreach (var (healthBar, localTransform) in SystemAPI.Query<RefRO<HealthBar>, RefRW<LocalTransform>>())
        {
            LocalTransform parenLocalTransform = SystemAPI.GetComponent<LocalTransform>(healthBar.ValueRO.healthEntity);

            if(localTransform.ValueRO.Scale == 1)
            {
                localTransform.ValueRW.Rotation = parenLocalTransform.InverseTransformRotation(quaternion.LookRotation(cameraForward, math.up()));
            }

            Health health = SystemAPI.GetComponent<Health>(healthBar.ValueRO.healthEntity);

            if (!health.onHealthChanged)
            {
                continue;
            }

            float healthNormalized = (float)health.healthAmount / health.healthAmountMax;

            if(healthNormalized == 1)
            {
                localTransform.ValueRW.Scale = 0f;
            }
            else
            {
                localTransform.ValueRW.Scale = 1f;
            }

            RefRW<PostTransformMatrix> barVisualLocalTransform = SystemAPI.GetComponentRW<PostTransformMatrix>(healthBar.ValueRO.barVisualEntity);
            barVisualLocalTransform.ValueRW.Value = float4x4.Scale(new float3(healthNormalized, 1f, 1f));

            //RefRW<LocalTransform> barVisualLocalTransform = SystemAPI.GetComponentRW<LocalTransform>(healthBar.ValueRO.barVisualEntity);
            //barVisualLocalTransform.ValueRW.Scale = healthNormalized;
        }
    }
}
