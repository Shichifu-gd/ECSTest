using Unity.Entities;
using System;

[Serializable]
public struct Rotator : IComponentData
{
    public float Speed;
}