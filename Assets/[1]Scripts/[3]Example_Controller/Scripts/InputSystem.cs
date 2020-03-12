using Unity.Entities;
using UnityEngine;

public class InputSystem : ComponentSystem
{
    private struct Data
    {
        public int Length;
    }

    protected override void OnUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        Entities.ForEach((InputComponent InputComponent) =>
        {
            InputComponent.Horizontal = horizontal;
            InputComponent.Vertical = vertical;
        });
    }
}