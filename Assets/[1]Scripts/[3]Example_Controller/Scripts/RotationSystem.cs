using Unity.Entities;
using UnityEngine;

public class RotationSystem : ComponentSystem
{
    private struct Data
    {
        public int Length;
    }

    protected override void OnUpdate()
    {
        Entities.ForEach((Rigidbody rigidbody, RotationComponent rotationComponent) =>
        {
            var rotation = rotationComponent.Value;
            rigidbody.MoveRotation(rotation);
        });
    }
}