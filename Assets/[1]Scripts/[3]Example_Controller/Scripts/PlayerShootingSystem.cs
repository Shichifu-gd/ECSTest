using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

public class PlayerShootingSystem : JobComponentSystem
{
    private struct PlayerShootingJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<Entity> EntityArray;
        public EntityCommandBuffer.Concurrent EntityCommandBuffer;
        public bool IsFiring;

        public void Execute(int index)
        {
            if (!IsFiring) return;
            EntityCommandBuffer.AddComponent(index, EntityArray[index], new Firing());
        }
    }

    private struct Data
    {
        public readonly int Length;
        public NativeArray<Entity> Entities;
        public ComponentDataProxy<Weapon> Weapon;
    }

    private Data data;
    private PlayerShootingBarrier barrier;

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        //fix
        throw new System.NotImplementedException();
    }
}

public class PlayerShootingBarrier
{

}