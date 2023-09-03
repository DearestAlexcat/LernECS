using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client
{
    // ShieldAbilitySystem, SecondAbilitySystem, DamageSystem, DestroyEntitySystem the execution of systems depends on the presence of the component
    // DamageRequestComponent на сущности

    sealed class ShieldAbilitySystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;

        private readonly EcsFilterInject<Inc<ShieldAbilityComponent, DamageRequestComponent, UnitComponent>> _filter = null;
        private readonly EcsCustomInject<RuntimeData> _runtimeData = default;

        public void Run (EcsSystems systems)
        {
            if (_runtimeData.Value.GameState != GameState.PLAYING) return;

            foreach (var item in _filter.Value)
            {
                ref var ability = ref _filter.Pools.Inc1.Get(item);
                ref var damage = ref _filter.Pools.Inc2.Get(item);
                ref var unit = ref _filter.Pools.Inc3.Get(item);

                if (Random.value * 100f <= ability.Chance)
                {
                    var blockHP = damage.Value * (ability.BlockValue / 100f);

                    _world.Value.SendMessage($"{unit.Name} поглотил щитом {blockHP} единиц урона");

                    damage.Value -= blockHP;
                }
            }
        }
    }
}