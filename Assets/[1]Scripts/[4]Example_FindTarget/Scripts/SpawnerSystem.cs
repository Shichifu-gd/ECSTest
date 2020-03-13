using Unity.Mathematics;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Entities;
using UnityEngine;
using System;

public class SpawnerSystem : MonoBehaviour
{
    [SerializeField] private SpawnderSettings spawnderSettings;
    [SerializeField] private ActorSettings actorSettings;

    [SerializeField] private Material UnitMaterial;
    [SerializeField] private Material TargetMaterial;
    [SerializeField] private Mesh QuadMesh;

    private static EntityManager entityManager;

    private void Start()
    {
        entityManager = World.Active.EntityManager;
        var amountActor = spawnderSettings.GetAmountActor();
        var amountStartedTarges = spawnderSettings.GetAmountStartedTarges();
        for (int i = 0; i < amountActor; i++)
        {
            SpawnUnitEntity();
        }
        for (int i = 0; i < amountStartedTarges; i++)
        {
            SpawnTargetEntity();
        }
    }

    private float SpawnTargetTimer;

    private void Update()
    {
        SpawnTargetTimer -= Time.deltaTime;
        if (SpawnTargetTimer < 0)
        {
            SpawnTargetTimer = spawnderSettings.GetTimer();
            for (int i = 0; i < 1; i++)
            {
                SpawnTargetEntity();
            }
        }
    }

    private void SpawnUnitEntity()
    {
        SpawnUnitEntity(new float3(UnityEngine.Random.Range(-8, +8f), UnityEngine.Random.Range(-5, +5f), 0));
    }

    private void SpawnUnitEntity(float3 position)
    {
        Entity entity = entityManager.CreateEntity(
            typeof(Translation),
            typeof(LocalToWorld),
            typeof(RenderMesh),
            typeof(Scale),
            typeof(Unit),
            typeof(MoveComponent)
        );
        SetEntityComponentData(entity, position, QuadMesh, UnitMaterial, true, actorSettings.GetSpeed());
        entityManager.SetComponentData(entity, new Scale { Value = 1f });
    }

    private void SpawnTargetEntity()
    {
        Entity entity = entityManager.CreateEntity(
            typeof(Translation),
            typeof(LocalToWorld),
            typeof(RenderMesh),
            typeof(Scale),
            typeof(Target)
        );
        SetEntityComponentData(entity, new float3(UnityEngine.Random.Range(-8, +8f), UnityEngine.Random.Range(-6, +6f), UnityEngine.Random.Range(-6, +6f)), QuadMesh, TargetMaterial);
        entityManager.SetComponentData(entity, new Scale { Value = UnityEngine.Random.Range(0.3f, +0.6f) });
    }

    private void SetEntityComponentData(Entity entity, float3 spawnPosition, Mesh mesh, Material material, bool canMove = false, float speed = 0)
    {
        entityManager.SetSharedComponentData<RenderMesh>(entity,
            new RenderMesh
            {
                material = material,
                mesh = mesh,
            });
        entityManager.SetComponentData<Translation>(entity,
            new Translation
            {
                Value = spawnPosition
            });
        if (canMove)
        {
            if (speed <= 0) speed = 5;
            entityManager.SetComponentData<MoveComponent>(entity,
                new MoveComponent
                {
                    Speed = UnityEngine.Random.Range(-6, speed)
                });
        }
    }
}

public struct Unit : IComponentData { }
public struct Target : IComponentData { }

public struct HasTarget : IComponentData
{
    public Entity targetEntity;
}

public class HasTargetDebug : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref Translation translation, ref HasTarget hasTarget) =>
        {
            if (World.Active.EntityManager.Exists(hasTarget.targetEntity))
            {
                Translation targetTranslation = World.Active.EntityManager.GetComponentData<Translation>(hasTarget.targetEntity);
                Debug.DrawLine(translation.Value, targetTranslation.Value);
            }
        });
    }
}

[Serializable]
public class SpawnderSettings
{
    [SerializeField] private int AmountActor;
    [SerializeField] private int AmountStartedTarges;
    [SerializeField] private float Timer;

    public int GetAmountActor() { return AmountActor; }
    public int GetAmountStartedTarges() { return AmountStartedTarges; }
    public float GetTimer() { return Timer; }
}

[Serializable]
public class ActorSettings
{
    [SerializeField] private float Speed;

    public float GetSpeed() { return Speed; }
}