using Unity.Collections;    
using Unity.Transforms;     
using Unity.Rendering;
using Unity.Entities;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int CountGO;
    [SerializeField] private Vector2 HorizontalPosotionGO;
    [SerializeField] private Vector2 VerticalPosotionGO;
    [SerializeField] private Vector2 DeepPosotionGO;

    [SerializeField] private Mesh[] meshs;
    [SerializeField] private Material[] materials;

    private void Start()
    {
        EntityManager entityManager = World.Active.EntityManager;
        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(LevelComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(MoveSpeedComponent));

        NativeArray<Entity> entities = new NativeArray<Entity>(CountGO, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entities);
        for (int index = 0; index < entities.Length; index++)
        {
            Entity entity = entities[index];
            entityManager.SetComponentData(entity, new LevelComponent { level = RandomInt(0, 15) });
            entityManager.SetComponentData(entity, new MoveSpeedComponent { Speed = RandomFloat(3f, 7f) });
            entityManager.SetComponentData(entity, new Translation
            {
                Value = new Vector3(
                RandomFloat(HorizontalPosotionGO.x, HorizontalPosotionGO.y),
                RandomFloat(VerticalPosotionGO.x, VerticalPosotionGO.y),
                RandomFloat(DeepPosotionGO.x, DeepPosotionGO.y))
            });
            entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = meshs[Random.Range(0, meshs.Length)],
                material = materials[Random.Range(0, materials.Length)],
            });
        }
        entities.Dispose();
    }

    private int RandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    private float RandomFloat(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }
}