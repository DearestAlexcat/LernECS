using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class SecondAbilitySystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<SecondChanceAblilityComponent, DamageRequestComponent, UnitComponent> _filter = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var second = ref _filter.Get1(item);
                ref var damage = ref _filter.Get2(item);
                ref var unit = ref _filter.Get3(item);

                if (unit.HP - damage.Value <= 0)
                {
                    if (Random.value * 100f <= second.Chance)
                    {
                        _filter.GetEntity(item).Del<DamageRequestComponent>();
                        _world.SendMessage($"{unit.Name} получил второй шанс");
                    }
                }
            }
        }
    }
}