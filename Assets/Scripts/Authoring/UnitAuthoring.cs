using UnityEngine;
using Unity.Entities;

public class UnitAuthoring : MonoBehaviour
{
    public Faction faction;

    public class Baker : Baker<UnitAuthoring>
    {
        public override void Bake(UnitAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<Unit>(entity, new Unit
            {
                faction = authoring.faction
            });
        }
    }
}

public struct Unit : IComponentData
{
    public Faction faction;
}
