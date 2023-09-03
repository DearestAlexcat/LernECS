using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    sealed class SecondAbilitySystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsFilterInject<Inc<SecondChanceAblilityComponent, DamageRequestComponent, UnitComponent>> _filter = default;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run (EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var item in _filter.Value)
            {
                ref var second = ref _filter.Pools.Inc1.Get(item);
                ref var damage = ref _filter.Pools.Inc2.Get(item);
                ref var unit = ref _filter.Pools.Inc3.Get(item);

                if (unit.HP - damage.Value <= 0)
                {
                    if (Random.value * 100f <= second.Chance)
                    {
                        _filter.Pools.Inc2.Del(item);
                        _world.Value.SendMessage($"{unit.Name} получил второй шанс");
                    }
                }
            }
        }
    }
}