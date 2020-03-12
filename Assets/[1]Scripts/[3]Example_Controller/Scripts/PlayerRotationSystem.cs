using Unity.Entities;
using UnityEngine;

public class PlayerRotationSystem : ComponentSystem
{
    private struct Filter
    {
        public Transform Transform;
        public RotationComponent RotationComponent;
    }

    protected override void OnUpdate()
    {
        var mousePosition = Input.mousePosition;
        var cameraRay = Camera.main.ScreenPointToRay(mousePosition);
        var layerMask = LayerMask.GetMask("Floor");
        if (Physics.Raycast(cameraRay, out var hit, 100, layerMask))
        {
            Entities.ForEach((RotationComponent rotationComponent) =>
            {
                var forward = hit.point - rotationComponent.transform.position;
                var rotation = Quaternion.LookRotation(forward);
                rotationComponent.Value = new Quaternion(0, rotation.y, 0, rotation.w).normalized;
            });
        }
    }
}