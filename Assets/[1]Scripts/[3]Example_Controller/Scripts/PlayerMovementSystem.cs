using Unity.Entities;
using UnityEngine;

public class PlayerMovementSystem : ComponentSystem
{
    private struct Filter
    {
        public Rigidbody Rigidbody;
        public InputComponent InputComponent;
    }

    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;
        Entities.ForEach((Rigidbody rigidbody, InputComponent InputComponent) =>
        {
            var moveVector = new Vector3(InputComponent.Horizontal, 0, InputComponent.Vertical);
            var movePosition = rigidbody.position + moveVector.normalized * 3 * deltaTime;
            rigidbody.MovePosition(movePosition);
        });
    }
}