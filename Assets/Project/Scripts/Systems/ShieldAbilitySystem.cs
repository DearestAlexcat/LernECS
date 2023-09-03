using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
	 // ShieldAbilitySystem, SecondAbilitySystem, DamageSystem, DestroyEntitySystem выполнение систем зависит от наличия компонента 
    // DamageRequestComponent на сущности

    sealed class ShieldAbilitySystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly EcsFilter<ShieldAbilityComponent, DamageRequestComponent, UnitComponent> _filter = null;

        public void Run()
        {
            foreach (var item in _filter)
            {
                ref var ability = ref _filter.Get1(item);
                ref var damage = ref _filter.Get2(item);
                ref var unit = ref _filter.Get3(item);

                if (Random.value * 100f <= ability.Chance)
                {
                    var blockHP = damage.Value * (ability.BlockValue / 100f);

                    _world.SendMessage($"{unit.Name} поглотил щитом {blockHP} единиц урона");

                    damage.Value -= blockHP;
                }
                
                // Данный блок с константами был рефакторизирован в данные ShieldComponent
                //if (Random.value >= 0.5f) // Шанс 50%
                //{
                //    damage.Value /= 2f; // Блокируемый урон

                //    _world.SendMessage("Щит поглотил половину урона");
                //}
            }
        }
    }
}