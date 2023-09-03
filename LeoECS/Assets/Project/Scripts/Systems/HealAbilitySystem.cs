using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    sealed class HealAbilitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewEventClick> _clicksFilter = null;   // При клике по юниту создаем сущность с компонентом

        private readonly EcsFilter<UnitComponent, AbilityHealerComponent> _healerFilter = null;  // Те кто лечят
        private readonly EcsFilter<UnitComponent>.Exclude<PlayerComponent> _unitFilter = null;   // Те которых лечат

        public void Run()
        {
            if (_clicksFilter.IsEmpty()) return;  // В случае клика по мобу

            foreach (var it1 in _healerFilter)  // поочередно с 1 по n лечат самого побитого юнита
            {
                var unitHealer = _healerFilter.Get1(it1);   // Юнит который лечит
                var ability = _healerFilter.Get2(it1);      // Способность этого юнита
                
                float maxHP = int.MaxValue;

                var unitEnityForHeal = EcsEntity.Null;

                // Ищем самого побитого юнита
                foreach (var it2 in _unitFilter)
                {
                    var unit = _unitFilter.Get1(it2);
                
                    if (unit.MaxHP - unit.HP > float.Epsilon && unit.HP < maxHP) // Если есть единицы для восполнения и это самый побитый юнит
                    {
                        maxHP = unit.HP;
                        unitEnityForHeal = _unitFilter.GetEntity(it2);
                    }
                }

                if (unitEnityForHeal == EcsEntity.Null) continue;
               
                //ref var unitHeal = ref unitEnityForHeal.Get<UnitComponent>();
                // Вешаем запрос на лечение
                unitEnityForHeal.Get<HealRequest>().Sender = unitHealer.Name;
                unitEnityForHeal.Get<HealRequest>().Value = Random.Range(ability.MinHealerValue, ability.MaxHealerValue);
            }
        }
    }
}