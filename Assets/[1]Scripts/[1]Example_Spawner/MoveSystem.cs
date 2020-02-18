using Unity.Transforms;
using Unity.Entities;
using UnityEngine;

public class MoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref MoveSpeedComponent moveSpeedComponent) =>
        {
            translation.Value.y += moveSpeedComponent.Speed * Time.deltaTime;
            if (translation.Value.y > 7) moveSpeedComponent.Speed = -Mathf.Abs(moveSpeedComponent.Speed);
            if (translation.Value.y < -7) moveSpeedComponent.Speed = +Mathf.Abs(moveSpeedComponent.Speed);
        });

    }
}