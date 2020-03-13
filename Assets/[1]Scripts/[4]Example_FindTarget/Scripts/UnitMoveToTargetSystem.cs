using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;
using UnityEngine;

public class UnitMoveToTargetSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity unitEntity, ref HasTarget hasTarget, ref Translation translation, ref MoveComponent moveComponent) =>
        {
            if (World.Active.EntityManager.Exists(hasTarget.targetEntity))
            {
                Translation targetTranslation = World.Active.EntityManager.GetComponentData<Translation>(hasTarget.targetEntity);
                float3 targetDir = math.normalize(targetTranslation.Value - translation.Value);
                translation.Value += targetDir * moveComponent.Speed * Time.deltaTime;
                if (math.distance(translation.Value, targetTranslation.Value) < .2f)
                {
                    PostUpdateCommands.DestroyEntity(hasTarget.targetEntity);
                    PostUpdateCommands.RemoveComponent(unitEntity, typeof(HasTarget));
                }
            }
            else PostUpdateCommands.RemoveComponent(unitEntity, typeof(HasTarget));

        });
    }
}