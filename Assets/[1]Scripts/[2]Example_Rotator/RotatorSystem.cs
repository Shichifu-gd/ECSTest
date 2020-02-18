using Unity.Mathematics;
using Unity.Transforms;
using Unity.Entities;
using UnityEngine;

public class RotatorSystem : ComponentSystem
{
    private EntityQuery entity;

    protected override void OnCreateManager()
    {
        entity = Entities
            .WithAll<Rotator, Rotation>()
            .ToEntityQuery();
    }

    protected override void OnUpdate()
    {
        Entities.With(entity).ForEach((ref Rotator rotationSpeed, ref Rotation rotation) =>
        {
            rotation.Value = math.mul(rotation.Value,
              quaternion.RotateY(
                  math.radians(rotationSpeed.Speed * Time.deltaTime)
                  ));
        });
    }
}