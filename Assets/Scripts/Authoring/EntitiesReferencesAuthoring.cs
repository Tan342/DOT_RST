using UnityEngine;
using Unity.Entities;

public class EntitiesReferencesAuthoring : MonoBehaviour
{
    public GameObject bulletPrefabGameObject;

    public class Baker : Baker<EntitiesReferencesAuthoring>
    {
        public override void Bake(EntitiesReferencesAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EntitiesReferences
            {
                bulletPrefabEntity = GetEntity(authoring.bulletPrefabGameObject, TransformUsageFlags.Dynamic),
            });
        }
    }
}

struct EntitiesReferences : IComponentData
{
    public Entity bulletPrefabEntity;
}
